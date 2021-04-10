using System;
using Skade.CrossCutting.Core.Interfaces.Services;

namespace Skade.CrossCutting.Common.Services
{
    /// <summary>
    ///     A service for accessing date/time information.
    /// </summary>
    ///
    /// <seealso cref="IDateTimeService"/>
    public class DateTimeService : IDateTimeService
    {
        #region Properties and Indexers

        /// <inheritdoc/>
        public DateTime Now => DateTime.Now;

        #endregion
    }
}