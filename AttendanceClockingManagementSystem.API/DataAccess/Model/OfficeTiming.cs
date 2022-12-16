using Microsoft.Data.SqlClient.Server;

namespace AttendanceClockingManagementSystem.API.DataAccess.Model
{
    public class OfficeTiming
    {
        public int Id { get; set; }
        public TimeSpan ArrivalTime { get; set; }
        public TimeSpan KnockOffTime { get; set; }
        
    }
}
