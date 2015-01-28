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

            OutlineTransformer.Tools.ConvertOutlineToXml("outline.txt", "Actions.xml");

            XDocument xdoc = XDocument.Load("..\\..\\..\\PrioritizedListTransformer\\Docs\\Actions.xml");

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
    }

}