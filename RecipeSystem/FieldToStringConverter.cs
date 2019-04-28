using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace RecipeSystem
{
	/// <summary>
	/// Conveter from FieldNameAttribure or defalut
	/// </summary>
	public class FieldToStringConverter : IValueConverter
	{
		private readonly Type m_EnumType;

		public FieldToStringConverter(Type enumType)
		{
			m_EnumType = enumType;
		}
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var fields = m_EnumType.GetFields().Where(x => x.IsLiteral);

			var field = fields.First(f => f.GetValue(m_EnumType).Equals(value));

			object[] attrs = field.GetCustomAttributes(typeof(FieldNameAttribute), false);
			if (attrs.Length == 1)
			{
				FieldNameAttribute brAttr = (FieldNameAttribute)attrs[0];
				return brAttr.Name;
			}
			return value.ToString();
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			string selectedName = (string)value;

			var fields = m_EnumType.GetFields().Where(x => x.IsLiteral);

			var field = fields.First(f =>
			{
				object[] attrs = f.GetCustomAttributes(typeof(FieldNameAttribute), false);
				if (attrs.Length == 1)
				{
					FieldNameAttribute brAttr = (FieldNameAttribute)attrs[0];
					return brAttr.Name == selectedName;
				}
				return selectedName == f.Name;
			});

			return field.GetValue(m_EnumType);
		}

	}
}
