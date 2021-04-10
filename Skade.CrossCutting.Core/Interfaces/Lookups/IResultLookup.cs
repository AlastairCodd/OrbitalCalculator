using System.Collections.Generic;
using JetBrains.Annotations;

namespace Skade.CrossCutting.Core.Interfaces.Lookups
{
    /// <summary>
    ///     Interface for obtaining a result.
    /// </summary>
    /// <typeparam name="T">    Generic type parameter. </typeparam>
    public interface IResultLookup< out T > : IResult<T>
    {
        #region Properties and Indexers

        /// <summary>
        ///     Gets the Errors.
        /// </summary>
        /// <value>
        ///     The errors.
        /// </value>
        [NotNull]
        [ItemNotNull]
        IReadOnlyCollection< string > Errors { get; }

        #endregion
    }
}