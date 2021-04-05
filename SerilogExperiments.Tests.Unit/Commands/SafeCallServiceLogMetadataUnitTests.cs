namespace SerilogExperiments.Tests.Unit.Commands
{
    using System;
    using NUnit.Framework;
    using SerilogExperiments.Commands;

    /// <summary>
    /// Unit Tests for <see cref="SafeCallServiceLogMetadata"/>
    /// </summary>
    [TestFixture]
    public class SafeCallServiceLogMetadataUnitTests
    {
        public SafeCallServiceLogMetadata Metadata { get; set; }

        [Test]
        public void Constuct_Successfully()
        {
            this.Metadata = new SafeCallServiceLogMetadata(Guid.NewGuid(), typeof(SafeCallServiceLogMetadataUnitTests), "Unit Tests");
        }
    }
}
