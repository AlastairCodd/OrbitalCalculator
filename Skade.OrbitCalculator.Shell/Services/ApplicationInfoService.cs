using System;
using System.IO;
using System.Reflection;
using JetBrains.Annotations;
using Skade.CrossCutting.Core.Interfaces.Lookups;
using Skade.CrossCutting.Core.Lookups;

// ReSharper disable InconsistentNaming

namespace Skade.OrbitCalculator.Shell.Services
{
    /// <summary>
    ///     A service for accessing information about the running application.
    /// </summary>
    public static class ApplicationInfoService
    {
        #region Static and constant fields

        /// <summary>
        ///     Name of the product.
        /// </summary>
        [NotNull] private static readonly Lazy< string > _productName = new Lazy< string >( GetProductName );

        /// <summary>
        ///     The version.
        /// </summary>
        [NotNull] private static readonly Lazy< string > _version = new Lazy< string >( GetVersion );

        /// <summary>
        ///     The company.
        /// </summary>
        [NotNull] private static readonly Lazy< string > _company = new Lazy< string >( GetCompany );

        /// <summary>
        ///     The copyright.
        /// </summary>
        [NotNull] private static readonly Lazy< string > _copyright = new Lazy< string >( GetCopyright );

        /// <summary>
        ///     Full pathname of the application file.
        /// </summary>
        [NotNull] private static readonly Lazy< string > _applicationPath = new Lazy< string >( GetApplicationPath );

        /// <summary>
        ///     The git branch this version was built from.
        /// </summary>
        [NotNull] private static readonly Lazy< IResultLookup< string > > _gitBranch = new Lazy< IResultLookup< string > >( GetGitBranch );

        /// <summary>
        ///     The git hash this version was built from.
        /// </summary>
        [NotNull] private static readonly Lazy< IResultLookup< string > > _gitHash = new Lazy< IResultLookup< string > >( GetGitHash );

        #endregion

        #region Properties and Indexers

        /// <summary>
        ///     Gets the product name of the application.
        /// </summary>
        ///
        /// <value>
        ///     The name of the product.
        /// </value>
        [NotNull]
        public static string ProductName => _productName.Value;

        /// <summary>
        ///     Gets the version number of the application.
        /// </summary>
        ///
        /// <value>
        ///     The version.
        /// </value>
        [NotNull]
        public static string Version => _version.Value;

        /// <summary>
        ///     Gets the company of the application.
        /// </summary>
        ///
        /// <value>
        ///     The company.
        /// </value>
        [NotNull]
        public static string Company => _company.Value;

        /// <summary>
        ///     Gets the copyright information of the application.
        /// </summary>
        ///
        /// <value>
        ///     The copyright.
        /// </value>
        [NotNull]
        public static string Copyright => _copyright.Value;

        /// <summary>
        ///     Gets the path for the executable file that started the application, not including the executable name.
        /// </summary>
        ///
        /// <value>
        ///     The full pathname of the application file.
        /// </value>
        [NotNull]
        public static string ApplicationPath => _applicationPath.Value;

        /// <summary>
        ///     Gets the pathname of the Application Data Folder.
        /// </summary>
        ///
        /// <value>
        ///     The pathname of the application data folder.
        /// </value>
        [NotNull]
        public static string ApplicationDataFolder => Path.Combine( Environment.GetFolderPath( Environment.SpecialFolder.CommonApplicationData ), Company, ProductName );

        /// <summary>
        ///     Gets the pathname of the User Data Folder.
        /// </summary>
        ///
        /// <value>
        ///     The pathname of the user data folder.
        /// </value>
        [NotNull]
        public static string UserDataFolder => Path.Combine( Environment.GetFolderPath( Environment.SpecialFolder.LocalApplicationData ), Company, ProductName );

        /// <summary>
        ///     Gets the git branch.
        /// </summary>
        ///
        /// <value>
        ///     The git branch. This will never be null.
        /// </value>
        [NotNull]
        public static IResultLookup< string > GitBranch => _gitBranch.Value;

        /// <summary>
        ///     Gets the git hash.
        /// </summary>
        ///
        /// <value>
        ///     The git hash. This will never be null.
        /// </value>
        [NotNull]
        public static IResultLookup< string > GitHash => _gitHash.Value;

        #endregion

        #region Other Members

        /// <summary>
        ///     Gets product name.
        /// </summary>
        ///
        /// <returns>
        ///     The product name.
        /// </returns>
        [NotNull]
        private static string GetProductName()
        {
            var result = string.Empty;
            var entryAssembly = Assembly.GetEntryAssembly();
            if ( !( entryAssembly is null ) )
            {
                var attribute = (AssemblyProductAttribute) Attribute.GetCustomAttribute( entryAssembly, typeof( AssemblyProductAttribute ) );
                result = attribute?.Product ?? string.Empty;
            }

            return result;
        }

        /// <summary>
        ///     Gets the version.
        /// </summary>
        ///
        /// <returns>
        ///     The version.
        /// </returns>
        [NotNull]
        private static string GetVersion()
        {
            var result = Assembly.GetEntryAssembly()?.GetName().Version?.ToString() ?? string.Empty;
            return result;
        }

        /// <summary>
        ///     Gets the company.
        /// </summary>
        ///
        /// <returns>
        ///     The company.
        /// </returns>
        [NotNull]
        private static string GetCompany()
        {
            var result = string.Empty;
            var entryAssembly = Assembly.GetEntryAssembly();
            if ( !( entryAssembly is null ) )
            {
                var attribute = (AssemblyCompanyAttribute) Attribute.GetCustomAttribute( entryAssembly, typeof( AssemblyCompanyAttribute ) );
                result = attribute?.Company ?? string.Empty;
            }

            return result;
        }

        /// <summary>
        ///     Gets the copyright.
        /// </summary>
        ///
        /// <returns>
        ///     The copyright.
        /// </returns>
        [NotNull]
        private static string GetCopyright()
        {
            var result = string.Empty;
            var entryAssembly = Assembly.GetEntryAssembly();
            if ( !( entryAssembly is null ) )
            {
                var attribute = (AssemblyCopyrightAttribute) Attribute.GetCustomAttribute( entryAssembly, typeof( AssemblyCopyrightAttribute ) );
                result = attribute?.Copyright ?? string.Empty;
            }

            return result;
        }

        /// <summary>
        ///     Gets application path.
        /// </summary>
        ///
        /// <returns>
        ///     The application path.
        /// </returns>
        [NotNull]
        private static string GetApplicationPath()
        {
            var entryAssembly = Assembly.GetEntryAssembly();
            var result = entryAssembly is null ? string.Empty : Path.GetDirectoryName( entryAssembly.Location );
            return result ?? string.Empty;
        }

        /// <summary>
        ///     Gets git branch.
        /// </summary>
        ///
        /// <returns>
        ///     The git branch. This will never be null.
        /// </returns>
        [NotNull]
        private static IResultLookup< string > GetGitBranch()
        {
            var buildInfoPath = @"./build-info";
            var fileExists = File.Exists( buildInfoPath );

            var result = new ResultLookup< string >( false, "Unknown", "File does not exist" );

            if ( fileExists )
            {
                var pathLines = File.ReadAllLines( buildInfoPath );

                if ( pathLines.Length >= 2 )
                {
                    result = new ResultLookup< string >( true, pathLines[ 0 ] );
                }
            }

            return result;
        }

        /// <summary>
        ///     Gets git hash.
        /// </summary>
        ///
        /// <returns>
        ///     The git hash. This will never be null.
        /// </returns>
        [NotNull]
        private static IResultLookup< string > GetGitHash()
        {
            var buildInfoPath = @"./build-info";
            var fileExists = File.Exists( buildInfoPath );

            IResultLookup< string > result = new ResultLookup< string >( false, "Unknown", "File does not exist" );

            if ( fileExists )
            {
                var pathLines = File.ReadAllLines( buildInfoPath );

                if ( pathLines.Length >= 2 )
                {
                    result = new ResultLookup< string >( true, pathLines[ 1 ] );
                }
            }

            return result;
        }

        #endregion
    }
}