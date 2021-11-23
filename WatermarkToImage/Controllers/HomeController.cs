using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WatermarkToImage.Models;
using Windows.UI.Xaml.Media.Imaging;

namespace WatermarkToImage.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            string path = "E:/Shariful Work GAKK/RND Work/WatermarkToImage/WatermarkToImage/testPic.jpg";
            ViewBag.imgeBase = AddTextToImageInBase64(path);
            return View();
        }



        public string AddTextToImageInBase64(string imagePath)
        {
            Image img = Image.FromFile(imagePath, true);

            int width = img.Width;
            int height = img.Height;

            int fontSize = (width > height ? height : width) / 9;
            Point textPosition = new Point(height / 3, (width / 3));
            Font textFont = new Font("Comic Sans MS", fontSize, FontStyle.Bold, GraphicsUnit.Pixel);
            Color color = Color.FromArgb(200, 204, 0, 0);
            SolidBrush brush = new SolidBrush(color);
            Graphics graphics = Graphics.FromImage(img);
            graphics.DrawString("Test Text", textFont, brush, textPosition);
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
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
