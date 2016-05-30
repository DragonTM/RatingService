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
			CreateMap<Rating, RatingViewModel>()
				.ForMember(vm => vm.EnterpriseName, m => m.MapFrom(r => r.Enterprise.Name));
			CreateMap<Suggestion, SuggestionViewModel>();
		}
	}
}