using AutoMapper;
using Kalvi.Core.Entities;
using Klavi.UI.Areas.Admin.ViewModels.TeacherViewModel;

namespace Klavi.UI.Mapper;

public class TeacherProfile:Profile
{
	public TeacherProfile()
	{
		CreateMap<TeacherPostVm, Teacher>().ReverseMap();
	}
}
