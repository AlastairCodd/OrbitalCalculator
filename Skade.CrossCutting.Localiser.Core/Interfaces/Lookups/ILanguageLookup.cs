using JetBrains.Annotations;
using Skade.CrossCutting.Localiser.Core.Enumerations;

namespace Skade.CrossCutting.Localiser.Core.Interfaces.Entities
{
    /// <summary>
    ///     Interface for language entity.
    /// </summary>
    public interface ILanguageLookup
    {
        #region Properties and Indexers

        /// <summary>
        ///     Gets the Key.
        /// </summary>
        /// <value>
        ///     The key.
        /// </value>
        LanguageEnum Key { get; }

        /// <summary>
        ///     Gets the Caption.
        /// </summary>
        /// <value>
        ///     The caption. This will never be null.
        /// </value>
        [NotNull]
        string Caption { get; }

        /// <summary>
        ///     Gets the full pathname of the Flag image file.
        /// </summary>
        /// <value>
        ///     The full pathname of the flag image file. This will never be null.
        /// </value>
        [NotNull]
        string FlagImagePath { get; }

        #endregion Properties and Indexers
    }
}