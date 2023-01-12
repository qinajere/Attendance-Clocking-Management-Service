using AttendanceClockingManagementSystem.API.DataAccess;
using AttendanceClockingManagementSystem.API.DataAccess.Model;
using AttendanceClockingManagementSystem.API.Resources.Parameters;
using AttendanceClockingManagementSystem.API.Resources.Responses;
using Serilog;

namespace AttendanceClockingManagementSystem.API.Repositories
{
    public class AbsentRepository : IAbsentRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly ILeaveRepository _leaveRepository;

        public AbsentRepository(ApplicationDbContext applicationDbContext, ILeaveRepository leaveRepository)
        {
            _applicationDbContext = applicationDbContext;
            _leaveRepository = leaveRepository;
        }
        public async Task<bool> AddAbsent(Absent absent)
        {
            try
            {
                _applicationDbContext.Absents.Add(absent);

                return true;    
            }
            catch (Exception ex)
            {

                Log.Error("Failed to add absent entry : " + ex);

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

        public async Task<GetAbsentByDateResponse> GetAbsentByDate(DateOnly date)
        {
            var response = new GetAbsentByDateResponse();
            // get all employee codes


            // get all employees on leave on this day

            var parameters = new GetLeaveResourceParameters() { StartDate = date, EndDate = date};

            var EmployeesOnLeave = _leaveRepository.GetAllLeave(parameters);

            // get all employees that are present


            return response;


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
    }
}
