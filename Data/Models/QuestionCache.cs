using Microsoft.Extensions.Caching.Memory;

namespace qAndA.Data.Models {
    public class QuestionCache: IQuestionCache {
        private MemoryCache _cache{get; set;}
        public QuestionCache()
        {
            _cache = new MemoryCache(new MemoryCacheOptions { SizeLimit = 100 });
        }
        private string GetCacheKey(int questionId) => $"Question-{questionId}";

        public QuestionGetSingleResponse Get(int questionId){
            QuestionGetSingleResponse q;
            _cache.TryGetValue(GetCacheKey(questionId), out q);
            return q;
        }

        public void set(QuestionGetSingleResponse question){
            var cacheEntryOptions = new MemoryCacheEntryOptions().SetSize(1);
            _cache.Set(
                GetCacheKey(question.QuestionId),
                question,
                cacheEntryOptions
            );
        }

        public void remove(int questionId){
            _cache.Remove(GetCacheKey(questionId));
        }
    }
}