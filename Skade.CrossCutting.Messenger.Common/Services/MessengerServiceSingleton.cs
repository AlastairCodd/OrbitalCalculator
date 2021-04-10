using System;
using System.Diagnostics.CodeAnalysis;
using Skade.CrossCutting.Core.Interfaces.Services;
using Skade.CrossCutting.Messenger.Core.Interfaces.Messages;
using Skade.CrossCutting.Messenger.Core.Interfaces.Services;
using TinyMessenger;

namespace Skade.CrossCutting.Messenger.Common.Services
{
    /// <summary>
    ///     A messenger service.
    /// </summary>
    ///
    /// <seealso cref="IMessengerService"/>
    [ExcludeFromCodeCoverage]
    public class MessengerServiceSingleton : IMessengerService
    {
        #region Fields

        /// <summary>
        ///     The message hub.
        /// </summary>
        [NotNull] private readonly ITinyMessengerHub _messageHub;

        /// <summary>
        ///     The date time services.
        /// </summary>
        [NotNull] private readonly IDateTimeService _dateTimeServices;

        /// <summary>
        ///     Identifier for the current message.
        /// </summary>
        private ulong _currentMessageId;

        #endregion

        #region Constructors

        /// <summary>
        ///     Constructor.
        /// </summary>
        ///
        /// <exception cref="ArgumentNullException">    Thrown when one or more required arguments are null. </exception>
        ///
        /// <param name="messageHub">       The message hub. </param>
        /// <param name="dateTimeServices"> The date time services. </param>
        public MessengerServiceSingleton(
            [NotNull] ITinyMessengerHub messageHub,
            [NotNull] IDateTimeService dateTimeServices )
        {
            _messageHub = messageHub ?? throw new ArgumentNullException( nameof(messageHub) );
            _dateTimeServices = dateTimeServices ?? throw new ArgumentNullException( nameof(dateTimeServices) );
            _currentMessageId = 1;
        }

        #endregion

        #region Interface Implementations

        /// <inheritdoc/>
        public void Send< TMessage >(
            TMessage message ) where TMessage : class, ITinyMessage
        {
            if ( message is null ) throw new ArgumentNullException( nameof(message) );

            if ( message is IMessageSendDataWriter writer )
            {
                writer.SequenceNumber = _currentMessageId++;
                writer.TimeStamp = _dateTimeServices.Now.Ticks;
            }

            _messageHub.Publish( message );
        }

        /// <inheritdoc/>
        public void SendAsync< TMessage >(
            TMessage message ) where TMessage : class, ITinyMessage
        {
            if ( message is null ) throw new ArgumentNullException( nameof(message) );

            if ( message is IMessageSendDataWriter writer )
            {
                writer.SequenceNumber = _currentMessageId++;
                writer.TimeStamp = _dateTimeServices.Now.Ticks;
            }

            _messageHub.PublishAsync( message );
        }

        /// <inheritdoc/>
        public void SendAsync< TMessage >(
            TMessage message,
            AsyncCallback callback ) where TMessage : class, ITinyMessage
        {
            if ( message is null ) throw new ArgumentNullException( nameof(message) );

            if ( message is IMessageSendDataWriter writer )
            {
                writer.SequenceNumber = _currentMessageId++;
                writer.TimeStamp = _dateTimeServices.Now.Ticks;
            }

            _messageHub.PublishAsync( message, callback );
        }

        /// <inheritdoc/>
        public ITinyMessageSubscriptionToken Subscribe< TMessage >(
            Action< TMessage > deliveryAction ) where TMessage : class, ITinyMessage
        {
            if ( deliveryAction is null ) throw new ArgumentNullException( nameof(deliveryAction) );

            return _messageHub.Subscribe( deliveryAction );
        }

        /// <inheritdoc/>
        public ITinyMessageSubscriptionToken Subscribe< TMessage >(
            Action< TMessage > deliveryAction,
            Func< TMessage, bool > messageFilter ) where TMessage : class, ITinyMessage
        {
            if ( deliveryAction is null ) throw new ArgumentNullException( nameof(deliveryAction) );
            if ( messageFilter is null ) throw new ArgumentNullException( nameof(messageFilter) );

            return _messageHub.Subscribe( deliveryAction, messageFilter );
        }

        /// <inheritdoc/>
        public void Unsubscribe< TMessage >(
            ITinyMessageSubscriptionToken subscriptionToken ) where TMessage : class, ITinyMessage
        {
            if (subscriptionToken is null) throw new ArgumentNullException(nameof(subscriptionToken));

            _messageHub.Unsubscribe< TMessage >( subscriptionToken );
        }

        /// <inheritdoc/>
        public void Unsubscribe(
            ITinyMessageSubscriptionToken subscriptionToken )
        {
            subscriptionToken?.Dispose();
        }

        #endregion
    }
}