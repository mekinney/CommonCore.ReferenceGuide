using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms.CommonCore;
using System.Linq;

namespace referenceguide
{
    public class FontItem
    {
        public string Key { get; set; }
        public string Character { get; set; }
        public string FontFamily { get; set; }
    }

    public class FontItemRow
    {
        public int Row { get; set; }
        public FontItem Item1 { get; set; }
        public FontItem Item2 { get; set; }
        public FontItem Item3 { get; set; }
    }
    public enum FontFamilyEnum
    {
        FontAwesome,
        EntypoPlus,
        Ionicons,
        Material,
        Meteocons,
        SimpleLineIcons,
        Typicons,
        WeatherIcons
    }
    public class FontsCollectionViewModel : ObservableViewModel
    {
        public FontFamilyEnum FontName { get; set; }
        public ObservableCollection<FontItemRow> Items { get; set; } = new ObservableCollection<FontItemRow>();

        public void BuildResourceList()
        {
            switch(FontName){
                case FontFamilyEnum.FontAwesome:
                    Items = CreateObservable(FontAwesome.Icons, FontAwesome.FontFamily);
                    break;
                case FontFamilyEnum.EntypoPlus:
                    Items = CreateObservable(EntypoPlus.Icons, EntypoPlus.FontFamily);
                    break;
                case FontFamilyEnum.Ionicons:
                    Items = CreateObservable(Ionicons.Icons, Ionicons.FontFamily);
                    break;
                case FontFamilyEnum.Material:
                    Items = CreateObservable(Material.Icons, Material.FontFamily);
                    break;
                case FontFamilyEnum.Meteocons:
                    Items = CreateObservable(Meteocons.Icons, Meteocons.FontFamily);
                    break;
                case FontFamilyEnum.SimpleLineIcons:
                    Items = CreateObservable(SimpleLineIcons.Icons, SimpleLineIcons.FontFamily);
                    break;
                case FontFamilyEnum.Typicons:
                    Items = CreateObservable(Typicons.Icons, Typicons.FontFamily);
                    break;
                case FontFamilyEnum.WeatherIcons:
                    Items = CreateObservable(WeatherIcons.Icons, WeatherIcons.FontFamily);
                    break;
            }

        }

        private ObservableCollection<FontItemRow> CreateObservable(Dictionary<string, char> dict, string fontFamily)
        {
            var temp = new List<FontItemRow>();
            var col = 0;
            var row = 0;

            foreach (var key in dict.Keys)
            {
                if (col == 3)
                {
                    col = 0;
                    row++;
                }

                FontItemRow rowItem = null;
                if (col == 0)
                {
                    rowItem = new FontItemRow() { Row = row };
                    temp.Add(rowItem);
                }
                else
                {
                    rowItem = temp.First(x => x.Row == row);
                }

                var item = new FontItem()
                {
                    Key = key,
                    FontFamily = fontFamily,
                    Character = dict[key].ToString()
                };

                switch (col)
                {
                    case 0:
                        rowItem.Item1 = item;
                        break;
                    case 1:
                        rowItem.Item2 = item;
                        break;
                    case 2:
                        rowItem.Item3 = item;
                        break;
                }

                col++;
            }

            return temp.ToObservable<FontItemRow>();
        }
    }
}
