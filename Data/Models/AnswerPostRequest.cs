using System.ComponentModel.DataAnnotations;

namespace qAndA.Data.Models {
    public class AnswerPostRequest {
        [Required(ErrorMessage = "please enter the content")]
        public string Content { get; set; }
    }
}