using Avalonia.Data.Converters;
using System.Drawing.Imaging;
using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace G2DataGUI.Converters;

/// <summary>
/// Implements converter for Avalonia Image source value.
/// Avalonia Image expects a IBitmap.
/// </summary>
public class BitmapConverter : IValueConverter
{
    public static BitmapConverter Instance = new BitmapConverter();

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null)
        {
            return null;
        }

        if (value is Pfim.IImage image && targetType.IsAssignableFrom(typeof(Avalonia.Media.Imaging.Bitmap)))
        {
            return ConvertToAvaloniaBitmap(image);
        }

        return null;
    }

    private Avalonia.Media.Imaging.Bitmap ConvertToAvaloniaBitmap(Pfim.IImage image)
    {
        Avalonia.Media.Imaging.Bitmap bitmap;
        var handle = GCHandle.Alloc(image.Data, GCHandleType.Pinned);
        try
        {
            var dataPtr = Marshal.UnsafeAddrOfPinnedArrayElement(image.Data, 0);
            bitmap = new Avalonia.Media.Imaging.Bitmap(
                Avalonia.Platform.PixelFormat.Bgra8888,
                Avalonia.Platform.AlphaFormat.Premul,
                dataPtr,
                new Avalonia.PixelSize(image.Width, image.Height),
                new Avalonia.Vector(96, 96),
                image.Stride);
        }
        finally
        {
            handle.Free();
        }

        return bitmap;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}