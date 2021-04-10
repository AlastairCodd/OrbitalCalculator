using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Skade.CrossCutting.Localiser.Core.Enumerations;
using Skade.CrossCutting.Localiser.Core.Interfaces.Messages;
using Skade.CrossCutting.Messenger.Common.Messages;
using Skade.CrossCutting.Messenger.Core.Enumerations;

namespace Skade.CrossCutting.Localiser.Common.Messages
{
    /// <summary>
    ///     A language changed message.
    /// </summary>
    ///
    /// <seealso cref="PayloadMessage{LanguageEnum}"/>
    /// <seealso cref="ILanguageChangedMessage"/>
    [ExcludeFromCodeCoverage]
    [UsedImplicitly]
    public class LanguageChangedMessage : PayloadMessage< LanguageEnum >, ILanguageChangedMessage
    {
        #region Constructors

        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <param name="source">       Source of the message. </param>
        /// <param name="payload">      The payload. </param>
        /// <param name="message">      The message. </param>
        /// <param name="messageLevel"> The message level. </param>
        /// <param name="callerFile">   (Optional) The caller file. </param>
        /// <param name="methodName">   (Optional) Name of the method. </param>
        /// <param name="lineNumber">   (Optional) The line number. </param>
        [SuppressMessage( "Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed" )]
        public LanguageChangedMessage(
            [JetBrains.Annotations.NotNull] object source,
            LanguageEnum payload,
            [JetBrains.Annotations.NotNull] string message,
            MessageLevelEnum messageLevel = MessageLevelEnum.Verbose,
            [CallerFilePath] string callerFile = null,
            [CallerMemberName] string methodName = null,
            [CallerLineNumber] int lineNumber = 0 ) : base( source, payload, message, messageLevel, callerFile, methodName, lineNumber ) { }

        #endregion
    }
}