using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChalengeImageTransformationAdvanced
{
    class PictureExtensions
    {
        /// <summary>
        /// dictionary of allowed extensions and image format
        /// </summary>
        public Dictionary<string, ImageFormat> AllovedExtensions = new Dictionary<string, ImageFormat>() {
             {"jpg", ImageFormat.Jpeg},
             {"bmp", ImageFormat.Bmp},
             {"gif", ImageFormat.Gif},
             {"png", ImageFormat.Png}
        };

        /// <summary>
        /// return list<string> extensions
        /// </summary>
        public List<string> GetAllowedExtensions()
        {
            return AllovedExtensions.Keys.ToList();
        }
    }
}
