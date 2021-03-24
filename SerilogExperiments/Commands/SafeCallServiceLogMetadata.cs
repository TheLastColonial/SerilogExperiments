namespace SerilogExperiments.Commands
{
    using System;

    public class SafeCallServiceLogMetadata
    {
        public Guid CorrelationId { get; private set; }
        public Type Context { get; private set; }
        public string Name { get; private set; }

        public SafeCallServiceLogMetadata(Guid correlationId, Type context, string name)
        {
            this.CorrelationId = correlationId;
            this.Context = context ?? throw new ArgumentNullException(nameof(context), $"Require {nameof(context)} to reference source in logs");

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name), $"Require {nameof(name)} to reference source in logs");
            }

            this.Name = name;
        }
    }
}
