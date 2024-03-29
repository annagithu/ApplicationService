using ApplicationService.InternalContracts.Application;
using ApplicationService.InternalContracts.Application.Models;

namespace ApplicationService.App.Services
{
    public class ApplicationService : IApplicationService
    {
        public async Task<ApplicationModel> CreateApplication(ApplicationModel source) 
        {
            using (var context = new Context())
            {
                source.Id = Guid.NewGuid();
                await context.AddAsync(source);
                await context.SaveChangesAsync();
            }
            return source;
        }
    }
}
