using Skade.CrossCutting.Localiser.Core.Enumerations;
using Skade.CrossCutting.Messenger.Core.Interfaces.Messages;

namespace Skade.CrossCutting.Localiser.Core.Interfaces.Messages
{
    /// <summary>
    ///     Interface for Language Changed Message.
    /// </summary>
    public interface ILanguageChangedMessage : IPayloadMessage< LanguageEnum > { }
}