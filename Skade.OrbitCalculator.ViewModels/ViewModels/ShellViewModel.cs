using System;
using Skade.CrossCutting.Core.Interfaces.ViewModels;

namespace Skade.OrbitCalculator.ViewModels.ViewModels
{
    /// <summary>
    ///     A ViewModel for the shell.
    /// </summary>
    ///
    /// <seealso cref="IShellViewModel"/>
    /// <seealso cref="IDisposable"/>
    public class ShellViewModel : IShellViewModel, IDisposable
    {
        /// <inheritdoc />
        public void Dispose() { }
    }
}