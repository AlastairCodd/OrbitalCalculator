using System;
using System.IO;
using Skade.CrossCutting.Messenger.Core.Enumerations;
using Skade.CrossCutting.Messenger.Core.Interfaces.Messages;
using TinyMessenger;

namespace Skade.CrossCutting.Messenger.Common.Messages
{
    /// <summary>
    ///     A base message.
    /// </summary>
    ///
    /// <seealso cref="TinyMessageBase"/>
    /// <seealso cref="IBaseMessage"/>
    /// <seealso cref="IMessageSendDataWriter"/>
    public abstract class BaseMessage : TinyMessageBase, IBaseMessage, IMessageSendDataWriter
    {
        #region Properties and Indexers

        /// <inheritdoc/>
        public string Message { get; }

        /// <inheritdoc/>
        public MessageLevelEnum MessageLevel { get; }

        /// <inheritdoc/>
        public string ClassName { get; }

        /// <inheritdoc/>
        public string MethodName { get; }

        /// <inheritdoc/>
        public int LineNumber { get; }

        /// <inheritdoc cref="IBaseMessage" />
        public long TimeStamp { get; set; }

        /// <inheritdoc cref="IBaseMessage" />
        public ulong SequenceNumber { get; set; }

        #endregion

        #region Constructors

        ///  <summary>
        ///      Specialised constructor for use only by derived class.
        ///  </summary>
        /// <param name="sender">       Source of the event. </param>
        /// <param name="message">      The message. </param>
        /// <param name="messageLevel"> The message level. </param>
        /// <param name="callerFile">   The caller file. </param>
        /// <param name="methodName">   Name of the method. </param>
        /// <param name="lineNumber">   The line number. </param>
        protected BaseMessage(
            object sender,
            string message,
            MessageLevelEnum messageLevel,
            string callerFile = null,
            string methodName = null,
            int lineNumber = 0 ) : base( sender )
        {
            if ( string.IsNullOrWhiteSpace( callerFile ) ) throw new ArgumentException( nameof(callerFile) );
            if ( string.IsNullOrWhiteSpace( methodName ) ) throw new ArgumentException( nameof(methodName) );
            if ( lineNumber <= 0 ) throw new ArgumentException( nameof(lineNumber) );

            Message = message;
            MessageLevel = messageLevel;
            ClassName = Path.GetFileNameWithoutExtension( callerFile );
            MethodName = methodName;
            LineNumber = lineNumber;
        }

        #endregion
    }
}