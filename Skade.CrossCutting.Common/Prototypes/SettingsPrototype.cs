using System.Diagnostics;
using JetBrains.Annotations;
using Skade.CrossCutting.Core.Entities;
using Skade.CrossCutting.Core.Interfaces.Models;
using Skade.CrossCutting.Core.Interfaces.Prototypes;
using Skade.CrossCutting.Core.Lookups;

namespace Skade.CrossCutting.Common.Prototypes
{
    /// <summary>
    ///     The settings prototype.
    /// </summary>
    ///
    /// <seealso cref="BaseImmutablePrototype{ISettings}"/>
    /// <seealso cref="ISettingsPrototype"/>
    public class SettingsPrototype : BaseImmutablePrototype<ISettings>, ISettingsPrototype
    {
        /// <inheritdoc />
        public override ISettings Clone(
            ISettings source )
        {
            Debug.Assert( !( source is null ), nameof(source) );

            var target = new SettingsEntity();
            Assign( source, target );

            return target;
        }

        /// <inheritdoc />
        public void Assign(
            ISettings source,
            ISettings target )
        {
            Debug.Assert( !( source is null ), nameof(source) );
            Debug.Assert( !( target is null ), nameof(target) );

            target.GitBranch = new ResultLookup< string >( source.GitBranch );
            target.GitHash = new ResultLookup< string >( source.GitHash );

            source.LastProjectEntity = target.LastProjectEntity;
        }
    }
}