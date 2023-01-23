namespace AttendanceClockingManagementSystem.API.Resources.Parameters
{
    public class GetLeaveResourceParameters
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? EmployeeCode { get; set; }
        public string? BranchName { get; set; }
        public string? DepartmentName { get; set; }
    }
}
