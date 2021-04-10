using JetBrains.Annotations;
using Skade.CrossCutting.Messenger.Core.Enumerations;
using TinyMessenger;

namespace Skade.CrossCutting.Messenger.Core.Interfaces.Messages
{
    /// <summary>
    ///     Interface for base message.
    /// </summary>
    public interface IBaseMessage : ITinyMessage
    {
        #region Properties and Indexers

        /// <summary>
        ///     Gets the message.
        /// </summary>
        ///
        /// <value>
        ///     The message.
        /// </value>
        [NotNull]
        string Message { get; }

        /// <summary>
        ///     Gets the message level.
        /// </summary>
        ///
        /// <value>
        ///     The message level.
        /// </value>
        MessageLevelEnum MessageLevel { get; }

        /// <summary>
        ///     Gets the name of the class.
        /// </summary>
        ///
        /// <value>
        ///     The name of the class.
        /// </value>
        [NotNull]
        string ClassName { get; }

        /// <summary>
        ///     Gets the name of the method.
        /// </summary>
        ///
        /// <value>
        ///     The name of the method.
        /// </value>
        [NotNull]
        string MethodName { get; }

        /// <summary>
        ///     Gets the line number.
        /// </summary>
        ///
        /// <value>
        ///     The line number.
        /// </value>
        int LineNumber { get; }

        /// <summary>
        ///     Gets the time stamp.
        /// </summary>
        /// <value>
        ///     The time stamp.
        /// </value>
        long TimeStamp { get; }

        /// <summary>
        ///     Gets the sequence number.
        /// </summary>
        /// <value>
        ///     The sequence number.
        /// </value>
        ulong SequenceNumber { get; }

        #endregion
    }
}