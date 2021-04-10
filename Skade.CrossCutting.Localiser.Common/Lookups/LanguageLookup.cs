using System;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Skade.CrossCutting.Localiser.Core.Enumerations;
using Skade.CrossCutting.Localiser.Core.Interfaces.Entities;

namespace Skade.CrossCutting.Localiser.Common.Entities
{
    /// <summary>
    ///     A language entity.
    /// </summary>
    ///
    /// <seealso cref="ILanguageLookup"/>
    [ExcludeFromCodeCoverage]
    [UsedImplicitly]
    public class LanguageLookup : ILanguageLookup
    {
        #region Properties and Indexers

        /// <inheritdoc />
        public LanguageEnum Key { get; }

        /// <inheritdoc />
        public string Caption { get; }

        /// <inheritdoc />
        public string FlagImagePath { get; }

        #endregion

        #region Constructors

        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <exception cref="ArgumentNullException">    Thrown when one or more required arguments are null. </exception>
        /// <param name="key">              The key. </param>
        /// <param name="caption">          The caption. This cannot be null. </param>
        /// <param name="flagImagePath">    Full pathname of the flag image file. This cannot be null. </param>
        public LanguageLookup(
            LanguageEnum key,
            [JetBrains.Annotations.NotNull] string caption,
            [JetBrains.Annotations.NotNull] string flagImagePath )
        {
            Key = key;
            Caption = caption ?? throw new ArgumentNullException( nameof(caption) );
            FlagImagePath = flagImagePath ?? throw new ArgumentNullException( nameof(flagImagePath) );
        }

        #endregion
    }
}