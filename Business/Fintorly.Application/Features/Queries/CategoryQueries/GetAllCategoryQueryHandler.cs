using AutoMapper;
using Fintorly.Application.Dtos.CategoryDto;
using Fintorly.Application.Interfaces.Repositories;
using Fintorly.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fintorly.Application.Features.Queries.CategoryQueries
{
    public class GetAllCategoryQueryHandler : IRequestHandler<GetAllCategoryQuery, IResult<CategoryDto>>
    {
        private ICategoryRepository _category;
        private IMapper _mapper;

        public GetAllCategoryQueryHandler(ICategoryRepository category, IMapper mapper)
        {
            _category = category;
            _mapper = mapper;
        }


        public async Task<IResult<CategoryDto>> Handle(GetAllCategoryQuery request, CancellationToken cancellationToken)
        {
            var result = await _category.GetByIdAsync(request.Id);
            var dto = _mapper.Map<CategoryDto>(result);
            return Result<CategoryDto>.Success(dto);


        }
    }
}
