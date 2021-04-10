namespace Skade.CrossCutting.Core.Constants
{
    /// <summary>
    ///     A development constants.
    /// </summary>
    public static class DevConstants
    {
        public const string DevError = "Dev Error";
        public const string DevNote = "Dev Note";

        public const string InstanceIsAlreadyInitialised = DevError + ": Instance is already initialised";
        public const string ValueCannotBeNullOrWhitespace = DevError + ": Value cannot be null or whitespace";
    }
}