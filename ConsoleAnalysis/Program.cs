using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAnalysis
{
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Reflection;

    using CommandLine;
    using CommandLine.Text;

    using Encog.Neural.Networks;

    using EncogPredictionIndicator;

    internal class Program
    {
        private static void Main(string[] args)
        {
            NLog.Logger mLogger = NLog.LogManager.GetCurrentClassLogger();

            mLogger.Info("Starting Logger....");
            Options options = new Options();

            CommandLineParserSettings settings = new CommandLineParserSettings();

            settings.CaseSensitive = false;
            settings.HelpWriter = Console.Error;
            ICommandLineParser parser = new CommandLineParser(settings);

            if (!parser.ParseArguments(args, options, Console.Error))
            {
                HelpText hlp = new HelpText(Options._headingInfo, "Olivier Guiglionda 2008/2012", options);
                // hlp.RenderParsingErrorsText(options);

                Console.ForegroundColor = ConsoleColor.Red;
                hlp.RenderParsingErrorsText(options);
                Console.ResetColor();
                return;
            }
            if (parser.ParseArguments(args, options, Console.Error))
            {
                EncogPredictionIndicator.PredictionNetworkIndicator myIndicator =
                    new EncogPredictionIndicator.PredictionNetworkIndicator(
                        options.networkfile, options.BaseDirectory, mLogger, options.InputFile, 5000);

                var train = Trainers.Generate(new FileInfo(options.BaseDirectory), myIndicator.NetworkStats, 10);

                ClooTrainer clooTrainer = new ClooTrainer();
             

                var ClooTraining = Task<Tuple<double, double>>.Factory.StartNew(() => clooTrainer.ClooTrain(train, (BasicNetwork)NetworkCreator.CreateFeedforwardNetwork(10 * 4, 20, 1)));

                ClooTraining.Wait();
               
                var t = Task<Tuple<double, double>>.Factory.StartNew(() => Trainers.StartTraining(train, (BasicNetwork)NetworkCreator.CreateFeedforwardNetwork(10 * 4, 20, 1), mLogger, "Feedforward"));
                t.Wait();
     
                var tElmans = Task<Tuple<double, double>>.Factory.StartNew(() => Trainers.StartTraining(train, (BasicNetwork)NetworkCreator.CreateElmanNetwork(10 * 4, 20, 1), mLogger, "Elman"));

                tElmans.Wait();
                

                //Lets do the training...
                mLogger.Error("Max Error Feedforward : " + t.Result.Item1 + " feedforward elapsed:" + t.Result.Item2 + " Elmhan : " + tElmans.Result.Item1 + " elman elapsed : " + tElmans.Result.Item2 + " cloo training Error :" + ClooTraining.Result.Item1 + " cloo elapsed : " + ClooTraining.Result.Item2);
            }

            //First lets make a new indicator...


            Console.WriteLine("End....");
            Console.ReadKey();
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

        #region class commandline
        private class Options : CommandLineOptionsBase
        {
            /// </summary>
            [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1603:DocumentationMustContainValidXml", Justification = "Reviewed. Suppression is OK here.")]
            public static readonly HeadingInfo _headingInfo = new HeadingInfo(
                "Neural Indicator Test", Assembly.GetExecutingAssembly().ImageRuntimeVersion);

            #region Standard Option Attribute

            [Option("d", "directory", Required = false,
                HelpText =
                    "Sets the directory in which to load / save networks and training files, if empty we use the current executing assembly directory.."
                )]
            public string BaseDirectory = AssemblyDirectory;

            [Option("n", "network", Required = false,
                HelpText = "sets a new network file..If default we just use inetwork.egb")]
            public string networkfile = "inetwork.egb";

            [Option("n", "network", Required = false,
                HelpText =
                    "Sets the training file..Not required as you can also just play your previously saved network.")]
            public string CSVFile = string.Empty;

            /// <summary>
            /// the input file.
            /// </summary>
            [Option("i", "input", Required = true, HelpText = "Input file to read the data from.")]
            public string InputFile = null;

            /// <summary>
            /// the input file.
            /// </summary>
            [Option("c", "input", Required = false, HelpText = "Changes the input count.")]
            public int Inputcount = 4;

            [Option("s", "separator", Required = false,
                HelpText = "Changes the default separator to whatever is needed.")]
            public char Separator = ',';

            [Option("f", "date", Required = false, HelpText = "Changes the format of the dates to a new format")]
            public string DateFormat = DateParseFormats.BaseQuantFormat;

            [HelpOption(HelpText = "Displays this help screen.")]
            public string GetUsage()
            {
                var help = new HelpText(_headingInfo)
                    {
                        AdditionalNewLineAfterOption = true,
                        Copyright = new CopyrightInfo("Olivier Guiglionda", 2008, 2012)
                    };

                help.AddPreOptionsLine("Welcome to symbol inserted in CSV.");
                help.FormatOptionHelpText += new EventHandler<FormatOptionHelpTextEventArgs>(help_FormatOptionHelpText);

                help.AddPreOptionsLine(
                    "You can change the date format by specifying -f yyyy-mm-dd hh or whatever format you need..");
                help.AddPreOptionsLine(
                    "Change the separator in your csv file by using -s , or whatever your separator is.");

                return help;
            }

            private void help_FormatOptionHelpText(object sender, FormatOptionHelpTextEventArgs e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                if (e.Option.Required)
                {
                    Console.WriteLine(
                        "This option is required: {0} Long Name :{1} Short Name:{2} ",
                        e.Option.Required,
                        e.Option.LongName,
                        e.Option.ShortName);
                }
                Console.ResetColor();
            }

            private HelpText helpTxt = new HelpText(_headingInfo, "Olivier Guiglionda");

            public void HandleParsingErrorsInHelp(HelpText help)
            {
                try
                {
                    string errors = help.RenderParsingErrorsText(new Options());
                    if (!string.IsNullOrEmpty(errors))
                    {
                        help.AddPreOptionsLine(
                            string.Concat(Environment.NewLine, "ERROR: ", errors, Environment.NewLine));
                    }
                }
                catch (Exception ex)
                {
                }
            }

            #endregion
        }
        #endregion
    }
}