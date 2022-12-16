namespace AttendanceClockingManagementSystem.API.Resources.Parameters
{
    public class GetQRCodesResourceParameters
    {
        public DateTimeOffset? StartDate { get; set; }
        public DateTimeOffset? EndDate { get; set; }
        public string? Code { get; set; }
        public string? EmployeeCode { get; set; }
        public bool? ScanStatus { get; set; }
       
    }
}
