using AttendanceClockingManagementSystem.API.DataAccess;
using AttendanceClockingManagementSystem.API.DataAccess.Model;
using AttendanceClockingManagementSystem.API.Resources.Parameters;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace AttendanceClockingManagementSystem.API.Repositories
{
    public class LeaveRepository : ILeaveRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public LeaveRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public async Task<bool> AddLeave(Leave leave)
        {
            try
            {
                _applicationDbContext.Leaves.Add(leave);
                _applicationDbContext.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {

                Log.Error("Failed to add leave entry : " + ex);

                return false;
            }
        }

        public async Task<bool> DeleteLeave(Leave leave)
        {
            try
            {
                _applicationDbContext.Leaves.Remove(leave);
                _applicationDbContext.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {

                Log.Error("Failed to delete leave entry : " + ex.Message, ex);

                return false;
            }
        }

        public async Task<bool> EditLeave(Leave leave)
        {
            try
            {
                _applicationDbContext.Leaves.Update(leave);

                await _applicationDbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {

                Log.Error("Failed to update leave entry : " + ex.Message, ex);

                return false;
            }
        }

        public async Task<List<Leave>> GetAllLeave(GetLeaveResourceParameters parameters)
        {
            try
            {
                var query = _applicationDbContext.Leaves.AsQueryable();


                if (parameters.StartDate != null && parameters.EndDate != null)
                {
                    if (parameters.StartDate == parameters.EndDate)
                    {
                        query = query.Where(u => parameters.StartDate >= u.From && parameters.EndDate <= u.To);
                    }
                    else 
                    {
                        query = query.Where(u => u.From <= parameters.EndDate && parameters.StartDate <= u.To);
                    }
                   
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

                Log.Error("Failed to get leave entries:  " + ex.Message, ex);
                return null;
            }
        }

        public async Task<Leave> GetLeave(int id)
        {
            try
            {
                var leave = _applicationDbContext.Leaves.FirstOrDefault(u => u.Id == id);

                if (leave != null)
                {
                    return leave;
                }

                return null;
            }
            catch (Exception ex)
            {

                Log.Error("Failed to get absent:  " + ex.Message, ex);
                return null;
            }
        }
    }
}
