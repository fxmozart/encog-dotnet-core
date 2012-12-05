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
using Encog.MathUtil.Matrices;
using Encog.ML.Data;
using Encog.ML.Data.Basic;
using Encog.Neural.SOM;
using Encog.Neural.SOM.Training.Neighborhood;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Encog.Neural.Networks.Training.Competitive
{
    using System;
    using ML.SVM;
    using ML.SVM.Training;

    [TestClass]
    public class TestCompetitive
    {
        public static double[][] SOMInput = {
                                                 new[] {0.0, 0.0, 1.0, 1.0},
                                                 new[] {1.0, 1.0, 0.0, 0.0}
                                             };



        public static double[][] SOMInput2 = {
                                                new[] {0.34, 0.010, -0.45, 0.76},
                                                 new[] {0.66, -0.3, -0.56, 0.45},
                                               new[] {0.89, -0.12, -0.98, 0.45},
                                                 new[] {0.55, -0.13, -0.46, -0.25}
                                                 
                                             };

        // Just a random starting matrix, but it gives us a constant starting point
        public static double[][] MatrixArray = {
                                                    new[]
                                                        {
                                                            0.9950675732277183, -0.09315692732658198, 0.9840257865083011,
                                                            0.5032129897356723
                                                        },
                                                    new[]
                                                        {
                                                            -0.8738960119753589, -0.48043680531294997, -0.9455207768842442,
                                                            -0.8612565984447569
                                                        }
                                                };

        [TestMethod]
        public void TestSOM()
        {
            // create the training set
            IMLDataSet training = new BasicMLDataSet(
                SOMInput, null);

            // Create the neural network.
            var network = new SOMNetwork(4, 2) {Weights = new Matrix(MatrixArray)};

            var train = new BasicTrainSOM(network, 0.4,
                                          training, new NeighborhoodSingle()) {ForceWinner = true};


            int iteration = 0;

            for (iteration = 0; iteration <= 100; iteration++)
            {
                train.Iteration();
            }

            IMLData data1 = new BasicMLData(
                SOMInput[0]);
            IMLData data2 = new BasicMLData(
                SOMInput[1]);

            int result1 = network.Classify(data1);
            int result2 = network.Classify(data2);

            Console.WriteLine(result1 + " result 2 :"+result2);
            Assert.IsTrue(result1 != result2);
        }

        [TestMethod]
        public void TestSOM2()
        {
            // create the training set
            IMLDataSet training = new BasicMLDataSet(
                SOMInput2, null);

            // Create the neural network.
            var network = new SOMNetwork(4,4);


             

            var train = new BasicTrainSOM(network, 0.01,
                                       training, new NeighborhoodSingle()) { ForceWinner = true };


            int iteration = 0;

            for (iteration = 0; iteration <= 1000; iteration++)
            {
                train.Iteration();
            }

         
            IMLData data1 = new BasicMLData(
                SOMInput2[2]);
            IMLData data2 = new BasicMLData(
                SOMInput2[0]);

            IMLData data3 = new BasicMLData(
               SOMInput2[1]);
            IMLData data4 = new BasicMLData(
                SOMInput2[3]);

            int result1 = network.Classify(data1);
            int result2 = network.Classify(data2);
            int result3 = network.Classify(data3);
            int result4 = network.Classify(data4);

           
            Console.WriteLine("Winner in someinput 2 "+network.Winner(new BasicMLData(SOMInput2[0])));

            Console.WriteLine("First  :" +result1);
            Console.WriteLine("Second "+result2);
            Console.WriteLine("Third  :" + result3);
            Console.WriteLine("Fourth " + result4);

            Assert.IsTrue(result1 != result2);

            train.TrainPattern(new BasicMLData(SOMInput2[2]));
            Console.WriteLine("After training pattern: " + network.Winner(new BasicMLData(SOMInput2[1])));




            var result = new SupportVectorMachine(4, SVMType.SupportVectorClassification, KernelType.Sigmoid);
            training = new BasicMLDataSet(
                SOMInput2, SOMInput2);
            SVMTrain trainsvm = new SVMTrain(result, training);
           
            trainsvm.Iteration(50);
            

            result1 = result.Classify(data1);

            result2 = result.Classify(data2);
            result3 = result.Classify(data3);
            result4 = result.Classify(data4);
            

            Console.WriteLine("SVM classification : EURUSD 1 :"+result1 + "  GBPUSD:"+result2 + " EURCHF :"+result3+  " EURJPY:"+result4 );

        }
    }
}
