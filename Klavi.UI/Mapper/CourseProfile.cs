using AutoMapper;
using Kalvi.Core.Entities;
using Klavi.UI.Areas.Admin.ViewModels.CourseViewModel;

namespace Klavi.UI.Mapper;

public class CourseProfile:Profile
{
	public CourseProfile()
	{
		CreateMap<CoursePostVM, Course>().ReverseMap();
	}
}
