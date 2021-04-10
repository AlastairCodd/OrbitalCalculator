using JetBrains.Annotations;

namespace Skade.CrossCutting.Core.Interfaces.Defaults
{
    /// <summary>
    ///     Interface for Default value assigner.
    /// </summary>
    /// <typeparam name="T">    Generic type parameter. </typeparam>
    public interface IDefault< in T > where T : class
    {
        #region Other Members

        /// <summary>
        ///     Assigns default values to the given source.
        /// </summary>
        /// <param name="source">   Source for the assignment. </param>
        void Assign(
            [NotNull] T source );

        #endregion
    }
}