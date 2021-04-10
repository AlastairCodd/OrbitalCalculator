namespace Skade.CrossCutting.Core.Interfaces.Lookups
{
    /// <summary>
    ///     Interface for result.
    /// </summary>
    ///
    /// <typeparam name="T">    Generic type parameter. </typeparam>
    public interface IResult< out T >
    {
        #region Properties and Indexers

        /// <summary>
        ///     Gets the value for this result.
        /// </summary>
        /// <value>
        ///     The value.
        /// </value>
        T Value { get; }

        /// <summary>
        ///     Gets a value indicating whether the flag has been set.
        /// </summary>
        /// <value>
        ///     True if flag set, false if not.
        /// </value>
        bool Flag { get; }

        #endregion
    }
}