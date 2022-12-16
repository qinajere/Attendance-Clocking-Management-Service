using AttendanceClockingManagementSystem.API.DataAccess.Model;

namespace AttendanceClockingManagementSystem.API.Resources.Parameters
{
    public class GetAttendanceResourceParameters
    {
        public DateTimeOffset? StartDate { get; set; }
        public DateTimeOffset? EndDate { get; set; }
        public string? EmployeeCode { get; set; }
        public string? QRCode { get; set; }
        public bool? Early { get; set; }
        public bool? Late { get; set; }


    }
}
