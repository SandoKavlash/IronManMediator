using IronManMediator.Abstractions.Abstractions;
using IronManMediator.App.Requests;
using Microsoft.Extensions.Hosting;

namespace IronManMediator.App.HostedServices
{
    public class TestHostedService : BackgroundService
    {
        private readonly IIronManMediator _mediator;
        public TestHostedService(IIronManMediator mediator)
        {
            _mediator = mediator;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            string text = "";
            int iterationCounter = 0;
            while(!stoppingToken.IsCancellationRequested)
            {
                text += iterationCounter;
                iterationCounter++;
                Console.WriteLine((await _mediator.SendAsync(new TestRequest() { Message = text})).LastCharacter);
                Thread.Sleep(100);
            }
        }
    }
}
