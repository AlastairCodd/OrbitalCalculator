using JetBrains.Annotations;
using Skade.CrossCutting.Core.Interfaces.Entities;

namespace Skade.CrossCutting.Core.Interfaces.Services
{
    /// <summary>
    ///     Interface for Configuration Loader Service.
    /// </summary>
    public interface IConfigurationLoaderService
    {
        #region Other Members

        /// <summary>
        ///     Loads the given Configuration Service.
        /// </summary>
        /// <returns>
        ///     An ISettingsEntity.
        /// </returns>
        [NotNull]
        [Pure]
        ISettingsEntity Load();

        #endregion
    }
}