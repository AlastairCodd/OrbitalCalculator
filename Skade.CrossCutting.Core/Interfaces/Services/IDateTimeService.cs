using System;

namespace Skade.CrossCutting.Core.Interfaces.Services
{
    /// <summary>
    ///     Interface for date time service.
    /// </summary>
    public interface IDateTimeService
    {
        #region Properties and Indexers

        /// <summary>
        ///     Gets the Date/Time of the now.
        /// </summary>
        ///
        /// <value>
        ///     The now.
        /// </value>
        DateTime Now { get; }

        #endregion
    }
}