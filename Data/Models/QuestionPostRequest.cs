using System.ComponentModel.DataAnnotations;

namespace qAndA.Data.Models {
    public class QuestionPostRequest {
        [StringLength(50)]
        [Required(ErrorMessage = "please enter a correct title")]
        public string Title { get; set; }

        [Required(ErrorMessage = "please enter the content")]
        public string Content { get; set; }
    }
}