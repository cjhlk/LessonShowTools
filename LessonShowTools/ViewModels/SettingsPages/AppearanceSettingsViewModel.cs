using System.Collections.Generic;
using System.Windows.Media;
using LessonShowTools.Core;
using CommunityToolkit.Mvvm.ComponentModel;

namespace LessonShowTools.ViewModels.SettingsPages;

public class AppearanceSettingsViewModel : ObservableRecipient
{
    public List<FontFamily> FontFamilies { get; } =
        AppBase.Current.IsAssetsTrimmed() ? [..Fonts.SystemFontFamilies] 
            :
        [..Fonts.SystemFontFamilies, new FontFamily("/LessonShowTools;component/Assets/Fonts/#HarmonyOS Sans SC")];
}