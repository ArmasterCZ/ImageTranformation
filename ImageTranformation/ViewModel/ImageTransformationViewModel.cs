using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;
using Prism.Commands;
using System.Windows.Input;
using GongSolutions.Wpf.DragDrop;
using System.Windows.Media.Imaging;
using System.IO;
using System.Windows;
using System.Collections.Specialized;
using ChalengeImageTransformationAdvanced;

namespace ImageTranformation.ViewModel
{
    class ImageTransformationViewModel : BindableBase , IDropTarget
    {
        #region prop

        private string tbPathText = @"D:\Armaster\Downloads\imageChalange\orginalBmp.bmp";

        public string TbPathText
        {
            get { return tbPathText; }
            set { SetProperty(ref tbPathText, value);
                loadImage();
            }
        }

        private List<string> cbOutputExtensionSource;

        public List<string> CbOutputExtensionSource
        {
            get { return cbOutputExtensionSource; }
            set { SetProperty(ref cbOutputExtensionSource, value); }
        }

        public ICommand BTransformClick { get; set; }

        private BitmapImage iPreviewSource;

        public BitmapImage IPreviewSource
        {
            get { return iPreviewSource; }
            set { SetProperty(ref iPreviewSource, value); }
        }

        private string cbOutputExtensionSelected;

        public string CbOutputExtensionSelected
        {
            get { return cbOutputExtensionSelected; }
            set { SetProperty(ref cbOutputExtensionSelected, value); }
        }

        #endregion prop

        public ImageTransformationViewModel()
        {
            BTransformClick = new DelegateCommand(BTransform_Click, BTransform_IsEnabled).ObservesProperty(() => TbPathText);
            setupComboBox();
            loadImage();
        }

        /// <summary>
        /// drag handler
        /// </summary>
        void IDropTarget.DragOver(IDropInfo dropInfo)
        {
            StringCollection item = ((DataObject)dropInfo.Data).GetFileDropList();
            if (item.Count > 0)
            {
                TbPathText = item[0].ToString();
            }
           
        }

        /// <summary>
        /// drop handler
        /// </summary>
        public void Drop(IDropInfo dropInfo)
        {
            TbPathText = "Droped";
        }

        /// <summary>
        /// button click handler
        /// </summary>
        private void BTransform_Click()
        {
            TbPathText = @"D:\Armaster\Downloads\imageChalange\orginalPng.png";
            ImageWorker imageWorker = new ImageWorker(TbPathText, CbOutputExtensionSelected);
            imageWorker.processImage();
        }

        /// <summary>
        /// condition for enable button
        /// </summary>
        private bool BTransform_IsEnabled()
        {
            return !String.IsNullOrWhiteSpace(TbPathText);
        }

        /// <summary>
        /// load image to <see cref="IPreviewSource"/>
        /// </summary>
        private void loadImage()
        {
            string imagePath = TbPathText;
            if (File.Exists(imagePath))
            {
                try
                {
                    IPreviewSource = new BitmapImage(new Uri(imagePath));
                }
                catch (Exception)
                {

                }
            }
            else
            {
                IPreviewSource = null;
            }
        }

        /// <summary>
        /// setup combo box <see cref="CbOutputExtensionSource"/>
        /// </summary>
        private void setupComboBox()
        {
            CbOutputExtensionSource = AllovedExtensions.getAllowedExtensions();
            if (CbOutputExtensionSource.Count > 0)
            {
                CbOutputExtensionSelected = CbOutputExtensionSource[0].ToString();
            }
        }

    }
}
