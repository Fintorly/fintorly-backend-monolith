using Fintorly.Domain.Enums;
using System;

public class CreateCategoryCommand : IRequest<IResult>
{
	public string Title { get; set; }
	public string Description { get; set; }
	public string Content { get; set; }
	public decimal Price { get; set; }
	public PackageType PackageType { get; set; }
}
