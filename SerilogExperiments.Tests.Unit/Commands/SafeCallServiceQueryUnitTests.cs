namespace SerilogExperiments.Tests.Unit.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using NUnit.Framework;
    using SerilogExperiments.Commands;

    /// <summary>
    /// Unit tests for <see cref="SafeCallServiceQuery{T}"/>
    /// </summary>
    [TestFixture]
    public class SafeCallServiceQueryUnitTests
    {
        public SafeCallServiceQuery<string> Query { get; set; }

        public SafeCallServiceLogMetadata Metadata { get; set; }

        public void Construct_Successfully()
        {
            this.Metadata = new SafeCallServiceLogMetadata(Guid.NewGuid(), typeof(SafeCallServiceQueryUnitTests), "Unit Tests");
            this.Query = new SafeCallServiceQuery<string>(() => Task.FromResult("Success"), this.Metadata);
        }
    }
}
