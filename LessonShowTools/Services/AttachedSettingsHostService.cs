using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using LessonShowTools.Core.Abstractions.Services;
using LessonShowTools.Shared;
using LessonShowTools.Shared.Interfaces;
using LessonShowTools.Shared.Models.Profile;

using Microsoft.Extensions.Hosting;

namespace LessonShowTools.Services;

public class AttachedSettingsHostService : IAttachedSettingsHostService
{
    public ObservableCollection<Type> TimePointSettingsAttachedSettingsControls { get; } = new();
    public ObservableCollection<Type> TimeLayoutSettingsAttachedSettingsControls { get; } = new();
    public ObservableCollection<Type> ClassPlanSettingsAttachedSettingsControls { get; } = new();
    public ObservableCollection<Type> SubjectSettingsAttachedSettingsControls { get; } = new();
}