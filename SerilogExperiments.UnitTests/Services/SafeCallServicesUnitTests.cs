namespace SerilogExperiments.UnitTests.Services
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Primitives;
    using Moq;
    using NUnit.Framework;
    using Serilog;
    using SerilogExperiments.Commands;
    using SerilogExperiments.Services;
    using SerilogExperiments.UnitTests.Helpers;

    [TestFixture]
    public class SafeCallServicesUnitTests
    {
        public SafeCallService CallService { get; private set; }
        public Mock<ILogger> MockLogger { get; private set; }

        [SetUp]
        public void Setup()
        {
            this.MockLogger = LoggerStub.GetLogger();
            this.CallService = new SafeCallService(this.MockLogger.Object);
        }

        /// <summary>
        /// Given IoC has valid dependencies available
        /// When constructing the <see cref="SafeCallService"/>
        /// Then do not throw any exceptions
        /// </summary>
        [Test]
        public void Construction_WithValidParameters_DoesNotThrowException()
        {
            Assert.DoesNotThrow(() => new SafeCallService(this.MockLogger.Object));
        }

        /// <summary>
        /// Given an operation taking 1 second
        /// When calling <see cref="SafeCallService.Call(Func{Task}, Type)"/>
        /// Then log the start and end with the time of the operation
        /// </summary>
        [Test]
        public async Task Call_WithValidParameters_LogTiming()
        {
            var command = new SafeCallServiceCommand(
                () => Task.Delay(1000),
                new SafeCallServiceLogMetadata(
                    Guid.NewGuid(),
                    typeof(SafeCallService),
                    "Unit Tests"));

            await this.CallService.Call(command);
        }
    }
}
