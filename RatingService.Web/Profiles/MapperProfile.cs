using RatingService.Domain.Entities;
using RatingService.Web.Models;

namespace RatingService.Web.Profiles
{
	public class MapperProfile : AutoMapper.Profile
	{
		protected override void Configure()
		{
			CreateMap<RegistrationViewModel, Enterprise>();
			CreateMap<Question, QuestionViewModel>();
			CreateMap<AnswerViewModel, Answer>();
		}
	}
}