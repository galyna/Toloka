using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

namespace EleksCookies.Widgets.Common.Extensions
{
  public static class CommonExtensions
  {
    public static string GetDescription(this Enum value)
    {
      string result = value.ToString();

      FieldInfo fieldInfo = value.GetType().GetField(result);

      DescriptionAttribute[] attributes =
          (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

      if (attributes != null && attributes.Length > 0)
        result = attributes[0].Description;

      return result;
    }

    public static IEnumerable<SelectListItem> ToSelectList(this Type type, byte selected = 0)
    {
      if (!type.IsEnum)
        throw new InvalidCastException();

      var list = new List<SelectListItem>();

      foreach (var value in Enum.GetValues(type))
      {
        var selectItem = new SelectListItem
        {
          Text = ((Enum)value).GetDescription(),
          Value = ((byte)value).ToString()
        };

        if ((byte)value == selected)
          selectItem.Selected = true;

        list.Add(selectItem);
      }

      return list;
    }

    public static IEnumerable<SelectListItem> ToSelectList<TKey, TValue>(this IDictionary<TKey, TValue> items, TKey defaultValue = default(TKey))
    {
      return items.Select(item =>
        new SelectListItem {
          Value = item.Key.ToString(),
          Text = item.Value.ToString(),
          Selected = item.Key.Equals(defaultValue)
        });
    }

    public static IEnumerable<SelectListItem> ToSelectList<TValue>(
      this IEnumerable<TValue> items,
      TValue selectedValue = default(TValue),
      string defaultValue = "")
    {

      var selectList = items.Select(item =>
        new SelectListItem {
          Value = item.ToString(),
          Text = item.ToString(),
          Selected = item.Equals(selectedValue)
        }).ToList();

      if (!string.IsNullOrEmpty(defaultValue))
        selectList.Insert(0,
          new SelectListItem {
            Text = defaultValue,
            Value = "0",
            Selected = true
          });

      return selectList;
    }
  }
}