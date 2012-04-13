﻿/*******************************************************
 * 
 * 作者：胡庆访
 * 创建时间：20111110
 * 说明：此文件只包含一个类，具体内容见类型注释。
 * 运行环境：.NET 4.0
 * 版本号：1.0.0
 * 
 * 历史记录：
 * 创建文件 胡庆访 20111110
 * 
*******************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OEA.ManagedProperty;
using OEA.MetaModel;

namespace OEA.Library
{
    /// <summary>
    /// OEA 中的属性元数据都从这个类继承下来。
    /// </summary>
    /// <typeparam name="TPropertyType"></typeparam>
    public class PropertyMetadata<TPropertyType> : ManagedPropertyMetadata<TPropertyType>, IPropertyMetadata
    {
        public PropertyMetadata()
            : base(new EntityPropertyMetaProvider())
        {
            this.DefaultValue = CreateDefaultValue<TPropertyType>();
        }

        private static TProperty CreateDefaultValue<TProperty>()
        {
            var propertyType = typeof(TProperty);
            object defaultValue = default(TProperty);

            //处理DateTime类型的默认值为当前时间。
            if (propertyType == typeof(DateTime) && (DateTime)defaultValue == default(DateTime)
                )
            {
                defaultValue = DateTime.Now;
            }
            else if (propertyType == typeof(DateRange) && defaultValue == null)
            {
                defaultValue = new DateRange()
                {
                    BeginValue = DateTime.Now,
                    EndValue = DateTime.Now
                };
            }
            else if (propertyType == typeof(string) && defaultValue == null)
            {
                defaultValue = string.Empty;
            }
            else if (propertyType == typeof(NumberRange) && defaultValue == null)
            {
                defaultValue = new NumberRange();
            }

            return (TProperty)defaultValue;
        }
    }
}