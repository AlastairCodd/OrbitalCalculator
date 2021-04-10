using System;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Skade.CrossCutting.Core.Interfaces.Services;

namespace Skade.CrossCutting.Common.Services.SettingServices
{
    /// <summary>
    ///     An Environment Service.
    /// </summary>
    ///
    /// <seealso cref="IEnvironmentService"/>
    [ExcludeFromCodeCoverage]
    [UsedImplicitly]
    public class EnvironmentServiceSingleton : IEnvironmentService
    {
        #region Properties and Indexers

        /// <inheritdoc/>
        public string UserName
        {
            get
            {
                var result = Environment.UserName;
                return result;
            }
        }

        /// <inheritdoc/>
        public IDictionary EnvironmentVariables
        {
            get
            {
                var result = Environment.GetEnvironmentVariables();
                return result;
            }
        }

        #endregion
    }
}