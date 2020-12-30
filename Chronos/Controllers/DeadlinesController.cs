using System;
using System.Collections.Generic;
using System.Linq;
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

        [HttpDelete("{id}")]
        public IActionResult DeleteDeadline(Guid id) {
            var deadlineToRemove = ChronosDb.Deadlines.Find(o => o.Id == id);
            if (deadlineToRemove == null) return NotFound();
            ChronosDb.Deadlines.Remove(deadlineToRemove);
            return Ok(deadlineToRemove);
        }

        [HttpGet]
        public ActionResult<IEnumerable<Deadline>> GetDeadlines(DateTime? dateFrom = null, DateTime? dateTo = null) {
            dateFrom ??= DateTime.Today;
            dateTo ??= DateTime.Today;
            if (dateTo < dateFrom) {
                return BadRequest($"DateTo {dateTo} cannot be before dateFrom {dateFrom}.");
            }
            var deadlines = ChronosDb.Deadlines
                .Where(a => a.Date.Date <= dateTo && a.Date.Date >= dateFrom);

            return Ok(deadlines);
        }

        [HttpGet("{id}")]
        public ActionResult<Deadline> GetDeadlineById(Guid id) {
            var deadline = ChronosDb.Deadlines.Find(o => o.Id == id);
            if (deadline == null) return NotFound();
            return Ok(deadline);
        }

        [HttpPatch("{id}")]
        public IActionResult PatchDeadline(Guid id, Deadline deadline) {
            var deadlineFromDb = ChronosDb.Deadlines.Find(o => o.Id == id);
            if (deadlineFromDb == null) return NotFound();
            var updatedDeadline = _mapper.Map(deadline, deadlineFromDb);
            return Ok(updatedDeadline);
        }
    }
}