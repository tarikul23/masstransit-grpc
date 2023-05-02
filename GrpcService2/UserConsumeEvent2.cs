using GprcShareEvent;
using MassTransit;

namespace GrpcService2
{

    public class UserConsumeEvent2 :
        IConsumer<UserEvent2>
    {
        public async Task Consume(ConsumeContext<UserEvent2> context)
        {
            Console.WriteLine("First Name: " + context.Message.firstname);
            Console.WriteLine("Last Name: " + context.Message.lastname);
            //await context.Publish<PersonEvent>(new PersonEvent("saddam"));
            await context.Publish<EmployeeEvent>(new EmployeeEvent("Arif"));
            //return context.RespondAsync(new UserResponse($"{context.Message.firstname} {context.Message.lastname}"));
        }
    }
}
