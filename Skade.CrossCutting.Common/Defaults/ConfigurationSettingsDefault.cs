using System.Diagnostics;
using Skade.CrossCutting.Core.Interfaces.Defaults;
using Skade.CrossCutting.Core.Interfaces.Models;

namespace Skade.CrossCutting.Common.Defaults
{
    /// <summary>
    ///     A configuration settings default.
    /// </summary>
    ///
    /// <seealso cref="IConfigurationSettingsDefault"/>
    public class ConfigurationSettingsDefault : IConfigurationSettingsDefault
    {
        /// <inheritdoc />
        public void Assign(
            ISettings source )
        {
            Debug.Assert( !( source is null ), nameof(source) );
        }
    }
}