using System;
using JetBrains.Annotations;
using Skade.CrossCutting.Messenger.Core.Enumerations;

namespace Skade.CrossCutting.Core.Interfaces.Lookups
{
    /// <summary>
    ///     Interface for watch lookup.
    /// </summary>
    public interface IWatchLookup
    {
        #region Properties and Indexers

        /// <summary>
        ///     Gets the Message Header.
        /// </summary>
        ///
        /// <value>
        ///     The header. This will never be null.
        /// </value>
        [NotNull]
        string Header { get; }

        /// <summary>
        ///     Gets the Message Contents.
        /// </summary>
        ///
        /// <value>
        ///     The message. This will never be null.
        /// </value>
        [NotNull]
        string Message { get; }

        /// <summary>
        ///     Gets the Date/Time for the message.
        /// </summary>
        ///
        /// <value>
        ///     The time.
        /// </value>
        DateTime Time { get; }

        /// <summary>
        ///     Gets the message level.
        /// </summary>
        ///
        /// <value>
        ///     The message level.
        /// </value>
        MessageLevelEnum MessageLevel { get; }

        #endregion
    }
}