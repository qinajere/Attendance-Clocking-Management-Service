using AttendanceClockingManagementSystem.API.DataAccess.Model;
using AttendanceClockingManagementSystem.API.Repositories;
using AttendanceClockingManagementSystem.API.Resources.DTOs;
using AttendanceClockingManagementSystem.API.Resources.Parameters;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Serilog;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AttendanceClockingManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveController : ControllerBase
    {
        private readonly ILeaveRepository _leaveRepository;
        private readonly IMapper _mapper;
        private readonly IAttendanceRepository _attendanceRepository;

        public LeaveController(ILeaveRepository leaveRepository, IMapper mapper, IAttendanceRepository attendanceRepository)
        {

            _leaveRepository = leaveRepository;
            _mapper = mapper;
            _attendanceRepository = attendanceRepository;
        }
        // GET: api/<AbsentController>
        [HttpGet]
        public async Task<ActionResult> Get([FromQuery] GetLeaveResourceParameters parameters)
        {
            var leaves = await _leaveRepository.GetAllLeave(parameters);

            return Ok(leaves);
        }

        // GET api/<AbsentController>/5
        [HttpGet("id")]
        public async Task<ActionResult> GetById(int id)
        {
            var leave = await _leaveRepository.GetLeave(id);

            return Ok(leave);
        }

        // POST api/<AbsentController>
        [HttpPost]
        public async Task<ActionResult> Post(AddLeaveDto addLeaveDto)
        {
            var employeeInfo = await _attendanceRepository.EmployeeInfo(addLeaveDto.EmployeeCode);


            if (employeeInfo == null)
            {
                Log.Error("Failed to add leave entry due to failure to get Employee info");

                return BadRequest("Failed to get employee");
            }

            var from = DateOnly.FromDateTime(dateTime: addLeaveDto.From);
            var to = DateOnly.FromDateTime( dateTime: addLeaveDto.To);
           



            var leave = new Leave()
            {
                EmployeeCode = employeeInfo.EmployeeCode,
                EmployeeName = employeeInfo.FirstName + " " + employeeInfo.LastName,
                BranchName = employeeInfo.BranchName,
                DepartmentName = employeeInfo.DepartmentName,
                DateCreated = DateTime.Now,
                From = from,
                To = to,
               
            };

            var result = await _leaveRepository.AddLeave(leave);

            return Ok(result);

        }

        // PUT api/<AbsentController>/5
        [HttpPut("id")]
        public async Task<ActionResult> Put(int id, [FromBody] UpdateLeaveDto updateLeave)
        {
            var existingLeave = await _leaveRepository.GetLeave(id);



            existingLeave.DateCreated = DateTime.Now;
            existingLeave.From = DateOnly.FromDateTime(dateTime: updateLeave.From); 
            existingLeave.To = DateOnly.FromDateTime(dateTime: updateLeave.To);

            var result = await _leaveRepository.EditLeave(existingLeave);

            return Ok(result);


        }

        [HttpDelete("id")]
        public async Task<ActionResult> Put(int id)
        {
            var existingLeave = await _leaveRepository.GetLeave(id);

            var result = await _leaveRepository.DeleteLeave(existingLeave);

            return Ok(result);


        }

    }
}
