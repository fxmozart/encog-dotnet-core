using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Encog.Plugin
{
    /// <summary>
    ///  All enums for plugins are stored in this class.
    /// </summary>

    /// <summary>
    ///  Bitfield of flags for specifying PluginType.
    /// </summary>
    [Flags]
    public enum EncogPluginType
    {
        None = 0,
        /// <summary>
        ///  A plugin used for logging.
        /// </summary>
        Logging = 1,

        /// <summary>
        ///  A plugin helping for training methods , adding, or modifying training methods.
        /// </summary>
        Training = 2,
        /// <summary>
        ///  A plugin modifying or adding new methods (Networks).
        /// </summary>
        Methods = 3,
        /// <summary>
        ///  A plugin adding or modifying services (see Service type enums).
        /// </summary>
        Services = 4,
    }

    /// <summary>
    ///  Values that represent ServiceType.
    /// </summary>
    public enum ServiceType
    {
        /// <summary>
        ///  A plugin that does nothing (basically used for testing , or when there has been a problem parsing the plugin service type.
        /// </summary>
        None = 0,
        /// <summary>
        /// A general plugin, you can have multiple plugins installed that provide
        /// general services.
        /// </summary>
        GENERAL = 1,

        /// <summary>
        /// A special plugin that provides logging. You may only have one logging
        /// plugin installed.
        /// </summary>
        LOGGING = 2,

        /// <summary>
        ///   This plugin provides
        /// the system calculation for layers and gradients. Therefore, this plugin returns Calculation.
        /// </summary>
        Calculation = 3,
    }

    


}
