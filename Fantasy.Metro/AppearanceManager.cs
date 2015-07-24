using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Fantasy.Metro
{
    public class AppearanceManager : NotifyPropertyChanged
    {
        public static readonly Uri LightThemeSource = new Uri("/Fantasy.Metro;component/Themes/Assets/FantasyMetro.Light.xaml", UriKind.Relative);
        public const string KeyAccentColor = "AccentColor";
        public const string KeyAccent = "Accent";
        public const string KeyDefaultFontSize = "DefaultFontSize";
        public const string KeyFixedFontSize = "FixedFontSize";
        private static AppearanceManager current = new AppearanceManager();

        public static AppearanceManager Current
        {
            get { return current; }
        }

        private AppearanceManager()
        {
            LightThemeCommand = new RelayCommand(o => ThemeSource = LightThemeSource, o => !LightThemeSource.Equals(ThemeSource));
            SetThemeCommand = new RelayCommand(o =>
            {
                if (o is Uri)
                {
                    ThemeSource = (Uri)o;
                }
                else
                {
                    var str = o as string;
                    if (str != null)
                    {
                        Uri source;
                        if (Uri.TryCreate(str, UriKind.RelativeOrAbsolute, out source))
                        {
                            ThemeSource = source;
                        }
                    }
                }
            }, o => o is Uri || o is string);
            //LargeFontSizeCommand = new RelayCommand(o => FontSize = FontSize.Large);
            //SmallFontSizeCommand = new RelayCommand(o => FontSize = FontSize.Small);
            AccentColorCommand = new RelayCommand(o =>
            {
                if (o is Color)
                {
                    AccentColor = (Color)o;
                }
                else
                {
                    // parse color from string
                    var str = o as string;
                    if (str != null)
                    {
                        AccentColor = (Color)ColorConverter.ConvertFromString(str);
                    }
                }
            }, o => o is Color || o is string);
        }

        private ResourceDictionary GetThemeDictionary()
        {
            // determine the current theme by looking at the app resources and return the first dictionary having the resource key 'WindowBackground' defined.
            return (from dict in Application.Current.Resources.MergedDictionaries
                    where dict.Contains("WindowBackground")
                    select dict).FirstOrDefault();
        }

        private Uri GetThemeSource()
        {
            var dict = GetThemeDictionary();
            if (dict != null)
            {
                return dict.Source;
            }

            // could not determine the theme dictionary
            return null;
        }

        private void SetThemeSource(Uri source, bool useThemeAccentColor)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            var oldThemeDict = GetThemeDictionary();
            var dictionaries = Application.Current.Resources.MergedDictionaries;
            var themeDict = new ResourceDictionary { Source = source };

            // if theme defines an accent color, use it
            var accentColor = themeDict[KeyAccentColor] as Color?;
            if (accentColor.HasValue)
            {
                // remove from the theme dictionary and apply globally if useThemeAccentColor is true
                themeDict.Remove(KeyAccentColor);

                if (useThemeAccentColor)
                {
                    ApplyAccentColor(accentColor.Value);
                }
            }

            // add new before removing old theme to avoid dynamicresource not found warnings
            dictionaries.Add(themeDict);

            // remove old theme
            if (oldThemeDict != null)
            {
                dictionaries.Remove(oldThemeDict);
            }

            OnPropertyChanged("ThemeSource");
        }

        private Color GetAccentColor()
        {
            var accentColor = Application.Current.Resources[KeyAccentColor] as Color?;

            if (accentColor.HasValue)
            {
                return accentColor.Value;
            }

            // default color: teal
            return Color.FromArgb(0xff, 0x1b, 0xa1, 0xe2);
        }

        private void ApplyAccentColor(Color accentColor)
        {
            // set accent color and brush resources
            Application.Current.Resources[KeyAccentColor] = accentColor;
            Application.Current.Resources[KeyAccent] = new SolidColorBrush(accentColor);
        }

        private void SetAccentColor(Color value)
        {
            ApplyAccentColor(value);

            // re-apply theme to ensure brushes referencing AccentColor are updated
            var themeSource = GetThemeSource();
            if (themeSource != null)
            {
                SetThemeSource(themeSource, false);
            }

            OnPropertyChanged("AccentColor");
        }

        public ICommand LightThemeCommand { get; private set; }
        public ICommand AccentColorCommand { get; private set; }
        public ICommand SetThemeCommand { get; private set; }

        public Uri ThemeSource
        {
            get { return GetThemeSource(); }
            set { SetThemeSource(value, true); }
        }

        public Color AccentColor
        {
            get { return GetAccentColor(); }
            set { SetAccentColor(value); }
        }
    }
}
