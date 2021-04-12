using System;
namespace Utility.BaiduAITools
{
    public class BaiduOcrWordsModel
    {
        public long log_id { get; set; }
        public int words_result_num { get; set; }
        public BaiduOcrWordsResultModel[] words_result { get; set; }
    }
    public class BaiduOcrWordsResultModel
    {
        public string words { get; set; }
    }
}
