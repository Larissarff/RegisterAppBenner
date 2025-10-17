using System;
using System.Globalization;
using System.Windows.Data;

namespace RegisterAppBenner.Converters
{
    /// <summary>
    /// Converter responsável por inverter um valor booleano.
    /// 
    /// Muito útil em bindings que controlam a visibilidade ou o estado habilitado
    /// de elementos de interface (por exemplo, desabilitar botões enquanto um processo está em execução).
    /// </summary>
    public class InverseBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => value is bool b ? !b : true;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => value is bool b ? !b : false;
    }
}
