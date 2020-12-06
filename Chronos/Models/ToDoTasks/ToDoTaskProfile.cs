using System;
using AutoMapper;
using Chronos.Models.ToDoTasks.Requests;

namespace Chronos.Models.ToDoTasks {
    public class ToDoTaskProfile : Profile {
        public ToDoTaskProfile() {
            CreateMap<ToDoTaskPost, ToDoTask>()
                .ForMember(t => t.Id, o => 
                    o.MapFrom(post => Guid.NewGuid()));
        }
    }
}