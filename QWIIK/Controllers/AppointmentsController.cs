using Microsoft.AspNetCore.Mvc;
using QWIIK.Models;
using QWIIK.Services;

[ApiController]
[Route("api/[controller]")]
public class AppointmentsController : ControllerBase
{
    private readonly IAppointmentService _appointmentService;
    private readonly List<DateTime> _offDays;
    private int _maxAppointmentsPerDay;
    public AppointmentsController(IAppointmentService appointmentService)
    {
        _appointmentService = appointmentService;
        _offDays = new List<DateTime>();
        _maxAppointmentsPerDay = 10;
    }

    [HttpGet("queue")]
    public IActionResult GetQueue(DateTime date)
    {
        var queue = _appointmentService.GetQueue(date);
        return Ok(queue);
    }

    [HttpPost("issueToken")]
    public IActionResult IssueToken([FromBody] CustomerModels customer, DateTime date)
    {
        var token = _appointmentService.IssueToken(customer, date);
        return Ok(token);
    }

    [HttpPost("bookAppointment")]
    public IActionResult BookAppointment([FromBody] CustomerModels customer, DateTime date)
    {
        if (IsDayOff(date))
            return BadRequest("This day is off.");

        if (_appointmentService.GetQueue(date).Count >= _maxAppointmentsPerDay)
            return BadRequest("Maximum appointments reached for this day.");

        _appointmentService.BookAppointment(customer, date);
        return Ok("Appointment booked successfully");
    }

    [HttpPost("offdays")]
    public ActionResult AddOffDay([FromBody] DateTime date)
    {
        _offDays.Add(date);
        return Ok();
    }

    [HttpPost("maxappointments")]
    public ActionResult SetMaxAppointmentsPerDay([FromBody] int maxAppointments)
    {
        _maxAppointmentsPerDay = maxAppointments;
        return Ok();
    }

    private bool IsDayOff(DateTime date)
    {
        return _offDays.Contains(date.Date);
    }

}