// -----------------------------------------------------------------------
// <copyright file="ClooTrainer.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace ClooTrainer
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using System.Threading;

    using Cloo;
    using Cloo.Bindings;

    using Encog.Engine.Network.Train.Prop;
    using Encog.Engine.Opencl;
    using Encog.Engine.Opencl.Exceptions;
    using Encog.ML.Data;
    using Encog.ML.Train;
    using Encog.ML.Train.Strategy;
    using Encog.ML.Train.Strategy.End;
    using Encog.Neural.Networks;
    using Encog.Neural.Networks.Training;
    using Encog.Neural.Networks.Training.Anneal;
    using Encog.Neural.Networks.Training.Lma;
    using Encog.Neural.Networks.Training.Propagation.Back;
    using Encog.Neural.Networks.Training.Propagation.Resilient;
    using Encog.Util;
    using Encog.Util.Banchmark;
    using Encog.Util.Simple;

    /// <summary>
    /// Trains a network given the imldataset and network using cloo
    /// </summary>
    public class ClooTrainer
    {


        private EncogCLDevice device;


        public OpenCLTrainingProfile GetTrainingProfile
        {
            get
            {
                return new OpenCLTrainingProfile(device);
            }
        }

        public Tuple<double, double> ClooTrain(IMLDataSet training, BasicNetwork network)
        {

            var stopwatch = new Stopwatch();
            stopwatch.Start();
            try
            {

                Console.WriteLine("Starting the Cloo training....");

                ClooDevice cloodevices = new ClooDevice();


                FoundDevice found = (FoundDevice)cloodevices.ClooDevices.First();

                this.device = found.Device;



                OpenCLTrainingProfile profile = new OpenCLTrainingProfile(this.device);


                IMLTrain train = new ResilientPropagation(network, training);

                // train the neural network


                var stop = new StopTrainingStrategy();

                ICalculateScore score = new TrainingSetScore(train.Training);
                IMLTrain trainAlt = new NeuralSimulatedAnnealing(network, score, 10, 2, 100);
                EndIterationsStrategy endIterationsStrategy = new EndIterationsStrategy(1000);
                EndMinutesStrategy endMinutesStrategy = new EndMinutesStrategy(4);
                EndMaxErrorStrategy endMax = new EndMaxErrorStrategy(1);

                train.AddStrategy(endIterationsStrategy);
                train.AddStrategy(endMinutesStrategy);
                train.AddStrategy(endMax);

                //   train.AddStrategy(new HybridStrategy(trainAlt));
                train.AddStrategy(stop);


                //  var train = new ResilientPropagation(network, training);


                int epoch = 0;
                while (epoch++ < 100)
                {
                    train.Iteration(10);

                    //Dispatcher.BeginInvoke(DispatcherPriority.Input, new ThreadStart(() =>
                    //{
                    //    this.StatusText.Content = "Iteration:" + train.IterationNumber;
                    //}));

                    Console.WriteLine("Cloo training iteration : " + train.IterationNumber + " Error :" + train.Error);
                }


                stopwatch.Stop();
                long clTime = stopwatch.ElapsedMilliseconds;


                Console.WriteLine(String.Format("Total elapsed in Cloo training:{0} For error :{1}", stopwatch.Elapsed, train.Error));
                // this.sendBenchmark = "" + clTime;


                return new Tuple<double, double>(train.Error, stopwatch.ElapsedTicks);
            }

            catch (OutOfOpenCLResources ex)
            {
                Console.WriteLine("Your GPU is out of resources.\nThis could also mean that your OS is timing your GPU out.  See\n http://www.heatonresearch.com/encog/troubleshooting : " + ex);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString(), "Error");
            }
            finally
            {
                //Dispatcher.BeginInvoke(DispatcherPriority.Input, new ThreadStart(() =>
                //{
                //    this.ButtonDefaults.IsEnabled = true;
                //    this.ButtonEncog.IsEnabled = true;
                //    this.StatusText.Content = "Ready.";
                //}));

                Console.WriteLine("Finished the cloo training time : " + stopwatch.Elapsed);
            }
            return null;
        }

    }
}
