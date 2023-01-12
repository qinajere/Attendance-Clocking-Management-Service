namespace AttendanceClockingManagementSystem.API.DataAccess.Model
{
    public class Leave
    {
        public int Id { get; set; }
        public string EmployeeCode { get; set; }
        public string EmployeeName { get; set; }
        public string BranchName { get; set; }
        public string DepartmentName { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        public DateOnly From { get; set; }
        public DateOnly To { get; set; }
    }
}
