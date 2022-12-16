using AttendanceClockingManagementSystem.API.DataAccess;
using AttendanceClockingManagementSystem.API.DataAccess.Model;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace AttendanceClockingManagementSystem.API.Repositories
{
    public class OfficeTimingRepository : IOfficeTimingRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public OfficeTimingRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<bool> AddOfficeTiming(OfficeTiming officeTiming)
        {

            try
            {
                var exists = await this.GetOfficeTiming();

                if (exists != null)
                {
                    return false;
                }

                _applicationDbContext.OfficeTimings.Add(officeTiming);

                await _applicationDbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {

                throw;
            }
          



            throw new NotImplementedException();
        }

        public async Task<bool> EditOfficeTiming(OfficeTiming officeTiming)
        {
            try
            {
                _applicationDbContext.Update(officeTiming);

                await _applicationDbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {

                Log.Error("Failed to update office timimg : " + ex.Message, ex);

                return false;
            }
        }

        public async Task<OfficeTiming> GetOfficeTiming()
        {
            try
            {
               var timing = await _applicationDbContext.OfficeTimings.FirstOrDefaultAsync();

                return timing;
            }
            catch (Exception ex)
            {

                Log.Error("Failed to get office timimg : " + ex.Message, ex);

                return null;
            }
        }
    }
}
