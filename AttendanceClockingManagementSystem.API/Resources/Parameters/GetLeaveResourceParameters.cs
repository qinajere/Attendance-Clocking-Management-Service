namespace AttendanceClockingManagementSystem.API.Resources.Parameters
{
    public class GetLeaveResourceParameters
    {
        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public string? EmployeeCode { get; set; }
        public string? BranchName { get; set; }
        public string? DepartmentName { get; set; }
    }
}
