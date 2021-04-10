using JetBrains.Annotations;

namespace Skade.CrossCutting.Core.Interfaces.Lookups
{
    /// <summary>
    ///     Interface for lookup.
    /// </summary>
    ///
    /// <typeparam name="T">    Generic type parameter. </typeparam>
    public interface ILookup< out T >
    {
        #region Properties and Indexers

        /// <summary>
        ///     Gets the identifier.
        /// </summary>
        ///
        /// <value>
        ///     The identifier.
        /// </value>
        [NotNull]
        T Id { get; }

        #endregion
    }
}