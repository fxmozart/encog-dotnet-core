using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Encog.Examples.ObservableNetwork
{
    using System.Reactive.Linq;
    using ConsoleExamples.Examples;
    using Encog.Util.NetworkUtil;
    using Encog.Util.Simple;
    using ML.Data.Basic;

    public class ObservableExample : IExample
    {

        //Each input is taken and placed after the other until we have input x 10 -Slided -windowed inputs.
        private const int WindowSize = 10;


        #region Implementation of IExample
        public static ExampleInfo Info
        {
            get
            {
                var info = new ExampleInfo(
                    typeof(ObservableExample),
                    "Observable",
                    "A quick example that simulates neuronal activty in live situation by using observables",
                    "Use a feedforward neural network with observable to compute the network output ");
                return info;
            }
        }

        public void Execute(IExampleInterface app)
        {
            double[] firstinput = MakeInputs(150);
            double[] SecondInput = MakeInputs(150);
            double[] ThirdInputs = MakeInputs(150);
            double[] ideals = MakeInputs(150);
            //our set holds both the normilization and the imldataset, we can put as many inputs as needed.
            var set = EasyData.Load(ideals, WindowSize, firstinput, SecondInput, ThirdInputs, ideals);

            var network = EncogUtility.SimpleFeedForward(4, 100, 1, 1, false);

            EncogUtility.TrainConsole(network, set.Item1,0.3);

            //Simulate live data ..

            //double[] live1 = MakeInputs(150);
            //double[] Live2 = MakeInputs(150);
            //double[] live3 = MakeInputs(150);

            //var computes = EasyData.GetReadiedComputePair(WindowSize, live1, Live2, live3);


            //var query =
            //    from window in GenerateRandomDoubles().Window(TimeSpan.FromMilliseconds(20))
            //    from ohlc in window.Aggregate(new double[4], (doubles, d) => doubles)
            //    select ohlc;

            //Console.WriteLine("Got query : "+query.Count());

            GenerateRandomDoubles().Window(TimeSpan.FromMilliseconds(20)).Aggregate(new double[4], (doubles, d) => doubles)
                .Subscribe(doubles => Console.WriteLine("Network compute : " + network.Compute(new BasicMLData(doubles))[0]));
         
            //Console.WriteLine("Network computed denormalized : " + computes.Item2.Stats.DeNormalize(network.Compute(new BasicMLData(computes.Item1.ToArray()))[0]));

            Console.WriteLine("Done waiting for computes");
            Console.ReadLine();
        }
        /// <summary>
        /// Makes the inputs by randomizing with encog randomize , the normal random from net framework library.
        /// </summary>
        /// <param name="number">The number.</param>
        /// <returns></returns>
        public static double[] MakeInputs(int number)
        {
            Random rdn = new Random();
            Encog.MathUtil.Randomize.RangeRandomizer encogRnd = new Encog.MathUtil.Randomize.RangeRandomizer(-1, 1);
            double[] x = new double[number];
            for (int i = 0; i < number; i++)
            {
                x[i] = encogRnd.Randomize((rdn.NextDouble()));

            }
            return x;
        }

        
        public static IObservable<double> GenerateRandomDoubles(double initial = 0.5d, double GenerateBySeconds = 1)
        {
            var rand = new Random();

            var prices = Observable.Generate(
                initial,      //initial state.
                i => i>20000,   //generate until
                i => i + rand.NextDouble() - 0.5,//How to generate
                i => i,  //Transform
                i => TimeSpan.FromMilliseconds(GenerateBySeconds)   //How many by time
            );

         
            Console.WriteLine("Generating doubles : "+prices.Subscribe(Console.WriteLine));
            return prices;
        }


        #endregion
    }
}
