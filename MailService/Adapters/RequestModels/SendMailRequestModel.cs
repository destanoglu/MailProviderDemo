using System;

namespace Mail.Host.Adapters.RequestModels
{
    public class SendMailRequestModel
    {
        public string Sender { get; set; }
        public string Destination { get; set; }
        public string Body { get; set; }
        public string Type { get; set; }
        public DateTime ScheduleAt { get; set; }
    }
}
