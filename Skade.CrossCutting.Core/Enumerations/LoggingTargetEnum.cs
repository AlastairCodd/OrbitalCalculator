using System;

namespace Skade.CrossCutting.Core.Enumerations
{
    /// <summary>
    ///     A bit-field of flags for specifying Logging Targets.
    /// </summary>
    [Flags]
    public enum LoggingTargetEnum
    {
        LogFile = 1,
        TraceTool = 2,
        OutputPanel = 4
    }
}