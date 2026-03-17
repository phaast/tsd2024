using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using GoldSavings.App.Model;

namespace GoldSavings.App.Services
{
    public class GoldXmlExport
    {
        public void ExportToXml(List<GoldPrice> prices, string fileName)
        {
            if (prices == null || prices.Count == 0)
            {
                Console.WriteLine("The list is empty. Nothing to save.");
                return;
            }

            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<GoldPrice>));

                using (StreamWriter writer = new StreamWriter(fileName))
                {
                    serializer.Serialize(writer, prices);
                }

                Console.WriteLine($"\nSaved {prices.Count} records to XML: {fileName}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nThere was an error with saving to XML: {ex.Message}");
            }
        }
    }
}
