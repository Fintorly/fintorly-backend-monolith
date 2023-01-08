using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fintorly.Application.Features.Queries.CategoryQueries
{
    public class GetByIdCategoryQueryHandler : IRequestHandler<GetByIdCategoryQuery, IResult>
    {
        private readonly ICategoryRepository _categoryRepository;

        public GetByIdCategoryQueryHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IResult> Handle(GetByIdCategoryQuery request, CancellationToken cancellationToken)
        {
            return await _categoryRepository.GetByIdAsync(request.Id);
        }
    }
}
