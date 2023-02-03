using qAndA.Data.Models;

namespace qAndA.Data {
    public interface IQuestionCache {
        QuestionGetSingleResponse Get(int questionId);
        void remove(int questionId);
        void set(QuestionGetSingleResponse question);
    }
}