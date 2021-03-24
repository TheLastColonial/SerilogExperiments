namespace SerilogExperiments.Services
{
    using System;
    using System.Diagnostics;
    using System.Threading.Tasks;
    using Serilog;

    /// <summary>
    /// Wrapping to make an IO call safely and with log relevent information
    /// </summary>
    public interface ISafeCallService
    {
        Task Call(Func<Task> call, Type callingContext);
        Task<T> Call<T>(Func<Task<T>> call, Type callingContext);
    }

    /// <inheritdoc/>
    public class SafeCallService : ISafeCallService
    {
        private readonly ILogger log;
        private readonly Stopwatch stopwatch;

        public SafeCallService(ILogger logger)
        {
            this.log = logger;
            this.stopwatch = new Stopwatch();
        }

        public async Task Call(Func<Task> callAction, Type callingContext)
        {
            this.log.ForContext(callingContext);

            try
            {
                this.log.Debug("Start IO call");
                this.stopwatch.Start();
                await callAction.Invoke();
                this.stopwatch.Stop();
                this.log.Information("Completed IO call");
            }
            catch (Exception ex)
            {
                this.stopwatch.Stop();
                this.log.Error(ex, "Failed IO call");
            }
        }

        public async Task<T> Call<T>(Func<Task<T>> callAction, Type callingContext)
        {
            this.log.ForContext(callingContext);

            try
            {
                this.log.Debug("Start IO call");
                this.stopwatch.Start();
                var result = await callAction.Invoke();
                this.stopwatch.Stop();
                this.log.Information("Completed IO call");

                return result;
            }
            catch (Exception ex)
            {
                this.stopwatch.Stop();
                this.log.Error(ex, "Failed IO call");
                return default(T);
            }
        }
    }
}
