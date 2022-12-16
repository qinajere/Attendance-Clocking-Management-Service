using AttendanceClockingManagementSystem.API.DataAccess.Model;
using AttendanceClockingManagementSystem.API.Resources.Parameters;
using AttendanceClockingManagementSystem.API.Resources.Responses;

namespace AttendanceClockingManagementSystem.API.Repositories
{
    public interface IAttendanceRepository
    {
        Task<bool> AddAttendance(Attendance attendance);
        Task<bool> EditAttendance(Attendance attendance);
        Task<List<Attendance>> GetAllAttendances(GetAttendanceResourceParameters parameters);
        Task<Attendance> GetAttendance(int id);
        Task<UMSResponse> EmployeeInfo(string code);

    }
}
