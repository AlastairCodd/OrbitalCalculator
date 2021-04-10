using System.Collections.ObjectModel;
using JetBrains.Annotations;
using Skade.CrossCutting.Localiser.Core.Enumerations;
using Skade.CrossCutting.Localiser.Core.Interfaces.Entities;

namespace Skade.CrossCutting.Localiser.Core.Interfaces.Services
{
    /// <summary>
    ///     Interface for localisation service.
    /// </summary>
    public interface ILocalisationService
    {
        #region Properties and Indexers

        /// <summary>
        ///     Gets the languages.
        /// </summary>
        /// <value>
        ///     The languages.
        /// </value>
        [NotNull]
        [ItemNotNull]
        ReadOnlyObservableCollection< ILanguageLookup > Languages { get; }

        /// <summary>
        ///     Gets the Current Language.
        /// </summary>
        /// <value>
        ///     The current language.
        /// </value>
        LanguageEnum CurrentLanguage { get; }

        #endregion

        #region Other Members

        /// <summary>
        ///     Change Language.
        /// </summary>
        /// <param name="language"> The language. </param>
        void ChangeLanguage(
            LanguageEnum language );

        /// <summary>
        ///     Change Language.
        /// </summary>
        /// <param name="cultureName">  Name of the culture. </param>
        /// <returns>
        ///     True if it succeeds, false if it fails.
        /// </returns>
        bool ChangeLanguage(
            string cultureName );

        /// <summary>
        ///     Reload languages.
        /// </summary>
        void ReloadLanguages();

        /// <summary>
        ///     Gets Localised String.
        /// </summary>
        /// <param name="key">  The key. This cannot be null. </param>
        /// <returns>
        ///     The localised string.
        /// </returns>
        [NotNull]
        [Pure]
        string GetLocalisedString(
            [NotNull] string key );

        /// <summary>
        ///     Builds a message.
        /// </summary>
        /// <param name="key">          The key. This cannot be null. </param>
        /// <param name="parameters">   A variable-length parameters list containing parameters. This cannot be null. </param>
        /// <returns>
        ///     A string.
        /// </returns>
        [NotNull]
        [Pure]
        string BuildMessage(
            [NotNull] string key,
            [NotNull] [ItemNotNull] params object[] parameters );

        #endregion
    }
}