using AspectsMvcApplication.Interceptors;
using AspectsMvcApplication.Services;
using Autofac;
using Autofac.Extras.DynamicProxy2;
using Castle.DynamicProxy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using FluentAssertions;

namespace AspectsTestProject
{
    [TestClass]
    public class LoggingAspectTest
    {
        private IContainer _container;

        [TestInitialize]
        public void SetUp()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<TestLoggingService>().As<ILoggingService>().SingleInstance();
            builder.RegisterType<LoggingAspect>();

            builder.RegisterType<LoggerTesting>()
                .As<ILoggerTesting>()
                .EnableInterfaceInterceptors()
                .InterceptedBy(typeof(LoggingAspect));

            _container = builder.Build();
        }

        [TestMethod]
        public void TestLoggingIntercept()
        {
            const string methodName = "TestLoggedMethod";

            var loggingService = new TestLoggingService();
            var loggingAspect = new LoggingAspect(loggingService);

            var mockInvocation = new Mock<IInvocation>();
            mockInvocation.Setup(inv => inv.Method.Name).Returns(methodName);
            
            loggingAspect.Intercept(mockInvocation.Object);

            loggingService.Count.Should().Be(2);
            loggingService[0].Should().Be(methodName + " started");
            loggingService[1].Should().Be(methodName + " completed");
        }

        [TestMethod]
        public void TestLoggingInterceptMocks()
        {
            const string methodName = "TestLoggedMethod";

            var mockLoggingService = new Mock<ILoggingService>();
            mockLoggingService.Setup(ls => ls.Log(It.IsAny<string>()));

            var loggingService = mockLoggingService.Object;
            var loggingAspect = new LoggingAspect(loggingService);
            var mockInvocation = new Mock<IInvocation>();
            mockInvocation.Setup(inv => inv.Method.Name).Returns(methodName);

            loggingAspect.Intercept(mockInvocation.Object);

            mockLoggingService.Verify(ls => ls.Log(methodName + " started"));
            mockLoggingService.Verify(ls => ls.Log(methodName + " completed"));
        }

        [TestMethod]
        public void TestLoggingFromContainer()
        {
            const string methodName = "TestLoggedMethod";

            var loggerTesting = _container.Resolve<ILoggerTesting>();
            var loggingService = _container.Resolve<ILoggingService>() as TestLoggingService;

            loggerTesting.TestLoggedMethod();

            loggingService.Count.Should().Be(2);
            loggingService[0].Should().Be(methodName + " started");
            loggingService[1].Should().Be(methodName + " completed");
        }
    }
}
