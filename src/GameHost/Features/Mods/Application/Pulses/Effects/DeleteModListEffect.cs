using GameHost.Features.Mods.Application.Mediator.Commands;
using GameHost.Features.Mods.Application.Pulses.Actions;
using GameHost.Features.Mods.Application.Pulses.States;
using MedihatR;
using StatePulse.Net;

namespace GameHost.Features.Mods.Application.Pulses.Effects;

internal sealed class DeleteModListEffect : IEffect<DeleteModListAction>
{
    private readonly IMedihater _medihater;
    private readonly IStateAccessor<ModListLocalState> _modlistLocalStateAccess;

    public DeleteModListEffect(IMedihater medihater, IStateAccessor<ModListLocalState> modlistLocalStateAccess)
    {
        _medihater = medihater;
        _modlistLocalStateAccess = modlistLocalStateAccess;
    }
    public async Task EffectAsync(DeleteModListAction action, IDispatcher dispatcher)
    {
        var command = new DeleteModListCommand(action.Id);
        await _medihater.Send(command);
        bool resetOrNot = _modlistLocalStateAccess.State.Current != default && _modlistLocalStateAccess.State.Current.Descriptor.Id == action.Id;

        await dispatcher
            .Prepare<DeleteModListDoneAction>()
            .With(p => p.ResetCurrent, resetOrNot)
            .DispatchAsync();
    }
}
