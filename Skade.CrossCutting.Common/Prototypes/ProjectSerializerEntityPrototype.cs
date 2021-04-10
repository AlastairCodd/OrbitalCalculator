using System.Diagnostics;
using Skade.CrossCutting.Core.Entities;
using Skade.CrossCutting.Core.Interfaces.Entities;
using Skade.CrossCutting.Core.Interfaces.Prototypes;

namespace Skade.CrossCutting.Common.Prototypes
{
    /// <summary>
    ///     A project serializer entity prototype.
    /// </summary>
    ///
    /// <seealso cref="BaseImmutablePrototype{IProjectSerializerEntity}"/>
    /// <seealso cref="IProjectSerializerEntityPrototype"/>
    public class ProjectSerializerEntityPrototype : BaseImmutablePrototype< IProjectSerializerEntity >, IProjectSerializerEntityPrototype
    {
        #region Interface Implementations

        /// <inheritdoc />
        public override IProjectSerializerEntity Clone(
            IProjectSerializerEntity source )
        {
            Debug.Assert( !( source is null ), nameof(source) );
            var target = new ProjectSerializerEntity();
            Assign( source, target );

            return target;
        }

        /// <inheritdoc />
        public void Assign(
            IProjectSerializerEntity source,
            IProjectSerializerEntity target )
        {
            Debug.Assert( !( source is null ), nameof(source) );
            Debug.Assert( !( target is null ), nameof(target) );
        }

        #endregion
    }
}