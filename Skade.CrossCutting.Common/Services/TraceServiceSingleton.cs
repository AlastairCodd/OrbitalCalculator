using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Skade.CrossCutting.Core.Constants;
using Skade.CrossCutting.Core.Interfaces.Lookups;
using Skade.CrossCutting.Core.Interfaces.Services;
using Skade.CrossCutting.Messenger.Core.Enumerations;
using TraceTool;

namespace Skade.CrossCutting.Common.Services
{
    /// <summary>
    ///     A Trace Tool facade.
    /// </summary>
    ///
    /// <seealso cref="ITraceService"/>
    [ExcludeFromCodeCoverage]
    [UsedImplicitly]
    public class TraceServiceSingleton : ITraceService
    {
        #region Fields

        /// <summary>
        ///     Dictionary of message levels.
        /// </summary>
        [JetBrains.Annotations.NotNull] private readonly IReadOnlyDictionary< MessageLevelEnum, TraceToSend > _traceDictionary;

        /// <summary>
        ///     Dictionary of windows.
        /// </summary>
        [JetBrains.Annotations.NotNull] private readonly IReadOnlyDictionary< MessageLevelEnum, Func< WinTrace, TraceToSend > > _windowDictionary;

        /// <summary>
        ///     Dictionary of trace windows.
        /// </summary>
        [JetBrains.Annotations.NotNull] private readonly IDictionary< string, WinTrace > _traceWindowDictionary;

        /// <summary>
        ///     Dictionary of watch windows.
        /// </summary>
        [JetBrains.Annotations.NotNull] private readonly IDictionary< string, WinWatch > _watchWindowDictionary;

        #endregion

        #region Properties and Indexers

        /// <inheritdoc/>
        public bool IsEnabled { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        ///     Default constructor.
        /// </summary>
        public TraceServiceSingleton()
        {
            _traceDictionary = new ReadOnlyDictionary< MessageLevelEnum, TraceToSend >
            (
                new Dictionary< MessageLevelEnum, TraceToSend >
                {
                    {MessageLevelEnum.Information, TTrace.Debug},
                    {MessageLevelEnum.Debug, TTrace.Debug},
                    {MessageLevelEnum.Verbose, TTrace.Debug},
                    {MessageLevelEnum.Warning, TTrace.Warning},
                    {MessageLevelEnum.Error, TTrace.Error},
                    {MessageLevelEnum.Fatal, TTrace.Error},
                } );

            _windowDictionary = new ReadOnlyDictionary< MessageLevelEnum, Func< WinTrace, TraceToSend > >
            (
                new Dictionary< MessageLevelEnum, Func< WinTrace, TraceToSend > >
                {
                    {MessageLevelEnum.Information, t => t.Debug},
                    {MessageLevelEnum.Debug, t => t.Debug},
                    {MessageLevelEnum.Verbose, t => t.Debug},
                    {MessageLevelEnum.Warning, t => t.Warning},
                    {MessageLevelEnum.Error, t => t.Error},
                    {MessageLevelEnum.Fatal, t => t.Error},
                } );

            _traceWindowDictionary = new Dictionary< string, WinTrace >();
            _watchWindowDictionary = new Dictionary< string, WinWatch >();
        }

        #endregion

        #region Interface Implementations

        /// <inheritdoc/>
        public void ClearAll()
        {
            if ( !IsEnabled )
            {
                return;
            }

            TTrace.ClearAll();

            ClearWatchWindows();
        }

        /// <inheritdoc/>
        public void EnterMethod(
            string leftMessage,
            MessageLevelEnum messageLevel = MessageLevelEnum.Debug )
        {
            if ( !IsEnabled )
            {
                return;
            }

            if ( string.IsNullOrWhiteSpace( leftMessage ) ) throw new ArgumentException( DevConstants.ValueCannotBeNullOrWhitespace, nameof(leftMessage) );

            try
            {
                _traceDictionary[ messageLevel ]
                    .EnterMethod( leftMessage );
            }
            catch ( ArgumentOutOfRangeException ) { }
        }

        /// <inheritdoc/>
        public void ExitMethod(
            string leftMessage,
            MessageLevelEnum messageLevel = MessageLevelEnum.Debug )
        {
            if ( !IsEnabled )
            {
                return;
            }

            if ( string.IsNullOrWhiteSpace( leftMessage ) ) throw new ArgumentException( DevConstants.ValueCannotBeNullOrWhitespace, nameof(leftMessage) );

            try
            {
                _traceDictionary[ messageLevel ]
                    .ExitMethod( leftMessage );
            }
            catch ( ArgumentOutOfRangeException ) { }
        }

        /// <inheritdoc/>
        public void ExitMethod(
            string leftMessage,
            string rightMessage,
            MessageLevelEnum messageLevel = MessageLevelEnum.Debug )
        {
            if ( !IsEnabled )
            {
                return;
            }

            if ( string.IsNullOrWhiteSpace( leftMessage ) ) throw new ArgumentException( DevConstants.ValueCannotBeNullOrWhitespace, nameof(leftMessage) );
            if ( string.IsNullOrWhiteSpace( rightMessage ) ) throw new ArgumentException( DevConstants.ValueCannotBeNullOrWhitespace, nameof(rightMessage) );

            try
            {
                _traceDictionary[ messageLevel ]
                    .ExitMethod( leftMessage, rightMessage );
            }
            catch ( ArgumentOutOfRangeException ) { }
        }

        /// <inheritdoc/>
        public void SendValue(
            string leftMessage,
            object rightObject,
            MessageLevelEnum messageLevel = MessageLevelEnum.Debug )
        {
            if ( !IsEnabled )
            {
                return;
            }

            if ( string.IsNullOrWhiteSpace( leftMessage ) ) throw new ArgumentException( DevConstants.ValueCannotBeNullOrWhitespace, nameof(leftMessage) );

            try
            {
                _traceDictionary[ messageLevel ]
                    .SendValue( leftMessage, rightObject );
            }
            catch ( ArgumentOutOfRangeException ) { }
        }

        /// <inheritdoc/>
        public void Send(
            string leftMessage,
            MessageLevelEnum messageLevel = MessageLevelEnum.Debug )
        {
            if ( !IsEnabled )
            {
                return;
            }

            if ( string.IsNullOrWhiteSpace( leftMessage ) ) throw new ArgumentException( DevConstants.ValueCannotBeNullOrWhitespace, nameof(leftMessage) );

            try
            {
                _traceDictionary[ messageLevel ]
                    .Send( leftMessage );
            }
            catch ( ArgumentOutOfRangeException ) { }
        }

        /// <inheritdoc/>
        public void SendObject(
            string leftMessage,
            object rightObject,
            MessageLevelEnum messageLevel = MessageLevelEnum.Debug )
        {
            if ( !IsEnabled )
            {
                return;
            }

            if ( string.IsNullOrWhiteSpace( leftMessage ) ) throw new ArgumentException( DevConstants.ValueCannotBeNullOrWhitespace, nameof(leftMessage) );
            if ( rightObject is null ) throw new ArgumentNullException( nameof(rightObject) );

            try
            {
                _traceDictionary[ messageLevel ]
                    .SendObject( leftMessage, rightObject );
            }
            catch ( ArgumentOutOfRangeException ) { }
        }

        /// <inheritdoc/>
        public void SendToWatch(
            string watchName,
            object rightObject )
        {
            if ( !IsEnabled )
            {
                return;
            }

            if ( rightObject is null ) throw new ArgumentNullException( nameof(rightObject) );
            if ( string.IsNullOrWhiteSpace( watchName ) ) throw new ArgumentException( DevConstants.ValueCannotBeNullOrWhitespace, nameof(watchName) );

            try
            {
                TTrace.Watches.Send( watchName, rightObject );
            }
            catch ( ArgumentOutOfRangeException ) { }
        }

        /// <inheritdoc/>
        public void Send(
            string leftMessage,
            string rightMessage,
            MessageLevelEnum messageLevel = MessageLevelEnum.Debug )
        {
            if ( !IsEnabled )
            {
                return;
            }

            if ( string.IsNullOrWhiteSpace( leftMessage ) ) throw new ArgumentException( DevConstants.ValueCannotBeNullOrWhitespace, nameof(leftMessage) );

            try
            {
                _traceDictionary[ messageLevel ]
                    .Send( leftMessage, rightMessage );
            }
            catch ( ArgumentOutOfRangeException ) { }
        }

        /// <inheritdoc/>
        public void SendBackgroundColor(
            string leftMessage,
            int colour,
            MessageLevelEnum messageLevel = MessageLevelEnum.Debug )
        {
            if ( !IsEnabled )
            {
                return;
            }

            if ( string.IsNullOrWhiteSpace( leftMessage ) ) throw new ArgumentException( DevConstants.ValueCannotBeNullOrWhitespace, nameof(leftMessage) );

            try
            {
                _traceDictionary[ messageLevel ]
                    .SendBackgroundColor( leftMessage, colour );
            }
            catch ( ArgumentOutOfRangeException ) { }
        }

        /// <inheritdoc/>
        public void SendDump(
            string leftMessage,
            string shortTitle,
            byte[] address,
            int count,
            MessageLevelEnum messageLevel = MessageLevelEnum.Debug )
        {
            if ( !IsEnabled )
            {
                return;
            }

            if ( address is null ) throw new ArgumentNullException( nameof(address) );
            if ( string.IsNullOrWhiteSpace( leftMessage ) ) throw new ArgumentException( DevConstants.ValueCannotBeNullOrWhitespace, nameof(leftMessage) );
            if ( string.IsNullOrWhiteSpace( shortTitle ) ) throw new ArgumentException( DevConstants.ValueCannotBeNullOrWhitespace, nameof(shortTitle) );

            try
            {
                _traceDictionary[ messageLevel ]
                    .SendDump( leftMessage, shortTitle, address, count );
            }
            catch ( ArgumentOutOfRangeException ) { }
        }

        /// <inheritdoc/>
        public void CreateTraceWindow(
            string key,
            string title )
        {
            if ( !IsEnabled )
            {
                return;
            }

            if ( string.IsNullOrWhiteSpace( key ) ) throw new ArgumentException( DevConstants.ValueCannotBeNullOrWhitespace, nameof(key) );
            if ( string.IsNullOrWhiteSpace( title ) ) throw new ArgumentException( DevConstants.ValueCannotBeNullOrWhitespace, nameof(title) );

            if ( !_traceWindowDictionary.TryGetValue( key, out var traceWindow ) )
            {
                traceWindow = new WinTrace( key, title );
                _traceWindowDictionary.Add( key, traceWindow );
            }
        }

        /// <inheritdoc/>
        public void CreateWatchWindow(
            string key,
            string title )
        {
            if ( !IsEnabled )
            {
                return;
            }

            if ( string.IsNullOrWhiteSpace( key ) ) throw new ArgumentException( DevConstants.ValueCannotBeNullOrWhitespace, nameof(key) );
            if ( string.IsNullOrWhiteSpace( title ) ) throw new ArgumentException( DevConstants.ValueCannotBeNullOrWhitespace, nameof(title) );

            if ( !_watchWindowDictionary.TryGetValue( key, out var watchWindow ) )
            {
                watchWindow = new WinWatch( key, title );
                _watchWindowDictionary.Add( key, watchWindow );
            }
        }

        /// <inheritdoc/>
        public void CleanTraceWindow(
            string key )
        {
            if ( !IsEnabled )
            {
                return;
            }

            if ( string.IsNullOrWhiteSpace( key ) ) throw new ArgumentException( DevConstants.ValueCannotBeNullOrWhitespace, nameof(key) );

            if ( _traceWindowDictionary.TryGetValue( key, out var traceWindow ) )
            {
                traceWindow.ClearAll();
            }
        }

        /// <inheritdoc/>
        public void CleanWatchWindow(
            string key )
        {
            if ( !IsEnabled )
            {
                return;
            }

            if ( string.IsNullOrWhiteSpace( key ) ) throw new ArgumentException( DevConstants.ValueCannotBeNullOrWhitespace, nameof(key) );

            if ( _watchWindowDictionary.TryGetValue( key, out var watchWindow ) )
            {
                watchWindow.ClearAll();
            }
        }

        /// <inheritdoc/>
        public void SendToTraceWindow(
            string key,
            string leftMessage,
            MessageLevelEnum messageLevel = MessageLevelEnum.Debug )
        {
            if ( !IsEnabled )
            {
                return;
            }

            if ( string.IsNullOrWhiteSpace( key ) ) throw new ArgumentException( "Value cannot be null or whitespace.", nameof(key) );
            if ( string.IsNullOrWhiteSpace( leftMessage ) ) throw new ArgumentException( "Value cannot be null or whitespace.", nameof(leftMessage) );

            if ( _traceWindowDictionary.TryGetValue( key, out var traceWindow ) )
            {
                try
                {
                    _windowDictionary[ messageLevel ]( traceWindow )
                        .Send( leftMessage );
                }
                catch ( ArgumentOutOfRangeException ) { }
            }
        }

        /// <inheritdoc/>
        public void SendToTraceWindow(
            string key,
            string leftMessage,
            string rightMessage,
            MessageLevelEnum messageLevel = MessageLevelEnum.Debug )
        {
            if ( !IsEnabled )
            {
                return;
            }

            if ( string.IsNullOrWhiteSpace( key ) ) throw new ArgumentException( "Value cannot be null or whitespace.", nameof(key) );
            if ( string.IsNullOrWhiteSpace( leftMessage ) ) throw new ArgumentException( "Value cannot be null or whitespace.", nameof(leftMessage) );
            if ( string.IsNullOrWhiteSpace( rightMessage ) ) throw new ArgumentException( "Value cannot be null or whitespace.", nameof(rightMessage) );

            if ( _traceWindowDictionary.TryGetValue( key, out var traceWindow ) )
            {
                try
                {
                    _windowDictionary[ messageLevel ]( traceWindow )
                        .Send( leftMessage, rightMessage );
                }
                catch ( ArgumentOutOfRangeException ) { }
            }
        }

        /// <inheritdoc/>
        public void SendToTraceWindow< T >(
            string key,
            string leftMessage,
            T rightMessage,
            MessageLevelEnum messageLevel = MessageLevelEnum.Debug )
        {
            if ( !IsEnabled )
            {
                return;
            }

            try
            {
                SendToTraceWindow( key, leftMessage, rightMessage.ToString(), messageLevel );
            }
            catch ( ArgumentOutOfRangeException ) { }
        }

        /// <inheritdoc/>
        public void SendDumpToTraceWindow(
            string key,
            string leftMessage,
            string shortTitle,
            byte[] memoryDump,
            short count,
            MessageLevelEnum messageLevel = MessageLevelEnum.Debug )
        {
            if ( !IsEnabled )
            {
                return;
            }

            if ( string.IsNullOrWhiteSpace( key ) ) throw new ArgumentException( DevConstants.ValueCannotBeNullOrWhitespace, nameof(key) );
            if ( string.IsNullOrWhiteSpace( leftMessage ) ) throw new ArgumentException( DevConstants.ValueCannotBeNullOrWhitespace, nameof(leftMessage) );
            if ( string.IsNullOrWhiteSpace( shortTitle ) ) throw new ArgumentException( DevConstants.ValueCannotBeNullOrWhitespace, nameof(shortTitle) );

            if ( _traceWindowDictionary.TryGetValue( key, out var traceWindow ) )
            {
                try
                {
                    _windowDictionary[ messageLevel ]( traceWindow )
                        .SendDump( leftMessage, shortTitle, memoryDump, count );
                }
                catch ( ArgumentOutOfRangeException ) { }
            }
        }

        /// <inheritdoc/>
        public void SendToWatchWindow< T >(
            string watchWindowKey,
            string watchName,
            T rightObject )
        {
            if ( !IsEnabled )
            {
                return;
            }

            if ( string.IsNullOrWhiteSpace( watchWindowKey ) ) throw new ArgumentException( DevConstants.ValueCannotBeNullOrWhitespace, nameof(watchWindowKey) );
            if ( string.IsNullOrWhiteSpace( watchName ) ) throw new ArgumentException( DevConstants.ValueCannotBeNullOrWhitespace, nameof(watchName) );

            if ( _watchWindowDictionary.TryGetValue( watchWindowKey, out var watchWindow ) )
            {
                var rightValue = rightObject as string ?? rightObject?.ToString();
                try
                {
                    watchWindow.Send( watchName, rightValue );
                }
                catch ( ArgumentOutOfRangeException ) { }
            }
        }

        /// <inheritdoc/>
        public void ResetWatchWindow< TKey, TLookup >(
            string watchWindowKey,
            IDictionary< TKey, TLookup > watchWindowContents ) where TLookup : class, ILookup< TKey >
        {
            if ( !IsEnabled )
            {
                return;
            }

            if ( string.IsNullOrWhiteSpace( watchWindowKey ) ) throw new ArgumentException( DevConstants.ValueCannotBeNullOrWhitespace, nameof(watchWindowKey) );

            if ( _watchWindowDictionary.TryGetValue( watchWindowKey, out var watchWindow ) )
            {
                watchWindow.ClearAll();

                foreach ( var content in watchWindowContents )
                {
                    var rightValue = content.Value.ToString();
                    try
                    {
                        watchWindow.Send( content.Key.ToString(), rightValue );
                    }
                    catch ( ArgumentOutOfRangeException ) { }
                }
            }
        }

        /// <inheritdoc/>
        public void ClearWatchWindows()
        {
            foreach ( var windows in _watchWindowDictionary.Values )
            {
                windows.ClearAll();
            }
        }

        #endregion
    }
}