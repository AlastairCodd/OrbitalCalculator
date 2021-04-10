using System;
using JetBrains.Annotations;
using Skade.CrossCutting.Messenger.Core.Enumerations;
using Skade.CrossCutting.Messenger.Core.Interfaces.Messages;

namespace Skade.CrossCutting.Messenger.Common.Messages
{
    /// <summary>
    ///     A payload message.
    /// </summary>
    ///
    /// <typeparam name="T">    Generic type parameter. </typeparam>
    ///
    /// <seealso cref="BaseMessage"/>
    /// <seealso cref="IPayloadMessage{T}"/>
    public abstract class PayloadMessage< T > : BaseMessage, IPayloadMessage< T >
    {
        #region Properties and Indexers

        /// <inheritdoc/>
        public T Payload { get; }

        #endregion

        #region Constructors

        ///  <summary>
        ///      Constructor.
        ///  </summary>
        /// <param name="source">       Source for the. </param>
        /// <param name="payload">      The payload. </param>
        /// <param name="message">      The message. </param>
        /// <param name="messageLevel"> The message level. </param>
        /// <param name="callerFile">   The caller file. </param>
        /// <param name="methodName">   Name of the method. </param>
        /// <param name="lineNumber">   The line number. </param>
        protected PayloadMessage(
            [NotNull] object source,
            [NotNull] T payload,
            [NotNull] string message,
            MessageLevelEnum messageLevel,
            string callerFile = null,
            string methodName = null,
            int lineNumber = 0 ) : base( source, message, messageLevel, callerFile, methodName, lineNumber )
        {
            if ( payload is null ) throw new ArgumentNullException( nameof(payload) );

            Payload = payload;
        }

        #endregion
    }
}