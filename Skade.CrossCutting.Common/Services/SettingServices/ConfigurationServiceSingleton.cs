using System;
using System.Diagnostics.CodeAnalysis;
using Skade.CrossCutting.Core.Constants;
using Skade.CrossCutting.Core.Interfaces.Defaults;
using Skade.CrossCutting.Core.Interfaces.Entities;
using Skade.CrossCutting.Core.Interfaces.Lookups;
using Skade.CrossCutting.Core.Interfaces.Models;
using Skade.CrossCutting.Core.Interfaces.Prototypes;
using Skade.CrossCutting.Core.Interfaces.Services;

namespace Skade.CrossCutting.Common.Services.SettingServices
{
    /// <summary>   The Configuration Service. </summary>
    /// <summary>   Default values are specified in the code. </summary>
    /// <summary>   Overrides can be specified in Environment Variables. </summary>
    /// <summary>   Further overrides can be specified in a config file. </summary>
    /// <summary>   Further overrides can be specified on the command-line. </summary>
    /// <remarks>
    ///     The settings can be specified in a cascading order:
    ///     <list>
    ///         <item>
    ///             <description>Default values are specified in the code.</description>
    ///             <description>Overrides can be specified in Environment Variables.</description>
    ///             <description>Further overrides can be specified in a config file.</description>
    ///             <description>Further overrides can be specified on the command-line.</description>
    ///         </item>
    ///     </list>
    /// </remarks>
    /// 
    /// <seealso cref="RuntimeConfigurationService"/>
    /// <seealso cref="IConfigurationService"/>
    /// <seealso cref="IInitialiser"/>
    public class ConfigurationServiceSingleton : RuntimeConfigurationService, IConfigurationService, IInitialiser
    {
        #region Fields

        /// <summary>
        ///     The Environment Variable Loader Service.
        /// </summary>
        [NotNull] private readonly IConfigurationLoaderService _environmentVariableLoaderService;

        /// <summary>
        ///     The Configuration File Loader Service.
        /// </summary>
        [NotNull] private readonly IConfigurationLoaderService _configFileLoaderService;

        /// <summary>
        ///     The Command Line Loader Service.
        /// </summary>
        [NotNull] private readonly IConfigurationLoaderService _commandLineLoaderService;

        /// <summary>
        ///     The Settings Prototype.
        /// </summary>
        [NotNull] private readonly ISettingsPrototype _settingsPrototype;

        /// <summary>
        ///     The Configuration Settings Default.
        /// </summary>
        [NotNull] private readonly IConfigurationSettingsDefault _configurationSettingsDefault;

        #endregion

        #region Properties and Indexers

        /// <inheritdoc />
        public bool IsInitialised { get; private set; }

        /// <inheritdoc cref='IServerRuntimeSettings'/>
        public IResult< string > GitBranch { get; set; }

        /// <inheritdoc cref='IServerRuntimeSettings'/>
        public IResult< string > GitHash { get; set; }

        /// <inheritdoc />
        public IProjectSerializerEntity LastProjectEntity { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        ///     Constructor.
        /// </summary>
        ///
        /// <exception cref="ArgumentNullException">    Thrown when one or more required arguments are null. </exception>
        ///
        /// <param name="environmentVariableLoaderService"> The Environment Variable Loader Service. This cannot be null. </param>
        /// <param name="configFileLoaderService">          The Configuration File Loader Service. This cannot be null. </param>
        /// <param name="commandLineLoaderService">         The Command Line Loader Service. This cannot be null. </param>
        /// <param name="settingsPrototype">                The Settings Prototype. This cannot be null. </param>
        /// <param name="configurationSettingsDefault">     The Configuration Settings Default. This cannot be null. </param>
        [SuppressMessage( "ReSharper", "NotNullMemberIsNotInitialized" )]
        public ConfigurationServiceSingleton(
            [NotNull] IEnvironmentVariableLoaderService environmentVariableLoaderService,
            [NotNull] IConfigFileLoaderService configFileLoaderService,
            [NotNull] ICommandLineLoaderService commandLineLoaderService,
            [NotNull] ISettingsPrototype settingsPrototype,
            [NotNull] IConfigurationSettingsDefault configurationSettingsDefault )
        {
            _environmentVariableLoaderService = environmentVariableLoaderService ?? throw new ArgumentNullException( nameof(environmentVariableLoaderService) );
            _configFileLoaderService = configFileLoaderService ?? throw new ArgumentNullException( nameof(configFileLoaderService) );
            _commandLineLoaderService = commandLineLoaderService ?? throw new ArgumentNullException( nameof(commandLineLoaderService) );
            _settingsPrototype = settingsPrototype ?? throw new ArgumentNullException( nameof(settingsPrototype) );
            _configurationSettingsDefault = configurationSettingsDefault ?? throw new ArgumentNullException( nameof(configurationSettingsDefault) );
        }

        #endregion

        #region Interface Implementations

        /// <inheritdoc />
        public void SaveValuesInConfigFile()
        {
            ( (IConfigurationSavingService) _configFileLoaderService ).Save( this );
        }

        /// <inheritdoc cref="IInitialiser" />
        public void Initialise()
        {
            if ( IsInitialised )
            {
                throw new InvalidOperationException( DevConstants.InstanceIsAlreadyInitialised );
            }

            AssignDefaultValues();
            AssignValuesFromConfigFile();
            AssignValuesFromEnvironmentVariables();
            AssignValuesFromCommandLine();

            IsInitialised = true;
        }

        #endregion

        #region Other Members

        /// <summary>
        ///     Assign default values.
        /// </summary>
        private void AssignDefaultValues()
        {
            _configurationSettingsDefault.Assign( this );
        }

        /// <summary>
        ///     Assign values from Environment Variables.
        /// </summary>
        private void AssignValuesFromEnvironmentVariables()
        {
            var settings = _environmentVariableLoaderService.Load();

            _settingsPrototype.Assign( settings, this );
        }

        /// <summary>
        ///     Assign values from Configuration File.
        /// </summary>
        private void AssignValuesFromConfigFile()
        {
            var settings = _configFileLoaderService.Load();

            _settingsPrototype.Assign( settings, this );
        }

        /// <summary>
        ///     Assign values from Command Line.
        /// </summary>
        private void AssignValuesFromCommandLine()
        {
            var settings = _commandLineLoaderService.Load();

            _settingsPrototype.Assign( settings, this );
        }

        #endregion
    }
}