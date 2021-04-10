using Skade.CrossCutting.Core.Interfaces.Models;

namespace Skade.CrossCutting.Core.Interfaces.Services
{
    /// <summary>
    ///     Interface for configuration service.
    /// </summary>
    public interface IConfigurationService : ISettings
    {
        #region Other Members

        /// <summary>
        ///     Saves the values in configuration file.
        /// </summary>
        void SaveValuesInConfigFile();

        #endregion
    }
}