using Framework.Http;
using Interface;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCrawler
{
    class Program
    {
        //private static Logger logger = new Logger(typeof(String));
        static void Main(string[] args)
        {

            try
            {
                //Get the category data from www.jd.com
                {
                    //Get data from an website
                    //string html = HttpHelper.DownloadHtml("https://www.jd.com");

                    //ICategoryInterface category = new CategoryService();
                    //category.Crawl();
                }

                //Get the commodity data from www.jd.com
                {
                    ICommodityCrawlerService commodity = new CommodityCrawlerService();
                    commodity.Crawl("http://list.jd.com/list.html?cat=9987,653,659");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }          
            

            Console.Read();
        }
    }
}
