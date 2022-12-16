namespace AttendanceClockingManagementSystem.API.Resources.DTOs
{
    public class AddOfficeTimingDto
    {
        public int ArrivatimeHours { get; set; }
        public int ArrivatimeMinutes { get; set; }
        public int KnockOffTimeHours { get; set; }
        public int knockOffTimeMinutes { get; set; }
    }
}
