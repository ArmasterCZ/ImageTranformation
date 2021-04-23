using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ChalengeImageTransformationAdvanced
{
    class ImageWorker
    {
        #region prop

        private string InputImagePath { get; set; }

        public string OutputFileExtension { get; set; }

        public ImageFormat OutputImageFormat { get; set; }

        public bool AllowDeleteExistOutputFile { get; set; } = false;

        public EventHandler<string> ReturnMessage;

        private string inputFileExtension;
        private string outputImagePath;
        private Bitmap picture;

        #endregion prop

        public ImageWorker(string imagePath)
        {
            InputImagePath = imagePath;
        }

        public ImageWorker(string imagePath, string outputFileExtension)
        {
            InputImagePath = imagePath;
            OutputFileExtension = outputFileExtension;
        }

        public ImageWorker(string imagePath, string outputFileExtension, ImageFormat OutputImageFormat)
        {
            InputImagePath = imagePath;
            OutputFileExtension = outputFileExtension;
            this.OutputImageFormat = OutputImageFormat;
        }

        public ImageWorker(ImageWorker imageWorker)
        {
            InputImagePath = imageWorker.InputImagePath;
        }

        /// <summary>
        /// process that save image
        /// </summary>
        public void ProcessImage()
        {
            LoadFileInfo();

            bool allowImageSave = CheckConditions();
            
            if (allowImageSave)
            {
                SaveImage();
            }
            else
            {
                ReturnMessage?.Invoke(this, $"Operation canceled.");
            }
        }

        /// <summary>
        /// load file extension and create output path
        /// </summary>
        private void LoadFileInfo()
        {
            //extension of input image
            inputFileExtension = System.IO.Path.GetExtension(InputImagePath).Substring(1).ToLower();
            //extension of output image
            OutputFileExtension = OutputFileExtension.ToLower();
            //path for created image
            outputImagePath = System.IO.Path.ChangeExtension(InputImagePath, $".{OutputFileExtension}");
        }

        /// <summary>
        /// check conditions for image save
        /// </summary>
        private bool CheckConditions()
        {
            //result same type
            bool allowImageSave = true;
            if (inputFileExtension.Equals(OutputFileExtension))
            {
                ReturnMessage?.Invoke(this, $"Cannot convert to same type. {inputFileExtension} to {OutputFileExtension}");
                allowImageSave = false;
            }

            //result existing file
            if (allowImageSave)
            {
                if (File.Exists(outputImagePath))
                {
                    if (AllowDeleteExistOutputFile)
                    {
                        File.Delete(outputImagePath);
                        allowImageSave = true;
                    }
                    else
                    {
                        allowImageSave = false;
                    }
                }
                else
                {
                    allowImageSave = true;
                }
                
            }
            return allowImageSave;
        }

        /// <summary>
        /// save image
        /// </summary>
        private void SaveImage()
        {
            try
            {
                //load image
                picture = new Bitmap(InputImagePath);
                //get extension format
                OutputImageFormat = (new PictureExtensions()).AllovedExtensions[OutputFileExtension];

                //save image
                picture.Save(outputImagePath, OutputImageFormat);

                ReturnMessage?.Invoke(this, $"Image Converted to {OutputFileExtension}");
            }
            catch (Exception ex)
            {
                ReturnMessage?.Invoke(this, $"Image Convertion failed. Reasons: {ex.Message}");
            }
        }
    }
}
