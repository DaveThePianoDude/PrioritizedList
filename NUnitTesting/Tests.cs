using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

using OutlineTransformer;

namespace PrioritizedListTransformer
{
    [TestFixture]
    public class NUnitTests
    {
        [Test]
        public void LoadOutlineFile()
        {
            System.IO.StreamReader file = new System.IO.StreamReader("..\\..\\..\\PrioritizedListTransformer\\Docs\\outline.txt");

            Assert.IsTrue(file != null);
        }

        [Test]
        public void ConvertOutlineFile()
        {
            bool passed = false;


            try
            {
                OutlineTransformer.Tools.ConvertOutlineToXml("outline.txt", "Actions.xml");
                passed = true;
            }
            catch
            {

            }
            
            Assert.IsTrue(passed);
        }
    }

}