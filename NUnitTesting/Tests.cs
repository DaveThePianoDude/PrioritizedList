using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;

using NUnit.Framework;

using OutlineTransformer;

namespace PrioritizedListTransformer
{
    [TestFixture("outline.txt","Items.xml")]
    public class NUnitTests
    {
        private string outlineFileName;
        private string actionFileName;

        public NUnitTests(string ofn, string afn)
        {
            this.outlineFileName = ofn;
            this.actionFileName = afn;
        }

        [Test]
        public void LoadOutlineFile()
        {
            System.IO.StreamReader file = new System.IO.StreamReader("..\\..\\..\\PrioritizedListTransformer\\Docs\\" + outlineFileName);

            Assert.IsTrue(file != null);
        }

        [Test]
        public void ConvertOutlineFile()
        {
            bool passed = false;

            OutlineTransformer.Tools.ConvertOutlineToXml(outlineFileName, actionFileName);

            XDocument xdoc = XDocument.Load("..\\..\\..\\PrioritizedListTransformer\\Docs\\" + actionFileName);

            XmlSchemaSet schemas = new XmlSchemaSet();

            string xsdMarkup =
                @"<xsd:schema xmlns:xsd='http://www.w3.org/2001/XMLSchema'>
                    </xsd:schema>";

            schemas.Add("", XmlReader.Create(new StringReader(xsdMarkup)));

            xdoc.Validate(schemas, (o, e) =>
            {
                Console.WriteLine("{0}", e.Message);
                passed = true;
            });
    
            Assert.IsTrue(passed);
        }

        [Test]
        public void ApplyXsltFile()
        {
            bool passed = false;

            OutlineTransformer.Tools.TransformXML("..\\..\\..\\PrioritizedListTransformer\\Docs\\" + actionFileName
                , "..\\..\\..\\PrioritizedListTransformer\\Docs\\Stylesheet.xslt"
                , "..\\..\\..\\PrioritizedListTransformer\\Docs\\results.txt", ref passed);

            Assert.IsTrue(passed);
        }
    }

}