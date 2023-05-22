using JobListingsApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace JobListingsApi.Controllers;

[ApiController]
public class JobListingsRpcController : ControllerBase
{
    [HttpPost("job-listings-rpc/{job}/openings")]
    public async Task<ActionResult> AddJobListing([FromRoute] string job, [FromBody] JobListingCreateModel request)
    {
        // the "model" is good with our validation, however, the job might not exist.
        // we need to make a call to the other API to check to see if that job exists.
        return Ok();
    }
  
}
