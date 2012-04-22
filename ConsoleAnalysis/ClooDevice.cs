// -----------------------------------------------------------------------
// <copyright file="ClooDevice.cs" company="Olivier ">
// Open source, no license.
// </copyright>
// -----------------------------------------------------------------------

namespace ConsoleAnalysis
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Encog.Engine.Opencl;

    /// <summary>
    /// Gets the first device that can be used by cloo.
    /// </summary>
    public class ClooDevice
    {

        List<FoundDevice> _devices = new List<FoundDevice>();
        public List<FoundDevice> ClooDevices
        {
            get
            {
                return _devices;
            }
            set
            {
                _devices = value;
            }
        }

        /// <summary>
        ///  Initializes a new instance of the ClooDevice class.
        ///  Inits all Cloo enabled devices to the CLooDevice list.
        /// </summary>
        public ClooDevice()
        {
            EncogCL cl = new EncogCL();

            foreach (EncogCLDevice device in cl.Devices)
            {
                //FounFoundDeviceList.Items.Add(new FoundDevice(device));
                ClooDevices.Add(new FoundDevice(device));
            }
        }
    }
}
