using AttendanceClockingManagementSystem.API.DataAccess.Model;
using AttendanceClockingManagementSystem.API.Repositories;
using AttendanceClockingManagementSystem.API.Resources.DTOs;
using AttendanceClockingManagementSystem.API.Resources.Parameters;
using AttendanceClockingManagementSystem.API.Resources.Responses;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AttendanceClockingManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QRCodeController : ControllerBase
    {
        private readonly IQRCodeRepository _qRCodeRepository;
        private readonly IMapper _mapper;

        public QRCodeController(IQRCodeRepository qRCodeRepository, IMapper mapper)
        {
            _qRCodeRepository = qRCodeRepository;
            _mapper = mapper;
        }
        // GET: api/<QRCodeController>
        [HttpGet]
        public async Task<ActionResult> Get([FromQuery]GetQRCodesResourceParameters parameters)
        {
            var qRCodes = await  _qRCodeRepository.GetAllQRCode(parameters);

            var responses = new List<GetQRCodeResponse>();

            foreach (var item in qRCodes)
            {
              var response = _mapper.Map<QRCode, GetQRCodeResponse>(item);

                responses.Add(response);

            }

            return Ok(responses);
        }

        // GET api/<QRCodeController>/5
        [HttpGet("id")]
        public async Task<ActionResult> GetById(string id)
        {
           var qRCode = await _qRCodeRepository.GetQRCode(id);
           var response = _mapper.Map<QRCode, GetQRCodeResponse>(qRCode);
            return Ok(response);
        }

        // POST api/<QRCodeController>
        [HttpPost("Create")]
        public async Task<ActionResult> Create(AddQRCodeDto addQRCodeDto)
        {
            // check if qr was already generated

            var parameters = new GetQRCodesResourceParameters()
            {
                StartDate = DateTime.Now.Date,
                EndDate = DateTime.Now.Date,
                EmployeeCode = addQRCodeDto.EmployeeCode
            };

            var exist = await _qRCodeRepository.GetAllQRCode(parameters);

            string existingcode = "";

            if (exist.Count > 0 )
            {
                foreach (var item in exist)
                {
                   existingcode = item.Code;
                }

                return Ok(existingcode);
            }

            var qRCode = _mapper.Map<AddQRCodeDto, QRCode>(addQRCodeDto);

            var result = await _qRCodeRepository.AddQRCode(qRCode);

            return Ok(result);
        }

       

       
    }
}
