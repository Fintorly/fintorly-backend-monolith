using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fintorly.Application.Features.Queries.CategoryQueries
{
    public class GetByIdCategoryQuery:IRequest<IResult>
    {
        public Guid Id { get; set; }

    }
}
