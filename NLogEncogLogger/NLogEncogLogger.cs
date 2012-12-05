using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NLogEncogLogger
{
    using System.ComponentModel.Composition;

    using Encog;
    [Export(typeof(IStatusReportable))]
    public class NLogEncogLogger : IStatusReportable
    {
        #region Implementation of IStatusReportable

        private NLog.Logger mLogger = NLog.LogManager.GetCurrentClassLogger();
        /// <summary>
        /// Report on current status.
        /// </summary>
        ///
        /// <param name="total">The total amount of units to process.</param>
        /// <param name="current">The current unit being processed.</param>
        /// <param name="message">The message to currently display.</param>
        public void Report(int total, int current, string message)
        {
            mLogger.Info(() => String.Format("Report : {0} / {1} Message : {2}", current, total, message));

        }

        #endregion
    }
}
