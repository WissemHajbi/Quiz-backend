using qAndA.Data.Models;
namespace qAndA.Data
{
    public interface IDataRepository
    {
        Task<IEnumerable<QuestionGetManyResponse>> GetQuestionsAsync();
        IEnumerable<QuestionGetManyResponse> GetQuestionsWithAnswers();
        IEnumerable<QuestionGetManyResponse> GetQuestionsBySearch(string search);
        IEnumerable<QuestionGetManyResponse> GetUnansweredQuestions();
        QuestionGetSingleResponse GetQuestion(int Id);
        bool QuestionExists(int Id);
        AnswerGetResponse GetAnswer(int id);
        QuestionGetSingleResponse PostQuestion(QuestionPostFullRequest question); 
        QuestionGetSingleResponse PutQuestion(int questionId, QuestionPutRequest question);
        void DeleteQuestion(int questionId);
        AnswerGetResponse PostAnswer(AnswerPostFullRequest answer);

    }
}
