using System;
using JetBrains.Annotations;
using Skade.CrossCutting.Core.Entities;
using Skade.CrossCutting.Core.Interfaces.Entities;
using Skade.CrossCutting.Core.Interfaces.Models;
using Skade.CrossCutting.Core.Interfaces.Services;

namespace Skade.CrossCutting.Common.Services.SettingServices
{
    /// <summary>
    ///     A Configuration File Loader Service.
    /// </summary>
    ///
    /// <seealso cref="IConfigFileLoaderService"/>
    public class ConfigFileLoaderService : IConfigFileLoaderService
    {
        #region Fields

        /// <summary>
        ///     The Settings Service.
        /// </summary>
        [NotNull] private readonly ISettingsService _settingsService;

        #endregion

        #region Constructors

        /// <summary>
        ///     Constructor.
        /// </summary>
        ///
        /// <exception cref="ArgumentNullException">    Thrown when one or more required arguments are null. </exception>
        ///
        /// <param name="settingsService">  The settings service. </param>
        public ConfigFileLoaderService(
            [NotNull] ISettingsService settingsService )
        {
            _settingsService = settingsService ?? throw new ArgumentNullException( nameof(settingsService) );
        }

        #endregion

        #region Interface Implementations

        /// <inheritdoc/>
        public ISettingsEntity Load()
        {
            var result = new SettingsEntity();

            LoadProjectValues( result );

            return result;
        }

        /// <inheritdoc/>
        public void Save(
            ISettings settings )
        {
            if ( settings is null ) throw new ArgumentNullException( nameof(settings) );

            _settingsService.SaveLastProjectEntity( settings.LastProjectEntity );
        }

        #endregion

        #region Other Members

        /// <summary>
        ///     Loads Project values.
        /// </summary>
        ///
        /// <exception cref="ArgumentNullException">    Thrown when one or more required arguments are null. </exception>
        ///
        /// <param name="result">   The result. </param>
        private void LoadProjectValues(
            [NotNull] ISettings result )
        {
            if ( result is null ) throw new ArgumentNullException( nameof(result) );

            if ( _settingsService.TryGetValue( "LastProjectEntity", out var value ) )
            {
                result.LastProjectEntity = (ProjectSerializerEntity) value;
            }
        }

        #endregion
    }
}