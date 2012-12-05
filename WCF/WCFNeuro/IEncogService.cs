using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WCFNeuro
{
    using Encog.ML;
    using Encog.ML.Data;
    using Encog.ML.Train;
    using Encog.Neural.Networks;

    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract(CallbackContract = typeof(IEncogCLientCallBackServices),SessionMode = SessionMode.Required)]
    public interface IEncogService
    {
        
        [OperationContract]
        BasicNetwork LoadBasicNetwork(string networkName);

        [OperationContract]
        void SaveBasicNetwork(BasicNetwork networkName);

        [OperationContract]
        double GetComputeNetwork(IMLData networkdata,string network);

        [OperationContract]
        void CreateBasicNetwork(int inputs , int outputs , int  hidden1,int hidden2,string name);
        [OperationContract]
        void SaveTraining(IMLDataSet training,string name);

        [OperationContract]
        IMLDataSet LoadTraining(string name);

         
    }

   
    public interface IEncogCLientCallBackServices
    {
       
        IObservable<double> NetworkOutputs();
       
    }
}
