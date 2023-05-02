using GprcShareEvent;
using MassTransit;

namespace GrpcService
{

    public class UserRequestResponseEvent :
        IConsumer<UserRequest>
    {
        public Task Consume(ConsumeContext<UserRequest> context)
        {
            Console.WriteLine("Request First Name: " + context.Message.firstname);
            Console.WriteLine("Request Last Name: " + context.Message.lastname);
            return context.RespondAsync(new UserResponse($"{context.Message.firstname} {context.Message.lastname}"));
        }
    }
}
