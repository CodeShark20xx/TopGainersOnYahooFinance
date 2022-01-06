using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace TopGainersOnYF
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("----- Begin -----");

            HtmlWeb web = new HtmlWeb();
            HtmlDocument siteHtmlDoc = web.Load("https://finance.yahoo.com/gainers");

            var thNodeCollection = ExtractNodeCollectionFromHtmlDocument(siteHtmlDoc, "thead");
            var thNode = ExtractFirstNodeFromNodeCollection(thNodeCollection);
            var trNode = ExtractFirstNodeFromNodeCollection(thNode.ChildNodes);
            List<string> headerLabels = new List<string>();
                
            foreach(var tdNode in trNode.ChildNodes)
            {
                headerLabels.Add(tdNode.InnerText);
            }

            var tbodyNodeCollection = ExtractNodeCollectionFromHtmlDocument(siteHtmlDoc, "tbody");
            var tbodyNode = ExtractFirstNodeFromNodeCollection(tbodyNodeCollection);
            var trNode2Collection = tbodyNode.ChildNodes;
            foreach(var trNode2 in trNode2Collection)
            {
                for(int i = 0; i < trNode2.ChildNodes.Count; i++)
                {
                    Console.WriteLine(headerLabels[i] + ": " + trNode2.ChildNodes[i].InnerText);
                }
                Console.WriteLine();
            }

            Console.WriteLine("----- End -----");
            Console.ReadLine();
        }

        static HtmlNodeCollection ExtractNodeCollectionFromHtmlDocument(HtmlDocument htmlDoc, string element)
        {
            string xpath = "//" + element;
            return htmlDoc.DocumentNode.SelectNodes(xpath);
        }

        static HtmlNode ExtractFirstNodeFromNodeCollection(HtmlNodeCollection nodeCollection)
        {
            return nodeCollection.First();
        }
    }

    // Future Features:
    // - Highlighted columns
    // - Column exclusion
    // - Choose between a list(default) or a grid layout
    // - Choose the # of rows displayed
    // - Search for the company's sector
}
