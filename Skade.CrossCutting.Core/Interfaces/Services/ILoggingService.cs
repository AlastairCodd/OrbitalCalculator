using JetBrains.Annotations;
using Skade.CrossCutting.Core.Enumerations;
using Skade.CrossCutting.Messenger.Core.Enumerations;

namespace Skade.CrossCutting.Core.Interfaces.Services
{
    /// <summary>
    ///     Interface for Logging Service.
    /// </summary>
    public interface ILoggingService
    {
        /// <summary>
        ///     Gets Target Mask for all Targets.
        /// </summary>
        ///
        /// <value>
        ///     The target mask for all targets.
        /// </value>
        LoggingTargetEnum TargetAll { get; }

        /// <summary>
        ///     Gets Target Mask for Log and Trace Targets.
        /// </summary>
        ///
        /// <value>
        ///     The target Mask for log and trace targets
        /// </value>
        LoggingTargetEnum TargetLogAndTrace { get; }

        #region Other Members

        /// <summary>
        ///     Logs a message.
        /// </summary>
        ///
        /// <param name="header">               The header. This cannot be null. </param>
        /// <param name="message">              The message. </param>
        /// <param name="level">                The level. </param>
        /// <param name="outputTarget">         (Optional) The output target. </param>
        /// <param name="shouldLogHeader">      (Optional) True if should log header. </param>
        /// <param name="isHeaderResourceKey">  (Optional) True if the header is resource key and should be localised, false if not. </param>
        void Log(
            [NotNull] string header,
            [NotNull] string message,
            MessageLevelEnum level,
            LoggingTargetEnum outputTarget = LoggingTargetEnum.LogFile | LoggingTargetEnum.TraceTool | LoggingTargetEnum.OutputPanel,
            bool shouldLogHeader = true,
            bool isHeaderResourceKey = true );

        #endregion
    }
}