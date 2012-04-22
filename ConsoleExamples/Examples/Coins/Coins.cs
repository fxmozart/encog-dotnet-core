using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Encog.Examples.Coins
{
    using System.Collections;
    using System.Globalization;
    using ConsoleExamples.Examples;
    using Encog.Util;
    using Encog.Util.Arrayutil;
    using Encog.Util.Normalize;
    using Encog.Util.Normalize.Input;
    using Encog.Util.Normalize.Output.Nominal;
    using Engine.Network.Activation;
    using Lunar;
    using ML;
    using ML.Data;
    using ML.Data.Basic;
    using ML.Train;
    using MathUtil.Randomize;
    using Neural.Networks;
    using Neural.Networks.Layers;
    using Neural.Networks.Training;
    using Neural.Networks.Training.Anneal;
    using Neural.Networks.Training.Genetic;
    using Neural.Pattern;
    using Plugin.SystemPlugin;

    public class Coins : IExample
    {
        #region Implementation of IExample



        public static ExampleInfo Info
        {
            get
            {
                var info = new ExampleInfo(
                    typeof(Coins),
                    "coins",
                    "Play heads or tail by NN",
                    "Encog will try to play heads or tail game....");
                return info;
            }
        }

        #region IExample Members

        /// <summary>
        /// Program entry point.
        /// </summary>
        /// <param name="app">Holds arguments and other info.</param>
        public void Execute(IExampleInterface app)
        {

            //Lets make ONE coin draw for all our test .
            CoinOne coin = new CoinOne(500);

            BasicNetwork network = CreateNetwork();

            IMLTrain train;
            CoinageScore coinScores = new CoinageScore();
            if (app.Args.Length > 0 && System.String.Compare(app.Args[0], "anneal", System.StringComparison.InvariantCultureIgnoreCase) == 0)
            {
                train = new NeuralSimulatedAnnealing(network, coinScores, 10, 2, 100);
            }
            else
            {
                train = new NeuralGeneticAlgorithm(network, new NguyenWidrowRandomizer(), coinScores, 500, 0.1, 0.25);
            }

            int epoch = 1;

            for (int i = 0; i < 3; i++)
            {
                train.Iteration();
                Console.WriteLine(@"Training : Epoch #" + epoch + @" Score:" + train.Error);
                epoch++;
            }

            Console.WriteLine(@"\nHere's how encog played heads or tail with error :" + train.Error);
            network = (BasicNetwork)train.Method;
            var pilot = new NeuralCoin(network, false);
            Console.WriteLine(pilot.ScoreCoin());
            EncogFramework.Instance.Shutdown();
        }

        #endregion

        public static BasicNetwork CreateNetwork()
        {
            var pattern = new FeedForwardPattern { InputNeurons = 3 };
            pattern.AddHiddenLayer(50);
            pattern.OutputNeurons = 1;
            pattern.ActivationFunction = new ActivationTANH();

            //   ILayer p = new BasicLayer(new ActivationStep(0, 0, 1), false, 1);

            var network = (BasicNetwork)pattern.Generate();
            network.Reset();
            return network;

            //BasicNetwork net = new BasicNetwork();
            //net.AddLayer(new BasicLayer(new ActivationTANH(), false, 3));
            //net.AddLayer(new BasicLayer(new ActivationTANH(), true, 6));
            //net.AddLayer(new BasicLayer(new ActivationLinear(), false, 1));
            //net.Structure.FinalizeStructure();
            //net.Reset();
            //return net;

            //            BasicNetwork network = new BasicNetwork ( ) ;
            //network . AddLayer (new BasicLayer ( null , true , 2 ) ) ;
            //network . AddLayer (new BasicLayer (new ActivationSigmoid ( ) , true , 3 ) ) ;
            //network . AddLayer (new BasicLayer (new ActivationSigmoid ( ) , false , 1 ) ) ;
            //network . St ruc tur e . Fi n a l i z e S t r u c t u r e ( ) ;
            //network . Reset ( ) ;
        }
        #endregion
    }


    /// <summary>
    /// This class holds the heads / tails series results in the form of 1 or 0 (0 = Heads , 1 = Tails).
    /// Also holds an enumerator (this[1] that returns a coinFace (heads or tails enum)).
    /// </summary>
    public class CoinOne : IEnumerable<int>, IEnumerator<int>
    {

        public static List<int> CoinDraws = new List<int>();

        /// <summary>
        /// initializes a new coin results with the number of results needed.
        /// </summary>
        /// <param name="ResultsCount"></param>
        public CoinOne(int ResultsCount)
        {
            MakeGame(ResultsCount);
        }

        /// <summary>
        /// Makes the game by filling the array with new random 0 or 1 (heads or tails).
        /// </summary>
        /// <param name="ResultsCount">The results count.</param>
        private void MakeGame(int ResultsCount)
        {
            Random rnd = new Random(1);
            while (CoinDraws.Count < ResultsCount)
            {
                CoinDraws.Add(rnd.Next(0, 2));
            }
        }

        #region IEnumerable<int> Members

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.
        /// </returns>
        /// <filterpriority>1</filterpriority>


        public IEnumerator<int> GetEnumerator()
        {
            return CoinDraws.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return CoinDraws.GetEnumerator();
        }

        #endregion

        //public CoinFace this[int currentTurn]
        //{
        //    get
        //    {
        //        CoinFace x = (CoinFace)Enum.Parse(typeof(CoinFace), CoinDraws[currentTurn].ToString(CultureInfo.InvariantCulture));
        //        return x;
        //    }
        //}

        public int this[int currentTurn]
        {
            get
            {
                //CoinFace x = (CoinFace)Enum.Parse(typeof(CoinFace), CoinDraws[currentTurn].ToString(CultureInfo.InvariantCulture));
                //return x;

                return CoinDraws[currentTurn];
            }
        }

        #region Implementation of IDisposable

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <filterpriority>2</filterpriority>
        public void Dispose()
        {
            CoinDraws = null;
        }

        #endregion

        #region Implementation of IEnumerator

        /// <summary>
        /// Advances the enumerator to the next element of the collection.
        /// </summary>
        /// <returns>
        /// true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.
        /// </returns>
        /// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception><filterpriority>2</filterpriority>
        public bool MoveNext()
        {
            return CoinDraws.GetEnumerator().MoveNext();
        }

        /// <summary>
        /// Sets the enumerator to its initial position, which is before the first element in the collection.
        /// </summary>
        /// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception><filterpriority>2</filterpriority>
        public void Reset()
        {

        }

        /// <summary>
        /// Gets the element in the collection at the current position of the enumerator.
        /// </summary>
        /// <returns>
        /// The element in the collection at the current position of the enumerator.
        /// </returns>
        public int Current
        {
            get
            {
                return CoinDraws.GetEnumerator().Current;
            }
        }

        /// <summary>
        /// Gets the current element in the collection.
        /// </summary>
        /// <returns>
        /// The current element in the collection.
        /// </returns>
        /// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element.</exception><filterpriority>2</filterpriority>
        object IEnumerator.Current
        {
            get
            {
                return Current;
            }
        }

        #endregion


    }






    public enum CoinFace
    {
        Heads = 0,
        Tails = 1
    }

    public class CoinageScore : ICalculateScore
    {
        public double CalculateScore(IMLRegression network)
        {
            NeuralCoin pilot = new NeuralCoin((BasicNetwork)network, true);
            return pilot.ScoreCoin();
        }


        public bool ShouldMinimize
        {
            get
            {
                return false;
            }
        }
    }

    public class NeuralCoin
    {
        private readonly NormalizedField CoinResultsStats;
        private readonly BasicNetwork _network;
        private readonly bool _track;
        private readonly NormalizedField EquityStatsNormalizedField;
        private readonly NormalizedField DrawDownStatsNormalized;
        DataNormalization normerEquity = new DataNormalization();
        DataNormalization normerDrawdown = new DataNormalization();
        public NeuralCoin(BasicNetwork network, bool track)
        {
            CoinResultsStats = new NormalizedField();

            EquityStatsNormalizedField = new NormalizedField();
            DrawDownStatsNormalized = new NormalizedField();
            // DrawDownStatsNormalized = new NormalizedField(NormalizationAction.Normalize, "DrawDown", CoinSimulator.., -LanderSimulator.TerminalVelocity, -0.9, 0.9);

            //DrawDownStatsNormalized = new NormalizedField(NormalizationAction.Normalize, "Drawdown", 1, -1, 1, -1);





            _track = track;
            _network = network;
        }
        public int DetermineHeadsOrTail(OutputEquilateral eqField, IMLData output)
        {
            int result;

            if (eqField != null)
            {
                result = eqField.Equilateral.Decode(output.Data);
            }
            else
            {
                double maxOutput = Double.NegativeInfinity;
                result = -1;

                for (int i = 0; i < output.Count; i++)
                {
                    if (output[i] > maxOutput)
                    {
                        maxOutput = output[i];
                        result = i;
                    }
                }
            }

            return result;
        }
        public int ScoreCoin()
        {
            var sim = new CoinSimulator();
            while (sim.CanPlay)
            {

                sim.CurrentTurn++;


                IMLData input = new BasicMLData(3);
                input[0] = CoinResultsStats.Normalize(sim.CurrentTurnSide);
                input[1] = EquityStatsNormalizedField.Normalize(sim.Equity);
                input[2] = DrawDownStatsNormalized.Normalize(sim.Drawdown);
                IMLData output = _network.Compute(input);
                double value = output[0];
                //      int SideToPlay = (int)output[1];





                if (_track)
                    Console.WriteLine(@"Values : " + input);


                //   int sid = DetermineHeadsOrTail(new OutputEquilateral(-1,1), )
                //   CoinFace side = (CoinFace)Enum.Parse(typeof(CoinFace), SideToPlay.ToString(CultureInfo.InvariantCulture));
                sim.Turn(CoinSimulator.DeTermineHeadsOrTails(value));


                if (_track)
                    Console.WriteLine(sim.Telemetry());
            }
            return (sim.Score);
        }
    }


    /// <summary>
    /// This class holds the heads / tails series results in the form of 1 or 0 (0 = Heads , 1 = Tails).
    /// Also holds an enumerator (this[1] that returns a coinFace (heads or tails enum)).
    /// </summary>
    public class CoinResults : IEnumerable<int>, IEnumerator<int>
    {

        public List<int> CoinDraws = new List<int>();

        /// <summary>
        /// initializes a new coin results with the number of results needed.
        /// </summary>
        /// <param name="ResultsCount"></param>
        public CoinResults(int ResultsCount)
        {
            MakeGame(ResultsCount);
        }

        /// <summary>
        /// Makes the game by filling the array with new random 0 or 1 (heads or tails).
        /// </summary>
        /// <param name="ResultsCount">The results count.</param>
        private void MakeGame(int ResultsCount)
        {
            Random rnd = new Random(1);
            while (CoinDraws.Count < ResultsCount)
            {
                CoinDraws.Add(rnd.Next(0, 2));
            }
        }

        #region IEnumerable<int> Members

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.
        /// </returns>
        /// <filterpriority>1</filterpriority>


        public IEnumerator<int> GetEnumerator()
        {
            return CoinDraws.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return CoinDraws.GetEnumerator();
        }

        #endregion

        //public CoinFace this[int currentTurn]
        //{
        //    get
        //    {
        //        CoinFace x = (CoinFace)Enum.Parse(typeof(CoinFace), CoinDraws[currentTurn].ToString(CultureInfo.InvariantCulture));
        //        return x;
        //    }
        //}

        public int this[int currentTurn]
        {
            get
            {
                //CoinFace x = (CoinFace)Enum.Parse(typeof(CoinFace), CoinDraws[currentTurn].ToString(CultureInfo.InvariantCulture));
                //return x;

                return this.CoinDraws[currentTurn];
            }
        }

        #region Implementation of IDisposable

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <filterpriority>2</filterpriority>
        public void Dispose()
        {
            CoinDraws = null;
        }

        #endregion

        #region Implementation of IEnumerator

        /// <summary>
        /// Advances the enumerator to the next element of the collection.
        /// </summary>
        /// <returns>
        /// true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.
        /// </returns>
        /// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception><filterpriority>2</filterpriority>
        public bool MoveNext()
        {
            return CoinDraws.GetEnumerator().MoveNext();
        }

        /// <summary>
        /// Sets the enumerator to its initial position, which is before the first element in the collection.
        /// </summary>
        /// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception><filterpriority>2</filterpriority>
        public void Reset()
        {

        }

        /// <summary>
        /// Gets the element in the collection at the current position of the enumerator.
        /// </summary>
        /// <returns>
        /// The element in the collection at the current position of the enumerator.
        /// </returns>
        public int Current
        {
            get
            {
                return CoinDraws.GetEnumerator().Current;
            }
        }

        /// <summary>
        /// Gets the current element in the collection.
        /// </summary>
        /// <returns>
        /// The current element in the collection.
        /// </returns>
        /// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element.</exception><filterpriority>2</filterpriority>
        object IEnumerator.Current
        {
            get
            {
                return Current;
            }
        }

        #endregion


    }



    public class CoinSimulator
    {
        public const double MinimumBet = 100;
        public const double MaximumBet = 1000;
        private int currentIndex = 0;

        // public CoinOne CurrentGameResults;
        public CoinSimulator()
        {
            Equity = 10000;
            PreviousEquity = Equity;
            Drawdown = 0;
            CurrentTurn = 0;

            DrawDownSerie = new List<double>();
            RunnupSeries = new List<double>();
            EquitySerie = new List<double>();
            EquitySerie.Add(Equity);


        }

        public int Bets = 10;
        private int Rightcount = 0;

        public int CurrentTurnSide
        {
            get
            {
                return CoinOne.CoinDraws[CurrentTurn];
            }

        }
        public double Equity { get; set; }
        public int CurrentTurn = 0;
        public double PreviousEquity { get; set; }
        public double Drawdown { get; set; }
        public List<double> EquitySerie = new List<double>();
        public List<double> DrawDownSerie = new List<double>();
        public double CurrentRunnup { get; set; }
        public List<double> RunnupSeries { get; set; }
        public int TotalCurrentPlayed = 0;
        private int nbofheadsPlayed = 0;
        public int Score
        {


            get
            {
                return (int)Equity + 100 * (int)Drawdown + (int)CurrentRunnup;
            }

        }

        public void Turn(double amountPlayed, int SideBet)
        {
            //Lets increment the current turn.


            // Console.WriteLine("Current Turn :" + CurrentTurn);
            PreviousEquity = Equity;
            //Check game result :

            amountPlayed = CheckAmountPlayed(amountPlayed);

            //Non random (well just random once..
            //   CurrentTurnSide = (int)CoinOne.CoinDraws[CurrentTurn];

            if (SideBet == 0)
                nbofheadsPlayed++;

            //  CoinFace Result = CurrentGameResults[CurrentTurn];
            if (SideBet == CoinOne.CoinDraws[CurrentTurn])
            {
                Equity = Equity + amountPlayed;
                CurrentRunnup = Equity - PreviousEquity;
                RunnupSeries.Add(CurrentRunnup);
                Drawdown = 0;
                DrawDownSerie.Add(Drawdown);
                Rightcount++;
            }
            else
            {
                Equity = Equity - amountPlayed;
                CurrentRunnup = 0;
                RunnupSeries.Add(CurrentRunnup);
                Drawdown = Equity - PreviousEquity;
                DrawDownSerie.Add(Drawdown);
            }
            //Velocity -= Gravity;
            //Altitude += Velocity;

            //if (thrust && Fuel > 0)
            //{
            //    Fuel--;
            //    Velocity += Thrust;
            //}

            //Velocity = Math.Max(-TerminalVelocity, Velocity);
            //Velocity = Math.Min(TerminalVelocity, Velocity);

            //if (Altitude < 0)
            //    Altitude = 0;
        }



        public static int DeTermineHeadsOrTails(double value)
        {
            if (value > 0)
                return 1;
            else return 0;
        }

        public void Turn(int SideBet)
        {
            //Lets increment the current turn.
            //  CurrentTurn++;

            // Console.WriteLine("Current Turn :" + CurrentTurn);
            PreviousEquity = Equity;
            //Check game result :


            // CurrentTurnSide = (int)CoinOne.CoinDraws[CurrentTurn];


            //  CoinFace Result = CurrentGameResults[CurrentTurn];
            if (SideBet == CurrentTurnSide)
            {
                Equity = Equity + 10;
                CurrentRunnup = Equity - PreviousEquity;
                RunnupSeries.Add(CurrentRunnup);
                Drawdown = 0;
                DrawDownSerie.Add(Drawdown);
                Rightcount++;
            }
            else
            {
                Equity = Equity - 10;
                CurrentRunnup = 0;
                RunnupSeries.Add(CurrentRunnup);
                Drawdown = Equity - PreviousEquity;
                DrawDownSerie.Add(Drawdown);
            }
            //Velocity -= Gravity;
            //Altitude += Velocity;

            //if (thrust && Fuel > 0)
            //{
            //    Fuel--;
            //    Velocity += Thrust;
            //}

            //Velocity = Math.Max(-TerminalVelocity, Velocity);
            //Velocity = Math.Min(TerminalVelocity, Velocity);

            //if (Altitude < 0)
            //    Altitude = 0;
        }
        private double CheckAmountPlayed(double amountPlayed)
        {
            //can't play below 0.1...
            if (amountPlayed < MinimumBet)
                amountPlayed = MinimumBet;

            //we can't play above maximum bet till we have more than the maximum bet...Maximum leverage of 10..
            if (amountPlayed > MaximumBet && Equity < MaximumBet)
                amountPlayed = MaximumBet;

            if (amountPlayed > MaximumBet * 10)
                amountPlayed = MaximumBet * 10;
            return amountPlayed;
        }

        public String Telemetry()
        {
            var result = new StringBuilder();
            result.Append("Elapsed Trades: ");
            result.Append(CurrentTurn);
            result.Append(" s, Equity: ");
            result.Append(Equity);
            result.AppendLine("Drawdown :");
            result.Append(Format.FormatDouble(Drawdown, 4));
            result.AppendLine("Current Run-up : " + CurrentRunnup);
            result.AppendLine("Number of heads played : " + nbofheadsPlayed + " on " + CurrentTurn + " Total :" + CoinOne.CoinDraws.Count);


            result.AppendLine("Current Right count:" + Rightcount);
            //result.Append(" m");
            return result.ToString();
        }

        public bool CanPlay
        {
            get
            {
                if (Equity >= 1000 && CurrentTurn < CoinOne.CoinDraws.Count - 1)
                    return true;
                else return false;

                // return Equity  >0 || CurrentTurn <= CurrentGameResults.CoinDraws.Count;
            }
        }
    }


}