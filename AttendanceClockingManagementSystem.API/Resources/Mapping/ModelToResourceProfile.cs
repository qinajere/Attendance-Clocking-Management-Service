using AttendanceClockingManagementSystem.API.DataAccess.Model;
using AttendanceClockingManagementSystem.API.Resources.Responses;
using AutoMapper;

namespace AttendanceClockingManagementSystem.API.Resources.Mapping
{
    public class ModelToResourceProfile : Profile
    {
        public ModelToResourceProfile()
        {
            CreateMap<Attendance, GetAttendanceResponse>();
            CreateMap<QRCode, GetQRCodeResponse>();
            CreateMap<OfficeTiming, GetOfficeTimingResponse>();
        }
    }
}
