using System;
using System.Collections;
using JetBrains.Annotations;
using Skade.CrossCutting.Core.Entities;
using Skade.CrossCutting.Core.Interfaces.Entities;
using Skade.CrossCutting.Core.Interfaces.Models;
using Skade.CrossCutting.Core.Interfaces.Services;

namespace Skade.CrossCutting.Common.Services.SettingServices
{
    /// <summary>
    ///     An Environment Variable Loader Service.
    /// </summary>
    ///
    /// <seealso cref="IEnvironmentVariableLoaderService"/>
    public class EnvironmentVariableLoaderService : IEnvironmentVariableLoaderService
    {
        #region Fields

        /// <summary>
        ///     The Environment Service.
        /// </summary>
        [NotNull] private readonly IEnvironmentService _environmentService;

        #endregion

        #region Constructors

        /// <summary>
        ///     Constructor.
        /// </summary>
        ///
        /// <exception cref="ArgumentNullException">    Thrown when one or more required arguments are null. </exception>
        ///
        /// <param name="environmentService">   The Environment Service. This cannot be null. </param>
        public EnvironmentVariableLoaderService(
            [NotNull] IEnvironmentService environmentService )
        {
            _environmentService = environmentService ?? throw new ArgumentNullException( nameof(environmentService) );
        }

        #endregion

        #region Interface Implementations

        /// <inheritdoc/>
        public ISettingsEntity Load()
        {
            var result = new SettingsEntity();
            var environmentVariables = _environmentService.EnvironmentVariables;

            LoadValues( environmentVariables, result );

            return result;
        }

        #endregion

        private void LoadValues(
            [NotNull] IDictionary environmentVariables,
            [NotNull] ISettings result )
        {
            if ( environmentVariables is null ) throw new ArgumentNullException( nameof(environmentVariables) );
            if ( result is null ) throw new ArgumentNullException( nameof(result) );

            // if ( environmentVariables.Contains( EnvironmentVariableName.DefaultProjectFolder ) )
            // {
            //     result.DefaultProjectFolder = Convert.ToString( environmentVariables[ EnvironmentVariableName.DefaultProjectFolder ], CultureInfo.InvariantCulture );
            // }
        }
    }
}