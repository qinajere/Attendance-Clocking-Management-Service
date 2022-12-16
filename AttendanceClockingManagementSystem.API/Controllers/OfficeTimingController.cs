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
    public class OfficeTimingController : ControllerBase
    {
        private readonly IOfficeTimingRepository _officeTimingRepository;
        private readonly IMapper _mapper;

        public OfficeTimingController(IOfficeTimingRepository officeTimingRepository, IMapper mapper)
        {
            _officeTimingRepository = officeTimingRepository;
            _mapper = mapper;
        }
        // GET: api/<OfficeTimingController>
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var result = await _officeTimingRepository.GetOfficeTiming();
            if (result != null)
            {
                var response = _mapper.Map<OfficeTiming, GetOfficeTimingResponse>(result);

                

                return Ok(response);
            }
            return BadRequest(result);
        }

      

        // POST api/<OfficeTimingController>
        [HttpPost]
        public async Task<ActionResult> Post(AddOfficeTimingDto time)
        {

            var arrivalTimespan = new TimeSpan(0,time.ArrivatimeHours, time.ArrivatimeMinutes,0);

            var knockOffTimespan = new TimeSpan(0,time.KnockOffTimeHours, time.knockOffTimeMinutes, 0);

            

            var officeTiming = new OfficeTiming()
            {
                ArrivalTime = arrivalTimespan,
                KnockOffTime = knockOffTimespan
            };

            var result =await  _officeTimingRepository.AddOfficeTiming(officeTiming);

            if (result)
            {
                return Ok(result);
            }

            return BadRequest(result);
            
        }

        // PUT api/<OfficeTimingController>/5
        [HttpPut("id")]
        public async Task<ActionResult> Put(AddOfficeTimingDto time)
        {
            var officeTiming = await _officeTimingRepository.GetOfficeTiming();



            var arrivalTimespan = new TimeSpan(0,time.ArrivatimeHours, time.ArrivatimeMinutes, 0);

            var knockOffTimespan = new TimeSpan(0,time.KnockOffTimeHours, time.knockOffTimeMinutes,0);





            officeTiming.ArrivalTime = arrivalTimespan;
            officeTiming.KnockOffTime = knockOffTimespan;

            var result = await _officeTimingRepository.EditOfficeTiming(officeTiming);

            if (result)
            {
                return Ok(result);
            }
            return BadRequest(result);

        }

        
    }
}
