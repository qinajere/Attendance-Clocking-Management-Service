using AttendanceClockingManagementSystem.API.DataAccess;
using AttendanceClockingManagementSystem.API.DataAccess.Model;
using AttendanceClockingManagementSystem.API.Resources.Parameters;
using AttendanceClockingManagementSystem.API.Resources.Responses;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Serilog;
using System.Net.Http;
using static System.Net.Mime.MediaTypeNames;

namespace AttendanceClockingManagementSystem.API.Repositories
{
    public class AttendanceRepository : IAttendanceRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;
        private readonly IOfficeTimingRepository _officeTimingRepository;
        private readonly IQRCodeRepository _qRCodeRepository;
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public AttendanceRepository(ApplicationDbContext applicationDbContext, IMapper mapper, IOfficeTimingRepository officeTimingRepository, IQRCodeRepository qRCodeRepository, IConfiguration configuration)
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
            _officeTimingRepository = officeTimingRepository;
            _qRCodeRepository = qRCodeRepository;
            _configuration = configuration;
            _httpClient = new HttpClient();
        }
        public async Task<bool> AddAttendance(Attendance attendance)
        {
            try
            {

                var qrcode = await _qRCodeRepository.GetQRCode(attendance.QRCodeID);

               

                if (qrcode == null)
                    return false;

              

                qrcode.ScanStatus = true;

                var result = await _qRCodeRepository.EditQRCode(qrcode);

                if (!result)
                    result = false;


                var employeeInfo = await this.EmployeeInfo(qrcode.EmployeeCode);

                attendance.EmployeeCode = qrcode.EmployeeCode;

                attendance.EmployeeName = employeeInfo.FirstName + " " + employeeInfo.LastName;

                attendance.Branch = employeeInfo.BranchName;

                attendance.Department = employeeInfo.DepartmentName;

                attendance.ClockIn = DateTime.Now.TimeOfDay;

                attendance.DateCreated = DateTime.Now;
                attendance.Comment = "";
                
               
                _applicationDbContext.Attendances.Add(attendance);

                await _applicationDbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {

                Log.Error("Failed to add attendance : " + ex.Message, ex);
                return false;
            }
        }

        public async Task<bool> EditAttendance(Attendance attendance)
        {
            try
            {
                _applicationDbContext.Attendances.Update(attendance);

                _applicationDbContext.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {

                Log.Error("Failed to update attendance : " + ex.Message, ex);
                return false;
            }
        }

        public async Task<UMSResponse> EmployeeInfo(string code)
        {
            try
            {
                var link = _configuration.GetSection("Constants").GetSection("EmployeeInfo").Value.ToString();


                var builder = new UriBuilder(link);
                builder.Query = "employeeCode=" + code;
                var url = builder.ToString();

                var response = await _httpClient.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }


                var createdTask = JsonConvert.DeserializeObject<UMSResponse>(await response.Content.ReadAsStringAsync());

                if (createdTask == null)
                {
                    return null;
                }

                return createdTask;
            }
            catch (Exception ex)
            {

                Log.Error("Failed to get employee info : " + ex.Message);
                return null;
            }
        }

        public async Task<List<Attendance>> GetAllAttendances(GetAttendanceResourceParameters parameters)
        {
            try
            {
                    var timing = await _officeTimingRepository.GetOfficeTiming();

                    var query =  _applicationDbContext.Attendances.Include(u => u.QRCode).AsQueryable();

                    

                    if (parameters.StartDate != null && parameters.EndDate != null)
                    {
                       parameters.EndDate = parameters.EndDate.Value.AddHours(23);

                       query = query.Where(u => u.DateCreated >= parameters.StartDate && u.DateCreated <= parameters.EndDate);
                    }

                    if (parameters.EmployeeCode != null)
                    {
                       query = query.Where(u => u.EmployeeCode == parameters.EmployeeCode);
                    }

                    if ( parameters.Late == true )
                    {

                       query = query.Where(u => u.ClockIn > timing.ArrivalTime);

                    }

                    if (parameters.Early == true)
                    {

                       query = query.Where(u => u.ClockIn <= timing.ArrivalTime);

                    }


                    var result = await query.ToListAsync();


                    if (parameters.QRCode != null)
                    {

                    result = result.Where(u => u.QRCode.Code == parameters.QRCode).ToList();

                    }

 

                    return result;
   

 

            }
            catch (Exception ex)
            {

                Log.Error("Failed to get all attendance : " + ex.Message, ex);
                return null;
            }
        }

        public async Task<Attendance> GetAttendance(int id)
        {
            try
            {
                var attendance = _applicationDbContext.Attendances.FirstOrDefault(u => u.Id == id);

                return attendance;
            }
            catch (Exception ex)
            {

                Log.Error("Failed to get attendance : " + ex.Message, ex);

                return null;
            }
           
        }
    }
}
