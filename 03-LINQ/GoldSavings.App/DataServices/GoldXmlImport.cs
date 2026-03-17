using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using GoldSavings.App.Model;

namespace GoldSavings.App.Services
{
    public class GoldXmlImport
    {
        public List<GoldPrice> ImportFromXml(string fileName) => 
            (List<GoldPrice>)new XmlSerializer(typeof(List<GoldPrice>)).Deserialize(File.OpenRead(fileName));
    }
}