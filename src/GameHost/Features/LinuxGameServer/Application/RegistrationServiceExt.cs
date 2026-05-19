
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
using MedihatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StatePulse.Net;

namespace GameHost.Features.LinuxGameServer.Application;

public static class RegistrationServiceExt
{
    public static void AddApplicationServices(this IServiceCollection services, IServiceProvider singletonCrossCircuitSp,
        IConfiguration configuration,
        bool isMaster)
    {
        services.AddMedihaterRequestHandler<GetAvailableGameManifestsQuery, GetAvailableGameManifestsHandler, ICollection<GameManifestResponse>?>();
        services.AddStatePulseService<PopulateAvailableGamesForInstallAction>();
        services.AddStatePulseService<PopulateAvailableGamesForInstallDoneAction>();
        services.AddStatePulseService<PopulateAvailableGamesForInstallEffect>();
        services.AddStatePulseService<PopulateAvailableGamesForInstallReducer>();
        services.AddStatePulseService<PopulateAvailableGamesForInstallDoneReducer>();

        services.AddStatePulseService<InstallGameServerStartFailedAction>();
        services.AddStatePulseService<InstallGameServerStartFailedReducer>();


        services.AddStatePulseService<GameServerInstalledAction>();
        services.AddStatePulseService<InstallGameServerAction>();
        services.AddStatePulseService<GameServerInstallFailedAction>();
        services.AddStatePulseService<InstallGameServerActionEffect>();
        services.AddStatePulseService<InstallGameServerActionReducer>();
        services.AddStatePulseService<GameServerInstalledReducer>();
        services.AddStatePulseService<GameServerInstallFailedReducer>();
        services.AddStatePulseService<InstallationState>();
        services.AddStatePulseService<GameServerInstallStateLoadedAction>();
        services.AddStatePulseService<PopulateAvailableGamesForInstallDoneReducer>();
        services.AddStatePulseService<PopulateAvailableGamesForInstallAction>();
        services.AddStatePulseService<GameRepositoryState>();
        services.AddStatePulseService<RepositoryDownloadStartedAction>();
        services.AddStatePulseService<RepositoryDownloadStartedReducer>();
        services.AddStatePulseService<SpreadInstallationStateMiddleware>();
        services.AddStatePulseService<UpdateProgressStateFromDiskDoneAction>();
        services.AddStatePulseService<UpdateProgressStateFromDiskAction>();
        services.AddStatePulseService<UpdateProgressStateFromDiskEffect>();
        services.AddStatePulseService<UpdateProgressStateFromDiskDoneReducer>();

        services.AddStatePulseService<UpdateInstalledGameServerAction>();
        services.AddStatePulseService<UpdateInstalledGameServerDoneAction>();
        services.AddStatePulseService<UpdateInstalledGameServerDoneReducer>();
        services.AddStatePulseService<UpdateInstalledGameServerEffect>();


        services.AddMedihaterRequestHandler<InstallGameServerCommand, InstallGameServerHandler>();
        services.AddMedihaterRequestHandler<GetInstallationProgressQuery, GetInstallationProgressHandler, GameServerInstallProgressResponse?>();
        services.AddMedihaterRequestHandler<GetInstalledGameQuery, GetInstalledGameHandler, GameServerInfoResponse?>();
    }
}
