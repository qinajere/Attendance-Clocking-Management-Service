namespace AttendanceClockingManagementSystem.API.Resources.DTOs
{
    public class AddAbsentDto
    {
        public string EmployeeCode { get; set; }
        public string Reason { get; set; }

        public bool Onleave { get; set; }
    }
}
