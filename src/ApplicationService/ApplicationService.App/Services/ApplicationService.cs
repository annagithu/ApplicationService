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
            using (var context = new Context())
            {
                source.Date = DateTime.Now;
                source.Status = false;
                await context.AddAsync(source);
                await context.SaveChangesAsync();
            }
            return source;
        }

        public async Task<ApplicationModel> DeleteApplication(ApplicationModel source)
        {
            using (var context = new Context())
            {            
                try
                {
                    var deletedApplication = new ApplicationModel { Id = source.Id };
                    context.Remove(deletedApplication);
                    context.SaveChanges();
                }
             catch 
                {
                   
                    
                }
            }
            return source;
        }

        public async Task<ApplicationModel> EditApplication(ApplicationModel source)
        {
            using (var context = new Context())
            {
                try
                {
                    var editedApplication = new ApplicationModel { Id = source.Id };
                    if(editedApplication != null)
                    {
                        editedApplication = source;
                        editedApplication.Status = false;
                        context.Update(editedApplication);
                        context.SaveChanges();
                    }
                }
                catch
                {

                }
            }
            return source;
        }

        public async Task<ApplicationModel> SubmitApplication(ApplicationModel source)
        {
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

        
    }
}
