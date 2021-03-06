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
using System.IO;
using ConsoleExamples.Examples;
using Encog.Examples.RangeandMarket;
using Encog.ML.Data.Basic;
using Encog.Neural.Networks;
using Encog.Util.NetworkUtil;

namespace Encog.Examples.Analyzer
{
    class MarketAnalyzer :IExample
    {
        public static ExampleInfo Info
        {
            get
            {
                var info = new ExampleInfo(
                    typeof(MarketAnalyzer),
                    "Range",
                    "Analyzes ranges and predicts them.",
                    "Predicts ranges with an elmhan neural network with a combo of 3 strategies."+
                    "\nYou can do range randomtrainer [numberofInputs] [OutputSize]")
                ;
                return info;
            }

        }

        #region IExample Members
        private IExampleInterface app;
        public void Execute(IExampleInterface app)
        {
            this.app = app;
            FileInfo dataDir = new FileInfo(Environment.CurrentDirectory);

            if (String.Compare(app.Args[0], "randomtrainer", true) == 0)
            {
                if (app.Args.Length > 1)
                {
                    RandomTrainer.RandomTrainerMethod(Convert.ToInt16(app.Args[1]), Convert.ToInt16(app.Args[2]));
                    MakeAPause();
                    app.Exit();
                }
                else
                {
                    Console.WriteLine(@"You didn't input enough args in your request, will default to 3000 inputs , and 50 prediction size");
                    Console.WriteLine(@"Error % "+ RandomTrainer.RandomTrainerMethod(3000, 50));
                   
                   
                }

                Console.ReadKey();
                return;
            }


            if (String.Compare(app.Args[0], "eval", true) == 0)
            {
                if (app.Args.Length > 0)
                {
                    //We have enough arguments, lets test them.
                    if (File.Exists(app.Args[1]))
                    {

                        BasicMLDataSet set = CreateEval.CreateEvaluationSetAndLoad(app.Args[1], CONFIG.EvalHowMany, CONFIG.EvalStartFrom, CONFIG.Inputs,
                                                  CONFIG.Outputs);

                         //create our network.
                        BasicNetwork network =
                            (BasicNetwork) NetworkUtility.LoadNetwork(CONFIG.DIRECTORY, CONFIG.NetWorkFile);
                        CreateEval.EvaluateNetworks(network, set);
                        MakeAPause();
                        return;
                    }


                }
            }



            if (String.Compare(app.Args[0], "prune", true) == 0)
            {

                //Start pruning.
                Console.WriteLine("Starting the pruning process....");

                Prunes.Incremental(new FileInfo(CONFIG.DIRECTORY), CONFIG.NetWorkFile,
                            CONFIG.TrainingFile);

                MakeAPause();
                app.Exit();


            }
            if (String.Compare(app.Args[0], "train", true) == 0)
            {
                if (app.Args.Length> 0)
                {
                    //We have enough arguments, lets test them.
                    if (File.Exists(app.Args[1]))
                    {
                        //the file exits lets build the training.

                        //create our basic ml dataset.
                       BasicMLDataSet set = CreateEval.CreateEvaluationSetAndLoad(app.Args[1], CONFIG.HowMany, CONFIG.StartFrom, CONFIG.Inputs,
                                                   CONFIG.Outputs);

                        //create our network.
                        BasicNetwork network = (BasicNetwork) CreateEval.CreateElmanNetwork(CONFIG.Inputs, CONFIG.Outputs);

                        //Train it..

                        double LastError = CreateEval.TrainNetworks(network, set);

                        Console.WriteLine("NetWork Trained to :" + LastError);
                        NetworkUtility.SaveTraining(CONFIG.DIRECTORY, CONFIG.TrainingFile, set);
                        NetworkUtility.SaveNetwork(CONFIG.DIRECTORY, CONFIG.NetWorkFile, network);
                        Console.WriteLine("Network Saved to :" + CONFIG.DIRECTORY + " File Named :" +
                                          CONFIG.NetWorkFile);

                        Console.WriteLine("Training Saved to :" + CONFIG.DIRECTORY + " File Named :" +
                                         CONFIG.TrainingFile);
                        MakeAPause();

                        app.Exit();
                        return;
                    }
                    else
                    {
                        Console.WriteLine("Couldnt find the file :" + app.Args[2].ToString());
                        Console.WriteLine("Exiting");
                        MakeAPause();
                        app.Exit();
                        return;
                    }
                }
            }
            else
            {
                Console.WriteLine("Couldnt understand your command..");
                Console.WriteLine(
                    "Valid commands are : RandomTrainer or Randomtrainer [inputs] [output] , or Train [File]");
                Console.WriteLine(
                   "Valid commands are : Range Prune, to prune your network.");
                Console.WriteLine(
                   "Valid commands are : Range eval , to evaluate your network.");
                    MakeAPause();
                app.Exit();
            }
           

        }

        private static void MakeAPause()
        {
            Console.WriteLine("Press a key to continue ..");
            Console.ReadKey();
        }

        #endregion




      
        
        public static class CONFIG
        {
            public const string DIRECTORY = @"C:\EncogOutput\";
           
            public static string DataFileDirectory = @"C:\Datas\";
            public static string NetWorkFile = "NetworkfileMarketAnalyzer.eg";
            public static string TrainingFile = "TrainingFileMarketAnalyzer.ega";
            public const int HowMany = 1200;
            public const int StartFrom = 1;
            public const int Inputs = 500;
            public const int Outputs = 10;
            public const int HiddenNeuron1 = 5;

            public const int EvalHowMany = 1200;
            public const int EvalStartFrom = 1200;
        }
    }
}
