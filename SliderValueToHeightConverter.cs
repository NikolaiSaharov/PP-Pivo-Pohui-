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
                // Преобразуем значение слайдера в высоту (можете настроить по своему усмотрению)
                return sliderValue * 1.47; // Примерное преобразование
            }
            return 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}