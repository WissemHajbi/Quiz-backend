using qAndA.Data.Models;
namespace qAndA.Data
{
    public interface IDataRepository
    {
        IEnumerable<QuestionGetManyResponse> GetQuestions();
        IEnumerable<QuestionGetManyResponse> GetQuestionsBySearch(string search);
        IEnumerable<QuestionGetManyResponse> GetUnansweredQuestions();
        QuestionGetSingleResponse GetQuestion(int Id);
        bool QuestionExists(int Id);
        AnswerGetResponse GetAnswer(int id);
    }
}
