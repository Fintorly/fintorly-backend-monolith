using Fintorly.Application.Dtos.CategoryDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fintorly.Application.Features.Queries.CategoryQueries
{
    public class GetAllCategoryQuery : IRequest<IResult<IList<CategoryDto>>>
    {
    }
}
