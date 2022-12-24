using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fintorly.Domain.Entities;
using Fintorly.Application.Features.Commands.CategoryCommands;
using Fintorly.Application.Dtos.CategoryDto;

namespace Fintorly.Application.Mapping
{
   
    public class CategoryProfile : Profile
    {
        public CategoryProfile() 
        {
            CreateMap<CreateCategoryCommand, Category>();
            CreateMap<Category, CategoryDto>().ReverseMap();

        }

    }
}
