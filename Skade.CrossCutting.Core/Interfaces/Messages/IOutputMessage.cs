using Skade.CrossCutting.Core.Interfaces.Lookups;
using Skade.CrossCutting.Messenger.Core.Interfaces.Messages;

namespace Skade.CrossCutting.Core.Interfaces.Messages
{
    /// <summary>
    ///     Interface for output message.
    /// </summary>
    public interface IOutputMessage : IPayloadMessage< IWatchLookup > { }
}