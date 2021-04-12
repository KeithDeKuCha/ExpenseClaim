using System;
using Newtonsoft.Json.Linq;
using Utility.BaiduAITools;

namespace Utility
{
    public class BaiduAI
    {
        private static string APP_ID = "23906940";
        private static string API_KEY = "mXZvWfhVwMAOOQw3ppGnTPR1";
        private static string SECRET_KEY = "UX00NZb5kmebsU1hBK2N0dnHQQSzDR6O";
        public BaiduAI()
        {
        }

        public static BaiduOcrWordsModel Ocr(byte[] image)
        {

            var client = new Baidu.Aip.Ocr.Ocr(API_KEY, SECRET_KEY);
            client.Timeout = 60000;  // 修改超时时间
                                     // 如果有可选参数
                                     //        var options = new Dictionary<string, object>{
                                     //    {"language_type", "CHN_ENG"},
                                     //    {"detect_direction", "true"},
                                     //    {"detect_language", "true"},
                                     //    {"probability", "true"}
                                     //};
            var result = client.GeneralBasic(image);
            return result.ToObject<BaiduOcrWordsModel>();
        }
    }
}
