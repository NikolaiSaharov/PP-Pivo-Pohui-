using System;
using System.Windows.Data;

namespace FirstTask
{
    public class SliderValueToHeightConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is double sliderValue)
            {
                // Максимальная высота 147 (как у слайдера), но с учетом скругления
                return Math.Min(sliderValue * 1.47, 147 - 2); // -4 для компенсации скругления
            }
            return 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}