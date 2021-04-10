using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Skade.CrossCutting.Core.Interfaces.Lookups;
using Skade.CrossCutting.Core.Interfaces.Messages;
using Skade.CrossCutting.Messenger.Core.Enumerations;
using NotNull = JetBrains.Annotations.NotNullAttribute;

namespace Skade.CrossCutting.Core.Interfaces.Factories
{
    /// <summary>
    ///     Interface for message factory.
    /// </summary>
    public interface IMessageFactory
    {
        #region Other Members

        /// <summary>
        ///     Creates an Output Message.
        /// </summary>
        ///
        /// <param name="messageSource">    The message source. This cannot be null. </param>
        /// <param name="payload">          The payload. </param>
        /// <param name="message">          (Optional) The message. </param>
        /// <param name="messageLevel">     (Optional) The message level. </param>
        /// <param name="callerFile">       (Optional) The caller file. </param>
        /// <param name="methodName">       (Optional) Name of the method. </param>
        /// <param name="lineNumber">       (Optional) The line number. </param>
        ///
        /// <returns>
        ///     The new output message. This will never be null.
        /// </returns>
        [SuppressMessage( "Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed" )]
        [NotNull]
        [Pure]
        IOutputMessage CreateOutputMessage(
            [NotNull] object messageSource,
            [NotNull] IWatchLookup payload,
            [NotNull] string message = "Output Message",
            MessageLevelEnum messageLevel = MessageLevelEnum.Warning,
            [CallerFilePath] string callerFile = null,
            [CallerMemberName] string methodName = null,
            [CallerLineNumber] int lineNumber = 0 );

        #endregion
    }
}