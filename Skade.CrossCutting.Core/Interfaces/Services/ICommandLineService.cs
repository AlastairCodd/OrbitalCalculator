using System.Collections.Generic;
using JetBrains.Annotations;

namespace Skade.CrossCutting.Core.Interfaces.Services
{
    /// <summary>
    ///     Interface for Command Line Service.
    /// </summary>
    public interface ICommandLineService
    {
        #region Properties and Indexers

        /// <summary>
        ///     Gets the Command Line Switches.
        /// </summary>
        /// <value>
        ///     The command line switches.
        /// </value>
        [NotNull]
        IReadOnlyDictionary< string, string > CommandLineSwitches { get; }

        #endregion
    }
}