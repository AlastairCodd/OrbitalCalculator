using System.Collections.Concurrent;
using System.Collections.Generic;
using JetBrains.Annotations;
using Skade.CrossCutting.Core.Interfaces.Services;

namespace Skade.CrossCutting.Common.Services.SettingServices
{
    /// <summary>
    ///     A runtime configuration service.
    /// </summary>
    ///
    /// <seealso cref="IRuntimeConfigurationService"/>
    public abstract class RuntimeConfigurationService : IRuntimeConfigurationService
    {
        #region Fields

        /// <summary>
        ///     The runtime configurations.
        /// </summary>
        [NotNull] private readonly IDictionary< string, object > _runtimeConfigurations;

        #endregion Fields

        #region Constructors

        /// <summary>
        ///     Default constructor.
        /// </summary>
        protected RuntimeConfigurationService()
        {
            _runtimeConfigurations = new ConcurrentDictionary< string, object >();
        }

        #endregion Constructors

        #region Interface Implementations

        /// <inheritdoc/>
        public virtual object GetRuntimeConfiguration(
            string key )
        {
            _runtimeConfigurations.TryGetValue( key, out var returnValue );

            return returnValue;
        }

        /// <inheritdoc/>
        public virtual void SetRuntimeConfiguration(
            string key,
            object value )
        {
            if ( _runtimeConfigurations.ContainsKey( key ) )
            {
                _runtimeConfigurations[ key ] = value;
            }
            else
            {
                _runtimeConfigurations.Add( key, value );
            }
        }

        #endregion Interface Implementations
    }
}