using MoraviaTest;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Moravia.Homework
{
    class Program
    {
        public static string sourceFileName = Path.Combine(Environment.CurrentDirectory, "..\\..\\..\\Source Files\\Document1.xml");
        public static string targetFileName = Path.Combine(Environment.CurrentDirectory, "..\\..\\..\\Target Files\\Document1.json");
        public static string input = "";
        static void Main(string[] args)
        {
            Document doc = new Document();
            
            input = ReadInput();
            if(!string.IsNullOrEmpty(input))
                doc = CreateDoc(input);

            if (!string.IsNullOrEmpty(doc.Title))
                WriteFile(doc);
        }

        private static string ReadInput()
        {
            string result = "";
            try
            {
                FileStream sourceStream = File.Open(sourceFileName, FileMode.Open);
                var reader = new StreamReader(sourceStream);
                result = reader.ReadToEnd();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }

        private static Document CreateDoc(string input)
        {
            try
            {
                var xdoc = XDocument.Parse(input);
                var doc = new Document
                {
                    Title = xdoc.Root.Element("title").Value,
                    Text = xdoc.Root.Element("text").Value
                };
                return doc;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }                 
        }

        private static void WriteFile(Document doc)
        {
            var serializedDoc = JsonConvert.SerializeObject(doc);

            var targetStream = File.Open(targetFileName, FileMode.Create, FileAccess.Write);
            var sw = new StreamWriter(targetStream);
            sw.Write(serializedDoc);
        }
    }
}
