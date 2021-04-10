using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Skade.CrossCutting.Core.Interfaces.Services;

namespace Skade.CrossCutting.Common.Services.SettingServices
{
    /// <summary>
    ///     A Command Line Service.
    /// </summary>
    ///
    /// <seealso cref="ICommandLineService"/>
    public class CommandLineService : ICommandLineService
    {
        #region Properties and Indexers

        /// <inheritdoc/>
        public IReadOnlyDictionary< string, string > CommandLineSwitches => GetCommandLineSwitches();

        #endregion

        #region Other Members

        /// <summary>
        ///     Gets command line switches.
        /// </summary>
        ///
        /// <exception cref="ArgumentException">    Thrown when one or more arguments have unsupported or illegal values. </exception>
        ///
        /// <returns>
        ///     The command line switches.
        /// </returns>
        [NotNull]
        [MustUseReturnValue]
        private IReadOnlyDictionary< string, string > GetCommandLineSwitches()
        {
            var result = new Dictionary< string, string >();
            var commandLineArgs = Environment.GetCommandLineArgs();

            // Loop starts at 1 because element 0 contains the filespec of the application
            for ( var index = 1; index < commandLineArgs.Length; index++ )
            {
                var commandLineArg = commandLineArgs[ index ];
                var arg = commandLineArg.Split( '=' );

                if ( arg.Length == 2 )
                {
                    result.Add( arg[ 0 ].ToUpperInvariant(), arg[ 1 ] );
                }
            }

            return result;
        }

        #endregion
    }
}