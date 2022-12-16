namespace AttendanceClockingManagementSystem.API.Resources.Responses
{
    public class GetQRCodeResponse
    {
        public string Id { get; set; }
        public string Code { get; set; }
        public string EmployeeCode { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        public bool ScanStatus { get; set; }
    }
}
