namespace AttendanceClockingManagementSystem.API.Resources.Parameters
{
    public class GetAbsentResourceParameters
    {
        public DateTimeOffset? StartDate { get; set; }
        public DateTimeOffset? EndDate { get; set; }
        public string? EmployeeCode { get; set; }
        public string BranchName { get; set; }
        public string DepartmentName { get; set; }
    }
}
