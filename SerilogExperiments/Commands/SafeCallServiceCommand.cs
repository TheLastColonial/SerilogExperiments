namespace SerilogExperiments.Commands
{
    using System;
    using System.Threading.Tasks;

    public class SafeCallServiceCommand
    {
        public Func<Task> Func { get; private set; }
        public SafeCallServiceLogMetadata Metadata { get; private set; }

        public SafeCallServiceCommand(Func<Task> func, SafeCallServiceLogMetadata metadata)
        {
            this.Func = func ?? throw new ArgumentNullException(nameof(func), $"Require {nameof(func)} to execute");
            this.Metadata = metadata ?? throw new ArgumentNullException(nameof(metadata), $"Require {nameof(metadata)} for logging");
        }
    }
}
