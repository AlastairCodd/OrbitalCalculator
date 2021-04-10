using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using JetBrains.Annotations;
using Skade.CrossCutting.Core.Interfaces.Lookups;

namespace Skade.CrossCutting.Core.Lookups
{
    /// <summary>
    ///     A Result Lookup.
    /// </summary>
    /// <typeparam name="T">    Generic type parameter. </typeparam>
    /// <seealso cref="IResultLookup{T}"/>
    [DebuggerDisplay( "Flag: {Flag}  Value: {Value}  Errors: {Errors.Count}" )]
    [UsedImplicitly]
    public class ResultLookup< T > : IResultLookup< T >
    {
        #region Properties and Indexers

        /// <inheritdoc/>
        [CanBeNull]
        public T Value { get; }

        /// <inheritdoc/>
        public bool Flag { get; }

        /// <inheritdoc/>
        public IReadOnlyCollection< string > Errors { get; }

        #endregion

        #region Constructors

        /// <summary>
        ///     Constructor.
        /// </summary>
        ///
        /// <param name="flag">     True if is valid, false if not. </param>
        /// <param name="value">    The value. This may be null. </param>
        /// <param name="errors">   (Optional) The errors. This may be null. </param>
        public ResultLookup(
            bool flag,
            [CanBeNull] T value,
            [CanBeNull] [ItemCanBeNull] ICollection< string > errors = null )
        {
            Flag = flag;
            Value = value;

            if ( !( errors is null ) && errors.Count > 0 && !errors.All( string.IsNullOrWhiteSpace ) )
            {
                Errors = new ReadOnlyCollection< string >(
                    errors.Where( s => !string.IsNullOrWhiteSpace( s ) )
                        .ToArray() );
            }
            else
            {
                Errors = new ReadOnlyCollection< string >( Array.Empty< string >() );
            }
        }

        /// <summary>
        ///     Constructor.
        /// </summary>
        ///
        /// <param name="flag">     True if is valid, false if not. </param>
        /// <param name="value">    The value. This may be null. </param>
        /// <param name="error">    (Optional) The error. This may be null. </param>
        public ResultLookup(
            bool flag,
            [CanBeNull] T value,
            [CanBeNull] string error = null ) : this( flag, value, new[] {error} ) { }

        /// <summary>
        ///     Constructor.
        /// </summary>
        ///
        /// <param name="flag">     True if is valid, false if not. </param>
        /// <param name="value">    The value. This may be null. </param>
        // ReSharper disable once RedundantOverload.Global
        public ResultLookup(
            bool flag,
            [CanBeNull] T value ) : this( flag, value, errors: null ) { }

        /// <summary>
        ///     Constructor.
        /// </summary>
        ///
        /// <param name="errors">   The errors. This may not be null. </param>
        public ResultLookup(
            [NotNull] [ItemNotNull] ICollection< string > errors ) : this( false, default, errors ) { }

        /// <summary>
        ///     Constructor.
        /// </summary>
        ///
        /// <param name="error">    The error. This may not be null. </param>
        public ResultLookup(
            [NotNull] string error ) : this( false, default, error ) { }

        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <param name="flag">     True if is valid, false if not. </param>
        /// <param name="errors">   (Optional) The errors. This may be null. </param>
        public ResultLookup(
            bool flag,
            [ItemCanBeNull] ICollection< string > errors = null ) : this( flag, default, errors ) { }

        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <param name="flag">     True if is valid, false if not. </param>
        /// <param name="errors">   (Optional) The errors. This may be null. </param>
        public ResultLookup(
            bool flag,
            [ItemCanBeNull] IReadOnlyCollection< string > errors = null ) : this( flag, default, (ICollection< string >) errors ) { }

        /// <summary>
        ///     Constructor.
        /// </summary>
        ///
        /// <param name="resultLookup"> The result lookup. This cannot be null. </param>
        public ResultLookup(
            [NotNull] IResultLookup< T > resultLookup ) : this( resultLookup.Flag, resultLookup.Value, (ICollection< string >) resultLookup.Errors ) { }

        /// <summary>
        ///     Constructor.
        /// </summary>
        ///
        /// <param name="result">   The result. This cannot be null. </param>
        public ResultLookup(
            [NotNull] IResult< T > result ) : this( result.Flag, result.Value, errors: null ) { }

        #endregion
    }
}