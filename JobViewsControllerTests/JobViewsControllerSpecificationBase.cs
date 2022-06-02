using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using NUnit.Framework;
using PandoLogic.Controllers;
using PandoLogic.Data;
using PandoLogic.Models;
using PandoLogic.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PandoLogicTests
{
    public class JobViewsControllerSpecificationBase
    {
        protected DbContextOptions<JobViewsContext> dbContextOptions = new DbContextOptionsBuilder<JobViewsContext>()
                                                .UseInMemoryDatabase(databaseName: "PrimeDb")
                                                .Options;

        protected JobViewsController controller = null!;

        [OneTimeSetUp]
        public void Setup()
        {
            SeedDb();

            controller = new JobViewsController(new JobViewsMetaDataService(new JobViewsContext(dbContextOptions), new NullLogger<JobViewsMetaDataService>()));
        }

        private void SeedDb()
        {
            using (var context = new JobViewsContext(dbContextOptions))
            {
                var jobs = new List<Job>
                {
                    new Job { CreationTime = DateTime.Now.AddDays(-15), IsActive = true },
                    new Job { CreationTime = DateTime.Now.AddDays(-15), IsActive = true },
                    new Job { CreationTime = DateTime.Now.AddDays(-10), IsActive = true },
                    new Job { CreationTime = DateTime.Now.AddDays(-7), IsActive = true },
                    new Job { CreationTime = DateTime.Now.AddDays(-7), IsActive = true },
                    new Job { CreationTime = DateTime.Now.AddDays(-2), IsActive = true },
                    new Job { CreationTime = DateTime.Now.AddDays(-2), IsActive = false },
                    new Job { CreationTime = DateTime.Now.AddDays(-2), IsActive = true },
                    new Job { CreationTime = DateTime.Now, IsActive = true },
                };

                context.Jobs.AddRange(jobs);
                context.SaveChanges();

                var views = new List<View>
                {
                    new View { ViewDate = DateTime.Now.AddDays(-15), Job = context.Jobs.First(x => x.Id == 1) },
                    new View { ViewDate = DateTime.Now.AddDays(-15), Job = context.Jobs.First(x => x.Id == 1)},
                    new View { ViewDate = DateTime.Now.AddDays(-15), Job = context.Jobs.First(x => x.Id == 1) },
                    new View { ViewDate = DateTime.Now.AddDays(-10), Job = context.Jobs.First(x => x.Id == 3) },
                    new View { ViewDate = DateTime.Now.AddDays(-10), Job = context.Jobs.First(x => x.Id == 3) },
                    new View { ViewDate = DateTime.Now.AddDays(-7), Job = context.Jobs.First(x => x.Id == 5) },
                    new View { ViewDate = DateTime.Now.AddDays(-7), Job = context.Jobs.First(x => x.Id == 5) },
                    new View { ViewDate = DateTime.Now.AddDays(-2), Job = context.Jobs.First(x => x.Id == 8) },
                };

                context.Views.AddRange(views);
                context.SaveChanges();
            }
        }
    }
}