using Dapper;
using Microsoft.Data.SqlClient;
using qAndA.Data.Models;

namespace qAndA.Data
{
    public class DataRepository : IDataRepository
    {
        private readonly string _connectionString;

        public DataRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection");
        }
        
        public AnswerGetResponse GetAnswer(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                return connection.QueryFirstOrDefault<AnswerGetResponse>(@"EXEC dbo.Answer_Get_ByAnswerId @AnswerId = @AnswerId", new { AnswerId = id });
            }

        }

        public QuestionGetSingleResponse GetQuestion(int Id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var q = connection.QueryFirstOrDefault<QuestionGetSingleResponse>(@"EXEC dbo.Question_GetSingle @QuestionId = @QuestionId", new { QuestionId = Id });
                if (q != null)
                {
                    q.Answers = connection.Query<AnswerGetResponse>(@"EXEC dbo.Answer_Get_ByQuestionId @QuestionId = @QuestionId", new { QuestionId = Id });
                }
                return q;
            }   
        }

        public IEnumerable<QuestionGetManyResponse> GetQuestions()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                return connection.Query<QuestionGetManyResponse>(@"EXEC dbo.Question_GetMany");
            }
        }

        public IEnumerable<QuestionGetManyResponse> GetQuestionsWithAnswers()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var questions = connection.Query<QuestionGetManyResponse>(@"EXEC dbo.Question_GetMany");
                foreach(var question in questions){
                    question.answers = connection.Query<AnswerGetResponse>(@"EXEC dbo.Answer_Get_ByQuestionId @QuestionId = @QuestionId", new {QuestionId = question.QuestionId}).ToList();
                }    
                return questions;
            }
        }

        public IEnumerable<QuestionGetManyResponse> GetQuestionsBySearch(string search)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                return connection.Query<QuestionGetManyResponse>(@"EXEC dbo.Question_GetMany_BySearch @Search = @Search", new { Search = search });
            }
        }

        public IEnumerable<QuestionGetManyResponse> GetUnansweredQuestions()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                return connection.Query<QuestionGetManyResponse>(@"EXEC dbo.Question_GetUnanswered");
            }
        }

        public bool QuestionExists(int Id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                return connection.QueryFirst<bool>(@"EXEC dbo.Question_Exists @QuestionId = @QuestionId", new { QuestionId = Id });
            }
        }

        public QuestionGetSingleResponse PostQuestion(QuestionPostFullRequest question){
            using(var connection = new SqlConnection(_connectionString)) {
                connection.Open();
                var questionId = connection.QueryFirst<int>(@"EXEC dbo.Question_Post @Title = @Title, @Content = @Content, @UserId = @UserId, @UserName = @UserName, @Created = @Created",question);
                return GetQuestion(questionId);
            }
        }

        public QuestionGetSingleResponse PutQuestion(int questionId,QuestionPutRequest question){
            using(var connection = new SqlConnection(_connectionString)){
                connection.Open();
                connection.Execute(@"EXEC dbo.Question_Put @QuestionId = @QuestionId, @Title = @Title, @Content = @Content",new {QuestionId = questionId,question.Title,question.Content});
            }
            return GetQuestion(questionId);
        }

        public void DeleteQuestion(int questionId){
            using(var connection = new SqlConnection(_connectionString)){
                connection.Open();
                connection.Execute(@"EXEC dbo.Question_Delete @QuestionId = @QuestionId",new {QuestionId = questionId});
            }
        }

        public AnswerGetResponse PostAnswer(AnswerPostFullRequest answer){
            using(var connection = new SqlConnection(_connectionString)){
                connection.Open();
                return connection.QueryFirst<AnswerGetResponse>(@"EXEC dbo.Answer_Post @QuestionId = @QuestionId, @Content = @Content, @UserId = @UserId, @UserName = @UserName, @Created = @Created", answer);
            }
        }
    }
}
