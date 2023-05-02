using GprcShareEvent;
using MassTransit;

namespace GrpcService2
{

    public class EmployeeConsumeEvent2 :
        IConsumer<EmployeeEvent>
    {
        public async Task Consume(ConsumeContext<EmployeeEvent> context)
        {
            Console.WriteLine("Employee Name: " + context.Message.name);
            //await context.Publish<EmployeeEvent>(new EmployeeEvent("Arif"));
        }

    }
}
