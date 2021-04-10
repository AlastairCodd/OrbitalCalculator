using System.Collections.Generic;
using JetBrains.Annotations;

namespace Skade.CrossCutting.Core.Interfaces.Prototypes
{
    /// <summary>
    ///     Interface for immutable prototype.
    /// </summary>
    ///
    /// <typeparam name="T">    Generic type parameter. </typeparam>
    public interface IImmutablePrototype< T >
    {
        #region Other Members

        /// <summary>
        ///     Makes a deep copy of this instance.
        /// </summary>
        ///
        /// <param name="source">   Another instance to copy. This cannot be null. </param>
        ///
        /// <returns>
        ///     A copy of this instance. This will never be null.
        /// </returns>
        [NotNull]
        [Pure]
        T Clone(
            [NotNull] T source );

        /// <summary>
        ///     Makes a deep copy of this instance.
        /// </summary>
        ///
        /// <param name="source">   Another instance to copy. This cannot be null. </param>
        ///
        /// <returns>
        ///     A copy of this instance. This will never be null.
        /// </returns>
        [NotNull]
        [Pure]
        IEnumerable< T > Clone(
            [NotNull] IEnumerable< T > source );

        #endregion
    }
}