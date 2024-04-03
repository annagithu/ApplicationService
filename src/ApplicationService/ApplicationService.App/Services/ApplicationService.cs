using ApplicationService.App.InternalContracts.Application;
using ApplicationService.InternalContracts.Application;
using ApplicationService.InternalContracts.Application.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Data;

namespace ApplicationService.App.Services
{

    public class ApplicationService : IApplicationService
    {

        

        public async Task<ApplicationModel> CreateApplication(ApplicationModel source)
        {
            if (!await HasDraft(source))
            {
                return ErrorModels.AlreadyHaveaDraft;
            }

            if(await IsExist(source))
            {
                return ErrorModels.IsExistAlready;
            }

            using var context = new Context();
            if (ActivityNames.AvailableNames.Contains(source.Activity))
            {
                source.Date = DateTime.Now;
                source.IsSubmitted = false;
                await context.AddAsync(source);
                await context.SaveChangesAsync();
            }
            else
            {
                return ErrorModels.WrongActivity(source);
            }
            return source;
        }

        public async Task<ApplicationModel> DeleteApplication(ApplicationModel source)
        {
            if (await IsExist(source) != true)
            {
                return ErrorModels.DoesntExist;
            }

            if (await IsSubmit(source))
            {
                return ErrorModels.AlreadySubmitted;
            }

            using var context = new Context();
            var deletedApplication = new ApplicationModel { Id = source.Id };
            context.Remove(deletedApplication);
            context.SaveChanges();
            return source;
        }

        public async Task<ApplicationModel> EditApplication(ApplicationModel source)
        {
            if (await IsExist(source) != true)
            {
                return ErrorModels.DoesntExist;
            }
            if (await IsSubmit(source))
            {
                return ErrorModels.AlreadySubmitted;
            }
            using var context = new Context();
            var editedApplication = new ApplicationModel { Id = source.Id };
            if (!ActivityNames.AvailableNames.Contains(source.Activity))
            {
                return ErrorModels.WrongActivity(source);
            }
            editedApplication = source;
            editedApplication.IsSubmitted = false;
            context.Update(editedApplication);
            context.SaveChanges();
            return source;
        }

        public async Task<ApplicationModel> SubmitApplication(ApplicationModel source)
        {
            if (await IsExist(source) != true)
            {
                return ErrorModels.DoesntExist;
            }

            var submittedApplication = new ApplicationModel { Id = source.Id };
            using var context = new Context();
            if (submittedApplication != null)
            {
                if (!ActivityNames.AvailableNames.Contains(source.Activity))
                {
                    return ErrorModels.WrongActivity(source);
                }
                submittedApplication = source;
                submittedApplication.IsSubmitted = true;
                context.Update(submittedApplication);
                context.SaveChanges();
            }
            return submittedApplication;
        }

        public async Task<List<ApplicationModel>> SubmittedAfter(ApplicationModel source)
        {
            using var context = new Context();
            return await context.Applications.Where(unsubmittedOlder => unsubmittedOlder.IsSubmitted == true && unsubmittedOlder.Date > source.Date).ToListAsync();
        }

        public async Task<List<ApplicationModel>> UnsubmittedOlder(ApplicationModel source)
        {
            using var context = new Context();
            return await context.Applications.Where(unsubmittedOlder => unsubmittedOlder.IsSubmitted == false && unsubmittedOlder.Date > source.Date).ToListAsync();
        }

        public async Task<ApplicationModel> CurrentApplication(ApplicationModel source)
        {
            using var context = new Context();
            return await context.Applications.Where(application => application.IsSubmitted == false && application.Author == source.Author).FirstOrDefaultAsync();
        }

        public async Task<ApplicationModel> FindApplication(ApplicationModel source)
        {
            using var context = new Context();
            return await context.Applications.Where(application => application.Id == source.Id).FirstOrDefaultAsync();
        }

        public async Task<List<ActivityModel>> ListActivities()
        {
            using var context = new Context();
            return await context.type_of_activities.ToListAsync();
        }

        private async Task<bool> HasDraft(ApplicationModel source)
        {
            using var context = new Context();
            return (await context.Applications.Where(application => application.Author == source.Author && application.IsSubmitted == false).FirstOrDefaultAsync() != null);
        }

        private async Task<bool> IsSubmit(ApplicationModel source)
        { 
            using var context = new Context();
            return (await context.Applications.Where(application => application.Id == source.Id && application.IsSubmitted == true).FirstOrDefaultAsync() != null);
        }

        private async Task<bool> IsExist(ApplicationModel source)
        {
            using var context = new Context();
            return (await context.Applications.Where(application => application.Id == source.Id).FirstOrDefaultAsync() != null);
        }
    }
}
