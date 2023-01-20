using AttendanceClockingManagementSystem.API.DataAccess.Model;
using AttendanceClockingManagementSystem.API.Resources.Parameters;

namespace AttendanceClockingManagementSystem.API.Repositories
{
    public interface ILeaveRepository
    {
        Task<bool> AddLeave(Leave leave);
        Task<bool> EditLeave(Leave leave);
        Task<List<Leave>> GetAllLeave(GetLeaveResourceParameters parameters);
        Task<Leave> GetLeave(int id);
        Task<bool> DeleteLeave(Leave leave);
    }
}
