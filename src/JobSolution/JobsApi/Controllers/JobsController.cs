using Microsoft.AspNetCore.Mvc;

namespace JobsApi.Controllers;

[Route("jobs")]
[ApiController]
public class JobsController : ControllerBase
{

    private readonly JobManager _jobManager;

    public JobsController(JobManager jobManager)
    {
        _jobManager = jobManager;
    }

    [HttpPost]
    public async Task<ActionResult> CreateJob([FromBody] JobCreateItem request)
    {
        JobItemModel response = await _jobManager.CreateJobAsync(request);
        return StatusCode(201, response);
    }

    [HttpGet]
    public async Task<ActionResult> GetAllJobs()
    {
        CollectionResponse<JobItemModel> response = await _jobManager.GetAllCurrentJobsAsync();
        return Ok(response);
    }
}
