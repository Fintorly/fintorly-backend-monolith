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
    public class GetAllCategoryQueryHandler : IRequestHandler<GetAllCategoryQuery, IResult<IList<CategoryDto>>>
    {
        private ICategoryRepository _category;
        private IMapper _mapper;

        public GetAllCategoryQueryHandler(ICategoryRepository category, IMapper mapper)
        {
            _category = category;
            _mapper = mapper;
        }


        public async Task<IResult<IList<CategoryDto>>> Handle(GetAllCategoryQuery request, CancellationToken cancellationToken)
        {
            var result = await _category.GetAllAsync();
            var dto = _mapper.Map<IList<CategoryDto>>(result);
            return Result<IList<CategoryDto>>.Success(dto);
        }
    }
}
