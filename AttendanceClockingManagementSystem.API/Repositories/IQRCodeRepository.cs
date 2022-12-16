using AttendanceClockingManagementSystem.API.DataAccess.Model;
using AttendanceClockingManagementSystem.API.Resources;
using AttendanceClockingManagementSystem.API.Resources.Parameters;
using AttendanceClockingManagementSystem.API.Resources.Responses;

namespace AttendanceClockingManagementSystem.API.Repositories
{
    public interface IQRCodeRepository
    {
        Task<string> AddQRCode(QRCode qRCode);
        Task<bool> EditQRCode(QRCode qRCode);
        Task<List<QRCode>> GetAllQRCode(GetQRCodesResourceParameters parameters);
        Task<QRCode> GetQRCode(string id);
    }
}
