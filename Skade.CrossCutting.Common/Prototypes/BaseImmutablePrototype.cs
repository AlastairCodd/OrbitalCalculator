using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using JetBrains.Annotations;
using Skade.CrossCutting.Core.Interfaces.Prototypes;

namespace Skade.CrossCutting.Common.Prototypes
{
    /// <summary>
    ///     A base immutable prototype.
    /// </summary>
    ///
    /// <typeparam name="T">    Generic type parameter. </typeparam>
    ///
    /// <seealso cref="IImmutablePrototype{T}"/>
    [ExcludeFromCodeCoverage]
    [UsedImplicitly]
    public abstract class BaseImmutablePrototype<T> : IImmutablePrototype<T>
    {
        /// <inheritdoc />
        public abstract T Clone(
            T source );

        /// <inheritdoc />
        public virtual IEnumerable< T > Clone(
            IEnumerable< T > source )
        {
            Debug.Assert( !( source is null ), nameof(source) );

            var result = source.Select( Clone ).ToList();
            return result;
        }
    }
}