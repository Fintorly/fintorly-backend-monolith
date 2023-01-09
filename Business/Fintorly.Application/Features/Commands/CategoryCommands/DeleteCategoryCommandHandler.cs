using System;

public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, IResult>
{
	private ICategoryRepository _categoryRepository;

	public DeleteCategoryCommandHandler(ICategoryRepository categoryRepository)
	{
		_categoryRepository = categoryRepository;
	}
	public async Task<IResult> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
	{
		return await _categoryRepository.DeleteByIdAsync(request.Id);
	}
}
