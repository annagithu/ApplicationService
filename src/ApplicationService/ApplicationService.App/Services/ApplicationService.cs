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
            using (var context = new Context())
            {
                if (await HasDraft(source) != true)
                {
                    if(source.Activity != "report" || source.Activity != "tutorial" || source.Activity != "discussion")
                    {
                        errorModel.Description = "You cannot choose this type of activity";
                        return errorModel;
                    }
                    source.Date = DateTime.Now;
                    source.Status = false;
                    await context.AddAsync(source);
                    await context.SaveChangesAsync();
                }
                else
                {
                    errorModel.Description = "You already have a draft";
                    return errorModel;
                }
            }
            return source;
        }

        public async Task<ApplicationModel> DeleteApplication(ApplicationModel source)
        {
            if (await IsExist(source) != true)
            {
                errorModel.Description = "Application with this ID not exist";
                return errorModel;
            }

            if (await IsSubmit(source) != true)
            {
                using (var context = new Context())
                {
                    var deletedApplication = new ApplicationModel { Id = source.Id };
                    context.Remove(deletedApplication);
                    context.SaveChanges();
                }
            }
            else
            {
                errorModel.Description = "This application is submit already.You cannot delete it.";
                return errorModel;
            }
            return source;
        }

        public async Task<ApplicationModel> EditApplication(ApplicationModel source)
        {
            if (await IsExist(source) != true)
            {
                errorModel.Description = "Application with this ID not exist";
                return errorModel;
            }

            if (await IsSubmit(source) != true)
            {
                using (var context = new Context())
                {
                    var editedApplication = new ApplicationModel { Id = source.Id };
                    if (editedApplication != null)
                    {
                        if (source.Activity != "report" || source.Activity != "tutorial" || source.Activity != "discussion")
                        {
                            errorModel.Description = "You cannot choose this type of activity";
                            return errorModel;
                        }
                        editedApplication = source;
                        editedApplication.Status = false;
                        context.Update(editedApplication);
                        context.SaveChanges();
                    }
                }
            }
            else
            {
                errorModel.Description = "This application is submit already.You cannot edit it.";
                return errorModel;
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
            using (var context = new Context())
            {
                try
                {
                    if (submittedApplication != null)
                    {

                        submittedApplication = source;
                        submittedApplication.Status = true;
                        context.Update(submittedApplication);
                        context.SaveChanges();
                    }
                }
                catch
                {

                }
            }
            return submittedApplication;
        }

        public async Task<List<ApplicationModel>> SubmittedAfter(ApplicationModel source)
        {
            DateTime dateTime = source.Date;
            var dataset = new List<ApplicationModel>();
            using (var context = new Context())
            {
                dataset = await context.Applications.Where(unsubmittedOlder => unsubmittedOlder.Status == true && unsubmittedOlder.Date > dateTime).ToListAsync();
            }

            return dataset;
        }

        public async Task<List<ApplicationModel>> UnsubmittedOlder(ApplicationModel source)
        {
            DateTime dateTime = source.Date;
            var dataset = new List<ApplicationModel>();
            using (var context = new Context())
            {
                dataset = await context.Applications.Where(unsubmittedOlder => unsubmittedOlder.Status == false && unsubmittedOlder.Date > dateTime).ToListAsync();
            }

            return dataset;
        }

        public async Task<ApplicationModel> CurrentApplication(ApplicationModel source)
        {
            var currentApplication = new ApplicationModel();
            using (var context = new Context())
            {
                currentApplication = await context.Applications.Where(application => application.Status == false && application.Author == source.Author).FirstOrDefaultAsync();

            }
            return currentApplication;
        }

        public async Task<ApplicationModel> FindApplication(ApplicationModel source)
        {
            var foundApplication = new ApplicationModel();
            using (var context = new Context())
            {
                foundApplication = await context.Applications.Where(application => application.Id == source.Id).FirstOrDefaultAsync();

            }
            return foundApplication;
        }

        public async Task<List<ActivityModel>> ListActivities()
        {
            var dataset = new List<ActivityModel>();
            using (var context = new Context())
            {
                dataset = await context.type_of_activities.ToListAsync();
            }
            return dataset;
        }

        public async Task<Boolean> HasDraft(ApplicationModel source)
        {
            bool hasDraft;
            var foundDraft = new ApplicationModel();
            using (var context = new Context())
            {
                try
                {
                    foundDraft = await context.Applications.Where(application => application.Author == source.Author && application.Status == false).FirstOrDefaultAsync();
                    hasDraft = true;
                }
                catch
                {
                    hasDraft = false;
                }
            }
            return hasDraft;
        }

        public async Task<Boolean> IsSubmit(ApplicationModel source)
        {
            bool isSubmit;
            var foundApplication = new ApplicationModel();
            using (var context = new Context())
            {
                try
                {
                    foundApplication = await context.Applications.Where(application => application.Id == source.Id && application.Status == true).FirstOrDefaultAsync();
                    isSubmit = true;
                }
                catch
                {
                    isSubmit = false;
                }
            }
            return isSubmit;
        }

        public async Task<Boolean> IsExist(ApplicationModel source)
        {
            bool isExist;
            var foundApplication = new ApplicationModel();
            using (var context = new Context())
            {
                try
                {
                    foundApplication = await context.Applications.Where(application => application.Id == source.Id).FirstOrDefaultAsync();
                    isExist = true;
                }
                catch
                {
                    isExist = false;
                }
            }
            return isExist;
        }


    }
}
