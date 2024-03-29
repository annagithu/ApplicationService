using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApplicationService.InternalContracts.Application.Models;
using ApplicationService.InternalContracts.Application;

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


        // GET: ApplicationController
        [HttpPost("/")]
        public async Task<ApplicationModel> Post( [FromBody] ApplicationModel application)
        {
           var createdApplication  = await _applicationService.CreateApplication(application);

            return createdApplication;
        }

        //

     





    }
}
