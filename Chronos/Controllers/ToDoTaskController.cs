﻿using Microsoft.AspNetCore.Mvc;
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

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteTask(Guid id) {
        //    var taskToRemove = ToDoTaskDb.Tasks.Find(o => o.Id == id);
        //    if (taskToRemove == null) return NotFound();
        //    ToDoTaskDb.Tasks.Remove(taskToRemove);
        //    return Ok(taskToRemove);
        //}

        //[HttpPatch("{id}")]
        //public async Task<IActionResult> PatchTask(Guid id, ToDoTask task) {
        //    var taskFromDb = ToDoTaskDb.Tasks.Find(o => o.Id == id);
        //    if (taskFromDb == null) return NotFound();
        //    var toDoTask = _mapper.Map(task, taskFromDb);
        //    return Ok(toDoTask);

        //}
    }
}