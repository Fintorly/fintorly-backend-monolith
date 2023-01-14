using System;
using Fintorly.Domain.Entities;

namespace Fintorly.Application.Features.Commands.CategoriesCommands;

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, IResult>
{
	private ICategoryRepository _category;
	private IMapper _mapper;

	public CreateCategoryCommandHandler(ICategoryRepository category, IMapper mapper)
	{
		_category = category;
		_mapper = mapper;
	}

	public async Task<IResult> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
	{
		if (string.IsNullOrEmpty(request.Title))
			return Result.Fail("Tilte Alanı boş kalamaz");
        if (string.IsNullOrEmpty(request.Content))
            return Result.Fail("Content Alanı boş kalamaz");
		if (request.Price <= 0)
			return Result.Fail("Fiyat 0'dan büyük olmalıdır.");


		var category = _mapper.Map<Category>(request);
		var result = await _category.AddAsync(category);
        return Result.Success();
    }


}
