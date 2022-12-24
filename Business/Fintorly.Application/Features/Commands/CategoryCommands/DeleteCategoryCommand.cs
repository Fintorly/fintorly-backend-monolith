using System;

public class DeleteCategoryCommand:IRequest<IResult>
{
	public Guid Id { get; set; }
}
