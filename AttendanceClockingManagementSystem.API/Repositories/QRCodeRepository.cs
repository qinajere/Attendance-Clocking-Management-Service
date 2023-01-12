using AttendanceClockingManagementSystem.API.DataAccess;
using AttendanceClockingManagementSystem.API.DataAccess.Model;
using AttendanceClockingManagementSystem.API.Resources.Parameters;
using Serilog;

namespace AttendanceClockingManagementSystem.API.Repositories
{
    public class QRCodeRepository : IQRCodeRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IConfiguration _configuration;

        public QRCodeRepository( ApplicationDbContext applicationDbContext, IConfiguration configuration)
        {
            _applicationDbContext = applicationDbContext;
            _configuration = configuration;
        }
        public async Task<string> AddQRCode(QRCode qRCode)
        {
            try
            {
                var link = _configuration.GetSection("Constants").GetSection("ScanLink").Value.ToString();

                qRCode.Id = Guid.NewGuid().ToString();

                qRCode.Code = $"{link}{qRCode.Id}";

                qRCode.DateCreated = DateTimeOffset.Now;

                qRCode.ScanStatus = false;
                  
               _applicationDbContext.QRCodes.Add(qRCode);

               await  _applicationDbContext.SaveChangesAsync();

   
               return qRCode.Code;

                

            }
            catch (ObjectDisposedException ex)
            {

                Log.Error("Failed to add QRCode : " + ex.Message, ex);
                return null;   
            }
            catch (Exception ex)
            {

                Log.Error("Failed to add QRCode : " + ex.Message, ex);
                return null;
            }
        }

        public async Task<bool> EditQRCode(QRCode qRCode)
        {
            try
            {

               
                _applicationDbContext.QRCodes.Update(qRCode);

                await _applicationDbContext.SaveChangesAsync();

                return true;



            }
            catch (Exception ex)
            {

                Log.Error("Failed to update QRCode : " + ex.Message, ex);
                return false;
            }
        }

        public async Task<List<QRCode>> GetAllQRCode(GetQRCodesResourceParameters parameters)
        {
            try
            {
                var query = _applicationDbContext.QRCodes.AsQueryable();


                if (parameters.StartDate != null && parameters.EndDate != null)
                {
                    parameters.EndDate = parameters.EndDate.Value.AddHours(23);

                    query = query.Where(u => u.DateCreated >= parameters.StartDate && u.DateCreated <= parameters.EndDate);
                }

                if (parameters.EmployeeCode != null)
                {
                    query = query.Where(u => u.EmployeeCode == parameters.EmployeeCode);
                }

                if (parameters.Code != null)
                {
                    query = query.Where(u => u.Code == parameters.Code);
                }

                if (parameters.ScanStatus != null)
                {
                    query = query.Where(u => u.ScanStatus == parameters.ScanStatus);
                }

                return query.ToList();
            }
            catch (Exception ex) 
            {

                Log.Error("Failed to get QRCodes:  " + ex.Message, ex);
                return null;
            }
            

        }

        public async Task<QRCode> GetQRCode(string id)
        {
            try
            {
                var qrCode = _applicationDbContext.QRCodes.FirstOrDefault(u => u.Id == id);

                if (qrCode != null)
                {
                    return qrCode;
                }

                return null;
            }
            catch (Exception ex) 
            {

                Log.Error("Failed to get QRCodes:  " + ex.Message, ex);
                return null;
            }
        }
    }
}
