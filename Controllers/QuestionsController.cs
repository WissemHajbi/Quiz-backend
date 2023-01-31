using Microsoft.AspNetCore.Mvc;
using qAndA.Data;

namespace qAndA.Controllers {
    // Path and the [controller] is substituted with the name of the controller minus the controller word
    [Route("api/[controller]")]
    // auto model validation
    [ApiController]
    public class QuestionsController: ControllerBase {
        private readonly IDataRepository _dataRepository;
        public QuestionsController(IDataRepository DR)
        {
            _dataRepository = DR;
        }
    }
}