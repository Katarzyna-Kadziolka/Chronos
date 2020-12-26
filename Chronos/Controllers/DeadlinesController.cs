using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Chronos.Data;
using Chronos.Models.Deadlines;
using Chronos.Models.Deadlines.Requests;

namespace Chronos.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class DeadlinesController : ControllerBase {
        private readonly IMapper _mapper;

        public DeadlinesController(IMapper mapper) {
            _mapper = mapper;
        }

        [HttpPost]
        public ActionResult<Deadline> PostDeadline(DeadlinePost deadline) {
            var t = _mapper.Map<Deadline>(deadline);
            ChronosDb.Deadlines.Add(t);
            return Ok(t);
        }

        //[HttpDelete("{id}")]
        //public IActionResult DeleteTask(Guid id) {
        //    var taskToRemove = ChronosDb.Tasks.Find(o => o.Id == id);
        //    if (taskToRemove == null) return NotFound();
        //    ChronosDb.Tasks.Remove(taskToRemove);
        //    return Ok(taskToRemove);
        //}

        //[HttpGet]
        //public ActionResult<IEnumerable<ToDoTask>> GetToDoTask(DateTime? dateFrom = null, DateTime? dateTo = null) {
        //    dateFrom ??= DateTime.Today;
        //    dateTo ??= DateTime.Today;
        //    if (dateTo < dateFrom) {
        //        return BadRequest($"DateTo {dateTo} cannot be before dateFrom {dateFrom}.");
        //    }
        //    var tasks = ChronosDb.Tasks
        //        .Where(a => a.Date.Date <= dateTo && a.Date.Date >= dateFrom);

        //    return Ok(tasks);
        //}

        //[HttpGet("{id}")]
        //public ActionResult<ToDoTask> GetToDoTaskById(Guid id) {
        //    var task = ChronosDb.Tasks.Find(o => o.Id == id);
        //    if (task == null) return NotFound();
        //    return Ok(task);
        //}



        //[HttpPatch("{id}")]
        //public IActionResult PatchTask(Guid id, ToDoTask task) {
        //    var taskFromDb = ChronosDb.Tasks.Find(o => o.Id == id);
        //    if (taskFromDb == null) return NotFound();
        //    var toDoTask = _mapper.Map(task, taskFromDb);
        //    return Ok(toDoTask);
        //}
    }
}