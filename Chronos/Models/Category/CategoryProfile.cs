using System;
using AutoMapper;
using Chronos.Models.Category.Requests;

namespace Chronos.Models.Category {
    public class CategoryProfile : Profile {
        public CategoryProfile() {
            CreateMap<CategoryPost, Category>()
                .ForMember(a => a.Id, o =>
                    o.MapFrom(post => Guid.NewGuid()));
        }
    }
}