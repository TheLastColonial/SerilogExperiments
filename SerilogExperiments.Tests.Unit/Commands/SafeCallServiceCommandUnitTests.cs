namespace SerilogExperiments.Tests.Unit.Commands
{
    using System;
    using System.Threading.Tasks;
    using NUnit.Framework;
    using SerilogExperiments.Commands;

    /// <summary>
    /// Unit Tests for <see cref="SafeCallServiceCommand"/>
    /// </summary>
    [TestFixture]
    public class SafeCallServiceCommandUnitTests
    {
        public SafeCallServiceCommand Command { get; set; }

        public SafeCallServiceLogMetadata Metadata { get; set; }

        [Test]
        public void Construct_Successfully()
        {
            this.Metadata = new SafeCallServiceLogMetadata(Guid.NewGuid(), typeof(SafeCallServiceCommandUnitTests), "Unit Test");
            this.Command = new SafeCallServiceCommand(() => Task.CompletedTask, this.Metadata);
        }

    }
}
