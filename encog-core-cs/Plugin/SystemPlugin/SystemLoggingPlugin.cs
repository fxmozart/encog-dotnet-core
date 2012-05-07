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
using System.Text;
using System.Threading;
using Encog.Engine.Network.Activation;
using Encog.Util.Logging;

namespace Encog.Plugin.SystemPlugin
{
    /// <summary>
    /// This is the built-in logging plugin for Encog. This plugin provides simple
    /// file and console logging.
    /// </summary>
    ///
    public class SystemLoggingPlugin : IEncogPluginLogging1
    {
        /// <summary>
        /// The current level.
        /// </summary>
        ///
        private EncogLogging.LogLevel currentLevel;

        /// <summary>
        /// True if we are logging to the console.
        /// </summary>
        ///
        private bool logConsole;

        /// <summary>
        /// Construct the object.
        /// </summary>
        public SystemLoggingPlugin()
        {
            currentLevel = EncogLogging.LogLevel.Warn;
            logConsole = false;
        }

        #region EncogPluginType1 Members

        /// <summary>
        /// Not used for this type of plugin.
        /// </summary>
        ///
        /// <param name="gradients">Not used.</param>
        /// <param name="layerOutput">Not used.</param>
        /// <param name="weights">Not used.</param>
        /// <param name="layerDelta">Not used.</param>
        /// <param name="af">Not used.</param>
        /// <param name="index">Not used.</param>
        /// <param name="fromLayerIndex">Not used.</param>
        /// <param name="fromLayerSize">Not used.</param>
        /// <param name="toLayerIndex">Not used.</param>
        /// <param name="toLayerSize">Not used.</param>
        public void CalculateGradient(double[] gradients,
                                      double[] layerOutput, double[] weights,
                                      double[] layerDelta, IActivationFunction af,
                                      int index, int fromLayerIndex, int fromLayerSize,
                                      int toLayerIndex, int toLayerSize)
        {
        }

        /// <summary>
        /// Not used for this type of plugin.
        /// </summary>
        ///
        /// <param name="weights">Not used.</param>
        /// <param name="layerOutput">Not used.</param>
        /// <param name="startIndex">Not used.</param>
        /// <param name="outputIndex">Not used.</param>
        /// <param name="outputSize">Not used.</param>
        /// <param name="inputIndex">Not used.</param>
        /// <param name="inputSize">Not used.</param>
        /// <returns>Not used.</returns>
        public int CalculateLayer(double[] weights,
                                  double[] layerOutput, int startIndex,
                                  int outputIndex, int outputSize, int inputIndex,
                                  int inputSize)
        {
            return 0;
        }

        /// <summary>
        /// Set the logging level.
        /// </summary>
        public EncogLogging.LogLevel LogLevel
        {
            get { return currentLevel; }
            set { currentLevel = value; }
        }


        /// <inheritdoc/>
        public String PluginDescription
        {
            get
            {
                return "This is the built in logging for Encog, it logs "
                       + "to either a file or System.out";
            }
        }


        /// <inheritdoc/>
        public String PluginName
        {
            get { return "HRI-System-Logging"; }
        }


        /// <value>Returns the service type for this plugin. This plugin provides
        /// the system calculation for layers and gradients. Therefore, this
        /// plugin returns SERVICE_TYPE_CALCULATION.</value>
        public ServiceType PluginServiceType
        {
            // get { return EncogPluginBaseConst.SERVICE_TYPE_LOGGING; }
            get { return ServiceType.LOGGING; }
        }


        /// <value>This is a type-1 plugin.</value>
        public EncogPluginType PluginType
        {
            get
            {
                return EncogPluginType.Logging;
            }
        }


        /// <summary>
        /// Log the message.
        /// </summary>
        ///
        /// <param name="level">The logging level.</param>
        /// <param name="message">The logging message.</param>
        public void Log(EncogLogging.LogLevel level, String message)
        {
            if (level.CompareTo(currentLevel) > 0)
            {
                DateTime now = DateTime.Now;
                var line = new StringBuilder();
                line.Append(now.ToString());
                line.Append(" [");
                //switch (level)
                //{
                //    case EncogLogLevel.Critical:
                //        line.Append("CRITICAL");
                //        break;
                //    case EncogLogging.LevelError:
                //        line.Append("ERROR");
                //        break;
                //    case EncogLogging.LogLevel.Info:
                //        line.Append("INFO");
                //        break;
                //    case EncogLogging.LogLevel.Debug:
                //        line.Append("DEBUG");
                //        break;
                //    default:
                //        line.Append("?");
                //        break;
                //}
                switch (level)
                {
                    case EncogLogging.LogLevel.None:
                        //Lets do nothing as we will not log.
                        break;
                    case EncogLogging.LogLevel.Trace:
                        line.Append("TRACE");
                        break;
                    case EncogLogging.LogLevel.Debug:
                        line.Append("DEBUG");
                        break;
                    case EncogLogging.LogLevel.Info:
                        line.Append("INFO");
                        break;
                    case EncogLogging.LogLevel.Warn:
                        line.Append("WARN");
                        break;
                    case EncogLogging.LogLevel.Error:
                        line.Append("ERROR");
                        break;
                    case EncogLogging.LogLevel.Critical:
                        line.Append("CRITICAL");
                        break;
                }
                line.Append("][");
                line.Append(Thread.CurrentThread.Name);
                line.Append("]: ");
                line.Append(message);

                if (logConsole)
                {
                    if (currentLevel == EncogLogging.LogLevel.Error)
                        Console.Error.WriteLine(line.ToString());
                    //if (currentLevel.CompareTo(EncogLogging.LogLevel.Error) > 0)
                    //{
                    //    Console.Error.WriteLine(line.ToString());
                    //}
                    else
                    {
                        Console.Out.WriteLine(line.ToString());
                    }
                }
                System.Diagnostics.Debug.WriteLine(line);
            }
        }

        /// <summary>
        ///  Logs a trace trace message
        /// </summary>
        /// <param name="message"> The message to log. </param>
        public void LogTrace(string message)
        {
            Log(EncogLogging.LogLevel.Trace, message);
        }

        /// <summary>
        ///  Logs a debug message
        /// </summary>
        /// <param name="message"> The message to log. </param>
        public void LogDebug(string message)
        {
            Log(EncogLogging.LogLevel.Debug, message);
        }

        /// <inheritdoc/>
        public void Log(EncogLogging.LogLevel level, Exception t)
        {
            Log(level, t.ToString());
        }

        /// <inheritdoc/>
        public void Log(string message)
        {
            Log(EncogLogging.LogLevel.Info, message);
        }

        #endregion

        /// <summary>
        /// Start logging to the console.
        /// </summary>
        ///
        public void StartConsoleLogging()
        {
            StopLogging();
            logConsole = true;
            LogLevel = EncogLogging.LogLevel.Info;

        }

        /// <summary>
        /// Stop any console or file logging.
        /// </summary>
        ///
        public void StopLogging()
        {
            logConsole = false;
        }
    }
}
