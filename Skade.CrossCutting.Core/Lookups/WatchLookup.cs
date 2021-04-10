using System;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Skade.CrossCutting.Core.Constants;
using Skade.CrossCutting.Core.Interfaces.Lookups;
using Skade.CrossCutting.Messenger.Core.Enumerations;

namespace Skade.CrossCutting.Core.Lookups
{
    /// <summary>
    ///     A watch lookup.
    /// </summary>
    ///
    /// <seealso cref="IWatchLookup"/>
    [ExcludeFromCodeCoverage]
    [UsedImplicitly]
    public class WatchLookup : IWatchLookup
    {
        #region Properties and Indexers

        /// <inheritdoc/>
        public string Header { get; }

        /// <inheritdoc/>
        public string Message { get; }

        /// <inheritdoc/>
        public DateTime Time { get; }

        /// <inheritdoc/>
        public MessageLevelEnum MessageLevel { get; }

        #endregion

        #region Constructors

        /// <summary>
        ///     Constructor.
        /// </summary>
        ///
        /// <exception cref="ArgumentException">    Thrown when one or more arguments have unsupported or illegal values. </exception>
        ///
        /// <param name="header">       The Message Header. This cannot be null or whitespace. </param>
        /// <param name="message">      The Message Contents. This cannot be null or whitespace. </param>
        /// <param name="time">         The Date/Time. </param>
        /// <param name="messageLevel"> The message level. </param>
        public WatchLookup(
            [JetBrains.Annotations.NotNull] string header,
            [JetBrains.Annotations.NotNull] string message,
            DateTime time,
            MessageLevelEnum messageLevel )
        {
            if ( string.IsNullOrWhiteSpace( header ) ) throw new ArgumentException( DevConstants.ValueCannotBeNullOrWhitespace, nameof(header) );
            if ( string.IsNullOrWhiteSpace( message ) ) throw new ArgumentException( DevConstants.ValueCannotBeNullOrWhitespace, nameof(message) );

            Header = header;
            Message = message;
            Time = time;
            MessageLevel = messageLevel;
        }

        #endregion
    }
}