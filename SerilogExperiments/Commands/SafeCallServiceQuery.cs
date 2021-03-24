namespace SerilogExperiments.Commands
{
    using System;
    using System.Threading.Tasks;

    public class SafeCallServiceQuery<T>
    {
        public Func<Task<T>> Func { get; private set; }
        public SafeCallServiceLogMetadata Metadata { get; private set; }

        public SafeCallServiceQuery(Func<Task<T>> func, SafeCallServiceLogMetadata metadata)
        {
            this.Func = func ?? throw new ArgumentNullException(nameof(func), $"Require {nameof(func)} to execute");
            this.Metadata = metadata ?? throw new ArgumentNullException(nameof(metadata), $"Require {nameof(metadata)} for logging");
        }
    }
}
