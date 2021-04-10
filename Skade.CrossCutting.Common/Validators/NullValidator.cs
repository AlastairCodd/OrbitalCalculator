using FluentValidation;
using Skade.CrossCutting.Core.Interfaces.Validators;

namespace Skade.CrossCutting.Common.Validators
{
    /// <summary>
    ///     A null validator.
    /// </summary>
    ///
    /// <typeparam name="T">    Generic type parameter. </typeparam>
    ///
    /// <seealso cref="AbstractValidator{T}"/>
    /// <seealso cref="INullValidator{T}"/>
    public class NullValidator< T > : AbstractValidator< T >, INullValidator< T > { }
}