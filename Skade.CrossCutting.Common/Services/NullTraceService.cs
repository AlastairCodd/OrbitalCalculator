using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Skade.CrossCutting.Core.Interfaces.Lookups;
using Skade.CrossCutting.Core.Interfaces.Services;
using Skade.CrossCutting.Messenger.Core.Enumerations;

namespace Skade.CrossCutting.Common.Services
{
    /// <summary>
    ///     A null pattern for the trace service.
    /// </summary>
    ///
    /// <seealso cref="INullTraceService"/>
    [ExcludeFromCodeCoverage]
    [UsedImplicitly]
    public class NullTraceService : INullTraceService
    {
        #region Properties and Indexers

        /// <inheritdoc />
        public bool IsEnabled { get; set; }

        #endregion

        #region Interface Implementations

        /// <inheritdoc />
        public void ClearAll() { }

        /// <inheritdoc />
        public void EnterMethod(
            string leftMessage,
            MessageLevelEnum messageLevel = MessageLevelEnum.Debug ) { }

        /// <inheritdoc />
        public void ExitMethod(
            string leftMessage,
            MessageLevelEnum messageLevel = MessageLevelEnum.Debug ) { }

        /// <inheritdoc />
        public void ExitMethod(
            string leftMessage,
            string rightMessage,
            MessageLevelEnum messageLevel = MessageLevelEnum.Debug ) { }

        /// <inheritdoc />
        public void SendValue(
            string leftMessage,
            object rightObject,
            MessageLevelEnum messageLevel = MessageLevelEnum.Debug ) { }

        /// <inheritdoc />
        public void Send(
            string leftMessage,
            MessageLevelEnum messageLevel = MessageLevelEnum.Debug ) { }

        /// <inheritdoc />
        public void Send(
            string leftMessage,
            string rightMessage,
            MessageLevelEnum messageLevel = MessageLevelEnum.Debug ) { }

        /// <inheritdoc />
        public void SendObject(
            string leftMessage,
            object rightObject,
            MessageLevelEnum messageLevel = MessageLevelEnum.Debug ) { }

        /// <inheritdoc />
        public void SendToWatch(
            string watchName,
            object rightObject ) { }

        /// <inheritdoc />
        public void SendBackgroundColor(
            string leftMessage,
            int colour,
            MessageLevelEnum messageLevel = MessageLevelEnum.Debug ) { }

        /// <inheritdoc />
        public void SendDump(
            string leftMessage,
            string shortTitle,
            byte[] address,
            int count,
            MessageLevelEnum messageLevel = MessageLevelEnum.Debug ) { }

        /// <inheritdoc />
        public void CreateTraceWindow(
            string traceWindowKey,
            string title ) { }

        /// <inheritdoc />
        public void CreateWatchWindow(
            string watchWindowKey,
            string title ) { }

        /// <inheritdoc />
        public void CleanTraceWindow(
            string traceWindowKey ) { }

        /// <inheritdoc />
        public void CleanWatchWindow(
            string watchWindowKey ) { }

        /// <inheritdoc />
        public void SendToTraceWindow(
            string traceWindowKey,
            string leftMessage,
            MessageLevelEnum messageLevel = MessageLevelEnum.Debug ) { }

        /// <inheritdoc />
        public void SendToTraceWindow(
            string traceWindowKey,
            string leftMessage,
            string rightMessage,
            MessageLevelEnum messageLevel = MessageLevelEnum.Debug ) { }

        /// <inheritdoc />
        public void SendToTraceWindow< T >(
            string traceWindowKey,
            string leftMessage,
            T rightMessage,
            MessageLevelEnum messageLevel = MessageLevelEnum.Debug ) { }

        /// <inheritdoc />
        public void SendDumpToTraceWindow(
            string traceWindowKey,
            string leftMessage,
            string shortTitle,
            byte[] memoryDump,
            short count,
            MessageLevelEnum messageLevel = MessageLevelEnum.Debug ) { }

        /// <inheritdoc />
        public void SendToWatchWindow< T >(
            string watchWindowKey,
            string watchName,
            T rightObject ) { }

        /// <inheritdoc/>
        public void ResetWatchWindow< TKey, TLookup >(
            string watchWindowKey,
            IDictionary< TKey, TLookup > watchWindowContents ) where TLookup : class, ILookup< TKey > { }

        /// <inheritdoc />
        public void ClearWatchWindows() { }

        #endregion
    }
}