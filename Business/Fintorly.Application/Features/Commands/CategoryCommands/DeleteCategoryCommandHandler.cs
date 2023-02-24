using System;
namespace Fintorly.Application.Features.Commands.CategoryCommands;
public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, IResult>
{
	private ICategoryRepository _categoryRepository;

	public DeleteCategoryCommandHandler(ICategoryRepository categoryRepository)
	{
		_categoryRepository = categoryRepository;
	}
	public async Task<IResult> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
	{
		var result = await _categoryRepository.DeleteByIdAsync(request.Id);
		if (result)
			return await Result.SuccessAsync();
		return await Result.FailAsync();
	}
}
