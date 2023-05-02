using GprcShareEvent;
using MassTransit;

namespace GrpcService
{

    public class PersonConsumeEvent :
        IConsumer<PersonEvent>
    {
        public async Task Consume(ConsumeContext<PersonEvent> context)
        {
            Console.WriteLine("Message: " + context.Message.Name);
            await context.Publish<UserRequest>(new UserRequest("Tarikul", "Islam"));
            //var df = await context.RespondAsync<UserResponse>(new UserRequest("Tarikul", "Islam"));
            //return context.RespondAsync(new ClaimSubmitted { ClaimId = context.Message.ClaimId });
        }
    }
}
