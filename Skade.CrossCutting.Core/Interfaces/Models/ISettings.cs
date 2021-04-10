using JetBrains.Annotations;
using Skade.CrossCutting.Core.Interfaces.Entities;

namespace Skade.CrossCutting.Core.Interfaces.Models
{
    /// <summary>
    ///     Interface for settings.
    /// </summary>
    public interface ISettings : IServerRuntimeSettings
    {
        #region Properties and Indexers

        /// <summary>
        ///     Gets or sets the last project entity.
        /// </summary>
        ///
        /// <value>
        ///     The last project entity. This will never be null.
        /// </value>
        [NotNull]
        IProjectSerializerEntity LastProjectEntity { get; set; }

        #endregion
    }
}