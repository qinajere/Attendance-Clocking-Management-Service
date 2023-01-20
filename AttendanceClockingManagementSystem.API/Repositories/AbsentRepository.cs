using AttendanceClockingManagementSystem.API.DataAccess;
using AttendanceClockingManagementSystem.API.DataAccess.Model;
using AttendanceClockingManagementSystem.API.Resources.Parameters;
using AttendanceClockingManagementSystem.API.Resources.Responses;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Serilog;
using System.Net.Http;

namespace AttendanceClockingManagementSystem.API.Repositories
{
    public class AbsentRepository : IAbsentRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly ILeaveRepository _leaveRepository;
        private readonly IConfiguration _configuration;
        private readonly IAttendanceRepository _attendanceRepository;
        private readonly HttpClient _httpClient;

        public AbsentRepository(ApplicationDbContext applicationDbContext, ILeaveRepository leaveRepository, IConfiguration configuration, IAttendanceRepository attendanceRepository)
        {
            _applicationDbContext = applicationDbContext;
            _leaveRepository = leaveRepository;
            _configuration = configuration;
            _attendanceRepository = attendanceRepository;
            _httpClient = new HttpClient();
        }
        public async Task<bool> AddAbsent(Absent absent)
        {
            try
            {
                _applicationDbContext.Absents.Add(absent);
                _applicationDbContext.SaveChanges();

                return true;    
            }
            catch (Exception ex)
            {

                Log.Error("Failed to add absent entry : " + ex);

                return false;
            }
        }

        public async Task<bool> AddAbsentByRange(List<Absent> absentList)
        {
            try
            {
                await _applicationDbContext.Absents.AddRangeAsync(absentList);
                
                 _applicationDbContext.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {

                Log.Error("Failed to update absent entry : " + ex.Message, ex);

                return false;
            }
            
        }

        public async Task<bool> EditAbsent(Absent absent)
        {

            try
            {
                _applicationDbContext.Absents.Update(absent);

                await _applicationDbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {

                Log.Error("Failed to update absent entry : " + ex.Message, ex);

                return false;
            }
           
        }

        public async Task<Absent> GetAbsent(int id)
        {
            try
            {
                var absent = _applicationDbContext.Absents.FirstOrDefault(u => u.Id == id);

                if (absent != null)
                {
                    return absent;
                }

                return null;
            }
            catch (Exception ex)
            {

                Log.Error("Failed to get absent:  " + ex.Message, ex);
                return null;
            }
        }

        public async Task<List<GetAbsentByDateResponse>> GetAbsentByDate(DateOnly date)
        {
            

            // get all employee codes

            var employeeCodes = await this.GetAllEmployeeCodes();

            // get all employees on leave on this day

            var leaveParameters = new GetLeaveResourceParameters() { StartDate = date, EndDate = date};

            var EmployeesOnLeave = await _leaveRepository.GetAllLeave(leaveParameters);

            // get all employees that are present

            var attendanceParameters = new GetAttendanceResourceParameters() { StartDate = DateTime.Today, EndDate = DateTime.Today };
            var presentEmployees = await _attendanceRepository.GetAllAttendances(attendanceParameters);

            // check for employees that were absent

            var absent = employeeCodes.Where(p => !presentEmployees.Any(p2 => p2.EmployeeCode == p.EmployeeCode));

            var responses = new List<GetAbsentByDateResponse>();

            foreach (var item in absent)
            {
                var response = new GetAbsentByDateResponse();

                response.EmployeeCode = item.EmployeeCode;
                response.EmployeeName = item.FirstName + " " + item.LastName;
                response.BranchName = item.BranchName;
                response.DepartmentName = item.DepartmentName;
                response.OnLeave = false; 
                
                responses.Add(response);
            }

            // mark those absent but on leave
            
            foreach (var item in responses)
            {
                foreach (var item2 in EmployeesOnLeave)
                {
                    if (item2.EmployeeCode == item.EmployeeCode)
                    {
                        item.OnLeave = true;
                    }
                }
            }
            
            return responses;


        }

        public async Task<List<Absent>> GetAllAbsent(GetAbsentResourceParameters parameters)
        {
            try
            {
                var query = _applicationDbContext.Absents.AsQueryable();


                if (parameters.StartDate != null && parameters.EndDate != null)
                {
                    parameters.EndDate = parameters.EndDate.Value.AddHours(23);

                    query = query.Where(u => u.DateCreated >= parameters.StartDate && u.DateCreated <= parameters.EndDate);
                }

                if (parameters.EmployeeCode != null)
                {
                    query = query.Where(u => u.EmployeeCode == parameters.EmployeeCode);
                }
                if (parameters.BranchName != null)
                {
                    query = query.Where(u => u.BranchName == parameters.BranchName);
                }
                if (parameters.DepartmentName != null)
                {
                    query = query.Where(u => u.DepartmentName == parameters.DepartmentName);
                }

                return query.ToList();
            }
            catch (Exception ex)
            {

                Log.Error("Failed to get absent entries:  " + ex.Message, ex);
                return null;
            }
        }

        public async Task<List<UMSEmployeeCode>> GetAllEmployeeCodes()
        {
            try
            {
                var link = _configuration.GetSection("Constants").GetSection("EmployeeCodes").Value.ToString();


                var builder = new UriBuilder(link);
                var url = builder.ToString();

                var response = await _httpClient.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }


                var createdTask = JsonConvert.DeserializeObject<List<UMSEmployeeCode>>(await response.Content.ReadAsStringAsync());

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
    }
}
