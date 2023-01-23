namespace AttendanceClockingManagementSystem.API.Resources.Responses
{
    public class GetLeaveResponse
    {
        public int Id { get; set; }
        public string EmployeeCode { get; set; }
        public string EmployeeName { get; set; }
        public string BranchName { get; set; }
        public string DepartmentName { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        public string From { get; set; }
        public string To { get; set; }
    }
}
