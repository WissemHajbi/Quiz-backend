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
        QuestionGetSingleResponse PostQuestion(QuestionPostRequest question); 
        QuestionGetSingleResponse PutQuestion(int questionId, QuestionPutRequest question);
        void DeleteQuestion(int questionId);
        AnswerGetResponse PostAnswer(AnswerPostRequest answer);

    }
}
