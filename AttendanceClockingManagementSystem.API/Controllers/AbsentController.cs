using AttendanceClockingManagementSystem.API.DataAccess.Model;
using AttendanceClockingManagementSystem.API.Repositories;
using AttendanceClockingManagementSystem.API.Resources.DTOs;
using AttendanceClockingManagementSystem.API.Resources.Parameters;
using AttendanceClockingManagementSystem.API.Resources.Responses;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Writers;
using Serilog;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AttendanceClockingManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AbsentController : ControllerBase
    {
        private readonly IAbsentRepository _absentRepository;
        private readonly IMapper _mapper;
        private readonly IAttendanceRepository _attendanceRepository;

        public AbsentController(IAbsentRepository absentRepository, IMapper mapper, IAttendanceRepository attendanceRepository)
        {

            _absentRepository = absentRepository;
            _mapper = mapper;
            _attendanceRepository = attendanceRepository;
        }
        // GET: api/<AbsentController>
        [HttpGet]
        public async Task<ActionResult> Get([FromQuery] GetAbsentResourceParameters parameters)
        {
            var absents = await _absentRepository.GetAllAbsent(parameters);

            return Ok(absents);
        }

        [HttpGet("Date")]
        public async Task<ActionResult> GetByDate()
        {
            var date =  DateTime.Now;

            var absents = await _absentRepository.GetAbsentByDate(date);

            return Ok(absents);
        }

        // GET api/<AbsentController>/5
        [HttpGet("id")]
        public async Task<ActionResult> GetById(int id)
        {
            var absent = await _absentRepository.GetAbsent(id);

            return Ok(absent);
        }


        [HttpPost("Add-Range")]
        public async Task<ActionResult> AddRange(List<AddAbsentRangeDto> addAbsentRangeDto)
        {
            var absentList = new List<Absent>();

            foreach (var item in addAbsentRangeDto)
            {
                var absent = _mapper.Map<AddAbsentRangeDto, Absent>(item);

                absent.DateCreated = DateTime.Now;

                absentList.Add(absent);
            }

            var result = await _absentRepository.AddAbsentByRange(absentList);

            return Ok(result);

        }


            // POST api/<AbsentController>
        [HttpPost]
        public async Task<ActionResult> Post(AddAbsentDto addAbsentDto)
        {
            var employeeInfo = await _attendanceRepository.EmployeeInfo(addAbsentDto.EmployeeCode);


            if (employeeInfo == null)
            {
                Log.Error("Failed to add absent entry due to failure to get Employee info");

                return BadRequest("Failed to get employee");
            }


            var absent = new Absent()
            {
                EmployeeCode = employeeInfo.EmployeeCode,
                EmployeeName = employeeInfo.FirstName + " " + employeeInfo.LastName,
                BranchName = employeeInfo.BranchName,
                DepartmentName = employeeInfo.DepartmentName,
                DateCreated = DateTime.Now,
                Reason = addAbsentDto.Reason,
                OnLeave = addAbsentDto.Onleave

            };

            var result = await _absentRepository.AddAbsent(absent);

            return Ok(result);

        }

        // PUT api/<AbsentController>/5
        [HttpPut("id")]
        public async Task<ActionResult> Put(int id, [FromBody] UpdateAbsentDto updateAbsent)
        {
            var existingAbsent = await _absentRepository.GetAbsent(id);

            existingAbsent.Reason = updateAbsent.Reason;
            

            var result = await _absentRepository.EditAbsent(existingAbsent);

            return Ok(result);


        }


    }
}
