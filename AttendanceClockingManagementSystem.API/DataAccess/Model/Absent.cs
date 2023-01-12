namespace AttendanceClockingManagementSystem.API.DataAccess.Model
{
    public class Absent
    {
        public int Id { get; set; }
        public string EmployeeCode { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        public string EmployeeName { get; set; }
        public string BranchName { get; set; }
        public string DepartmentName { get; set; }
        public string? Reason { get; set; }
        public bool OnLeave { get; set; }
    }
}
