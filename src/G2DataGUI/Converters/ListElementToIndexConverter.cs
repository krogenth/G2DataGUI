using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Avalonia.Data.Converters;

namespace G2DataGUI.Converters;
public class ListElementToIndexConverter : IMultiValueConverter
{
	public object Convert(IList<object> value, Type targetType, object parameter, CultureInfo culture)
	{
		if (value[0] is IList list)
		{
			return $"{list.IndexOf(value[1])}";
		}

		return "-1";
	}

	public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
		throw new NotImplementedException();
}
