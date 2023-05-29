# Post Class Update

Hey Everyone!

You can watch a little video of this here: https://clipchamp.com/watch/WiR7pFoSKo4

So that little issue I was having with the header at the end of the class... well, remember I took that shortcut and used the `InMemory` store for the events on the HR.Acl project? It's broken. I switched it over to use Postgres and it worked like a champ.

This is a pretty good "Final enough for the end of class" version, if you want to copy this repo.

I added another ACL project that recieves the blah and produces a blah `Hr.EmployeeHired` event, reads the opening-id from the header, and produces a new `JobListings.JobListingFilled` message.


```csharp
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
```


This *should* be handled by our `JobListingsApi` to get rid of the listing, but even then you'd *also* have to handle it in th MVC application, which I did.

So now when you get hired for a job, it removes it from the website.