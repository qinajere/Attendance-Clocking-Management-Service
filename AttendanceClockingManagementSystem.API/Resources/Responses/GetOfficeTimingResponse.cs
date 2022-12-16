namespace AttendanceClockingManagementSystem.API.Resources.Responses
{
    public class GetOfficeTimingResponse
    {
        public TimeSpan ArrivalTime { get; set; }
        public TimeSpan KnockOffTime { get; set; }
    }
}
