using GprcShareEvent;
using MassTransit;

namespace GprcWebApi
{

    public class EmployeeConsumeEvent :
        IConsumer<EmployeeEvent>
    {
        public async Task Consume(ConsumeContext<EmployeeEvent> context)
        {
            Console.WriteLine("Employee Name : " + context.Message.name);
        }
    }
}
