using Framework.Http;
using Framework.Log;
using HtmlAgilityPack;
using Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class CommodityCrawlerService : ICommodityCrawlerService
    {
        private Logger logger = new Logger(typeof(CommodityCrawlerService));
        public void Crawl(string url)
        {
            try
            {
                Console.WriteLine(url);
                string html = HttpHelper.DownloadHtml(url);
                HtmlDocument document = new HtmlDocument();
                document.LoadHtml(html);

                {
                    //Fetch image urls from www.jd.com
                    string commodityInfoPath = "//*[@id='plist']/ul/li";
                    var commodityNodeArray = document.DocumentNode.SelectNodes(commodityInfoPath);

                    if (!url.Contains("page"))
                    {
                        string totalPagepath = "//*[@id='J_topPage']/span/i";
                        var node = document.DocumentNode.SelectSingleNode(totalPagepath);
                        if (node == null) throw new Exception("J_toPage is empty");
                        int.TryParse(node.InnerHtml, out int totalPageCount);

                        for (int i=1; i<totalPageCount+1; i++)
                        {
                            this.Crawl($"{url}&page={i}");  //Gothrought the all pages recursively
                        }

                    }

                    if (commodityNodeArray != null)
                    {
                        foreach (var node in commodityNodeArray)
                        {
                            string allHtml = node.OuterHtml;
                            HtmlDocument documentCommodity = new HtmlDocument();
                            documentCommodity.LoadHtml(allHtml);
                            string imagePath = "//*[@class='p-img']/a/img";
                            var imageNode = documentCommodity.DocumentNode.SelectSingleNode(imagePath);
                            if (imageNode != null)
                            {
                                string imageUrl = null;
                                if (imageNode.Attributes["src"] != null)
                                {
                                    imageUrl = imageNode.Attributes["src"].Value;
                                }
                                else if (imageNode.Attributes["data-lazy-img"] != null)
                                {
                                    //Handle the lazy loading images
                                    imageUrl = imageNode.Attributes["data-lazy-img"].Value;
                                }
                                else
                                {
                                    Console.WriteLine("imageUrl is empty");
                                }

                                if (imageUrl != null && imageUrl.StartsWith("//"))
                                {
                                    imageUrl = $"http:{imageUrl}";
                                    logger.Info(imageUrl);
                                }

                                string urlPath = "//*[@class='p-name']/a";
                                var urlNode = documentCommodity.DocumentNode.SelectSingleNode(urlPath);
                                if (urlNode != null)
                                {
                                    string commodityUrl = $"https://{urlNode.Attributes["href"].Value}";
                                    string sId = Path.GetFileNameWithoutExtension(commodityUrl);
                                    long productId = long.Parse(sId);
                                    //this.ClawlProce(productId);
                                }

                                string namePath = "//*[@class='p-name']/a/em";
                                var nameNode = documentCommodity.DocumentNode.SelectSingleNode(namePath);
                                if (nameNode != null)
                                {
                                    string name = nameNode.InnerHtml;
                                }

                                //string pricePath = "//*[@class='p-price']/strong/em";
                                //var priceNode = documentCommodity.DocumentNode.SelectSingleNode(pricePath);
                                //string price;
                                //if (priceNode != null && priceNode.InnerHtml != "")
                                //{
                                //    price = priceNode.InnerHtml;
                                //}
                                //else
                                //{
                                //    pricePath = "//*[@class='p-price']/strong/li";
                                //    priceNode = documentCommodity.DocumentNode.SelectSingleNode(pricePath);
                                //    price = priceNode.InnerHtml;
                                //}

                            }
                        }
                    }

                }


            }
            catch (Exception ex)
            {
                logger.Error("Catching data failed", ex);
                throw;
            }
        }

        private void ClawlProce(long productId)
        {
            string idString = $"J_{productId}%2CJ_6748060";

            string url = $"https://p.3.cn/prices/mgets?callback=jQuery9339461&ext=11000000&pin=&type=1&area=1_72_4137_0&skuIds={idString}&pdbp = 0 & pdtk = &pdpin = &pduid = 1957366719 & source = list_pc_front & _ = 1531312537525";
            string html = HttpHelper.DownloadHtml(url);
        }
    }
}
