using DotNetCore.CAP;
using Marten;
using MessageContracts.JobsApi;
using JobDomainEvents = MessageContracts.JobsApi;
using JobOfferingEvents = MessageContracts.JobListingsApi;
using WebEvents = MessageContracts.WebMessages;
namespace Acl;

public class DataSubscribers : ICapSubscribe
{

    private readonly IDocumentSession _documentSession;
    private readonly ILogger<DataSubscribers> _logger;

    public DataSubscribers(IDocumentSession documentSession, ILogger<DataSubscribers> logger)
    {
        _documentSession = documentSession;
        _logger = logger;
    }

    [CapSubscribe("JobsApi.JobCreated")]
    public async Task SaveJobAsync(JobDomainEvents.JobCreated jobCreated)
    {
        _logger.LogInformation($"Got a JobCreated {jobCreated.Title}");
        _documentSession.Store(jobCreated);
        await _documentSession.SaveChangesAsync();
    }

    [CapSubscribe("JobListings.JobListingCreated")]
    public async Task SaveJobOfferAsync(JobOfferingEvents.JobListingCreated offer)
    {
        _logger.LogInformation($"Got a JobListingCreated {offer.JobName}");
        _documentSession.Store(offer);
        await _documentSession.SaveChangesAsync();  
    }

    [CapSubscribe("WebMessages.ApplicantCreated")]
    public async Task SaveApplicantAsync(WebEvents.ApplicantCreated applicant)
    {
        _logger.LogInformation($"Got a ApplicantCreated {applicant.EmailAddress}");
        _documentSession.Store(applicant);
        await _documentSession.SaveChangesAsync();
    }


}
