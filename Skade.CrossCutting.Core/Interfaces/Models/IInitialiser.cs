namespace Skade.CrossCutting.Core.Interfaces.Models
{
    /// <summary>
    ///     Interface for initialiser.
    /// </summary>
    public interface IInitialiser
    {
        #region Properties and Indexers

        /// <summary>
        ///     Gets a value indicating whether this instance is initialised.
        /// </summary>
        ///
        /// <value>
        ///     True if this instance is initialised, false if not.
        /// </value>
        bool IsInitialised { get; }

        #endregion

        #region Other Members

        /// <summary>
        ///     Initialises this instance.
        /// </summary>
        void Initialise();

        #endregion
    }
}