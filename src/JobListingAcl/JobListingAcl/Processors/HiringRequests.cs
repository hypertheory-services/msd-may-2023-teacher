using DotNetCore.CAP;
using System.Runtime.CompilerServices;
using HiringAclEvents = MessageContracts.HrAcl;
using JobListingAclEvents = MessageContracts.JobListingsApi;
namespace JobListingAcl.Processors;

public class HiringRequests : ICapSubscribe
{
    private readonly ICapPublisher _capPublisher;

    public HiringRequests(ICapPublisher capPublisher)
    {
        _capPublisher = capPublisher;
    }

    [CapSubscribe("Hr.EmployeeHired")]
    public async Task ClosingJobOpeningOnPositionFilledAsync(HiringAclEvents.EmployeeHired message, [FromCap]CapHeader header)
    {
        var offeringId = header["offering-id"];
        if (offeringId == null)
        {
            throw new ArgumentOutOfRangeException(nameof(offeringId));
        }
        var filledJobListing = new JobListingAclEvents.JobListingFilled
        {
            JobListingId = offeringId,
        };

        await _capPublisher.PublishAsync(JobListingAclEvents.JobListingFilled.MessageId, filledJobListing);

    }
}
