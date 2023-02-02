using System.ComponentModel.DataAnnotations;

namespace qAndA.Data.Models {
    public class AnswerPostFullRequest {
        [Required(ErrorMessage = "please enter the questionId")]
        // make QuestionId nullable because int type defaults to 0, so the Required attribute wont catch the error
        public int? QuestionId { get; set; }
        [Required(ErrorMessage = "please enter the content")]
        public string Content { get; set; }
        public string UserId { get; set; }
        public string Username { get; set; }
        public DateTime Created { get; set; }
    }
}