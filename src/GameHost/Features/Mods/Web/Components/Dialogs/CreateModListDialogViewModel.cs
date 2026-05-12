using LunaticPanel.Core.Abstraction.Widgets.Enum;
using GameHost.Features.Mods.Application.Pulses.Actions;
using GameHost.Features.Mods.Application.Pulses.States;
using GameHost.Features.Mods.Web.Components.Dialogs.ViewModels;
using GameHost.Kernel.Abstractions.Services.Notification.Enums;
using GameHost.Kernel.Abstractions.Services.Notification.Services;
using StatePulse.Net;
using System.Text.RegularExpressions;

namespace GameHost.Features.Mods.Web.Components.Dialogs;

internal class CreateModListDialogViewModel : ICreateModListDialogViewModel
{

    public bool IsLoading { get; set; } = false;

    public Guid Id { get; set; } = Guid.NewGuid();
    public bool FirstRenderCompleted { get; set; } = true;

    public event Func<SpreadChangeOption, Task>? SpreadChanges;

    private const string ALPHA_NUMERIC_N_SPACES = @"^[A-Za-z0-9 ]+$";
    private readonly IStatePulse _statePulse;
    private readonly INotificationService _notification;

    private IDispatcher Dispatcher => _statePulse.Dispatcher;
    public ModListState ModListState => _statePulse.StateOf<ModListState>(() => this, UpdateChanges);
    public ModListLocalState ModListLocalState => _statePulse.StateOf<ModListLocalState>(() => this, UpdateChanges);
    public bool IsAllowedComplete { get; set; }
    public string Name { get; set; } = string.Empty;
    private bool _performInitialAttemptToSave = false;
    public bool IsBusy()
        => IsLoading || ModListState.IsLoadingAvailable || ModListLocalState.IsCreationLoading;

    public void OnParameterSet()
    {
        Console.WriteLine($"ModListLocalState.IsCreationLoading = {ModListLocalState.IsCreationLoading}");
        Console.WriteLine($"ModListLocalState.DidLastCreationFailed = {ModListLocalState.DidLastCreationFailed}");
        Console.WriteLine($"_performInitialAttemptToSave = {_performInitialAttemptToSave}");
        IsAllowedComplete = !ModListLocalState.IsCreationLoading && !ModListLocalState.DidLastCreationFailed && _performInitialAttemptToSave;

    }

    private async Task UpdateChanges()
    {
        SpreadChanges?.Invoke(SpreadChangeOption.TouchMyComponentOnly);
    }

    public CreateModListDialogViewModel(IStatePulse statePulse, INotificationService notification)
    {
        _statePulse = statePulse;
        _notification = notification;
    }
    public bool IsFormValid()
        => IsNotNullPass(Name) &&
           IsMaxCharacterPass(Name, 256) &&
           IsMinCharacterPass(Name, 5) &&
           IsAlphaNumericAndSpacePass(Name);

    public bool IsNotNullPass(string? str) => !string.IsNullOrWhiteSpace(str);
    public bool IsMaxCharacterPass(string str, int maxLength) => str.Length <= maxLength;
    public bool IsMinCharacterPass(string str, int minLength) => str.Length >= minLength;
    public bool IsAlphaNumericAndSpacePass(string str) => Regex.IsMatch(str, ALPHA_NUMERIC_N_SPACES);
    public async Task CreateAsync()
    {
        if (!IsFormValid())
        {
            await _notification.NotifyAsync("Validation Error", NotificationSeverity.Error);
            return;
        }
        IsLoading = true;
        _performInitialAttemptToSave = true;
        await UpdateChanges();
        await Dispatcher
            .Prepare<CreateModListAction>()
            .With(p => p.Id, Id)
            .With(p => p.Name, Name)
            .DispatchAsync();
        IsLoading = false;
        await UpdateChanges();

    }
}
