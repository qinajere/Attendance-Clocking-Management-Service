﻿namespace AttendanceClockingManagementSystem.API.Resources.Responses
{
    public class GetAbsentByDateResponse
    {
        public string EmployeeCode { get; set; }
        public string EmployeeName { get; set; }
        public string BranchName { get; set; }
        public string DepartmentName { get; set; }
        public bool OnLeave { get; set; }
    }
}
