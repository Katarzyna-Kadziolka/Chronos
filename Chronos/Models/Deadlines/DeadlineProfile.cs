using System;
using AutoMapper;
using Chronos.Models.Deadlines.Requests;

namespace Chronos.Models.Deadlines {
    public class DeadlineProfile : Profile {
        public DeadlineProfile() {
            CreateMap<DeadlinePost, Deadline>()
                .ForMember(t => t.Id, o =>
                    o.MapFrom(post => Guid.NewGuid()));
        }
    }
}