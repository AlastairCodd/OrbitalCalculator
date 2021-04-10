using JetBrains.Annotations;
using Skade.CrossCutting.Core.Interfaces.Lookups;

namespace Skade.CrossCutting.Core.Interfaces.Models
{
    /// <summary>
    ///     Interface for server runtime settings.
    /// </summary>
    public interface IServerRuntimeSettings  {
        
        /// <inheritdoc cref='IServerRuntimeSettings'/>
        [NotNull]
        IResult< string > GitBranch { get; set; }

        /// <inheritdoc cref='IServerRuntimeSettings'/>
        [NotNull]
        IResult< string > GitHash { get; set; }
    }
}