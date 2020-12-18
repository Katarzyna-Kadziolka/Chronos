﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Chronos.Data;
using Chronos.Models.ToDoTasks;
using Chronos.Models.ToDoTasks.Requests;

namespace Chronos.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoTaskController : ControllerBase {
        private readonly IMapper _mapper;

        public ToDoTaskController(IMapper mapper) {
            _mapper = mapper;
        }

        [HttpPost]
        public ActionResult<ToDoTask> PostToDoTask(ToDoTaskPost task) {
            var t = _mapper.Map<ToDoTask>(task);
            ToDoTaskDb.Tasks.Add(t);
            return Ok(t);
        }

        [HttpGet]
        public ActionResult<IEnumerable<ToDoTask>> GetToDoTask(DateTime? dateFrom = null, DateTime? dateTo = null) {
            dateFrom ??= DateTime.Today;
            dateTo ??= DateTime.Today;
            if (dateTo < dateFrom) {
                return BadRequest($"DateTo {dateTo} cannot be before dateFrom {dateFrom}.");
            }
            var tasks = ToDoTaskDb.Tasks
                .Where(a => a.Date.Date <= dateTo && a.Date.Date >= dateFrom);

            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public ActionResult<ToDoTask> GetToDoTaskById(Guid id) {
            var task = ToDoTaskDb.Tasks.Find(o => o.Id == id);
            if (task == null) return NotFound();
            return Ok(task);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTask(Guid id) {
            var taskToRemove = ToDoTaskDb.Tasks.Find(o => o.Id == id);
            if (taskToRemove == null) return NotFound();
            ToDoTaskDb.Tasks.Remove(taskToRemove);
            return Ok(taskToRemove);
        }

        [HttpPatch("{id}")]
        public IActionResult PatchTask(Guid id, ToDoTask task) {
            var taskFromDb = ToDoTaskDb.Tasks.Find(o => o.Id == id);
            if (taskFromDb == null) return NotFound();
            var toDoTask = _mapper.Map(task, taskFromDb);
            return Ok(toDoTask);
        }
    }
}