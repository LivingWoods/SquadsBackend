using Squads.Domain.Common;

namespace Squads.Domain.Subscriptions;

public class Subscription : Entity
{
    private List<SubscriptionLine> _subscriptionLines = new();

    public bool IsCanceled { get; private set; } = false;

    /// <summary>
    /// Returns the latest subscription line
    /// </summary>
    public SubscriptionLine GetLatestSubscriptionLine => _subscriptionLines.Last();

    /// <summary>
    /// Cancels the subscription
    /// </summary>
    public void CancelSubscription()
    {
        if (!IsCanceled)
        {
            IsCanceled = true;
        }
    }

    /// <summary>
    /// Reactivates the subscription
    /// </summary>
    public void ReactivateSubscription()
    {
        if (IsCanceled)
        {
            IsCanceled = false;
        }
    }

    /// <summary>
    /// Adds a new subscription line
    /// </summary>
    /// <param name="validFrom"></param>
    /// <param name="validTill"></param>
    /// <param name="payment"></param>
    public void RenewSubscription(DateTime validFrom, Payment? payment)
    {
        _subscriptionLines.Add(new SubscriptionLine(validFrom, payment));
    }
}
