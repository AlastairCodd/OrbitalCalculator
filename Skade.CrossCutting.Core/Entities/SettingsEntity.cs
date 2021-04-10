using Skade.CrossCutting.Core.Constants;
using Skade.CrossCutting.Core.Interfaces.Entities;
using Skade.CrossCutting.Core.Interfaces.Lookups;
using Skade.CrossCutting.Core.Lookups;

namespace Skade.CrossCutting.Core.Entities
{
    /// <summary>
    ///     The settings entity.
    /// </summary>
    ///
    /// <seealso cref="ISettingsEntity"/>
    public class SettingsEntity : ISettingsEntity
    {
        #region Properties and Indexers

        /// <inheritdoc />
        public IResult< string > GitBranch { get; set; }

        /// <inheritdoc />
        public IResult< string > GitHash { get; set; }

        /// <inheritdoc />
        public IProjectSerializerEntity LastProjectEntity { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        ///     Default constructor.
        /// </summary>
        public SettingsEntity()
        {
            GitBranch = new ResultLookup< string >( DevConstants.DevError );
            GitHash = new ResultLookup< string >( DevConstants.DevError );

            LastProjectEntity = new ProjectSerializerEntity();
        }

        #endregion
    }
}