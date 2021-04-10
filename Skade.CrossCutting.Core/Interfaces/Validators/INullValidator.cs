using FluentValidation;

namespace Skade.CrossCutting.Core.Interfaces.Validators
{
    /// <summary>
    ///     Interface for null validator.
    /// </summary>
    ///
    /// <typeparam name="T">    Generic type parameter. </typeparam>
    public interface INullValidator< in T > : IValidator< T > { }
}