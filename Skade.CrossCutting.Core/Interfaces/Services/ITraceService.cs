using System.Collections.Generic;
using JetBrains.Annotations;
using Skade.CrossCutting.Core.Interfaces.Lookups;
using Skade.CrossCutting.Messenger.Core.Enumerations;

namespace Skade.CrossCutting.Core.Interfaces.Services
{
    /// <summary>
    ///     Interface for The tracing service.
    /// </summary>
    public interface ITraceService
    {
        #region Properties and Indexers

        /// <summary>
        ///     Gets or sets a value indicating whether this object is enabled.
        ///     
        /// </summary>
        ///
        /// <value>
        ///     True if this object is enabled, false if not.
        /// </value>
        bool IsEnabled { get; set; }

        #endregion

        #region Other Members

        /// <summary>
        ///     Clears all windows from TTrace.
        /// </summary>
        void ClearAll();

        /// <summary>
        ///     Enter method.
        /// </summary>
        ///
        /// <param name="leftMessage">  Message to be displayed in the left window. This cannot be null. </param>
        /// <param name="messageLevel"> (Optional) The logging level. </param>
        void EnterMethod(
            [NotNull] string leftMessage,
            MessageLevelEnum messageLevel = MessageLevelEnum.Debug );

        /// <summary>
        ///     Exit method.
        /// </summary>
        ///
        /// <param name="leftMessage">  Message to be displayed in the left window. This cannot be null. </param>
        /// <param name="messageLevel"> (Optional) The logging level. </param>
        void ExitMethod(
            [NotNull] string leftMessage,
            MessageLevelEnum messageLevel = MessageLevelEnum.Debug );

        /// <summary>
        ///     Exit method.
        /// </summary>
        ///
        /// <param name="leftMessage">  Message to be displayed in the left window. This cannot be null. </param>
        /// <param name="rightMessage"> Message to be displayed in the right window. This cannot be null. </param>
        /// <param name="messageLevel"> (Optional) The logging level. </param>
        void ExitMethod(
            [NotNull] string leftMessage,
            [NotNull] string rightMessage,
            MessageLevelEnum messageLevel = MessageLevelEnum.Debug );

        /// <summary>
        ///     Sends a value.
        /// </summary>
        ///
        /// <param name="leftMessage">  Message to be displayed in the left window. This cannot be null. </param>
        /// <param name="rightObject">  The right object. This cannot be null. </param>
        /// <param name="messageLevel"> (Optional) The logging level. </param>
        void SendValue(
            [NotNull] string leftMessage,
            [NotNull] object rightObject,
            MessageLevelEnum messageLevel = MessageLevelEnum.Debug );

        /// <summary>
        ///     Send this message.
        /// </summary>
        ///
        /// <param name="leftMessage">  Message to be displayed in the left window. This cannot be null. </param>
        /// <param name="messageLevel"> (Optional) The logging level. </param>
        ///
        /// <returns>
        ///     An ITraceNodeEntity. This will never be null.
        /// </returns>
        void Send(
            [NotNull] string leftMessage,
            MessageLevelEnum messageLevel = MessageLevelEnum.Debug );

        /// <summary>
        ///     Send this message.
        /// </summary>
        ///
        /// <param name="leftMessage">  Message to be displayed in the left window. This cannot be null. </param>
        /// <param name="rightMessage"> Message to be displayed in the right window. This maybe be null. </param>
        /// <param name="messageLevel"> (Optional) The logging level. </param>
        void Send(
            [NotNull] string leftMessage,
            [CanBeNull] string rightMessage,
            MessageLevelEnum messageLevel = MessageLevelEnum.Debug );

        /// <summary>
        ///     Sends an object.
        /// </summary>
        ///
        /// <param name="leftMessage">  Message to be displayed in the left window. This cannot be null. </param>
        /// <param name="rightObject">  The right object. This cannot be null. </param>
        /// <param name="messageLevel"> (Optional) The logging level. </param>
        void SendObject(
            [NotNull] string leftMessage,
            [NotNull] object rightObject,
            MessageLevelEnum messageLevel = MessageLevelEnum.Debug );

        /// <summary>
        ///     Sends to watch.
        /// </summary>
        ///
        /// <param name="watchName">    Name of the watch line. This cannot be null. </param>
        /// <param name="rightObject">  The right object. This cannot be null. </param>
        void SendToWatch(
            [NotNull] string watchName,
            [NotNull] object rightObject );

        /// <summary>
        ///     Sends a background color.
        /// </summary>
        ///
        /// <param name="leftMessage">  Message to be displayed in the left window. This cannot be null. </param>
        /// <param name="colour">       The colour. </param>
        /// <param name="messageLevel"> (Optional) The logging level. </param>
        void SendBackgroundColor(
            [NotNull] string leftMessage,
            int colour,
            MessageLevelEnum messageLevel = MessageLevelEnum.Debug );

        /// <summary>
        ///     Sends a block of memory.
        /// </summary>
        ///
        /// <param name="leftMessage">  Message to be displayed in the left window. This cannot be null. </param>
        /// <param name="shortTitle">   Message to be displayed in the right window. This cannot be null. </param>
        /// <param name="address">      The address. </param>
        /// <param name="count">        Length of the block. </param>
        /// <param name="messageLevel"> True if is debug message, false if not. </param>
        void SendDump(
            [NotNull] string leftMessage,
            [NotNull] string shortTitle,
            [NotNull] byte[] address,
            int count,
            MessageLevelEnum messageLevel = MessageLevelEnum.Debug );

        /// <summary>
        ///     Creates trace window.
        /// </summary>
        ///
        /// <param name="traceWindowKey">   The trace window key. This cannot be null. </param>
        /// <param name="title">            The title. This cannot be null. </param>
        void CreateTraceWindow(
            [NotNull] string traceWindowKey,
            [NotNull] string title );

        /// <summary>
        ///     Creates watch window.
        /// </summary>
        ///
        /// <param name="watchWindowKey">   The watch window key. This cannot be null. </param>
        /// <param name="title">            The title. This cannot be null. </param>
        void CreateWatchWindow(
            [NotNull] string watchWindowKey,
            [NotNull] string title );

        /// <summary>
        ///     Clean window.
        /// </summary>
        ///
        /// <param name="traceWindowKey">   The trace window key. This cannot be null. </param>
        void CleanTraceWindow(
            [NotNull] string traceWindowKey );

        /// <summary>
        ///     Clean watch window.
        /// </summary>
        ///
        /// <param name="watchWindowKey">   The watch window key. This cannot be null. </param>
        void CleanWatchWindow(
            [NotNull] string watchWindowKey );

        /// <summary>
        ///     Sends to window.
        /// </summary>
        ///
        /// <param name="traceWindowKey">   The trace window key. This cannot be null. </param>
        /// <param name="leftMessage">      Message to be displayed in the left window. This cannot be null. </param>
        /// <param name="messageLevel">     (Optional) The logging level. </param>
        void SendToTraceWindow(
            [NotNull] string traceWindowKey,
            [NotNull] string leftMessage,
            MessageLevelEnum messageLevel = MessageLevelEnum.Debug );

        /// <summary>
        ///     Sends to window.
        /// </summary>
        ///
        /// <param name="traceWindowKey">   The trace window key. This cannot be null. </param>
        /// <param name="leftMessage">      Message to be displayed in the left window. This cannot be null. </param>
        /// <param name="rightMessage">     Message to be displayed in the right window. This cannot be null. </param>
        /// <param name="messageLevel">     (Optional) The logging level. </param>
        void SendToTraceWindow(
            [NotNull] string traceWindowKey,
            [NotNull] string leftMessage,
            [NotNull] string rightMessage,
            MessageLevelEnum messageLevel = MessageLevelEnum.Debug );

        /// <summary>
        ///     Overload for send to a trace window which takes an object (prevents boxing).
        /// </summary>
        ///
        /// <typeparam name="T">    Generic type parameter. </typeparam>
        /// <param name="traceWindowKey">   The trace window key. This cannot be null. </param>
        /// <param name="leftMessage">      Message to be displayed in the left window. This cannot be null. </param>
        /// <param name="rightMessage">     Object to be displayed in the right window. This cannot be null. </param>
        /// <param name="messageLevel">     (Optional) The logging level. </param>
        void SendToTraceWindow< T >(
            [NotNull] string traceWindowKey,
            [NotNull] string leftMessage,
            [NotNull] T rightMessage,
            MessageLevelEnum messageLevel = MessageLevelEnum.Debug );

        /// <summary>
        ///     Sends a dump to window.
        /// </summary>
        ///
        /// <param name="traceWindowKey">   The trace window key. This cannot be null. </param>
        /// <param name="leftMessage">      Message to be displayed in the left window. This cannot be null. </param>
        /// <param name="shortTitle">       The short title to be displayed in the right window. This cannot be null. </param>
        /// <param name="memoryDump">       The memory dump. This may be null. </param>
        /// <param name="count">            Length of the block. </param>
        /// <param name="messageLevel">     (Optional) The logging level. </param>
        void SendDumpToTraceWindow(
            [NotNull] string traceWindowKey,
            [NotNull] string leftMessage,
            [NotNull] string shortTitle,
            [CanBeNull] byte[] memoryDump,
            short count,
            MessageLevelEnum messageLevel = MessageLevelEnum.Debug );

        /// <summary>
        ///     Sends to watch window.
        /// </summary>
        ///
        /// <typeparam name="T">    Generic type parameter. </typeparam>
        /// <param name="watchWindowKey">   The watch window key. This cannot be null. </param>
        /// <param name="watchName">        Name of the watch line. This cannot be null. </param>
        /// <param name="rightObject">      The right object. This cannot be null. </param>
        void SendToWatchWindow< T >(
            [NotNull] string watchWindowKey,
            [NotNull] string watchName,
            [CanBeNull] T rightObject );

        /// <summary>
        ///     Clears and re-ups the watch window with data from the given dictionary.
        /// </summary>
        ///
        /// <typeparam name="TKey">     Type of the key. </typeparam>
        /// <typeparam name="TLookup">  Type of the lookup. </typeparam>
        /// <param name="watchWindowKey">       The watch window key. This cannot be null. </param>
        /// <param name="watchWindowContents">  The watch window contents. This cannot be null. </param>
        void ResetWatchWindow< TKey, TLookup >(
            [NotNull] string watchWindowKey,
            [NotNull] IDictionary< TKey, TLookup > watchWindowContents ) where TLookup : class, ILookup< TKey >;

        /// <summary>
        ///     Clears all of the watch windows.
        /// </summary>
        void ClearWatchWindows();

        #endregion
    }
}