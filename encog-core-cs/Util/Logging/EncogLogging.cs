//
// Encog(tm) Core v3.1 - .Net Version
// http://www.heatonresearch.com/encog/
//
// Copyright 2008-2012 Heaton Research, Inc.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//  http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//   
// For more information on Heaton Research copyrights, licenses 
// and trademarks visit:
// http://www.heatonresearch.com/copyright
//
using System;

namespace Encog.Util.Logging
{
    using Encog.Plugin;


    /// <summary>
    /// This class provides logging for Encog. Programs using Encog can make use of
    /// it as well. All logging is passed on to the current logging plugin. By
    /// default the SystemLoggingPlugin is used.
    /// </summary>
    public class EncogLogging
    {
        /// <value>The current logging level.</value>
        public EncogLogging.LogLevel CurrentLevel
        {
            get { return EncogFramework.Instance.LoggingPlugin.LogLevel; }
        }

        /// <summary>
        /// Log the message.
        /// </summary>
        ///
        /// <param name="level">The level to log at.</param>
        /// <param name="message">The message to log.</param>
        public static void Log(LogLevel level, String message)
        {
            EncogFramework.Instance.LoggingPlugin.Log(level, message);
        }

        /// <summary>
        /// Log the error.
        /// </summary>
        /// <param name="level">The level to log at.</param>
        /// <param name="t">The exception to log.</param>
        public static void Log(LogLevel level, Exception t)
        {
            EncogFramework.Instance.LoggingPlugin.Log(level, t);
        }

        /// <summary>
        /// Log the error at ERROR level.
        /// </summary>
        /// <param name="t">The exception to log.</param>
        public static void Log(Exception t)
        {
            Log(LogLevel.Error, t);
        }

        /// <summary>
        ///  Values that represent LogLevel.
        /// </summary>
        public enum LogLevel
        {
            /// <summary>
            ///  This stops all logging.
            /// </summary>
            None = 0,

            /// <summary>
            /// This is the lowest level of tracing.
            /// </summary>
            Trace = 1,

            /// <summary>
            ///  All debugging information will be printed to the logger.
            /// </summary>
            Debug = 2,

            /// <summary>
            ///  All informational logs will be printed to the current logger.
            /// </summary>
            Info = 3,

            /// <summary>
            ///  All warnings will be printed to the current logger (all below won't).
            /// </summary>
            Warn = 4,

            /// <summary>
            ///  Only errors are printed to the current logger.
            /// </summary>
            Error = 5,

            /// <summary>
            ///  This kind of errors happen and will most likely crash your instance.
            /// </summary>
            Critical = 6
        }
    }
}
