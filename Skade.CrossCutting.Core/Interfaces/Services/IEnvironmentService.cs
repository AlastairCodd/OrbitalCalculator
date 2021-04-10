using System.Collections;
using JetBrains.Annotations;

namespace Skade.CrossCutting.Core.Interfaces.Services
{
    /// <summary>
    ///     Interface for environment service.
    /// </summary>
    public interface IEnvironmentService
    {
        #region Properties and Indexers

        /// <summary>
        ///     Gets the name of the user.
        /// </summary>
        ///
        /// <value>
        ///     The name of the user. This will never be null.
        /// </value>
        [NotNull]
        string UserName { get; }

        /// <summary>
        ///     Gets the environment variables.
        /// </summary>
        ///
        /// <value>
        ///     The environment variables. This will never be null.
        /// </value>
        [NotNull]
        IDictionary EnvironmentVariables { get; }

        #endregion
    }
}