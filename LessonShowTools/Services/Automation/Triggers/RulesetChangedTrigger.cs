using System;
using LessonShowTools.Core.Abstractions.Automation;
using LessonShowTools.Core.Abstractions.Services;
using LessonShowTools.Core.Attributes;
using MaterialDesignThemes.Wpf;

namespace LessonShowTools.Services.Automation.Triggers;

[TriggerInfo("LessonShowTools.ruleSet.rulesetChanged", "规则集更新时", PackIconKind.TagMultipleOutline)]
public class RulesetChangedTrigger(IRulesetService rulesetService) : TriggerBase
{
    private IRulesetService RulesetService { get; } = rulesetService;

    public override void Loaded()
    {
        RulesetService.StatusUpdated += RulesetServiceOnStatusUpdated;
    }

    public override void UnLoaded()
    {
        RulesetService.StatusUpdated -= RulesetServiceOnStatusUpdated;
    }

    private void RulesetServiceOnStatusUpdated(object? sender, EventArgs e)
    {
        Trigger();
    }
}