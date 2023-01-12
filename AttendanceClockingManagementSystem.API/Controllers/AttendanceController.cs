using AttendanceClockingManagementSystem.API.DataAccess.Model;
using AttendanceClockingManagementSystem.API.Repositories;
using AttendanceClockingManagementSystem.API.Resources.DTOs;
using AttendanceClockingManagementSystem.API.Resources.Parameters;
using AttendanceClockingManagementSystem.API.Resources.Responses;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AttendanceClockingManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        private readonly IAttendanceRepository _attendanceRepository;
        private readonly IOfficeTimingRepository _officeTimingRepository;
        private readonly IMapper _mapper;

        public AttendanceController(IAttendanceRepository attendanceRepository, IOfficeTimingRepository officeTimingRepository, IMapper mapper)
        {
            _attendanceRepository = attendanceRepository;
            _officeTimingRepository = officeTimingRepository;
            _mapper = mapper;
        }
        // GET: api/<AttendanceController>
        [HttpGet]
        public async Task<ActionResult> Get([FromQuery]GetAttendanceResourceParameters parameters)
        {
            var timing = await _officeTimingRepository.GetOfficeTiming();

            if (timing != null)
            {
                var results = await _attendanceRepository.GetAllAttendances(parameters);

                var responses = new List<GetAttendanceResponse>();

                foreach (var r in results)
                {

                    if (r.ClockIn > timing.ArrivalTime)
                    {
                        var response = _mapper.Map<Attendance, GetAttendanceResponse>(r);

                        response.Status = "Late";

                        responses.Add(response);


                    }
                    else
                    {
                        var response = _mapper.Map<Attendance, GetAttendanceResponse>(r);

                        response.Status = "Early";

                        responses.Add(response);


                    }

                   
                }

                return Ok(responses);

            }
            return BadRequest();
        }

        // GET api/<AttendanceController>/5
        [HttpGet("id")]
        public async Task<ActionResult> Get(int id)
        {
            var timing = await _officeTimingRepository.GetOfficeTiming();
            var attendance = await _attendanceRepository.GetAttendance(id);

            if  (attendance != null)
            {
                var response = new GetAttendanceResponse();
               
                response = _mapper.Map<Attendance, GetAttendanceResponse>(attendance);

               
                    if (response.ClockIn > timing.ArrivalTime)
                    {

                        response.Status = "Late";

                    }
                    else
                    {
                       
                        response.Status = "Early";

                    }

                    return Ok(response);
            }

            return NotFound(attendance);
        }

        // POST api/<AttendanceController>
        [HttpPost]
        public async Task<ActionResult> Post(AddAttendanceDto addAttendanceDto)
        {
            var attendance = _mapper.Map<AddAttendanceDto,Attendance>(addAttendanceDto);    

            var result = await _attendanceRepository.AddAttendance(attendance);

            if (result)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        // PUT api/<AttendanceController>/5
        [HttpPut("ClockOut")]
        public async Task<ActionResult> Put(int id)
        {
            var exist =  await _attendanceRepository.GetAttendance(id);

            exist.ClockOut = DateTime.Now.TimeOfDay;

            var result = await _attendanceRepository.EditAttendance(exist);

            if (result)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPut]
        public async Task<ActionResult> Put(int id, EditAttendanceDto editAttendanceDto)
        {
            var exist = await _attendanceRepository.GetAttendance(id);

            exist.Comment = editAttendanceDto.Comment;

            var result = await _attendanceRepository.EditAttendance(exist);

            if (result)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }


    }
}
