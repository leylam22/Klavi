using AutoMapper;
using Kalvi.Core.Entities;
using Klavi.UI.Areas.Admin.ViewModels.TestimonialViewModel;

namespace Klavi.UI.Mapper;

public class TestimonialProfile:Profile
{
	public TestimonialProfile()
	{
		CreateMap<TestimonialPostVM, Testimonial>().ReverseMap();
	}
}
