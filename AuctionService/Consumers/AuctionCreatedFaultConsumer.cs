using Contracts;
using MassTransit;

namespace AuctionService.Consumers
{
    public class AuctionCreatedFaultConsumer : IConsumer<Fault<AuctionCreated>>
    {
        public async Task Consume(ConsumeContext<Fault<AuctionCreated>> context)
        {
            Console.WriteLine("--> Consuming faulty creation");

            var exception = context.Message.Exceptions.First();

            // Search Consumer 'da ArgumentException hatası fırlatıldı ve msg işlenmediyse mesaja direk böyle müdehale edilebilir 
            // Ancak yapılması gereken log'lamak ve manuel olarak incelenmesi olucaktır.
            if (exception.ExceptionType == "System.ArgumentException")
            {
                context.Message.Message.Model = "FooBar";
                await context.Publish(context.Message.Message);
            }
            else
            {
                Console.WriteLine($"{exception.Message}");
            }
        }
    }
}
