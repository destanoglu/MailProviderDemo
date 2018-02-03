namespace Mail.Shared.Contracts
{
    public interface IMailContent
    {
        string Sender { get; }
        string Destination { get; }
        string Body { get; }
        MessageType Type { get; }
    }
}
