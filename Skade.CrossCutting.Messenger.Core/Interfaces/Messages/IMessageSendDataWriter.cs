namespace Skade.CrossCutting.Messenger.Core.Interfaces.Messages
{
    /// <summary>
    ///     Interface for message send data writer.
    /// </summary>
    public interface IMessageSendDataWriter
    {
        /// <summary>
        ///     Sets the time stamp.
        /// </summary>
        /// <value>
        ///     The time stamp.
        /// </value>
        long TimeStamp { set; }

        /// <summary>
        ///     Sets the sequence number.
        /// </summary>
        /// <value>
        ///     The sequence number.
        /// </value>
        ulong SequenceNumber { set; }
    }
}