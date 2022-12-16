using AttendanceClockingManagementSystem.API.DataAccess.Model;
using AttendanceClockingManagementSystem.API.Resources.Parameters;
using AttendanceClockingManagementSystem.API.Resources.Responses;

namespace AttendanceClockingManagementSystem.API.Repositories
{
    public interface IOfficeTimingRepository
    {
        Task<bool> AddOfficeTiming(OfficeTiming officeTiming);
        Task<bool> EditOfficeTiming(OfficeTiming officeTiming);
        Task<OfficeTiming> GetOfficeTiming();
    }
}
