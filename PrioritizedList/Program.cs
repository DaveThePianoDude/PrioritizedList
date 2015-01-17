using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace PrioritizedList
{
    class Program
    {
        static void Main(string[] args)
        {
           ConvertOutlineToXml("outline.txt", "Actions.xml");

            TransformXML("Actions.xml", "Stylesheet.xslt");
        }

        public static void ConvertOutlineToXml(string input, string output)
        {
            Stack<int> elements = new Stack<int>();

            int counter = 0;
            string line;

            System.IO.StreamWriter outputFile = new System.IO.StreamWriter(output);

            // Read the file and display it line by line.
            System.IO.StreamReader file = new System.IO.StreamReader(input);

            outputFile.WriteLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");

            outputFile.WriteLine("<action name=\"The main heading\" >");

            int prevLevel = 0;
            int currLevel = 0;

            while ((line = file.ReadLine()) != null)
            {
                string indent = "";

                string[] tokes = line.Split(' ');

                string toke = tokes[0].TrimStart('\t');

                if (toke.Substring(0, 1).Equals("I"))
                {
                    indent += "\t";
                    currLevel = 1;
                    
                }
                else if (Char.IsDigit(Convert.ToChar(toke.Substring(0, 1))))
                {
                    indent += "\t\t\t";
                    currLevel = 3;
                    
                }
                else if (toke.Substring(0, 1).Equals("i"))
                {
                    indent += "\t\t\t\t";
                    currLevel = 4;
                }
                else
                {
                    indent += "\t\t";
                    currLevel = 2;
                    
                }

                if (currLevel < prevLevel)
                {

                }

                outputFile.WriteLine(indent+"<action name = \"" + line + "\" >");

                if (currLevel <= prevLevel)
                    outputFile.WriteLine(indent + "</action>");

                prevLevel = currLevel;

                counter++;
            }

            outputFile.WriteLine("</action>");

            file.Close();
            outputFile.Close();

            System.Console.WriteLine("There were {0} lines.", counter);
            // Suspend the screen.
            System.Console.ReadLine();
        }

        public static void TransformXML(string sXmlPath, string sXslPath)
        {
            try
            {
                /* loading XML */

                XmlWriterSettings settings = new XmlWriterSettings();
                settings.Indent = true;
                settings.NewLineOnAttributes = true;

                using (XmlReader myXmlReader = XmlReader.Create(sXmlPath))
                {
                    XslCompiledTransform myXslTrans = new XslCompiledTransform();
                    /* loading XSLT */
                    myXslTrans.Load(sXslPath);
                    /* creating Output Stream */

                    XmlTextWriter myWriter = new XmlTextWriter("result.xml", null);
                    /* XML transformation */
                    myXslTrans.Transform(myXmlReader, null, myWriter);
                    myWriter.Close();
                }
            }
                
            catch (Exception e)
            {
                Console.WriteLine("Exception: {0}", e.ToString());
            }
        }
    }
}
