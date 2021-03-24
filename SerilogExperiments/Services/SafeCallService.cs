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
        private const string startMessage = "Start IO call";
        private const string completeMessage = "Completed IO call";
        private const string failedMessage = "Failed IO call";

        private readonly ILogger log;
        private readonly Stopwatch stopwatch;

        public SafeCallService(ILogger logger, Stopwatch stopwatch)
        {
            this.log = logger;
            this.stopwatch = stopwatch;
        }

        public async Task Call(Func<Task> callAction, Type callingContext)
        {
            this.log.ForContext(callingContext);

            try
            {
                this.log.Debug(startMessage);
                this.stopwatch.Start();
                await callAction.Invoke();
                this.stopwatch.Stop();
                this.log.Information(completeMessage);
            }
            catch (Exception ex)
            {
                this.stopwatch.Stop();
                this.log.Error(ex, failedMessage);
            }
        }

        public async Task<T> Call<T>(Func<Task<T>> callAction, Type callingContext)
        {
            this.log.ForContext(callingContext);

            try
            {
                this.log.Debug(startMessage);
                this.stopwatch.Start();
                var result = await callAction.Invoke();
                this.stopwatch.Stop();
                this.log.Information(completeMessage);

                return result;
            }
            catch (Exception ex)
            {
                this.stopwatch.Stop();
                this.log.Error(ex, failedMessage);
                return default(T);
            }
        }
    }
}
