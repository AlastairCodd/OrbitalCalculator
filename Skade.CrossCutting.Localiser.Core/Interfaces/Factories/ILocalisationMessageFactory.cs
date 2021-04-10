using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Skade.CrossCutting.Localiser.Core.Enumerations;
using Skade.CrossCutting.Localiser.Core.Interfaces.Messages;
using Skade.CrossCutting.Messenger.Core.Enumerations;

namespace Skade.CrossCutting.Localiser.Core.Interfaces.Factories
{
    /// <summary>
    ///     Interface for localisation message factory.
    /// </summary>
    public interface ILocalisationMessageFactory
    {
        #region Other Members

        /// <summary>
        ///     Creates a Language Changed Message.
        /// </summary>
        /// <param name="source">       Source of the message. This cannot be null. </param>
        /// <param name="payload">      The current language. This cannot be null. </param>
        /// <param name="message">      The message. This cannot be null. </param>
        /// <param name="messageLevel"> (Optional) The message level. </param>
        /// <param name="callerFile">   (Optional) The caller file. </param>
        /// <param name="methodName">   (Optional) Name of the method. </param>
        /// <param name="lineNumber">   (Optional) The line number. </param>
        /// <returns>
        ///     The new language changed message. This will never be null.
        /// </returns>
        [SuppressMessage( "Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed" )]
        [JetBrains.Annotations.NotNull]
        ILanguageChangedMessage CreateLanguageChangedMessage(
            [NotNull] object source,
            LanguageEnum payload,
            [NotNull] string message,
            MessageLevelEnum messageLevel = MessageLevelEnum.Information,
            [CallerFilePath] string callerFile = null,
            [CallerMemberName] string methodName = null,
            [CallerLineNumber] int lineNumber = 0 );

        #endregion
    }
}