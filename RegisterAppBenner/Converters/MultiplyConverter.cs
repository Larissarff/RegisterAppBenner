using System;
using System.Globalization;
using System.Windows.Data;

namespace RegisterAppBenner.Converters
{
    public class MultiplyConverter : IValueConverter
    /// <summary>
    /// Converter utilizado para multiplicar dois valores (ex: preço * quantidade)
    /// diretamente em um binding no XAML. 
    /// 
    /// Permite calcular dinamicamente valores derivados de propriedades do ViewModel,
    /// como o subtotal de um item de pedido.
    /// </summary>
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is decimal price && parameter is int qty)
                return price * qty;
            return 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
