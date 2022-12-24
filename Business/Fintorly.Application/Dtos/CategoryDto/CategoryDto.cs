using Fintorly.Domain.Enums;
using System;


namespace Fintorly.Application.Dtos.CategoryDto;

public class CategoryDto
{
	public string Title { get; set; }
	public string Description { get; set; }
	public string Content { get; set; }	
	public decimal Price { get; set; }
	public PackageType PackageType { get; set; }
}
