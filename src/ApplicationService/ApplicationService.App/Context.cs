﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationService.InternalContracts.Application.Models;
using Npgsql;

namespace ApplicationService.App
{
    class Context : DbContext
    {
        public DbSet<ApplicationModel> Applications { get; set; }

        
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
