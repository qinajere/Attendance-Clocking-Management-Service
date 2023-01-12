namespace AttendanceClockingManagementSystem.API.DataAccess.Model
{
    public class Attendance
    {
        public int Id { get; set; }
        public string EmployeeCode { get; set; }
        public string EmployeeName { get; set; }
        public string Branch { get; set; }
        public string Department { get; set; }
        public TimeSpan ClockIn { get; set; }
        public TimeSpan ClockOut { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        public string QRCodeID { get; set; }
        public QRCode QRCode { get; set; }
        public string Comment { get; set; }
       
    }
}
