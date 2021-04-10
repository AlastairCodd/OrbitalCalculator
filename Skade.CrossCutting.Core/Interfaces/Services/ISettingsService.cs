using JetBrains.Annotations;
using Skade.CrossCutting.Core.Interfaces.Entities;

namespace Skade.CrossCutting.Core.Interfaces.Services
{
    /// <summary>
    ///     Interface for Settings Service.
    /// </summary>
    public interface ISettingsService
    {
        #region Other Members

        /// <summary>
        ///     Query if this object contains the given propertyName.
        /// </summary>
        ///
        /// <param name="propertyName"> The string to test for containment. </param>
        ///
        /// <returns>
        ///     True if the object is in this collection, false if not.
        /// </returns>
        bool Contains(
            [NotNull] string propertyName );

        /// <summary>
        ///     Gets a value.
        /// </summary>
        ///
        /// <param name="propertyName"> The property to get from the settings file. This cannot be null. </param>
        ///
        /// <returns>
        ///     The value.
        /// </returns>
        [CanBeNull]
        [MustUseReturnValue]
        object GetValue(
            [NotNull] string propertyName );

        /// <summary>
        ///     Attempts to get value an object from the given string.
        /// </summary>
        ///
        /// <param name="propertyName"> The string to test for containment. </param>
        /// <param name="value">        [out] The value. </param>
        ///
        /// <returns>
        ///     True if it succeeds, false if it fails.
        /// </returns>
        bool TryGetValue(
            [NotNull] string propertyName,
            out object value );

        /// <summary>
        ///     Saves a last project entity.
        /// </summary>
        ///
        /// <param name="projectSerializerEntity">  The project serializer entity. This cannot be null. </param>
        void SaveLastProjectEntity(
            [NotNull] IProjectSerializerEntity projectSerializerEntity );

        #endregion
    }
}