using GameHost.Features.SystemInfo.Application.Mediator.Queries;
using GameHost.Features.SystemInfo.Application.Mediator.Queries.Handlers;
using GameHost.Features.SystemInfo.Application.Payloads.Responses;
using GameHost.Features.SystemInfo.Application.Pulses.Actions;
using GameHost.Features.SystemInfo.Application.Pulses.Effects;
using GameHost.Features.SystemInfo.Application.Pulses.Reducers;
using GameHost.Features.SystemInfo.Application.Pulses.Reducers.Middlewares;
using GameHost.Features.SystemInfo.Application.Pulses.States;
using LunaticPanel.Core.Abstraction.DependencyInjection;
using MedihatR;
using StatePulse.Net;

namespace GameHost.Features.SystemInfo.Application;

public static class ServiceRegistrationExt
{
    public static void AddApplicationServices(this IPluginServiceCollection services)
    {
        services.CrossCircuitServices.AddStatePulseService<SystemInfoState>();

        services.Services.AddMedihaterRequestHandler<GetSystemInfoQuery, GetSystemInfoHandler, SystemInfoResponse?>();
        services.Services.AddStatePulseService<SystemInfoUpdateAction>();
        services.Services.AddStatePulseService<SystemInfoUpdatedAction>();
        services.Services.AddStatePulseService<SystemInfoUpdateEffect>();
        services.Services.AddStatePulseService<ServerSystemInfoUpdatedReducer>();
        services.Services.AddStatePulseService<OnServerInfoUpdateMiddleware>();
    }

}
