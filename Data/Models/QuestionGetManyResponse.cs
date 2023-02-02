namespace qAndA.Data.Models
{
    public class QuestionGetManyResponse
    {
        public int QuestionId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Username { get; set; }
        public DateTime Created { get; set; }
        public List<AnswerGetResponse> answers { get; set; }
    }
}
