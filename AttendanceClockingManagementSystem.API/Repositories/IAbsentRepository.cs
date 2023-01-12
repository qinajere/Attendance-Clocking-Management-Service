﻿using AttendanceClockingManagementSystem.API.DataAccess.Model;
using AttendanceClockingManagementSystem.API.Resources.Parameters;
using AttendanceClockingManagementSystem.API.Resources.Responses;

namespace AttendanceClockingManagementSystem.API.Repositories
{
    public interface IAbsentRepository
    {
        Task<bool> AddAbsent(Absent absent);
        Task<bool> EditAbsent(Absent absent);
        Task<List<Absent>> GetAllAbsent(GetAbsentResourceParameters parameters);
        Task<Absent> GetAbsent(int id);
        Task<GetAbsentByDateResponse> GetAbsentByDate(DateOnly date);
    }
}
