﻿namespace OmniXaml.Wpf
{
    using System;
    using System.ComponentModel;
    using System.Reflection;
    using Builder;
    using TypeConversion;
    using TypeConverterAttribute = System.ComponentModel.TypeConverterAttribute;

    internal class WpfTypeConverterProvider : ITypeConverterProvider
    {
        private readonly TypeConverterProvider fallback;

        public WpfTypeConverterProvider()
        {
            fallback = new TypeConverterProvider();
        }

        public ITypeConverter GetTypeConverter(Type type)
        {
            var converter = fallback.GetTypeConverter(type);
            if (converter == null)
            {
                var typeConverterAttribute = type.GetTypeInfo().GetCustomAttribute<TypeConverterAttribute>();

                if (typeConverterAttribute == null)
                {
                    return null;
                }

                var qualifiedName = typeConverterAttribute.ConverterTypeName;
                var converterType = Type.GetType(qualifiedName, true);
                var converterInstance = (TypeConverter) Activator.CreateInstance(converterType);
                converter = new ConverterAdapter(converterInstance);
            }
            return converter;
        }

        public void RegisterConverter(TypeConverterRegistration typeConverterRegistration)
        {
            throw new NotImplementedException();
        }
    }
}