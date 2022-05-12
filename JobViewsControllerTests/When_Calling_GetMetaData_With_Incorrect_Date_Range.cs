using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using PandoLogic.Data;
using System;
using System.Threading;

namespace PandoLogicTests.JobViewsControllerTests
{
    internal class When_Calling_GetMetaData_With_Incorrect_Date_Range : JobViewsControllerSpecificationBase
    {
        [Test]
        public void It_Should_Return_Bad_Request_Result()
        {
            using (var context = new JobViewsContext(dbContextOptions))
            {
                var startDate = DateTime.Now;
                var endDate = DateTime.Now.AddDays(-15);

                var metaDataResult = controller.GetAsync(startDate, endDate, new CancellationToken());
                var badRequestResult = metaDataResult as BadRequestObjectResult;

                Assert.IsNotNull(badRequestResult);
            }
        }
    }
}
