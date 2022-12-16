namespace AttendanceClockingManagementSystem.API.DataAccess.Model
{
    public class QRCode
    {
        public string Id { get; set; }
        public string Code { get; set; }
        public string EmployeeCode { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        public bool ScanStatus { get; set; }
        public Attendance Attendance { get; set; }

    }
}
