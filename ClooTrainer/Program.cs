using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClooTrainer
{
    using System.IO;
    using System.Reflection;

    using Encog.ML.Data;
    using Encog.Neural.Networks;

    class Program
    {
        static void Main(string[] args)
        {

            try
            {
                if (args.Length < 4)
                    throw new Exception("You need to input in the format -n pathtoNetworkfile -t PathtoTrainingFile");
               var tuple= GetArgs(args);
                ClooTrainer trainer = new ClooTrainer();

                trainer.ClooTrain(tuple.Item2, tuple.Item1);



            }
            catch (Exception ex)
            {

                Console.WriteLine(ex);
            }
            
        }
        public static string AssemblyDirectory
        {
            get
            {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }

        private static Tuple<BasicNetwork,IMLDataSet> GetArgs(string[] args)
        {

            BasicNetwork net = null;
            IMLDataSet set;
            if (args[0] == "-n")
            {
              //Lets get the directory specified .
              net = Encog.Util.NetworkUtil.NetworkUtility.LoadNetwork(@args[1]);
            }
            if (args[2] == "-t")
            {
                //Load training file.
                set = Encog.Util.NetworkUtil.NetworkUtility.LoadTraining(@args[3]);

            }
            else
            {
                throw new Exception("You need to input in the format -n pathtoNetworkfile -t PathtoTrainingFile");
            }
            if(set !=null && net !=null)
            {
                return new Tuple<BasicNetwork, IMLDataSet>(net,set);
            }
            throw new Exception("Couldn't load network or training...");
        }
    }
}
