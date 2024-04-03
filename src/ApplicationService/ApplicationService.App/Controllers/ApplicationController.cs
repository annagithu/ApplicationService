using Microsoft.AspNetCore.Mvc;
using ApplicationService.InternalContracts.Application.Models;
using ApplicationService.InternalContracts.Application;
using Microsoft.EntityFrameworkCore;

namespace ApplicationService.Controllers
{

    [ApiController]
    [Route("[controller]")]
    
    public class ApplicationController : Controller
    {
        private readonly IApplicationService _applicationService;

        public ApplicationController(IApplicationService applicatonService)
        {
            _applicationService = applicatonService;
        }


        [HttpPost("Create")]
        public async Task<ApplicationModel> Post( [FromBody] ApplicationModel application)
        {
           var createdApplication  = await _applicationService.CreateApplication(application);

            return createdApplication;
        }


        [HttpPost("Delete")]
        public async Task<ApplicationModel> Delete([FromBody] ApplicationModel application)
        {
            var deletedApplication = await _applicationService.DeleteApplication(application);

            return deletedApplication;
        }


        [HttpPost("Edit")]
        public async Task<ApplicationModel> Edit([FromBody] ApplicationModel application)
        {
            var editedApplication = await _applicationService.EditApplication(application);

            return editedApplication;
        }

        [HttpPost("Submit")]
        public async Task<ApplicationModel> Submit([FromBody] ApplicationModel application)
        {
            var editedApplication = await _applicationService.SubmitApplication(application);

            return editedApplication;
        }

        [HttpPost("UnsubmittedOlder")]
        public async Task<List<ApplicationModel>> UnsubmittedOlder([FromBody] ApplicationModel application)
        {
            
            var ListUnsubmittedOlder = await _applicationService.UnsubmittedOlder(application);

            return ListUnsubmittedOlder;
        }

        [HttpPost("SubmittedAfter")]
        public async Task<List<ApplicationModel>> SubmittedAfter([FromBody] ApplicationModel application)
        {
          
           var SubmittedAfter = await _applicationService.SubmittedAfter(application);
            return SubmittedAfter;
        }

        [HttpPost("CurrentApplication")]
        public async Task<ApplicationModel> CurrentApplication([FromBody] ApplicationModel application)
        {
            var CurrentApplication = await _applicationService.CurrentApplication(application);
            return CurrentApplication;
        }

        [HttpPost("FindApplication")]
        public async Task<ApplicationModel> FindApplication([FromBody] ApplicationModel application)
        {
            var findApplication = await _applicationService.FindApplication(application);
            return findApplication;
        }

        [HttpPost("ListActivities")]
        public async Task<List<ActivityModel>> ListActivities()
        { 
            var listActivities = await _applicationService.ListActivities();
            return listActivities;
        }

    }


}

