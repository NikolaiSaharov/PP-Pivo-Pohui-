using System.Windows.Media;
using System.Windows.Media.Imaging;

public static class ImageColorChanger
{
    public static ImageSource ChangeColor(ImageSource source, Color newColor)
    {
        if (source == null) return null;

        // Преобразуем ImageSource в FormatttedImage
        var bitmapSource = source as BitmapSource;
        if (bitmapSource == null) return source;

        // Создаем новый Bitmap для изменения цвета
        var width = bitmapSource.PixelWidth;
        var height = bitmapSource.PixelHeight;
        var stride = width * 4; // 4 байта на пиксель (ARGB)
        var pixelData = new byte[height * stride];
        bitmapSource.CopyPixels(pixelData, stride, 0);

        // Применяем новый цвет к каждому пикселю
        for (int i = 0; i < pixelData.Length; i += 4)
        {
            // Если пиксель не прозрачный, меняем его цвет
            if (pixelData[i + 3] > 0) // Проверка альфа-канала
            {
                pixelData[i] = newColor.B; // Синий
                pixelData[i + 1] = newColor.G; // Зеленый
                pixelData[i + 2] = newColor.R; // Красный
            }
        }

        // Создаем новое изображение с измененными пикселями
        var newBitmap = BitmapSource.Create(
            width, height,
            bitmapSource.DpiX, bitmapSource.DpiY,
            bitmapSource.Format,
            bitmapSource.Palette,
            pixelData,
            stride
        );

        return newBitmap;
    }
}