using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Markup;

[assembly: ThemeInfo(
    ResourceDictionaryLocation.None,            //where theme specific resource dictionaries are located
                                                //(used if a resource is not found in the page,
                                                // or application resource dictionaries)
    ResourceDictionaryLocation.SourceAssembly   //where the generic resource dictionary is located
                                                //(used if a resource is not found in the page,
                                                // app, or any theme specific resource dictionaries)
)]
[assembly: InternalsVisibleTo("LessonShowTools")]

[assembly: XmlnsPrefix("http://cjhdevact.github.io/schemas/xaml/core", "ci")]
[assembly: XmlnsDefinition("http://cjhdevact.github.io/schemas/xaml/core", "LessonShowTools.Core", AssemblyName = "LessonShowTools.Core")]
[assembly: XmlnsDefinition("http://cjhdevact.github.io/schemas/xaml/core", "LessonShowTools.Core.Converters", AssemblyName = "LessonShowTools.Core")]
[assembly: XmlnsDefinition("http://cjhdevact.github.io/schemas/xaml/core", "LessonShowTools.Core.Controls", AssemblyName = "LessonShowTools.Core")]
[assembly: XmlnsDefinition("http://cjhdevact.github.io/schemas/xaml/core", "LessonShowTools.Core.Controls.CommonDialog", AssemblyName = "LessonShowTools.Core")]
[assembly: XmlnsDefinition("http://cjhdevact.github.io/schemas/xaml/core", "LessonShowTools.Core.Controls.LessonsControls", AssemblyName = "LessonShowTools.Core")]
[assembly: XmlnsDefinition("http://cjhdevact.github.io/schemas/xaml/core", "LessonShowTools.Core.Controls.IconControl", AssemblyName = "LessonShowTools.Core")]
[assembly: XmlnsDefinition("http://cjhdevact.github.io/schemas/xaml/core", "LessonShowTools.Core.Controls.NavHyperlink", AssemblyName = "LessonShowTools.Core")]
[assembly: XmlnsDefinition("http://cjhdevact.github.io/schemas/xaml/core", "LessonShowTools.Core.Controls.Ruleset", AssemblyName = "LessonShowTools.Core")]
[assembly: XmlnsDefinition("http://cjhdevact.github.io/schemas/xaml/core", "LessonShowTools.Core.Commands", AssemblyName = "LessonShowTools.Core")]
[assembly: XmlnsDefinition("http://cjhdevact.github.io/schemas/xaml/core", "LessonShowTools.Core.Abstractions.Controls", AssemblyName = "LessonShowTools.Core")]
[assembly: XmlnsDefinition("http://cjhdevact.github.io/schemas/xaml/core", "LessonShowTools.Core.Controls.StickerControl", AssemblyName = "LessonShowTools.Core")]