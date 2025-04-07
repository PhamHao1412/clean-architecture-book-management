// using Hangfire;
// using Microsoft.AspNetCore.Http;
// using Microsoft.AspNetCore.Mvc;
// using My_Movie.Service;
//
// namespace My_Movie.Controller
// {
//     [Route("api/[controller]")]
//     [ApiController]
//     public class TestHangFire : ControllerBase
//     {
//         private readonly IJobTestService _jobTestService;
//         private readonly IBackgroundJobClient _backgroundJobClient;
//         private readonly IRecurringJobManager _recurringJobManager;
//         public TestHangFire(IJobTestService jobTestService, IBackgroundJobClient backgroundJobClient, IRecurringJobManager recurringJobManager)
//         {
//             _jobTestService = jobTestService;
//             _backgroundJobClient = backgroundJobClient;
//             _recurringJobManager = recurringJobManager;
//         }
//
//         [HttpGet("/FireAndForgetJob")] //The Enqueue method is responsible for creating and saving the job definition to the database.
//         public ActionResult CreateFireAndForgetJob()
//         {
//             _backgroundJobClient.Enqueue(() => _jobTestService.FireAndForgetJob()); 
//             return Ok();
//         }
//
//         [HttpGet("/DelayedJob")] //Delayed Jobs are jobs that we definitely want to do, but not right now. We can schedule them to do at a specific time, which could be in a minute or three months.
//         public ActionResult CreateDelayedJob()
//         {
//             _backgroundJobClient.Schedule(() => _jobTestService.DelayedJob(), TimeSpan.FromSeconds(60)); 
//             return Ok();
//         }
//
//         [HttpGet("/ReccuringJob")] //it will repeat after a specific interval. Hangfire will use CRON.
//         public ActionResult CreateReccuringJob()
//         {
//             _recurringJobManager.AddOrUpdate("HaoId", () => _jobTestService.ReccuringJob(), Cron.Minutely);
//             return Ok();
//         }
//
//         [HttpGet("/ContinuationJob")] // The special thing about this job is that it connects the task execution.
//         public ActionResult CreateContinuationJob()
//         {
//             var parentJobId = _backgroundJobClient.Enqueue(() => _jobTestService.FireAndForgetJob());
//             _backgroundJobClient.ContinueJobWith(parentJobId, () => _jobTestService.ContinuationJob());
//
//             return Ok();
//         }
//     }
// }
