
using GameHost.Features.LinuxGameServer.Application.Mediator.Commands;
using GameHost.Features.LinuxGameServer.Application.Mediator.Commands.Handlers;
using GameHost.Features.LinuxGameServer.Application.Mediator.Queries;
using GameHost.Features.LinuxGameServer.Application.Mediator.Queries.Handlers;
using GameHost.Features.LinuxGameServer.Application.Payloads.Responses;
using GameHost.Features.LinuxGameServer.Application.Pulses.Actions;
using GameHost.Features.LinuxGameServer.Application.Pulses.Effects;
using GameHost.Features.LinuxGameServer.Application.Pulses.Reducers;
using GameHost.Features.LinuxGameServer.Application.Pulses.Reducers.Middlewares;
using GameHost.Features.LinuxGameServer.Application.Pulses.States;
using LunaticPanel.Core.Abstraction.DependencyInjection;
using MedihatR;
using Microsoft.Extensions.Configuration;
using StatePulse.Net;

namespace GameHost.Features.LinuxGameServer.Application;

public static class RegistrationServiceExt
{
    public static void AddApplicationServices(this IPluginServiceCollection services, IServiceProvider singletonCrossCircuitSp,
        IConfiguration configuration,
        bool isMaster)
    {
        services.Services.AddMedihaterRequestHandler<GetAvailableGameManifestsQuery, GetAvailableGameManifestsHandler, ICollection<GameManifestResponse>?>();
        services.Services.AddStatePulseService<PopulateAvailableGamesForInstallAction>();
        services.Services.AddStatePulseService<PopulateAvailableGamesForInstallDoneAction>();
        services.Services.AddStatePulseService<PopulateAvailableGamesForInstallEffect>();
        services.Services.AddStatePulseService<PopulateAvailableGamesForInstallReducer>();
        services.Services.AddStatePulseService<PopulateAvailableGamesForInstallDoneReducer>();

        services.Services.AddStatePulseService<InstallGameServerStartFailedAction>();
        services.Services.AddStatePulseService<InstallGameServerStartFailedReducer>();
        services.CrossCircuitServices.AddStatePulseService<InstallationState>();
        services.CrossCircuitServices.AddStatePulseService<GameRepositoryState>();


        services.Services.AddStatePulseService<GameServerInstalledAction>();
        services.Services.AddStatePulseService<InstallGameServerAction>();
        services.Services.AddStatePulseService<GameServerInstallFailedAction>();
        services.Services.AddStatePulseService<InstallGameServerActionEffect>();
        services.Services.AddStatePulseService<InstallGameServerActionReducer>();
        services.Services.AddStatePulseService<GameServerInstalledReducer>();
        services.Services.AddStatePulseService<GameServerInstallFailedReducer>();
        services.Services.AddStatePulseService<GameServerInstallStateLoadedAction>();
        services.Services.AddStatePulseService<PopulateAvailableGamesForInstallDoneReducer>();
        services.Services.AddStatePulseService<PopulateAvailableGamesForInstallAction>();
        services.Services.AddStatePulseService<RepositoryDownloadStartedAction>();
        services.Services.AddStatePulseService<RepositoryDownloadStartedReducer>();
        services.Services.AddStatePulseService<SpreadInstallationStateMiddleware>();
        services.Services.AddStatePulseService<UpdateProgressStateFromDiskDoneAction>();
        services.Services.AddStatePulseService<UpdateProgressStateFromDiskAction>();
        services.Services.AddStatePulseService<UpdateProgressStateFromDiskEffect>();
        services.Services.AddStatePulseService<UpdateProgressStateFromDiskDoneReducer>();

        services.Services.AddStatePulseService<UpdateInstalledGameServerAction>();
        services.Services.AddStatePulseService<UpdateInstalledGameServerDoneAction>();
        services.Services.AddStatePulseService<UpdateInstalledGameServerDoneReducer>();
        services.Services.AddStatePulseService<UpdateInstalledGameServerEffect>();


        services.Services.AddMedihaterRequestHandler<InstallGameServerCommand, InstallGameServerHandler>();
        services.Services.AddMedihaterRequestHandler<GetInstallationProgressQuery, GetInstallationProgressHandler, GameServerInstallProgressResponse?>();
        services.Services.AddMedihaterRequestHandler<GetInstalledGameQuery, GetInstalledGameHandler, GameServerInfoResponse?>();
    }
}
