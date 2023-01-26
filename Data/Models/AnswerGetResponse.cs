namespace qAndA.Data.Models
{
    public class AnswerGetResponse
    {
        public int AnswerId { get; set; }
        public string Content { get; set; }
        public string Username { get; set; }
        public DateTime Created { get; set; }
    }
}
