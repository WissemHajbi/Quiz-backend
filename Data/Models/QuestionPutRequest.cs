using System.ComponentModel.DataAnnotations;

namespace qAndA.Data.Models {
    public class QuestionPutRequest {
        [StringLength(50)]
        public string Title { get; set; }
        public string Content { get; set; }
    }
}