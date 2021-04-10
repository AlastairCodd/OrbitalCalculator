using JetBrains.Annotations;

namespace Skade.CrossCutting.Core.Interfaces.Prototypes
{
    /// <summary>
    ///     Interface for prototype.
    /// </summary>
    ///
    /// <typeparam name="T">    Generic type parameter. </typeparam>
    public interface IPrototype< T > : IImmutablePrototype< T >
    {
        #region Other Members

        /// <summary>
        ///     Writes the values from the source to the target.
        /// </summary>
        ///
        /// <param name="source">   Source for the values. This cannot be null. </param>
        /// <param name="target">   Target for the values. This cannot be null. </param>
        void Assign(
            [NotNull] T source,
            [NotNull] T target );

        #endregion
    }
}