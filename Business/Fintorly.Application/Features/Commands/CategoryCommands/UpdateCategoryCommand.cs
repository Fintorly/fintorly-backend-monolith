using Fintorly.Application.Dtos.CategoryDto;
using Fintorly.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Fintorly.Application.Features.Commands.CategoryCommands
{
    public class UpdateCategoryCommand : IRequest<IResult<CategoryDto>>
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public IFormFile? File { get; set; }
        public string Content { get; set; }
        public decimal Price { get; set; }
        public PackageType PackageType { get; set; }
    }
}
