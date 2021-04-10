using System.Windows;

namespace Skade.OrbitCalculator.Shell.Infrastructure
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        ///     The Bootstrapper.
        /// </summary>
        private Bootstrapper _bootstrapper;

        /// <inheritdoc />
        public App() { }

        /// <inheritdoc/>
        protected override void OnStartup(
            StartupEventArgs e )
        {
            base.OnStartup( e );

            _bootstrapper = new Bootstrapper();
            _bootstrapper.Run( this );
        }
    }
}
