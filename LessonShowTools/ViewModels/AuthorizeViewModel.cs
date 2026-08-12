using System.Collections.ObjectModel;
using LessonShowTools.Core.Attributes;
using LessonShowTools.Models;
using LessonShowTools.Models.Authorize;
using CommunityToolkit.Mvvm.ComponentModel;
using AuthorizeProviderDisplayingModel = LessonShowTools.Models.Authorize.AuthorizeProviderDisplayingModel;

namespace LessonShowTools.ViewModels;

public partial class AuthorizeViewModel : ObservableObject
{
    [ObservableProperty] private ObservableCollection<AuthorizeProviderDisplayingModel> _providers = [];

    [ObservableProperty] private bool _isEditingMode = false;

    [ObservableProperty] private Credential _credential = new();

    [ObservableProperty] private AuthorizeProviderInfo? _selectedAuthorizeProviderInfo;

    [ObservableProperty] private CredentialItem? _selectedCredentialItem;
}