using System;
using JetBrains.Annotations;
using TinyMessenger;

namespace Skade.CrossCutting.Messenger.Core.Interfaces.Services
{
    /// <summary>
    ///     Interface for messenger service.
    /// </summary>
    public interface IMessengerService
    {
        #region Other Members

        /// <summary>
        ///     Subscribe to a message type with the given destination and delivery action. All references are held with WeakReferences
        ///     
        ///     All messages of this type will be delivered.
        /// </summary>
        ///
        /// <typeparam name="TMessage"> Type of message. </typeparam>
        /// <param name="deliveryAction">   Action to invoke when message is delivered. </param>
        ///
        /// <returns>
        ///     TinyMessageSubscription used to unsubscribe.
        /// </returns>
        [NotNull] 
        ITinyMessageSubscriptionToken Subscribe< TMessage >(
            [NotNull] Action< TMessage > deliveryAction ) where TMessage : class, ITinyMessage;

        /// <summary>
        ///     Subscribe to a message type with the given destination and delivery action with the given filter. All references are held with WeakReferences
        ///     
        ///     Only messages that "pass" the filter will be delivered.
        /// </summary>
        ///
        /// <typeparam name="TMessage"> Type of message. </typeparam>
        /// <param name="deliveryAction">   Action to invoke when message is delivered. </param>
        /// <param name="messageFilter">    The message filter. </param>
        ///
        /// <returns>
        ///     TinyMessageSubscription used to unsubscribe.
        /// </returns>
        [NotNull] 
        ITinyMessageSubscriptionToken Subscribe< TMessage >(
            [NotNull] Action< TMessage > deliveryAction,
            [NotNull] Func< TMessage, bool > messageFilter ) where TMessage : class, ITinyMessage;

        /// <summary>
        ///     Unsubscribe from a particular message type.
        ///     
        ///     Does not throw an exception if the subscription is not found.
        /// </summary>
        ///
        /// <typeparam name="TMessage"> Type of message. </typeparam>
        /// <param name="subscriptionToken">    Subscription token received from Subscribe. </param>
        void Unsubscribe< TMessage >(
            [NotNull] ITinyMessageSubscriptionToken subscriptionToken ) where TMessage : class, ITinyMessage;

        /// <summary>
        ///     Publish a message to any subscribers.
        /// </summary>
        ///
        /// <typeparam name="TMessage"> Type of message. </typeparam>
        /// <param name="message">  Message to deliver. </param>
        void Send< TMessage >(
            [NotNull] TMessage message ) where TMessage : class, ITinyMessage;

        /// <summary>
        ///     Publish a message to any subscribers asynchronously.
        /// </summary>
        ///
        /// <typeparam name="TMessage"> Type of message. </typeparam>
        /// <param name="message">  Message to deliver. </param>
        void SendAsync< TMessage >(
            [NotNull] TMessage message ) where TMessage : class, ITinyMessage;

        /// <summary>
        ///     Publish a message to any subscribers asynchronously.
        /// </summary>
        ///
        /// <typeparam name="TMessage"> Type of message. </typeparam>
        /// <param name="message">  Message to deliver. </param>
        /// <param name="callback"> AsyncCallback called on completion. </param>
        void SendAsync< TMessage >(
            [NotNull] TMessage message,
            [NotNull] AsyncCallback callback ) where TMessage : class, ITinyMessage;

        /// <summary>
        ///     Unsubscribe from a particular message type.
        ///     
        ///     Does not throw an exception if the subscription is not found.
        /// </summary>
        ///
        /// <param name="subscriptionToken">    Subscription token received from Subscribe. </param>
        void Unsubscribe(
            [NotNull] ITinyMessageSubscriptionToken subscriptionToken );

        #endregion
    }
}