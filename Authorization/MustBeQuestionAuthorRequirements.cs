using Microsoft.AspNetCore.Authorization;

namespace qAndA.Authorization {
    public class MustBeQuestionAuthorRequirements : IAuthorizationRequirement {
        public MustBeQuestionAuthorRequirements()
        {
            
        }
    }
}