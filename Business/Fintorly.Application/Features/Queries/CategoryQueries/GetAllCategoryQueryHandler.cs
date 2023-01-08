using Fintorly.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fintorly.Application.Features.Queries.CategoryQueries
{
    public class GetAllCategoryQueryHandler : IRequestHandler<GetAllCategoryQuery, IResult>
    {
        private ICategoryRepository _category;
        private IMapper _mapper;

        public GetAllCategoryQueryHandler(ICategoryRepository category, IMapper mapper)
        {
            _category = category;
            _mapper = mapper;
        }


        public async Task<IResult> Handle(GetAllCategoryQuery request, CancellationToken cancellationToken)
        {
            var result = await _category.GetAllAsync();
            var categories = result.Data as List<Category>;
            var categoriesDto = _mapper.Map<List<Category>>(categories);
            result.Data = categoriesDto;
            return result;


        }
    }
}
