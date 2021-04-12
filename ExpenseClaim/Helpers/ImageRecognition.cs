using System;
using System.IO;
using Newtonsoft.Json;
using Tesseract;

namespace Helper
{
    public static class ImageRecognition
    {
        public static void ReadFromFile<T>(this T t,byte[] bytedata)
        {
            if (bytedata.Length==0)
            {
                return;
            }

            using(var engine=new TesseractEngine("tessdata", "eng",EngineMode.Default))
            {
                using(var pix = Pix.LoadFromMemory(bytedata))
                {
                    using (var page = engine.Process(pix, PageSegMode.Auto))
                    {
                        var ret= page.GetText().Trim();
                        //需要仔细拆解返回的内容

                        //t = JsonConvert.DeserializeObject<T>(ret);
                    }
                }
            }
        }
    }
}
