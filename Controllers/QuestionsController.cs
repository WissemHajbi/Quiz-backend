using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using qAndA.Data;
using qAndA.Data.Models;

namespace qAndA.Controllers 
{
    // Path and the [controller] is substituted with the name of the controller minus the controller word
    [Route("[controller]")]
    // auto model validation
    [ApiController]
    public class QuestionsController: ControllerBase {
        private readonly IDataRepository _dataRepository;
        private readonly IQuestionCache _cache;

        public QuestionsController(IDataRepository DR, IQuestionCache QC)
        {
            _dataRepository = DR;
            _cache = QC;
        }

        [HttpGet]
        public async Task<IEnumerable<QuestionGetManyResponse>> GetQuestionsAsync(bool answers,[FromQuery]string search="") {
            if(string.IsNullOrEmpty(search)){
                if(answers) return _dataRepository.GetQuestionsWithAnswers();
                else return await _dataRepository.GetQuestionsAsync();
            }else{
                return _dataRepository.GetQuestionsBySearch(search);
            }
        }
        
        [HttpGet("unanswered")]
        public IEnumerable<QuestionGetManyResponse> GetUnansweredQuestions(){
            return _dataRepository.GetUnansweredQuestions();
        }

        [HttpGet("{questionId}")]
        public ActionResult<QuestionGetSingleResponse> GetQuestion(int questionId){
            var question = _cache.Get(questionId);

            if(question == null) {
                question = _dataRepository.GetQuestion(questionId);
                if(question == null) return NotFound();
                _cache.set(question);
            }

            return question;
        }

        [Authorize]
        [HttpPost]
        public ActionResult<QuestionGetSingleResponse> PostQuestion(QuestionPostRequest _question){
            var question = _dataRepository.PostQuestion(new QuestionPostFullRequest{
                Title = _question.Title,
                Content = _question.Content,
                UserId = "1",
                Username = "wissem",
                Created = DateTime.UtcNow,
            });
            return CreatedAtAction(nameof(GetQuestion),new{questionId = question.QuestionId},question);
        }

        [Authorize]
        [HttpPut("{questionId}")]
        public ActionResult<QuestionGetSingleResponse> PutQuestion(int questionId, QuestionPutRequest _question){
            var question = _dataRepository.GetQuestion(questionId);
            if(question == null){
                return NotFound();
            }
            _question.Title = string.IsNullOrEmpty(_question.Title) ? question.Title : _question.Title;
            _question.Content = string.IsNullOrEmpty(_question.Content) ? question.Content : _question.Content;
            _cache.remove(questionId);
            return _dataRepository.PutQuestion(questionId, _question);
        }

        [Authorize]
        [HttpDelete("{questionId}")]
        public ActionResult DeleteQuestion(int questionId){
            var exists = _dataRepository.QuestionExists(questionId);
            if(exists == false){
                return NotFound();
            }
            _cache.remove(questionId);
            _dataRepository.DeleteQuestion(questionId);
            return NoContent();
        }

        [Authorize]
        [HttpPost("{questionId}/answer")]
        public ActionResult<AnswerGetResponse> PostAnswer(int questionId, AnswerPostRequest _answer){
            var question = _dataRepository.QuestionExists(questionId);
            if(!question) {
                return NotFound();
            }
            _cache.remove(questionId);
            return _dataRepository.PostAnswer(
                new AnswerPostFullRequest{
                    QuestionId = questionId,
                    Content = _answer.Content,
                    UserId = "1",
                    Username = "wissem",
                    Created = DateTime.UtcNow,
                }
            );
        }
    }
}