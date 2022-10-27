namespace SubscriberNoDapr.Events;

public record struct MessageReceived(string sender, string message);