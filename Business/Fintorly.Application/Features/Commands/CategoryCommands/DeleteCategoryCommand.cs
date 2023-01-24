using System;

namespace Fintorly.Application.Features.Commands.CategoryCommands;
public class DeleteCategoryCommand:IRequest<IResult>
{
	public Guid Id { get; set; }
}
