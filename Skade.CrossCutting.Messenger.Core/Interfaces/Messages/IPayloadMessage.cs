using JetBrains.Annotations;

namespace Skade.CrossCutting.Messenger.Core.Interfaces.Messages
{
    /// <summary>
    ///     Interface for Payload message.
    /// </summary>
    public interface IPayloadMessage< out T > : IBaseMessage
    {
        #region Properties and Indexers

        /// <summary>
        ///     Gets the Payload.
        /// </summary>
        ///
        /// <value>
        ///     The payload.
        /// </value>
        [NotNull]
        T Payload { get; }

        #endregion
    }
}