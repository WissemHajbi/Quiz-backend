using Microsoft.AspNetCore.Authorization;
using qAndA.Data;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace qAndA.Authorization {
    public class MustBeQuestionAuthorHandler: AuthorizationHandler<MustBeQuestionAuthorRequirements> {
        private readonly IDataRepository _dataRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public MustBeQuestionAuthorHandler(IDataRepository dr, IHttpContextAccessor hca)
        {
            _dataRepository = dr;
            _httpContextAccessor = hca;
        }

        protected async override Task HandleRequirementAsync(AuthorizationHandlerContext context, MustBeQuestionAuthorRequirements req){
            if(!context.User.Identity.IsAuthenticated){
                context.Fail();
                return;
            }
            var questionId =Convert.ToInt32(_httpContextAccessor.HttpContext.Request.RouteValues["questionId"]);
            var userId = context.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var question = _dataRepository.GetQuestion(questionId);
            if (question == null)
            {
                context.Succeed(req);
                return;
            }
            if (question.UserId != userId)
            {
                context.Fail();
                return;
            }
                context.Succeed(req);
        }
    }
}