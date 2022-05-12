using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using PandoLogic.Data;
using PandoLogic.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace PandoLogicTests.JobViewsControllerTests
{
    internal class When_Calling_GetMetaData_With_Correct_Date_Range : JobViewsControllerSpecificationBase
    {
        [Test]
        public void It_Should_Return_Ok_Result()
        {
            using var context = new JobViewsContext(dbContextOptions);

            var startDate = DateTime.Now.AddDays(-15);
            var endDate = DateTime.Now;

            var metaDataResult = controller.GetAsync(startDate, endDate, new CancellationToken());
            var okResult = metaDataResult as OkObjectResult;

            Assert.IsNotNull(okResult);
        }

        [Test]
        public void It_Should_Return_The_Correct_Number_Of_Objects()
        {
            using var context = new JobViewsContext(dbContextOptions);

            var startDate = DateTime.Now.AddDays(-15);
            var endDate = DateTime.Now;

            var metaDataResult = controller.GetAsync(startDate, endDate, new CancellationToken());
            var okResult = metaDataResult as OkObjectResult;

            Assert.IsNotNull(okResult);

            var metaDataList = okResult?.Value as List<JobViewsMetaDataResource>;

            metaDataList?.Should().HaveCount(16);
        }

        [Test]
        public void It_Should_Return_The_Correct_TotalJobs()
        {
            using var context = new JobViewsContext(dbContextOptions);

            var startDate = DateTime.Now.AddDays(-15);
            var endDate = DateTime.Now;

            var metaDataResult = controller.GetAsync(startDate, endDate, new CancellationToken());
            var okResult = metaDataResult as OkObjectResult;

            Assert.IsNotNull(okResult);

            var metaDataList = okResult?.Value as List<JobViewsMetaDataResource>;

            metaDataList?.Sum(x => x.TotalJobs).Should().Be(66);
            metaDataList?.First().TotalJobs.Should().Be(2);    
        }

        [Test]
        public void It_Should_Return_The_Correct_TotalViews()
        {
            using var context = new JobViewsContext(dbContextOptions);

            var startDate = DateTime.Now.AddDays(-15);
            var endDate = DateTime.Now;

            var metaDataResult = controller.GetAsync(startDate, endDate, new CancellationToken());
            var okResult = metaDataResult as OkObjectResult;

            Assert.IsNotNull(okResult);

            var metaDataList = okResult?.Value as List<JobViewsMetaDataResource>;

            metaDataList?.Sum(x => x.TotalViews).Should().Be(8);
            metaDataList?.First().TotalViews.Should().Be(3);
        }
    }
}
