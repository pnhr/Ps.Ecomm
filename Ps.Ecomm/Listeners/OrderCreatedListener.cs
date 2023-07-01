namespace Ps.Ecomm.Listeners
{
    public class OrderCreatedListener : IHostedService
    {
        public Task StartAsync(CancellationToken cancellationToken)
        {


            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
