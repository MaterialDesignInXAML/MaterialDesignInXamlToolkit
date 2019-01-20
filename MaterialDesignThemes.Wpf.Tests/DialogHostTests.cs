using System;
using System.Threading.Tasks;
using System.Windows;
using Xunit;

namespace MaterialDesignThemes.Wpf.Tests
{
    public class DialogHostTests : IDisposable
    {
        private readonly DialogHost _dialogHost;

        public DialogHostTests()
        {
            _dialogHost = new DialogHost();
            _dialogHost.RaiseEvent(new RoutedEventArgs(FrameworkElement.LoadedEvent));
        }

        public void Dispose()
        {
            _dialogHost.RaiseEvent(new RoutedEventArgs(FrameworkElement.UnloadedEvent));
        }

        [StaFact(Timeout = 500)]
        public void CanOpenAndCloseDialogWithIsOpen()
        {
            _dialogHost.IsOpen = true;
            DialogSession session = _dialogHost.CurrentSession;
            Assert.False(session.IsEnded);
            _dialogHost.IsOpen = false;

            Assert.False(_dialogHost.IsOpen);
            Assert.Null(_dialogHost.CurrentSession);
            Assert.True(session.IsEnded);
        }

        [StaFact(Timeout = 500)]
        public async Task CanOpenAndCloseDialogWithShowMethod()
        {
            var id = Guid.NewGuid();
            _dialogHost.Identifier = id;

            object result = await DialogHost.Show("Content", id,
                new DialogOpenedEventHandler(((sender, args) => { args.Session.Close(42); })));

            Assert.Equal(42, result);
            Assert.False(_dialogHost.IsOpen);
        }

        [StaFact(Timeout = 500)]
        public async Task CanOpenDialogWithShowMethodAndCloseWithIsOpen()
        {
            var id = Guid.NewGuid();
            _dialogHost.Identifier = id;

            object result = await DialogHost.Show("Content", id,
                new DialogOpenedEventHandler(((sender, args) => { _dialogHost.IsOpen = false; })));

            Assert.Null(result);
            Assert.False(_dialogHost.IsOpen);
        }

        [StaFact(Timeout = 500)]
        public async Task DialogHostExposesSessionAsProperty()
        {
            var id = Guid.NewGuid();
            _dialogHost.Identifier = id;

            await DialogHost.Show("Content", id,
                new DialogOpenedEventHandler(((sender, args) =>
                {
                    Assert.True(ReferenceEquals(args.Session, _dialogHost.CurrentSession));
                    args.Session.Close();
                })));
        }

        [StaFact(Timeout = 500)]
        public async Task CannotShowDialogWhileItIsAlreadyOpen()
        {
            var id = Guid.NewGuid();
            _dialogHost.Identifier = id;

            await DialogHost.Show("Content", id,
                new DialogOpenedEventHandler((async (sender, args) =>
                {
                    var ex = await Assert.ThrowsAsync<InvalidOperationException>(() => DialogHost.Show("Content", id));
                    args.Session.Close();
                    Assert.Equal("DialogHost is already open.", ex.Message);
                })));
        }

        [StaFact(Timeout = 500)]
        public async Task WhenNoDialogsAreOpenItThrows()
        {
            var id = Guid.NewGuid();
            _dialogHost.RaiseEvent(new RoutedEventArgs(FrameworkElement.UnloadedEvent));

            var ex = await Assert.ThrowsAsync<InvalidOperationException>(() => DialogHost.Show("Content", id));

            Assert.Equal("No loaded DialogHost instances.", ex.Message);
        }

        [StaFact(Timeout = 500)]
        public async Task WhenNoDialogsMatchIdentifierItThrows()
        {
            var id = Guid.NewGuid();

            var ex = await Assert.ThrowsAsync<InvalidOperationException>(() => DialogHost.Show("Content", id));

            Assert.Equal($"No loaded DialogHost have an {nameof(DialogHost.Identifier)} property matching dialogIdentifier argument.", ex.Message);
        }

        [StaFact(Timeout = 500)]
        public async Task WhenMultipleDialogHostsHaveTheSameIdentifierItThrows()
        {
            var id = Guid.NewGuid();
            _dialogHost.Identifier = id;
            var otherDialogHost = new DialogHost { Identifier = id };
            otherDialogHost.RaiseEvent(new RoutedEventArgs(FrameworkElement.LoadedEvent));

            var ex = await Assert.ThrowsAsync<InvalidOperationException>(() => DialogHost.Show("Content", id));

            otherDialogHost.RaiseEvent(new RoutedEventArgs(FrameworkElement.UnloadedEvent));


            Assert.Equal("Multiple viable DialogHosts.  Specify a unique Identifier on each DialogHost, especially where multiple Windows are a concern.", ex.Message);
        }

        [StaFact(Timeout = 500)]
        public async Task WhenNoIdentifierIsSpecifiedItUsesSingleDialogHost()
        {
            bool isOpen = false;
            await DialogHost.Show("Content", new DialogOpenedEventHandler(((sender, args) =>
            {
                isOpen = _dialogHost.IsOpen;
                args.Session.Close();
            })));

            Assert.True(isOpen);
        }

        [StaFact(Timeout = 500)]
        public async Task WhenContentIsNullItThrows()
        {
            var ex = await Assert.ThrowsAsync<ArgumentNullException>(() => DialogHost.Show(null));
            
            Assert.Equal("content", ex.ParamName);
        }
    }
}