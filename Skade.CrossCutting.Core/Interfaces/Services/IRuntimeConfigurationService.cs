using JetBrains.Annotations;

namespace Skade.CrossCutting.Core.Interfaces.Services
{
    /// <summary>
    ///     Interface for runtime configuration service.
    /// </summary>
    public interface IRuntimeConfigurationService
    {
        #region Other Members

        /// <summary>
        ///     Gets runtime configuration associated with the supplied key.
        /// </summary>
        ///
        /// <param name="key">  The key. </param>
        ///
        /// <returns>
        ///     The runtime configuration. This may be null.
        /// </returns>
        [CanBeNull]
        [MustUseReturnValue]
        object GetRuntimeConfiguration(
            [NotNull] string key );

        /// <summary>
        ///     Sets runtime configuration stored against the supplied key (if it does not yet exist it is created).
        /// </summary>
        ///
        /// <param name="key">      The key. This cannot be null. </param>
        /// <param name="value">    The value. This may be null. </param>
        void SetRuntimeConfiguration(
            [NotNull] string key,
            [CanBeNull] object value );

        #endregion
    }
}