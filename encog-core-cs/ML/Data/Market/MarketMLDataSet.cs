//
// Encog(tm) Core v3.0 - .Net Version
// http://www.heatonresearch.com/encog/
//
// Copyright 2008-2011 Heaton Research, Inc.
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
using System.Collections.Generic;
using Encog.ML.Data.Market.Loader;
using Encog.ML.Data.Temporal;
using Encog.Util.Time;

namespace Encog.ML.Data.Market
{
    /// <summary>
    /// A data set that is designed to hold market data. This class is based on the
    /// TemporalNeuralDataSet.  This class is designed to load financial data from
    /// external sources.  This class is designed to track financial data across days.
    /// However, it should be usable with other levels of granularity as well. 
    /// </summary>
    public sealed class MarketMLDataSet : TemporalMLDataSet
    {
        /// <summary>
        /// The loader to use to obtain the data.
        /// </summary>
        private readonly IMarketLoader _loader;


        /// <summary>
        /// Gets or sets the types needed (MarketDataTypes) that you want to use in your market dataset.
        /// </summary>
        /// <value>
        /// The types needed.
        /// </value>
        private List<MarketDataType> TypesNeeded = new List<MarketDataType>();
        /// <summary>
        /// A map between the data points and actual data.
        /// </summary>
        private readonly IDictionary<UInt64, TemporalPoint> _pointIndex =
            new Dictionary<UInt64, TemporalPoint>();

        /// <summary>
        /// Construct a market data set object.
        /// </summary>
        /// <param name="loader">The loader to use to get the financial data.</param>
        /// <param name="inputWindowSize">The input window size, that is how many datapoints do we use to predict.</param>
        /// <param name="predictWindowSize">How many datapoints do we want to predict.</param>
        public MarketMLDataSet(IMarketLoader loader,UInt64 inputWindowSize, UInt64 predictWindowSize)
            : base(inputWindowSize, predictWindowSize)
        {
            _loader = loader;
            SequenceGrandularity = TimeUnit.Ticks;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MarketMLDataSet"/> class.
        /// </summary>
        /// <param name="loader">The loader.</param>
        /// <param name="inputWindowSize">Size of the input window.</param>
        /// <param name="predictWindowSize">Size of the predict window.</param>
        /// <param name="unit">The time unit to use.</param>
        public MarketMLDataSet(IMarketLoader loader,  UInt64 inputWindowSize, UInt64 predictWindowSize, TimeUnit unit)
            : base(inputWindowSize, predictWindowSize)
        {

            _loader = loader;
            SequenceGrandularity =unit;
        }

        /// <summary>
        /// The loader that is being used for this set.
        /// </summary>
        public IMarketLoader Loader
        {
            get { return _loader; }
        }

        /// <summary>
        /// Add one description of the type of market data that we are seeking at
        /// each datapoint.
        /// </summary>
        /// <param name="desc"></param>
        public override void AddDescription(TemporalDataDescription desc)
        {
            if (!(desc is MarketDataDescription))
            {
                throw new MarketError(
                    "Only MarketDataDescription objects may be used "
                    + "with the MarketMLDataSet container.");
            }
            base.AddDescription(desc);
        }


        /// <summary>
        /// Create a datapoint at the specified date.
        /// </summary>
        /// <param name="when">The date to create the point at.</param>
        /// <returns>Returns the TemporalPoint created for the specified date.</returns>
        public override TemporalPoint CreatePoint(DateTime when)
        {
            UInt64 sequence = GetSequenceFromDate(when);
            TemporalPoint result;

            if (_pointIndex.ContainsKey(sequence))
            {
                result = _pointIndex[sequence];
            }
            else
            {
                result = base.CreatePoint(when);
                _pointIndex[result.Sequence] = result;
            }

            return result;
        }


        /// <summary>
        /// Load data from the loader.
        /// </summary>
        /// <param name="begin">The beginning date.</param>
        /// <param name="end">The ending date.</param>
        public void Load(DateTime begin, DateTime end)
        {
            // define the starting point if it is not already defined
            if (StartingPoint == DateTime.MinValue)
            {
                StartingPoint = begin;
            }

            // clear out any loaded points
            Points.Clear();
            string symbolstring = "";
            // first obtain a collection of symbols that need to be looked up
            IDictionary<TickerSymbol, object> set = new Dictionary<TickerSymbol, object>();
            foreach (TemporalDataDescription desc in Descriptions)
            {
                var mdesc = (MarketDataDescription) desc;
                set[mdesc.Ticker] = mdesc.DataType;
                TypesNeeded.Add(mdesc.DataType);
                symbolstring = mdesc.Ticker.Symbol;
            }
          
            
            LoadSymbol(new TickerSymbol(symbolstring),begin, end);

            //// now loop over each symbol and load the data
            //foreach (TickerSymbol symbol in set.Keys)
            //{
            //    LoadSymbol(symbol, begin, end);
            //}

            // resort the points
            SortPoints();
        }


        /// <summary>
        /// Load one point of market data.
        /// </summary>
        /// <param name="ticker">The ticker symbol to load.</param>
        /// <param name="point">The point to load at.</param>
        /// <param name="item">The item being loaded.</param>
        private void LoadPointFromMarketData(TickerSymbol ticker,
                                             TemporalPoint point, LoadedMarketData item)
        {
            foreach (TemporalDataDescription desc in Descriptions)
            {
                var mdesc = (MarketDataDescription) desc;

                if (mdesc.Ticker.Equals(ticker))
                {
                    point.Data[mdesc.Index] = item.Data[mdesc.DataType];
                }
            }
        }

        /// <summary>
        /// Load one ticker symbol.
        /// </summary>
        /// <param name="ticker">The ticker symbol to load.</param>
        /// <param name="from">Load data from this date.</param>
        /// <param name="to">Load data to this date.</param>
        private void LoadSymbol(TickerSymbol ticker, DateTime from,DateTime to)
        {
            ICollection<LoadedMarketData> data = Loader.Load(ticker, TypesNeeded, from, to);
            foreach (LoadedMarketData item in data)
            {
                     
                TemporalPoint point = CreatePoint(item.When);
                LoadPointFromMarketData(ticker, point, item);
            }
        }

    }
}