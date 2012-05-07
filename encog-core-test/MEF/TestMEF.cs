using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Encog.MEF
{
    using System.ComponentModel.Composition;
    using System.ComponentModel.Composition.Hosting;
    using System.IO;
    using System.Reflection;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class TestMEF
    {

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

        [Import(typeof(IStatusReportable))]
        public IStatusReportable reporter;

        [TestMethod]
        public void TestMefStatusReportable()
        {
            string dir = AssemblyDirectory;

            //Lets get the nlog status reportable from MEF directory..
            CompositionContainer _container;
            //An aggregate catalog that combines multiple catalogs
            var catalog = new AggregateCatalog();
            //Adds all the parts found in the same assembly as the Program class
            catalog.Catalogs.Add(new AssemblyCatalog(typeof(TestMEF).Assembly));
            catalog.Catalogs.Add(new DirectoryCatalog(AssemblyDirectory));

            //Create the CompositionContainer with the parts in the catalog
            _container = new CompositionContainer(catalog);

            //Fill the imports of this object
            try
            {
               _container.ComposeParts(this);
            }
            catch (CompositionException compositionException)
            {
                Console.WriteLine(compositionException.ToString());
            }


            reporter.Report(2,1,"Test Report");

        }
    }
}
