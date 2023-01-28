namespace qAndA.Data.Models {
    public class AnswerPostRequest {
        public int QuestionId { get; set; }
        public string Content { get; set; }
        public string UserId { get; set; }
        public string Username { get; set; }
        public DateTime Created { get; set; }
    }
}