namespace SerilogExperiments.UnitTests.Services
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Moq;
    using NUnit.Framework;
    using Serilog;
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
            this.CallService = new SafeCallService(this.MockLogger.Object, new Stopwatch());
        }

        /// <summary>
        /// Given IoC has valid dependencies available
        /// When constructing the <see cref="SafeCallService"/>
        /// Then do not throw any exceptions
        /// </summary>
        [Test]
        public void Construction_WithValidParameters_DoesNotThrowException()
        {
            Assert.DoesNotThrow(() => new SafeCallService(this.MockLogger.Object, new Stopwatch()));
        }

        /// <summary>
        /// Given an operation taking 1 second
        /// When calling <see cref="SafeCallService.Call(Func{Task}, Type)"/>
        /// Then log the start and end with the time of the operation
        /// </summary>
        [Test]
        public async Task Call_WithValidParameters_LogTiming()
        {
            await this.CallService.Call(() => Task.Delay(1000), typeof(SafeCallService));

            this.MockLogger.Verify(x => x.Debug(It.IsAny<string>()), Times.Once);
            this.MockLogger.Verify(x => x.Information(It.IsAny<string>()), Times.Once);
        }
    }
}
