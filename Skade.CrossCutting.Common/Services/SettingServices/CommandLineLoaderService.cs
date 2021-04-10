using System;
using JetBrains.Annotations;
using Skade.CrossCutting.Core.Entities;
using Skade.CrossCutting.Core.Interfaces.Entities;
using Skade.CrossCutting.Core.Interfaces.Services;

namespace Skade.CrossCutting.Common.Services.SettingServices
{
    /// <summary>
    ///     A Command Line Loader Service.
    /// </summary>
    ///
    /// <seealso cref="ICommandLineLoaderService"/>
    public class CommandLineLoaderService : ICommandLineLoaderService
    {
        #region Fields

        /// <summary>
        ///     The Command-line Service.
        /// </summary>
        [NotNull] private readonly ICommandLineService _commandLineService;

        #endregion

        #region Constructors

        /// <summary>
        ///     Constructor.
        /// </summary>
        ///
        /// <exception cref="ArgumentNullException">    Thrown when one or more required arguments are null. </exception>
        ///
        /// <param name="commandLineService">   The Command-line Service. This cannot be null. </param>
        public CommandLineLoaderService(
            [NotNull] ICommandLineService commandLineService )
        {
            _commandLineService = commandLineService ?? throw new ArgumentNullException( nameof(commandLineService) );
        }

        #endregion

        #region Interface Implementations

        /// <inheritdoc/>
        public ISettingsEntity Load()
        {
            var result = new SettingsEntity();
            var commandLineArgs = _commandLineService.CommandLineSwitches;

            return result;
        }

        #endregion
    }
}