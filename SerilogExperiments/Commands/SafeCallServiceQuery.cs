namespace SerilogExperiments.Commands
{
    using System;
    using System.Threading.Tasks;

    public class SafeCallServiceQuery<T>
    {
        public Func<Task<T>> Func { get; private set; }
        public Guid CorrelationId { get; private set; }
        public Type Context { get; private set; }
        public string Name { get; private set; }


        public SafeCallServiceQuery(Func<Task<T>> func, Guid correlationId, Type context, string name)
        {
            this.Func = func ?? throw new ArgumentNullException(nameof(func), $"Require {nameof(func)} to execute");
            this.CorrelationId = correlationId;
            this.Context = context ?? throw new ArgumentNullException(nameof(func), $"Require {nameof(context)} to reference source in logs");

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name), $"Require {nameof(name)} to reference source in logs");
            }

            this.Name = name;
        }
    }
}
