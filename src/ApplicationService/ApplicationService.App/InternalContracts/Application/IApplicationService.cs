using ApplicationService.InternalContracts.Application.Models;
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

    }
}
