namespace AttendanceClockingManagementSystem.API.Resources.Responses
{
    public class GetAttendanceResponse
    {
        public int Id { get; set; }
        public string EmployeeCode { get; set; }
        public string EmployeeName { get; set; }
        public TimeSpan ClockIn { get; set; }
        public TimeSpan ClockOut { get; set; }
        public string QRCodeID { get; set; }
        public string Status { get; set; }

    }
}
