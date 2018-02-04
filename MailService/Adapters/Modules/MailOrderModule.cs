using System;
using Mail.Host.Adapters.RequestModels;
using Mail.Host.Domain.Commands;
using Mail.Host.Domain.Exceptions;
using Nancy;
using Nancy.ModelBinding;
using Paramore.Brighter;

namespace Mail.Host.Adapters.Modules
{
    public class MailOrderModule : NancyModule
    {
        public MailOrderModule(IAmACommandProcessor messageProcessor)
        {
            Post["/mail/send"] = _ =>
            {
                var request = this.Bind<SendMailRequestModel>();

                try
                {
                    messageProcessor.Send(
                        new CreateSendMailOrderCommand(
                            request.Sender,
                            request.Destination,
                            request.Body,
                            request.Type,
                            request.ScheduleAt
                        ));
                }
                catch (MailOrderValidationException e)
                {
                    Console.WriteLine(e);
                    return HttpStatusCode.BadRequest;
                }
                
                return HttpStatusCode.OK;
            };
        }
    }
}
