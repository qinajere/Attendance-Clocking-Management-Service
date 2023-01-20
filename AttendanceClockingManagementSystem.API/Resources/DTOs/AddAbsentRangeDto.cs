namespace AttendanceClockingManagementSystem.API.Resources.DTOs
{
    public class AddAbsentRangeDto
    {
        public string EmployeeCode { get; set; }
        public string EmployeeName { get; set; }
        public string BranchName { get; set; }
        public string DepartmentName { get; set; }
        public bool OnLeave { get; set; }
    }
}
