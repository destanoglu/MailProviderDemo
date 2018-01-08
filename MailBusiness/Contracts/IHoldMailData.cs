namespace Mail.Shared.Contracts
{
    public interface IHoldMailData
    {
        string Sender { get; }
        string Destination { get; }
        string Body { get; }
        MessageType Type { get; }
    }
}
