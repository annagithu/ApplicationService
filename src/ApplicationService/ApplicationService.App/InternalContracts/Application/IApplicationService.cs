﻿using ApplicationService.InternalContracts.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.InternalContracts.Application
{
    public interface IApplicationService
    {
        Task<ApplicationModel> CreateApplication(ApplicationModel source);

        Task<ApplicationModel> DeleteApplication(ApplicationModel source);

        Task<ApplicationModel> EditApplication(ApplicationModel source);

        Task<ApplicationModel> SubmitApplication(ApplicationModel source);

        Task<List<ApplicationModel>> UnsubmittedOlder(ApplicationModel source);

        Task<List<ApplicationModel>> SubmittedAfter(ApplicationModel source);
        
        Task<List<ActivityModel>> ListActivities();

        Task<ApplicationModel> CurrentApplication(ApplicationModel source);

        Task<ApplicationModel> FindApplication(ApplicationModel source);
    }
}
