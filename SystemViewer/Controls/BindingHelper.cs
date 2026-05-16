using System;
using System.Windows;
using System.ComponentModel;
using System.Windows.Markup;
using System.Windows.Data;

namespace BindingsHelpers
{

    class BindingConvertor : ExpressionConverter
    {
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(MarkupExtension))
                return true;
            else return false;
        }
        public override object ConvertTo(ITypeDescriptorContext context,
                                        System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(MarkupExtension))
            {
                BindingExpression bindingExpression = value as BindingExpression;
                if (bindingExpression == null)
                    throw new Exception();
                return bindingExpression.ParentBinding;
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }
    }

    static class EditorHelper
    {
        public static void Register<T, TC>()
        {
            Attribute[] attr = new Attribute[1];
            TypeConverterAttribute vConv = new TypeConverterAttribute(typeof(TC));
            attr[0] = vConv;
            TypeDescriptor.AddAttributes(typeof(T), attr);
        }
    }
}