using System.ComponentModel.DataAnnotations;

namespace qAndA.Data.Models {
    public class QuestionPostFullRequest {
        [StringLength(50)]
        [Required(ErrorMessage = "please enter a correct title")]
        public string Title { get; set; }
        
        [Required(ErrorMessage = "please enter the content")]
        public string Content { get; set; }
        public string UserId { get; set; }
        public string Username { get; set; }
        public DateTime Created { get; set; }
    }
}