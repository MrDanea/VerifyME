using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace VerifyME_Desktop
{
    public class ImageSizeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Image image)
            {
                var source = image.Source as BitmapSource;
                if (source != null)
                {
                    double scaleX = image.ActualWidth / source.PixelWidth;
                    double scaleY = image.ActualHeight / source.PixelHeight;
                    double scale = Math.Min(scaleX, scaleY);
                    return new Size(source.PixelWidth * scale, source.PixelHeight * scale);
                }
            }
            return new Size(0, 0);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}