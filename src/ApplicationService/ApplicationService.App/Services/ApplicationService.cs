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

        public ApplicationModel errorModel = new ApplicationModel { Name = "Invalid Value. Please, try again" };

        public async Task<ApplicationModel> CreateApplication(ApplicationModel source)
        {
            if (await HasDraft(source) != true)
            {
                errorModel.Description = "You already have a draft";
                return errorModel;
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
                errorModel.Description = $"You cannot choose {source.Activity} type of activity. Available types: {string.Join(" ", ActivityNames.AvailableNames)}";
                return errorModel;
            }
            return source;
        }

        public async Task<ApplicationModel> DeleteApplication(ApplicationModel source)
        {
            if (await IsExist(source) != true)
            {
                errorModel.Description = "Application with this ID doesn't exist";
                return errorModel;
            }

            if (await IsSubmit(source) == true)
            {
                errorModel.Description = "This application is submit already.You cannot delete it.";
                return errorModel;
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
                errorModel.Description = "Application with this ID  doesn't exist";
                return errorModel;
            }

            if (await IsSubmit(source) == true)
            {
                errorModel.Description = "This application is submit already.You cannot edit it.";
                return errorModel;
            }
                using var context = new Context();
                var editedApplication = new ApplicationModel { Id = source.Id };
                if (editedApplication != null)
                {
                    if (ActivityNames.AvailableNames.Contains(source.Activity))
                    {
                        editedApplication = source;
                        editedApplication.IsSubmitted = false;
                        context.Update(editedApplication);
                        context.SaveChanges();
                    }
                    else
                    {
                        errorModel.Description = $"You cannot choose {source.Activity} type of activity. Available types: {string.Join(" ", ActivityNames.AvailableNames)}";
                        return errorModel;
                    }
                } 
            return source;
        }

        public async Task<ApplicationModel> SubmitApplication(ApplicationModel source)
        {
            if (await IsExist(source) != true)
            {
                errorModel.Description = "Application with this ID not exist";
                return errorModel;
            }

            var submittedApplication = new ApplicationModel { Id = source.Id };
            using var context = new Context();
            if (submittedApplication != null)
            {
                submittedApplication = source;
                submittedApplication.IsSubmitted = true;
                context.Update(submittedApplication);
                context.SaveChanges();
            }
            return submittedApplication;
        }

        public async Task<List<ApplicationModel>> SubmittedAfter(ApplicationModel source)
        {
            DateTime dateTime = source.Date;
            using var context = new Context();
            var subAfterList = await context.Applications.Where(unsubmittedOlder => unsubmittedOlder.IsSubmitted == true && unsubmittedOlder.Date > dateTime).ToListAsync();
            return subAfterList;
        }

        public async Task<List<ApplicationModel>> UnsubmittedOlder(ApplicationModel source)
        {
            DateTime dateTime = source.Date;
            using var context = new Context();
            var unsubAfterList = await context.Applications.Where(unsubmittedOlder => unsubmittedOlder.IsSubmitted == false && unsubmittedOlder.Date > dateTime).ToListAsync();
            return unsubAfterList;
        }

        public async Task<ApplicationModel> CurrentApplication(ApplicationModel source)
        {
            using var context = new Context();
            var currentApplication = await context.Applications.Where(application => application.IsSubmitted == false && application.Author == source.Author).FirstOrDefaultAsync();
            return currentApplication;
        }

        public async Task<ApplicationModel> FindApplication(ApplicationModel source)
        {
            using var context = new Context();
            var foundApplication = await context.Applications.Where(application => application.Id == source.Id).FirstOrDefaultAsync();
            return foundApplication;
        }

        public async Task<List<ActivityModel>> ListActivities()
        {
            using var context = new Context();
            var listActivities = await context.type_of_activities.ToListAsync();
            return listActivities;
        }

        private async Task<bool> HasDraft(ApplicationModel source)
        {
            bool hasDraft = false;
            using var context = new Context();
            var foundDraft = await context.Applications.Where(application => application.Author == source.Author && application.IsSubmitted == false).FirstOrDefaultAsync();
            if (foundDraft != null)
            {
                hasDraft = true;
            }
            return hasDraft;
        }

        private async Task<bool> IsSubmit(ApplicationModel source)
        {
            bool isSubmit = false;
            using var context = new Context();
            var foundApplication = await context.Applications.Where(application => application.Id == source.Id && application.IsSubmitted == true).FirstOrDefaultAsync();
            if (foundApplication != null)
            {
                isSubmit = true;
            }
            return isSubmit;
        }

        private async Task<bool> IsExist(ApplicationModel source)
        {
            bool isExist = false;
            using var context = new Context();
            var foundApplication = await context.Applications.Where(application => application.Id == source.Id).FirstOrDefaultAsync();
            if (foundApplication != null)
            {
                isExist = true;
            }
            return isExist;
        }
    }
}
