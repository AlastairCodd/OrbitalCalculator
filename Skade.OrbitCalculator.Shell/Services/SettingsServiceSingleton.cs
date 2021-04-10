using System;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Skade.CrossCutting.Core.Constants;
using Skade.CrossCutting.Core.Entities;
using Skade.CrossCutting.Core.Interfaces.Entities;
using Skade.CrossCutting.Core.Interfaces.Prototypes;
using Skade.CrossCutting.Core.Interfaces.Services;
using NotNull = JetBrains.Annotations.NotNullAttribute;

namespace Skade.OrbitCalculator.Shell.Services
{
    /// <summary>
    ///     The Settings Service.
    /// </summary>
    ///
    /// <seealso cref="ISettingsService"/>
    [ExcludeFromCodeCoverage]
    [UsedImplicitly]
    public class SettingsServiceSingleton : ISettingsService
    {
        #region Fields

        /// <summary>
        ///     The project serializer entity prototype. This cannot be null.
        /// </summary>
        [NotNull] private readonly IProjectSerializerEntityPrototype _projectSerializerEntityPrototype;

        #endregion

        #region Constructors

        /// <summary>
        ///     Constructor.
        /// </summary>
        ///
        /// <exception cref="ArgumentNullException">    Thrown when one or more required arguments are null. </exception>
        ///
        /// <param name="projectSerializerEntityPrototype">             The project serializer entity prototype. </param>
        public SettingsServiceSingleton(
            [NotNull] IProjectSerializerEntityPrototype projectSerializerEntityPrototype )
        {
            _projectSerializerEntityPrototype = projectSerializerEntityPrototype ?? throw new ArgumentNullException( nameof(projectSerializerEntityPrototype) );
        }

        #endregion

        #region Public Methods

        /// <inheritdoc/>
        public bool Contains(
            string propertyName )
        {
            if ( string.IsNullOrWhiteSpace( propertyName ) ) throw new ArgumentException( DevConstants.ValueCannotBeNullOrWhitespace, nameof(propertyName) );

            var property = Settings.Default.PropertyValues[ propertyName ];
            var result = property != null;
            return result;
        }

        /// <inheritdoc/>
        public object GetValue(
            string propertyName )
        {
            if ( string.IsNullOrWhiteSpace( propertyName ) ) throw new ArgumentException( DevConstants.ValueCannotBeNullOrWhitespace, nameof(propertyName) );

            object result = null;
            var property = Settings.Default.PropertyValues[ propertyName ];
            if ( !( property is null ) )
            {
                result = property.PropertyValue;
            }

            return result;
        }

        /// <inheritdoc />
        public bool TryGetValue(
            string propertyName,
            out object value )
        {
            if ( Contains( propertyName ) )
            {
                value = GetValue( propertyName );
                return true;
            }
            else
            {
                value = null;
                return false;
            }
        }

        /// <inheritdoc/>
        public void SaveLastProjectEntity(
            IProjectSerializerEntity projectSerializerEntity )
        {
            if ( projectSerializerEntity is null ) throw new ArgumentNullException( nameof(projectSerializerEntity) );

            var clonedSerializedProj = _projectSerializerEntityPrototype.Clone( projectSerializerEntity );
            Settings.Default.LastProjectEntity = (ProjectSerializerEntity) clonedSerializedProj;
            Settings.Default.Save();
        }

        #endregion
    }
}