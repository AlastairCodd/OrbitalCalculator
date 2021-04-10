using JetBrains.Annotations;
using Skade.CrossCutting.Core.Interfaces.Models;

namespace Skade.CrossCutting.Core.Interfaces.Services
{
    /// <summary>
    ///     Interface for configuration saving service.
    /// </summary>
    public interface IConfigurationSavingService
    {
        #region Other Members

        /// <summary>
        ///     Saves the given settings.
        /// </summary>
        ///
        /// <param name="settings"> The settings to save. This cannot be null. </param>
        void Save(
            [NotNull] ISettings settings );

        #endregion
    }
}