using System;
using System.Collections.Generic;
using System.Diagnostics;
using JetBrains.Annotations;
using Serilog;
using Skade.CrossCutting.Core.Constants;
using Skade.CrossCutting.Core.Enumerations;
using Skade.CrossCutting.Core.Interfaces.Factories;
using Skade.CrossCutting.Core.Interfaces.Services;
using Skade.CrossCutting.Core.Lookups;
using Skade.CrossCutting.Localiser.Core.Interfaces.Services;
using Skade.CrossCutting.Messenger.Core.Enumerations;
using Skade.CrossCutting.Messenger.Core.Interfaces.Services;

namespace Skade.CrossCutting.Common.Services
{
    /// <summary>
    ///     A Logging Service.
    /// </summary>
    ///
    /// <seealso cref="ILoggingService"/>
    public class LoggingServiceSingleton : ILoggingService
    {
        #region Fields

        /// <summary>
        ///     The Messenger Service.
        /// </summary>
        [NotNull] private readonly IMessengerService _messengerService;

        /// <summary>
        ///     The Localisation Service.
        /// </summary>
        [NotNull] private readonly ILocalisationService _localisationService;

        /// <summary>
        ///     The Trace Service.
        /// </summary>
        [NotNull] private readonly ITraceService _traceService;

        /// <summary>
        ///     The Message Factory.
        /// </summary>
        [NotNull] private readonly IMessageFactory _messageFactory;

        /// <summary>
        ///     The Date Time Service.
        /// </summary>
        [NotNull] private readonly IDateTimeService _dateTimeService;

        /// <summary>
        ///     Dictionary of Log Actions.
        /// </summary>
        [NotNull] private readonly IReadOnlyDictionary< MessageLevelEnum, Action< string > > _logActionDictionary;

        #endregion

        #region Properties and Indexers

        /// <inheritdoc/>
        public LoggingTargetEnum TargetAll => LoggingTargetEnum.LogFile | LoggingTargetEnum.TraceTool | LoggingTargetEnum.OutputPanel;

        /// <inheritdoc/>
        public LoggingTargetEnum TargetLogAndTrace => LoggingTargetEnum.LogFile | LoggingTargetEnum.TraceTool;

        #endregion

        #region Constructors

        /// <summary>
        ///     Constructor.
        /// </summary>
        ///
        /// <exception cref="ArgumentNullException">    Thrown when one or more required arguments are null. </exception>
        ///
        /// <param name="messengerService">     The messenger service. This cannot be null. </param>
        /// <param name="localisationService">  The localisation service. This cannot be null. </param>
        /// <param name="traceService">         The trace service. This cannot be null. </param>
        /// <param name="logger">               The logger. This cannot be null. </param>
        /// <param name="messageFactory">       The message factory. This cannot be null. </param>
        /// <param name="dateTimeService">      The date time service. This cannot be null. </param>
        public LoggingServiceSingleton(
            [NotNull] IMessengerService messengerService,
            [NotNull] ILocalisationService localisationService,
            [NotNull] ITraceService traceService,
            [NotNull] ILogger logger,
            [NotNull] IMessageFactory messageFactory,
            [NotNull] IDateTimeService dateTimeService)
        {
            if ( logger is null ) throw new ArgumentNullException( nameof(logger) );

            _messengerService = messengerService ?? throw new ArgumentNullException( nameof(messengerService) );
            _localisationService = localisationService ?? throw new ArgumentNullException( nameof(localisationService) );
            _traceService = traceService ?? throw new ArgumentNullException( nameof(traceService) );
            _messageFactory = messageFactory ?? throw new ArgumentNullException( nameof(messageFactory) );
            _dateTimeService = dateTimeService ?? throw new ArgumentNullException( nameof(dateTimeService) );

            _logActionDictionary = new Dictionary< MessageLevelEnum, Action< string > >( 6 )
            {
                {MessageLevelEnum.Verbose, logger.Verbose},
                {MessageLevelEnum.Debug, logger.Debug},
                {MessageLevelEnum.Information, logger.Information},
                {MessageLevelEnum.Warning, logger.Warning},
                {MessageLevelEnum.Error, logger.Error},
                {MessageLevelEnum.Fatal, logger.Fatal}
            };
        }

        #endregion

        #region Interface Implementations

        /// <inheritdoc/>
        public void Log(
            string header,
            string message,
            MessageLevelEnum level,
            LoggingTargetEnum outputTarget = LoggingTargetEnum.LogFile | LoggingTargetEnum.TraceTool | LoggingTargetEnum.OutputPanel,
            bool shouldLogHeader = true,
            bool isHeaderResourceKey = false )
        {
            Debug.Assert( !string.IsNullOrWhiteSpace( header ), DevConstants.ValueCannotBeNullOrWhitespace );
            Debug.Assert( !string.IsNullOrWhiteSpace( message ), DevConstants.ValueCannotBeNullOrWhitespace );

            var logToOutputPanel = ( outputTarget & LoggingTargetEnum.OutputPanel ) == LoggingTargetEnum.OutputPanel;

            var messageToLog = message;

            if ( shouldLogHeader || logToOutputPanel )
            {
                var headerToLog = isHeaderResourceKey ? _localisationService.GetLocalisedString( header ) : header;

                if ( shouldLogHeader )
                {
                    messageToLog = $"{headerToLog}: {message}";
                }

                if ( logToOutputPanel )
                {
                    var watchLookup = new WatchLookup( headerToLog, message, _dateTimeService.Now, level );

                    var outputMessage = _messageFactory.CreateOutputMessage( this, watchLookup, messageLevel: level );
                    _messengerService.Send( outputMessage );
                }
            }

            if ( ( outputTarget & LoggingTargetEnum.LogFile ) == LoggingTargetEnum.LogFile )
            {
                _logActionDictionary[ level ]( messageToLog );
            }

            if ( _traceService.IsEnabled && ( outputTarget & LoggingTargetEnum.TraceTool ) == LoggingTargetEnum.TraceTool )
            {
                _traceService.Send( messageToLog, level );
            }
        }

        #endregion
    }
}