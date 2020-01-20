using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Project.COMMON.CommonTools
{
    #region Açıklama
    //--------------Cagri Hocanın Yazdıgı Not (Burada Calisanın daha ıyı ogrenmesı ıcın)

    //Eger bir sınıfın görevi belliyse ve instance'lari sürekli aynı görevi yapacak ise onun instance'inin alınması bos yere ram'de yer kaplamasına neden olur. 

    //Static sınıfların icerisinde kesinlikle static ögeler olmalıdır. Instance ögeler barındıramaz. Static olmayan sınıflar instance ögeler barındırabilir ayrıca static ögeler de barındırabilir. Ancak instance ögelerine nesnelerinden, static ögelerine de sınıfın kendisinden ulasılabilir.

    //HttpPostedFileBase , bize post ile gelen dosyayı tutabilen bir tiptir. 
    #endregion

    public static class ImageUploader
    {
        public static string UploadImage(string serverPath, HttpPostedFileBase file)
        {
            if (file != null)
            {
                Guid uniqueName = Guid.NewGuid();       //~/ProjectImages/asdsadasdasdsad

                serverPath = serverPath.Replace("~", string.Empty);

                #region Açıklama
                //eger gelen serverPath parametresinde ~(tilda) diye bir işaret varsa onu boslukla degiştir.

                //Cagri.UzayGemisi.EnSevdigi.IsıkHızı.Png

                //"Cagri","UzayGemisi","EnSevdigi","IsıkHızı","Png" 
                #endregion

                string[] fileArray = file.FileName.Split('.');
                #region Açıklama
                //burada uzaygemisi.jpg  diye bir veri gelirse Split('.') metodu sayesinde ben bunu ["uzaygemisi"]["jpg"] olarak noktalardan kesip iki elemanlı bir string arraye dönüstürdüm.  
                #endregion

                string extension = fileArray[fileArray.Length - 1].ToLower(); //burada dosya uzantısını yakalayarak onu kücük harflere cevirdik.

                string fileName = $"{uniqueName}.{extension}";//Olusturdugumuz Guid tipinkindeki degişkenimizle yakaladıgımız extension'

                //alt tarafta uzantı kontrolü ile dosyanın bir resim olup olmadıgını anlamaya calısıyoruz
                if (extension == "jpg" || extension == "gif" || extension == "png" || extension == "jpeg")
                {
                    if (File.Exists(HttpContext.Current.Server.MapPath(serverPath + fileName)))
                    {
                        return "Dosya zaten var";
                    }
                    else
                    {
                        string filePath = HttpContext.Current.Server.MapPath(serverPath + fileName);
                        file.SaveAs(filePath);
                        return serverPath + fileName;
                    }
                }
                else
                {
                    return "Arkadasım niye resim secmedin?";
                }
            }
            else
            {
                return "Dosya bos";
            }
        }
    }
}
