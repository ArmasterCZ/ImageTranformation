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
        private string inputImagePath;
        private string outputFileExtension;

        private string inputFileExtension;
        private string outputImagePath;
        private ImageFormat outputImageFormat;
        private Bitmap picture;
        //private Dictionary<string, ImageFormat>

        #region getersSeters
        
        public string OutputFileExtension
        {
            get { return outputFileExtension; }
            set {
                outputFileExtension = value;
                outputImageFormat = AllovedExtensions.allovedExtensions[value];
            }
        }

        public string InputImagePath
        {
            get { return inputImagePath; }
            set { inputImagePath = value; }
        }

        #endregion getersSeters

        public ImageWorker(string imagePath)
        {
            InputImagePath = imagePath;
        }

        public ImageWorker(string imagePath, string outputFileExtension)
        {
            InputImagePath = imagePath;
            OutputFileExtension = outputFileExtension;
        }
        public ImageWorker(ImageWorker imageWorker)
        {
            InputImagePath = imageWorker.inputImagePath;
        }

        /// <summary>
        /// check conditions for image save
        /// </summary>
        public void processImage()
        {
            //extension of input image
            inputFileExtension = System.IO.Path.GetExtension(inputImagePath).Substring(1).ToLower();
            //extension of output image
            outputFileExtension = outputFileExtension.ToLower();
            //path for created image
            outputImagePath = System.IO.Path.ChangeExtension(inputImagePath, $".{outputFileExtension}");

            //check conditions
            bool allowImageSave = resultSameType(inputFileExtension, outputFileExtension, outputImagePath);
            if (allowImageSave)
            {
                allowImageSave = resultExistingFile(outputImagePath);
            }

            //save
            if (allowImageSave)
            {
                saveImage(outputFileExtension, outputImagePath);
            }
            else
            {
                MessageBox.Show($"Operation canceled.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }


        /// <summary>
        /// save image
        /// </summary>
        private void saveImage(string outputType, string newFilePath)
        {
            try
            {
                //load image
                picture = new Bitmap(inputImagePath);
                //get extension format
                outputImageFormat = AllovedExtensions.allovedExtensions[outputType];

                //save image
                picture.Save(newFilePath, outputImageFormat);

                MessageBox.Show($"Image Converted to {outputType}", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Image Convertion failed. Reasons: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }


        /// <summary>
        /// load image
        /// </summary>
        private Bitmap getPicture(string currentPath, int height = 0, int width = 0)
        {
            System.Drawing.Image imgSize = System.Drawing.Image.FromFile(currentPath);

            if (height < 0)
            {
                height = imgSize.Height;
            }

            if (width < 0)
            {
                width = imgSize.Width;
            }

            System.Drawing.Size size = new System.Drawing.Size(width, height);
            System.Drawing.Image image = System.Drawing.Image.FromFile(currentPath);
            Bitmap picture = new Bitmap(image, size);
            return picture;

            //var y = picture.Size.Height;
            //var x = picture.Size.Width;
            //picture.SetResolution(x, y);
        }

        /// <summary>
        /// check if file from path exist
        /// </summary>
        private bool validatePath(string path)
        {
            bool fileExist = File.Exists(path);
            return fileExist;
        }

        private bool checkConditions(string inputType, string outputType, string newFilePath)
        {
            //resultSameType
            if (inputType.Equals(outputType))
            {
                MessageBoxResult result = MessageBox.Show($"Cannot convert to same type. {inputType} to {outputType}", "Info", MessageBoxButton.OK, MessageBoxImage.Stop);
                return false;
            }

            //resultExistingFile
            if (File.Exists(newFilePath))
            {
                MessageBoxResult result = MessageBox.Show($"Do you want replace orginal image?", "Confirm", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    File.Delete(newFilePath);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        private bool resultSameType(string inputType, string outputType, string newFilePath)
        {
            if (inputType.Equals(outputType))
            {
                MessageBoxResult result = MessageBox.Show($"Cannot convert to same type. {inputType} to {outputType}", "Info", MessageBoxButton.OK, MessageBoxImage.Stop);
                return false;
            }
            return true;
        }

        private bool resultExistingFile(string newFilePath)
        {
            if (File.Exists(newFilePath))
            {
                MessageBoxResult result = MessageBox.Show($"Do you want replace orginal image?", "Confirm", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    File.Delete(newFilePath);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return true;
        }
    }
}
