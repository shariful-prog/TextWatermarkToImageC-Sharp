using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WatermarkToImage.Models;
//using Windows.UI.Xaml.Media.Imaging;

namespace WatermarkToImage.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHostingEnvironment env;

        public HomeController(ILogger<HomeController> logger, IHostingEnvironment env)
        {
            _logger = logger;
            this.env = env;
        }

        public  IActionResult Index()
        {
            string imgPath = "NoorPic/NoorCertificate.PNG";
            var fullPath = env.WebRootFileProvider.GetFileInfo(imgPath)?.PhysicalPath;
 
            //string path = "E:/Shariful Work GAKK/RND Work/WatermarkToImage/WatermarkToImage/NoorCertificate.PNG";
            ViewBag.imgeBase = AdTxtNew(fullPath);
            return View();
        }


        public string AdTxtNew(string imagePath)
        {
            string initial = "NQS_CERTI_";
            string dtValues = DateTime.Now.ToString("yyyyMMddHHmmssf");
            string NameCongratu = "Shariful Islam Shaon";
            string course_completed_on = "16 December,2021";
            string course_duration = "24 Hours";
            string certificateId = "Certificate ID : "+ initial+ dtValues;
            // rs value is set 2 because the pic resulation is 2x from original size;
            int rs = 2;

            Image img = Image.FromFile(imagePath);
            Bitmap bmp = new Bitmap(img);

            Color color_Name = Color.FromArgb(200, 80, 80, 80);
            SolidBrush brush_Name = new SolidBrush(color_Name);

            //styling 
            Font font_Name = new Font("Roboto", 19*rs, FontStyle.Regular, GraphicsUnit.Pixel);
            Point point_Name = new Point(335* rs, 277* rs);
  


            Font font_Course = new Font("Roboto", 18*rs, FontStyle.Regular, GraphicsUnit.Pixel);
            Point point_Course = new Point(310* rs, 393* rs);


            Point point_Duration = new Point(278 * rs, 415 * rs);

            Font font_Certificate = new Font("Source Code Pro", 14 * rs, FontStyle.Italic, GraphicsUnit.Pixel);

            Point point_Certificate = new Point(770 * rs, 810 * rs);





            Graphics g = Graphics.FromImage(bmp);

            g.DrawString(NameCongratu, font_Name, brush_Name, point_Name);
            g.DrawString(course_completed_on, font_Course, brush_Name, point_Course);
            g.DrawString(course_duration, font_Course, brush_Name, point_Duration);

            g.DrawString(certificateId, font_Certificate, brush_Name, point_Certificate);

            // Nothing just checking 

            g.Dispose();
            bmp.Save("../drawpic.jpg");
  
            //new test

            MemoryStream ms = new MemoryStream();
            bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            byte[] byteImage = ms.ToArray();
           string imgB64 = Convert.ToBase64String(byteImage); //here you should get a base64 string
                                                           //string imgB64 = ImageToBase64(img);
            img.Dispose();
            imgB64 = "data:image/jpg;base64," + imgB64;
            return imgB64;



        }



        public string AddTextToImageInBase64(string imagePath)
        {
            Image img = Image.FromFile(imagePath, true);

            int width = img.Width;
            int height = img.Height;

            int fontSize = 30;
            Point textPosition = new Point(203,184);
            Point textPosition2 = new Point(103,220);
            Font textFont = new Font("Comic Sans MS", fontSize, FontStyle.Bold, GraphicsUnit.Pixel);
            Color color = Color.FromArgb(200, 204, 0, 0);
            SolidBrush brush = new SolidBrush(color);
            Graphics graphics = Graphics.FromImage(img);
            graphics.DrawString("Test Text", textFont, brush, textPosition);
            graphics.DrawString("Shariful", textFont, brush, textPosition2);
            graphics.Dispose();
            //img.Save(targetP);
            string imgB64 = ImageToBase64(img);
            img.Dispose();
            imgB64 = "data:image/jpg;base64," + imgB64;
            return imgB64;

        }



        public string ImageToBase64(Image image)
        {
                using (MemoryStream m = new MemoryStream())
                {
                    image.Save(m, image.RawFormat);
                    byte[] imageBytes = m.ToArray();
                  string  base64String = Convert.ToBase64String(imageBytes);
                    return base64String;
                }
            
        }


        public IActionResult Privacy()
        {
            string path = "E:/Shariful Work GAKK/RND Work/WatermarkToImage/WatermarkToImage/testPic.jpg";
            Image img = Image.FromFile(path, true);
            int width = img.Width;
            int height = img.Height;
            int font_size = (width > height ? height : width) / 9;
            Point text_starting_point = new Point(height / 4, (width / 4));
            Font text_font = new Font("Comic Sans MS", font_size, FontStyle.Bold, GraphicsUnit.Pixel);
            Color color = Color.FromArgb(255, 0, 255, 0);
            SolidBrush brush = new SolidBrush(color);

            //Graphics graphics = Graphics.FromImage(img);
            //graphics.DrawString("good day test", text_font, brush, text_starting_point);
            //graphics.Dispose();

            img.Save("../drawpic.jpg");
            img.Dispose();
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
