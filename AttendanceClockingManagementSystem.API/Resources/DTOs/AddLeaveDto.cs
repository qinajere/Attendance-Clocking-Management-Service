namespace AttendanceClockingManagementSystem.API.Resources.DTOs
{
    public class AddLeaveDto
    {
        public string EmployeeCode { get; set; }
        public DateOnly From { get; set; }
        public DateTime To { get; set; }
    }
}
