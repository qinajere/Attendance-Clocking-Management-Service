using AttendanceClockingManagementSystem.API.DataAccess.Model;
using AttendanceClockingManagementSystem.API.Resources.DTOs;
using AutoMapper;

namespace AttendanceClockingManagementSystem.API.Resources.Mapping
{
    public class ResourcetoModelProfile : Profile
    {
        public ResourcetoModelProfile()
        {
            CreateMap<AddQRCodeDto, QRCode>();
            CreateMap<AddOfficeTimingDto, OfficeTiming>();
            CreateMap<AddAttendanceDto, Attendance>();
            CreateMap<AddAbsentRangeDto, Absent>();

        }
    }
}
