using GprcShareEvent;
using MassTransit;

namespace GrpcService
{

    public class UserConsumeEvent :
        IConsumer<UserEvent>
    {
        public async Task Consume(ConsumeContext<UserEvent> context)
        {
            Console.WriteLine("First Name: " + context.Message.firstname);
            Console.WriteLine("Last Name: " + context.Message.lastname);
            await context.Publish<UserEvent2>(new UserEvent2("Tarikul Islam", "Saddam"));
            //return context.RespondAsync(new UserResponse($"{context.Message.firstname} {context.Message.lastname}"));
        }
    }
}
