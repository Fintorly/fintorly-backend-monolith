using System;
using Fintorly.Application.Dtos.FileDtos;
using Fintorly.Application.Utilities;
using Fintorly.Domain.Entities;

namespace Fintorly.Application.Features.Commands.CategoryCommands;

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
            return await Result.FailAsync("Tilte Alanı boş kalamaz");
        if (string.IsNullOrEmpty(request.Content))
            return await Result.FailAsync("Content Alanı boş kalamaz");
        if (request.Price <= 0)
            return await Result.FailAsync("Fiyat 0'dan büyük olmalıdır.");

        var category = _mapper.Map<Category>(request);
        var uploadResult =await FileUpload.UploadAlternative(request.File, "Categories");
        if (uploadResult.Succeeded)
        {
            var uploadResultData=_mapper.Map<UploadResult>(uploadResult.Data);
            category.FileName = uploadResultData.FilePath;
            category.FileName = uploadResultData.FileVirtualPath;
        }
        var result = await _category.AddAsync(category);
        return await Result.SuccessAsync();
    }
}