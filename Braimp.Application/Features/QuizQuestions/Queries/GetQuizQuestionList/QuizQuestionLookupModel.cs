using AutoMapper;
using Braimp.Application.Mapping;
using Braimp.Domain.Entities.Quizzes;
using Braimp.Domain.Entities.Quizzes.Enums;
using System.Text.Json.Serialization;

namespace Braimp.Application.Features.QuizQuestions.Queries.GetQuizQuestionList;
public class QuizQuestionLookupModel : IMapWith<QuizQuestion>
{
    public Guid Id { get; set; }
    public string Text { get; set; } = string.Empty;
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public QuestionType QuestionType { get; set; }
    public int Weight { get; set; } = 1;

    public string File { get; set; } = string.Empty;
    public List<QuestionOptionModel> QuestionOptionModels { get; set; } 
        = new List<QuestionOptionModel>();

    public void Mapping(Profile profile)
    {
        profile.CreateMap<QuizQuestion, QuizQuestionLookupModel>()
            .ForMember(
            dest => dest.QuestionOptionModels,
            opt => opt.MapFrom(src => src.QuestionOptions)
        );

        profile.CreateMap<QuestionOption, QuestionOptionModel>();
    }
}
