using GameHost.Features.SystemInfo.Application.Pulses.Actions;
using GameHost.Features.SystemInfo.Application.Pulses.States;
using StatePulse.Net;

namespace GameHost.Features.SystemInfo.Application.Pulses.Reducers;

public class ServerSystemInfoUpdatedReducer : IReducer<SystemInfoState, SystemInfoUpdatedAction>
{
    public SystemInfoState Reduce(SystemInfoState state, SystemInfoUpdatedAction action)
    {
        Console.WriteLine("ServerSystemInfoUpdatedReducer OK! = " + state.ToString() + " || " + action.SystemInfo.ToString());
        return state with { SystemInfo = action.SystemInfo, LastUpdate = DateTime.UtcNow };
    }

}
