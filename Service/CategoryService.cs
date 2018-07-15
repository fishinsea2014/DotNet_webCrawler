using Framework.Http;
using Framework.Log;
using HtmlAgilityPack;
using Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class CategoryService : ICategoryInterface
    {
        private Logger logger = new Logger(typeof(CategoryService));
        public void Crawl()
        {
            string html = HttpHelper.DownloadHtml("https://www.jd.com");
            //There are two ways to parse html, one is by regular expression, the other is by xpath
            if (string.IsNullOrEmpty(html))
            {
                throw new Exception("Nothing fetched.");
            }

            HtmlDocument document = new HtmlDocument();

            document.LoadHtml(html);
            {
                string firstPath = "//dl/dd/a";
                HtmlNodeCollection nodeList = document.DocumentNode.SelectNodes(firstPath);
                if (nodeList != null)
                {
                    foreach (HtmlNode node in nodeList) //Locate first level
                    {
                        string firstHtml = node.OuterHtml; //Get the whole html in the node

                        HtmlDocument documentChild = new HtmlDocument();
                        documentChild.LoadHtml(firstHtml);
                        //TODO : Explore lower level nodes.

                    }
                }                
            }

            {
                string secondPath = "//dl/dt/a";
                HtmlNodeCollection nodeList = document.DocumentNode.SelectNodes(secondPath);
                if (nodeList != null)
                {
                    foreach (HtmlNode node in nodeList)
                    {
                        string url = node.Attributes["href"].Value;
                        string name = node.InnerText;
                        logger.Info($"{name}:{url}");
                    }
                }
            }

            {
                string thirdPath = "//dl/dd/a";
                HtmlNodeCollection nodeList = document.DocumentNode.SelectNodes(thirdPath);
                if (nodeList != null)
                {
                    foreach (HtmlNode node in nodeList)
                    {
                        string url = node.Attributes["href"].Value;
                        string name = node.InnerText;
                        logger.Info($"{name}:{url}");
                    }
                }
            }


        }
    }
}
