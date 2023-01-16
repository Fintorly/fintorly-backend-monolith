using AutoMapper;
using Fintorly.Application.Dtos.CategoryDto;
using Fintorly.Application.Interfaces.Repositories;
using Fintorly.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fintorly.Application.Features.Commands.CategoryCommands
{
    

    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, IResult<CategoryDto>>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public UpdateCategoryCommandHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<IResult<CategoryDto>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var result = await _categoryRepository.GetByIdAsync(request.Id);
            var dto = _mapper.Map<CategoryDto>(result);
            return Result<CategoryDto>.Success(dto);
        }
    }
}
