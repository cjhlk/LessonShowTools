using System.Windows.Controls;
using LessonShowTools.Core.Abstractions.Services.Management;
using LessonShowTools.Services.Management;

namespace LessonShowTools.Controls;

/// <summary>
/// ManagementInfoControl.xaml 的交互逻辑
/// </summary>
public partial class ManagementInfoControl : UserControl
{
    public ManagementInfoControl()
    {
        InitializeComponent();
    }

    public string ManagementOrganization => App.GetService<IManagementService>().Manifest.OrganizationName;

    public IManagementService ManagementService { get; } = App.GetService<IManagementService>();
}