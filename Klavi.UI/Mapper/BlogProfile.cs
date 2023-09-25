using AutoMapper;
using Kalvi.Core.Entities;
using Klavi.UI.Areas.Admin.ViewModels.BlogViewModel;

namespace Klavi.UI.Mapper;

public class BlogProfile:Profile
{
	public BlogProfile()
	{
		CreateMap<BlogPostVM, Blog>().ReverseMap();
	}
}
