﻿using Microsoft.EntityFrameworkCore;
using ApplicationService.InternalContracts.Application.Models;

namespace ApplicationService.App
{
    class Context : DbContext
    {
        public DbSet<ApplicationModel> Applications { get; set; }
        public DbSet<ActivityModel> type_of_activities { get; set; }

        public Context()
        {
            
        }

        [Obsolete]
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(@"host=host.docker.internal;port=5432;database=applications;username=postgres;password=1").LogTo(Console.WriteLine);
            ReloadTypesAsync();
        }

        public Task ReloadTypesAsync()
        {
            return Task.CompletedTask;
        }

    }
}
