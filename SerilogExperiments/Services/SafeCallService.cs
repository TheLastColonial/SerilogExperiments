namespace SerilogExperiments.Services
{
    using System;
    using System.Threading.Tasks;
    using Serilog;

    /// <summary>
    /// Wrapping to make an IO call safely and with log relevent information
    /// </summary>
    public interface ISafeCallService
    {
        Task Call(Func<Task> call, Guid correlationId, Type callingContext);
        Task<T> Call<T>(Func<Task<T>> call, Guid correlationId, Type callingContext);
    }

    /// <inheritdoc/>
    public class SafeCallService : ISafeCallService
    {
        private readonly ILogger log;

        public SafeCallService(ILogger logger)
        {
            this.log = logger;
        }

        public async Task Call(Func<Task> callAction, Guid correlationId, Type callingContext)
        {
            this.log.ForContext(callingContext);

            try
            {
                using(this.log.BeginTimedOperation(callingContext.Name, identifier: correlationId.ToString()))
                {
                    await callAction.Invoke();
                }
            }
            catch (Exception ex)
            {
                this.log.Error(ex, $"Failed {callingContext.Name}");
            }
        }

        public async Task<T> Call<T>(Func<Task<T>> callAction, Guid correlationId, Type callingContext)
        {
            this.log.ForContext(callingContext);

            try
            {
                using (this.log.BeginTimedOperation(callingContext.Name, identifier: correlationId.ToString()))
                {
                   return await callAction.Invoke();
                }
            }
            catch (Exception ex)
            {
                this.log.Error(ex, $"Failed {callingContext.Name}");
                return default(T);
            }
        }
    }
}
