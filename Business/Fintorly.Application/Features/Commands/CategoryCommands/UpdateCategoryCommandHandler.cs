using AutoMapper;
using Fintorly.Application.Interfaces.Repositories;
using Fintorly.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fintorly.Application.Features.Commands.CategoryCommands
{
    

    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, IResult>
    {
        private ICategoryRepository _categoryRepository;
        private IMapper _mapper;

        public UpdateCategoryCommandHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<IResult> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = _mapper.Map<Category>(request);
            return await _categoryRepository.UpdateAsync(category);
        }
    }
}
