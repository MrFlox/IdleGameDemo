using MessagePipe;
using Modules.Signals;
using VContainer;

namespace DI
{
    public class SignalInstaller
    {
        public void Configure(IContainerBuilder builder)
        {
            var options = builder.RegisterMessagePipe( /* configure option */);
            builder.RegisterBuildCallback(c => GlobalMessagePipe.SetProvider(c.AsServiceProvider()));
            builder.RegisterMessageBroker<LoadingSignal, float>(options);
            builder.RegisterMessageBroker<FlyingTextSignal>(options);
            builder.RegisterMessageBroker<WindowSignal>(options);
            builder.RegisterMessageBroker<OpenWindowSignal>(options);
            builder.RegisterMessageBroker<BuyUnitSignal>(options);
        }
    }
}