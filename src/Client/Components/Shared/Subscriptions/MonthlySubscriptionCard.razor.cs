using Microsoft.AspNetCore.Components;

namespace Squads.Client.Components.Shared.Subscriptions;

public partial class MonthlySubscriptionCard
{
    [Parameter]
    public DateTime? ExpiryDate { get; set; }
}