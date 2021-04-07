namespace SerilogExperiments.Services
{
    using System;
    using System.Threading.Tasks;
    using Serilog;
    using SerilogExperiments.Commands;

    /// <summary>
    /// Wrapping to make an IO call safely and with log relevent information
    /// </summary>
    public interface ISafeCallService
    {
        Task CallAsync(SafeCallServiceCommand command);
        Task<T> CallAsync<T>(SafeCallServiceQuery<T> command);
    }

    /// <inheritdoc/>
    public class SafeCallService : ISafeCallService
    {
        private readonly ILogger log;

        public SafeCallService(ILogger logger)
        {
            this.log = logger;
        }

        public async Task CallAsync(SafeCallServiceCommand command)
        {
            this.log.ForContext(command.Metadata.Context);

            try
            {
                using (this.log.BeginTimedOperation(command.Metadata.Name, identifier: command.Metadata.CorrelationId.ToString()))
                {
                    await command.Func.Invoke();
                }
            }
            catch (Exception ex)
            {
                this.log.Error(ex, $"Failed {command.Metadata.Context.Name}");
            }
        }

        public async Task<T> CallAsync<T>(SafeCallServiceQuery<T> command)
        {
            this.log.ForContext(command.Metadata.Context);

            try
            {
                using (this.log.BeginTimedOperation(command.Metadata.Name, identifier: command.Metadata.CorrelationId.ToString()))
                {                    
                    return await command.Func.Invoke();
                }
            }
            catch (Exception ex)
            {
                this.log.Error(ex, $"Failed {command.Metadata.Context.Name}");
                return default(T);
            }
        }
    }
}
