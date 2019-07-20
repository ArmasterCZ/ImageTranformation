using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChalengeImageTransformationAdvanced
{
    class AllovedExtensions
    {
        /// <summary>
        /// dictionary of allowed extensions and image format
        /// </summary>
        public static Dictionary<string, ImageFormat> allovedExtensions = new Dictionary<string, ImageFormat>() {
             {"jpg", ImageFormat.Jpeg},
             {"bmp", ImageFormat.Bmp},
             {"gif", ImageFormat.Gif},
             {"png", ImageFormat.Png}
        };

        /// <summary>
        /// return list<string> extensions
        /// </summary>
        public static List<string> getAllowedExtensions()
        {
            List<string> extension = new List<string>();

            foreach (var item in allovedExtensions)
            {
                extension.Add(item.Key);
            }

            return extension;
        }
    }
}
