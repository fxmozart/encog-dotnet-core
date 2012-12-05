using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WCFNeuro
{
    using System.Collections.Concurrent;
    using System.IO;
    using System.Reflection;
    using System.Runtime.Remoting.Contexts;
    using Encog.Engine.Network.Activation;
    using Encog.ML.Data;
    using Encog.ML.Data.Basic;
    using Encog.ML.Train;
    using Encog.Neural.Networks;
    using Encog.Neural.Networks.Layers;
    using Extensions;
    using System.Reactive;
    using System.Reactive.Linq;

    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class EncogService : IEncogService
    {
        #region Implementation of IEncogService

        public static ConcurrentDictionary<string, IEncogCLientCallBackServices> Subscribers =
          new ConcurrentDictionary<string, IEncogCLientCallBackServices>();

        public static int CurrentSubs = 0;


        public EncogService()
        {
            
        }
        
        public void Connect(string clientName)
        {
            IEncogCLientCallBackServices contex = OperationContext.Current.GetCallbackChannel<IEncogCLientCallBackServices>();
            //Lets add the current connector to our dictionary. 
            Subscribers.TryAdd(clientName, contex);

            //Lets inc the number of connectors we have.
            System.Threading.Interlocked.Increment(ref CurrentSubs);
        }
        static public string AssemblyDirectory
        {
            get
            {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }
        public BasicNetwork LoadBasicNetwork(string networkName)
        {
            var net = Encog.Util.NetworkUtil.NetworkUtility.LoadNetwork(AssemblyDirectory + Path.DirectorySeparatorChar, networkName + ".net");
            return net;
        }

        public void SaveBasicNetwork(BasicNetwork networkName)
        {
            Encog.Util.NetworkUtil.NetworkUtility.SaveNetwork(AssemblyDirectory + Path.DirectorySeparatorChar,
                                                              networkName + ".net",networkName);
        }

        public double GetComputeNetwork(IMLData networkdata, string network)
        {  
            var net =  Encog.Util.NetworkUtil.NetworkUtility.LoadNetwork(AssemblyDirectory + Path.DirectorySeparatorChar,network+".net");
            return net.Compute(networkdata)[0];
        }

        public void CreateBasicNetwork(int inputs, int outputs, int hidden1, int hidden2, string name)
        {
            BasicNetwork net = CreateBasicNetwork(inputs, outputs, hidden1, new ActivationTANH(), hidden1);

            Encog.Util.NetworkUtil.NetworkUtility.SaveNetwork(AssemblyDirectory + Path.DirectorySeparatorChar,
                                                              name + ".net", net);




        }

        public BasicNetwork CreateBasicNetwork(int input,int output,int hidden,IActivationFunction function,int hidden1=0)
        {
            var network = new BasicNetwork();
            network.AddLayer(new BasicLayer(function,false,input));
            network.AddLayer(new BasicLayer(function,true,hidden));
            if(hidden1>0)
            network.AddLayer(new BasicLayer(new ActivationTANH(),true,hidden1));
            network.AddLayer(new BasicLayer(function,false,output));
            network.Structure.FinalizeStructure();
            network.Reset();
            return network;

        }
        public void SaveTraining(IMLDataSet training, string name)
        {
              Encog.Util.NetworkUtil.NetworkUtility.SaveTraining(AssemblyDirectory+Path.DirectorySeparatorChar+name+".trai",training);
        }

        public IMLDataSet LoadTraining(string name)
        {
            return
                Encog.Util.NetworkUtil.NetworkUtility.LoadTraining(AssemblyDirectory + Path.DirectorySeparatorChar +
                                                                   name +
                                                                   ".trai") ;


        }

        /// <summary>
        /// Computes the network with the given inputs and network.
        /// </summary>
        /// <param name="ideal"></param>
        /// <param name="input"></param>
        public void ComputeNetworkWithArrays(string networkName, IObservable<double[]> input)
        {
            //var net = LoadBasicNetwork(networkName);
            //net.Compute(input.Subscribe(OnNext));

           
            //input.Subscribe(OnNext);


            //                              ;

        }

        //private double OnNext(double[] doubles)
        //{
        //                                                                                         ());
        //}

        
        
        #endregion
    }
}
