using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Windows;
using Ninject;
using Ninject.Extensions.Conventions;
using Ninject.Extensions.Factory;
using Serilog;
using Skade.CrossCutting.Common.Services;
using Skade.CrossCutting.Common.Services.SettingServices;
using Skade.CrossCutting.Common.Validators;
using Skade.CrossCutting.Core.Interfaces.Factories;
using Skade.CrossCutting.Core.Interfaces.Models;
using Skade.CrossCutting.Core.Interfaces.Services;
using Skade.CrossCutting.Core.Interfaces.Validators;
using Skade.CrossCutting.Core.Interfaces.ViewModels;
using Skade.CrossCutting.Localiser.Common.Entities;
using Skade.CrossCutting.Localiser.Common.Services;
using Skade.CrossCutting.Localiser.Core.Interfaces.Factories;
using Skade.CrossCutting.Localiser.Core.Interfaces.Services;
using Skade.CrossCutting.Messenger.Common.Services;
using Skade.CrossCutting.Messenger.Core.Enumerations;
using Skade.CrossCutting.Messenger.Core.Interfaces.Services;
using Skade.CrossCutting.Resources;
using Skade.OrbitCalculator.Shell.Services;
using Skade.OrbitCalculator.Shell.Views;
using Skade.OrbitCalculator.ViewModels.ViewModels;
using TinyMessenger;

namespace Skade.OrbitCalculator.Shell.Infrastructure
{
    /// <summary>
    ///     The bootstrapper.
    /// </summary>
    ///
    /// <seealso cref="IDisposable"/>
    public class Bootstrapper : IDisposable
    {
        /// <summary>
        ///     The assembly prefix.
        /// </summary>
        private const string AssemblyPrefix = "Skade";

        /// <summary>
        ///     The kernel.
        /// </summary>
        [NotNull] private readonly IKernel _kernel;

        /// <summary>
        ///     The shell view model.
        /// </summary>
        [NotNull] private IShellViewModel _shellViewModel;

        /// <summary>
        ///     Pathname of the log folder.
        /// </summary>
        [NotNull] private readonly string _logFolder;

        #region Constructor

        /// <summary>
        ///     Default constructor.
        /// </summary>
        public Bootstrapper()
        {
            _logFolder = Path.Combine( ApplicationInfoService.ApplicationDataFolder, "logs" );
            var logFileTemplate = Path.Combine( _logFolder, "abd-gtc-.log" );
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
//                .WriteTo.Async( x => x.Trace( LogEventLevel.Verbose ) )
                .WriteTo.Async(
                    x => x.File(
                        logFileTemplate,
                        rollingInterval: RollingInterval.Day,
                        restrictedToMinimumLevel: Settings.Default.LogEventLevelSetting ) )
                .CreateLogger();

            _kernel = new StandardKernel();

            _kernel.Bind(
                x => x
                    .FromAssemblyContaining< IMessageFactory >()
                    .SelectAllInterfaces()
                    .EndingWith( "Factory" )
                    .BindToFactory()
            );

            _kernel.Bind< ILocalisationMessageFactory >()
                .ToFactory();

            _kernel
                .Bind< ITinyMessengerHub >()
                .To< TinyMessengerHub >()
                .InSingletonScope();

            _kernel
                .Bind< IMessengerService >()
                .To< MessengerServiceSingleton >()
                .InSingletonScope();

            IDictionary< string, int > resourceDictionary = new Dictionary< string, int >( 1 )
            {
                {$"{Assembly.GetAssembly( typeof( UserInterfaceRes ) )?.GetName().Name}:UserInterfaceRes", 0},
            };

            _kernel
                .Bind< ILocalisationService >()
                .To< LocalisationServiceSingleton >()
                .InSingletonScope()
                .WithConstructorArgument( "resourceFiles", resourceDictionary );

            var culture = new CultureInfo( "en-GB" );
            CultureInfo.CurrentUICulture = culture;
            CultureInfo.CurrentCulture = culture;

            _kernel
                .Bind< IConfigurationService >()
                .To< ConfigurationServiceSingleton >()
                .InSingletonScope();

            _kernel
                .Bind< ILogger >()
                .ToConstant( Log.Logger );

            ConfigureKernel();

            ( (IInitialiser) _kernel.Get< IConfigurationService >() ).Initialise();
            ( (IInitialiser) _kernel.Get< ILocalisationService >() ).Initialise();

            Log.Information( "*********************************************" );
            Log.Information( "***                                       ***" );
            Log.Information( "*** Starting instance of Orbit Calculator ***" );
            Log.Information( "***                                       ***" );
            Log.Information( "*********************************************" );
        }

        #endregion

        #region Interface Implementations

        /// <inheritdoc />
        public void Dispose()
        {
            Dispose( true );
            GC.SuppressFinalize( this );
        }

        #endregion

        #region Other Members

        /// <summary>
        ///     Finaliser.
        /// </summary>
        ~Bootstrapper()
        {
            Dispose( false );
        }

        /// <summary>
        ///     Runs the application.
        /// </summary>
        ///
        /// <exception cref="ArgumentNullException">    Thrown when one or more required arguments are null. </exception>
        ///
        /// <param name="app">  The application. </param>
        public void Run(
            [NotNull] Application app )
        {
            if ( app is null ) throw new ArgumentNullException( nameof(app) );

            var configurationService = _kernel.Get< IConfigurationService >();

            // The logging service is used indirectly - it sits in the background
            // listening for messages and forwarding them to the SeriLog logger.
            _kernel.Get< ILoggingService >();

            AssignBuildInformation();

            _shellViewModel = _kernel.Get< IShellViewModel >();
            var shellView = _kernel.Get< ShellView >();
            shellView.DataContext = _shellViewModel;

            app.MainWindow = shellView;
            app.MainWindow.Show();

            var osBitType = Environment.Is64BitOperatingSystem ? "64 bit" : "32 bit";
            var processBitType = Environment.Is64BitProcess ? "64 bit" : "32 bit";

            Log.Information( $"{ApplicationInfoService.ProductName} v{ApplicationInfoService.Version}", MessageLevelEnum.Information );
            Log.Information( $"{Environment.OSVersion} ({osBitType})", MessageLevelEnum.Information );
            Log.Information( $"Running in {processBitType} process.", MessageLevelEnum.Information );
            Log.Information( $"Application Path    = {ApplicationInfoService.ApplicationPath}", MessageLevelEnum.Information );
            Log.Information( $"Client Machine Name = {Environment.MachineName}", MessageLevelEnum.Information );
            Log.Information( $"User Name           = {Environment.UserName}", MessageLevelEnum.Information );
            Log.Information( $"User Domain Name    = {Environment.UserDomainName}", MessageLevelEnum.Information );
            Log.Information( $"Processors          = {Environment.ProcessorCount.ToString()}", MessageLevelEnum.Information );
            Log.Information( $"Context Memory      = {Environment.WorkingSet.ToString( "N0" )} bytes", MessageLevelEnum.Information );
            Log.Information( $"Log files           = {_logFolder}", MessageLevelEnum.Information );
            Log.Information( $"Git Branch          = {configurationService.GitBranch.Value}", MessageLevelEnum.Information );
            Log.Information( $"Git Hash            = {configurationService.GitHash.Value}", MessageLevelEnum.Information );
        }

        /// <summary>
        ///     Releases the unmanaged resources used by the ABD.DBS.Shell.Bootstrapper and optionally releases the managed resources.
        /// </summary>
        ///
        /// <param name="disposing">    True to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
        private void Dispose(
            bool disposing )
        {
            ( _shellViewModel as IDisposable )?.Dispose();

            if ( disposing )
            {
                _kernel.Dispose();
            }
        }

        /// <summary>
        ///     Assign build information.
        /// </summary>
        private void AssignBuildInformation()
        {
            var configService = _kernel.Get< IConfigurationService >();
            configService.GitBranch = ApplicationInfoService.GitBranch;
            configService.GitHash = ApplicationInfoService.GitHash;
        }

        /// <summary>
        ///     Configure the kernel.
        /// </summary>
        private void ConfigureKernel()
        {
            var assemblyExample = new[]
            {
                typeof( EnvironmentVariableLoaderService ),
                typeof( LanguageLookup ),
                typeof( SettingsServiceSingleton ),
                typeof( ShellViewModel )
            };

            var transientDefaultClasses = new[]
            {
                "Builder",
                "Facade",
                "Forge",
                "Formatter",
                "Handler",
                "Message",
                "Parser",
                "Service",
                "View",
                "ViewModel",
            };

            var transientCompleteClasses = new[]
            {
                "Entity",
                "EqualityComparer",
                "Default",
                "Lookup",
                "Prototype",
                "Repository",
                "Transformer",
                "Validator",
            };

            foreach ( var type in assemblyExample )
            {
                foreach ( var ending in transientDefaultClasses )
                {
                    _kernel.Bind(
                        x => x
                            .FromAssemblyContaining( type )
                            .SelectAllClasses()
                            .EndingWith( ending )
                            .BindAllInterfaces()
                            .Configure( b => b.InTransientScope() ) );
                }

                foreach ( var ending in transientCompleteClasses )
                {
                    _kernel.Bind(
                        x => x
                            .FromAssemblyContaining( type )
                            .SelectAllClasses()
                            .EndingWith( ending )
                            .BindAllInterfaces()
                            .Configure( b => b.InTransientScope() ) );
                }

                _kernel.Bind(
                    x => x
                        .FromAssemblyContaining( type )
                        .SelectAllClasses()
                        .EndingWith( "Singleton" )
                        .Excluding< MessengerServiceSingleton >()
                        .Excluding< LocalisationServiceSingleton >()
                        .Excluding< ConfigurationServiceSingleton >()
                        .Excluding< TraceServiceSingleton >()
                        .BindAllInterfaces()
                        .Configure( b => { b.InSingletonScope(); } ) );
            }

            _kernel
                .Bind( typeof( INullValidator<> ) )
                .To( typeof( NullValidator<> ) );
        }

        #endregion
    }
}