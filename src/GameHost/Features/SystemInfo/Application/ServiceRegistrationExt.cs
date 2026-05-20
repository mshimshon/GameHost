using GameHost.Features.SystemInfo.Application.Mediator.Queries;
using GameHost.Features.SystemInfo.Application.Mediator.Queries.Handlers;
using GameHost.Features.SystemInfo.Application.Payloads.Responses;
using GameHost.Features.SystemInfo.Application.Pulses.Actions;
using GameHost.Features.SystemInfo.Application.Pulses.Effects;
using GameHost.Features.SystemInfo.Application.Pulses.Reducers;
using GameHost.Features.SystemInfo.Application.Pulses.Reducers.Middlewares;
using GameHost.Features.SystemInfo.Application.Pulses.States;
using MedihatR;
using Microsoft.Extensions.DependencyInjection;
using StatePulse.Net;

namespace GameHost.Features.SystemInfo.Application;

public static class ServiceRegistrationExt
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddMedihaterRequestHandler<GetSystemInfoQuery, GetSystemInfoHandler, SystemInfoResponse?>();
        services.AddStatePulseService<SystemInfoUpdateAction>();
        services.AddStatePulseService<SystemInfoUpdatedAction>();
        services.AddStatePulseService<SystemInfoUpdateEffect>();
        services.AddStatePulseService<ServerSystemInfoUpdatedReducer>();
        services.AddStatePulseService<SystemInfoState>();
        services.AddStatePulseService<OnServerInfoUpdateMiddleware>();
    }

}
