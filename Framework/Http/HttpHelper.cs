using Framework.Log;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Http
{
    public class HttpHelper
    {
        private static Logger logger = new Logger(typeof(HttpHelper));

        public static string DownloadHtml (string url)
        {
            string html = string.Empty;

            try
            {
                logger.Info($"start fetching {url}");
                //Pre sending get request
                HttpWebRequest request = HttpWebRequest.Create(url) as HttpWebRequest;
                request.Timeout = 30 * 1000;
                request.UserAgent = "Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/67.0.3396.99 Mobile Safari/537.36";
                request.ContentType= "text/html; charset=utf-8";
                //request.Host = "d.jd.com";
                request.Headers.Add("Cookie", @"__jdu=15266175813201987934273; unpl=V2_bzNtbURQQhF1DU5VfRlUV2IFE1gSVBcdfVpAXH8RDlUwBBAOclRCFXwUR1JnGV8UZgQZX0pcQxVFCEdkfiksBWYCEV9HUUoTRThFVEsRbAVjABVaQlFHHHUKRVV7EVUDbwEXXkRVcyVyOHYJI0YGQDNRSzNCZ0QQcg1DVXoQbARXAiIWLFYOFXELQVN7H1gMZwERXEJfShN9CkNXfRtsBFcA; __jdc=122270672; __jdv=122270672|nclick.linktech.cn|t_4_A100234787|tuiguang|76051490719c4704a2e99c7959caf63b|1531348754478; PCSYCityID=country_2431; __jda=122270672.15266175813201987934273.1526617581.1531551370.1531560109.5; TrackID=1_lZCtlmGNIYQATuSJk-PeKo0OtNFNJB-CLHgdHrhZU98H7YJVtymmQLlJihOjK8_oBg6Wj8V2PxqWYgXGPvvrsRzJcN4I9WeqljZNWFiEas; pinId=4Bro57TQh-0; pin=qsgdl; thor=13B765361D69AD904705EB7C0960F18643995113F82786AD8D7DE643DA076B902D52C986C5D43A3F3B8C23215DB911B4CC833A0144F5036E63E13DF0E627B3CBAE61F6D3FB122E11D0136F372880328F142E5DA5DA8CA9F7AC14E3C5C17FA7B4AFE12105DB3D360A31B958D941969F159A9D5EBE6A683BE910AD65EA8CF959F9; _tp=ZEJRMaKz%2BeMSoG9bDzyNWg%3D%3D; _pst=qsgdl; ceshi3.com=000; shshshfpb=0347a85663d3272490d6f005947604f3fb12f2d5a49f1ec925b0e908b5; user-key=52fc0a3b-b79b-4286-a3ba-7b0dd982228f; cn=7; abtest=20180714172234056_14; subAbTest=20180714172234056_35; mobilev=html5; USER_FLAG_CHECK=9634e12e948d96f51712f731761fc5fe; sid=5580917bdc68be1dd14e3bb2a000ac6e; __jdb=122270672.5.15266175813201987934273|5.1531560109; mba_muid=15266175813201987934273; mba_sid=15315601568283008138347928395.1; intlIpLbsCountryIp=222.155.164.236; intlIpLbsCountrySite=en; mhome=1");

                

                request.Method = "GET";

                {
                    //In case of POST request
                    //int sort = 2;
                    //string dataString = string.Format("k={0}&n=24&st={1}&iso=0&src=1&v=4093&p={2}&isRecommend=false&city_id=0&from=1&ldw=1361580739", keyword, sort, 1);
                    //Encoding encoding = Encoding.UTF8;  
                    //byte[] postData = encoding.GetBytes(dataString);
                    //request.ContentLength = postData.Length;
                    //Stream requestStream = request.GetRequestStream();
                    //requestStream.Write(postData, 0, postData.Length);
                }

                Encoding enc = Encoding.UTF8;
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        logger.Warn(string.Format($"Request {url} failed, response.StatusCode is {response.StatusCode} "));
                    }
                    else
                    {
                        try
                        {
                            StreamReader sr = new StreamReader(response.GetResponseStream(), enc);
                            html = sr.ReadToEnd(); //Read data from response
                            sr.Close();
                        }catch(Exception ex)
                        {
                            logger.Error(string.Format($"Fetch {url} failed"), ex);
                        }
                    }
                }                
            }
            catch (System.Net.WebException ex)
            {
                if (ex.Message.Equals("Remote server returns error: (306).")) 
                {
                    logger.Error("Remote server returns error: (306).", ex);
                    html = null;
                }
            }
            catch (Exception ex)
            {
                logger.Error(string.Format($"DownloadHtml fetch {url} failed"), ex);
            }
            return html;
        }
    }
}
