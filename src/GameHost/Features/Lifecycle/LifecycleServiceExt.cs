using GameHost.Core.Features;
using GameHost.Features.Lifecycle.Application.Mediator.Commands;
using GameHost.Features.Lifecycle.Application.Mediator.Commands.Handlers;
using GameHost.Features.Lifecycle.Application.Mediator.Queries;
using GameHost.Features.Lifecycle.Application.Mediator.Queries.Handlers;
using GameHost.Features.Lifecycle.Application.Payloads.Responses.GameInfo;
using GameHost.Features.Lifecycle.Application.Payloads.Responses.ServerInfo;
using GameHost.Features.Lifecycle.Application.Pulses.Actions;
using GameHost.Features.Lifecycle.Application.Pulses.Effects;
using GameHost.Features.Lifecycle.Application.Pulses.Reducers;
using GameHost.Features.Lifecycle.Application.Pulses.Reducers.Middlewares;
using GameHost.Features.Lifecycle.Application.Pulses.States;
using GameHost.Features.Lifecycle.Application.Services;
using GameHost.Features.Lifecycle.Infrastructure.Services;
using GameHost.Features.Lifecycle.Web.Components;
using GameHost.Features.Lifecycle.Web.Components.ViewModels;
using GameHost.Kernel.Abstractions.Services.ActionFileWatcher.Enums;
using GameHost.Kernel.Extensions;
using LunaticPanel.Core.Abstraction.DependencyInjection;
using MedihatR;
using Microsoft.Extensions.DependencyInjection;
using StatePulse.Net;

namespace GameHost.Features.Lifecycle;

public static class LifecycleServiceExt
{
    private const string USER_DEF_STARTUP_PARAM_FILE = "user_defined_startup_params.json";

    public static void AddLifecycleFeatureServices(this IPluginServiceCollection services, bool isMaster)
    {
        services.AddScoped<IServerControlViewModel, ServerControlViewModel>();
        services.AddScoped<IStartupParameterViewModel, StartupParameterViewModel>();

        services.AddTransient<IStartupParameterFieldViewModel, StartupParameterFieldViewModel>();
        services.AddScoped<LifecycleServices>();
        services.AddScoped<ILifecycleServices>(sp => sp.GetRequiredService<LifecycleServices>());
        services.AddScoped<IGameInfoService>(sp => sp.GetRequiredService<LifecycleServices>());
        services.AddScoped<IStartupParameterService>(sp => sp.GetRequiredService<LifecycleServices>());

        services.Services.AddStatePulseService<SpreadGameInfoStateMiddleware>();
        services.Services.AddStatePulseService<ServerTransitionState>();
        services.Services.AddStatePulseService<TransitionAction>();
        services.Services.AddStatePulseService<TransitionEffect>();
        services.Services.AddStatePulseService<TransitionInstigatorSetMeUpAction>();
        services.Services.AddStatePulseService<TransitionInstigatorUnSetMeAction>();
        services.Services.AddStatePulseService<TransitionCareAddAction>();
        services.Services.AddStatePulseService<TransitionCareAddReducer>();
        services.Services.AddStatePulseService<TransitionCareRemoveAction>();
        services.Services.AddStatePulseService<TransitionCareRemoveReducer>();
        services.Services.AddStatePulseService<TransitionInstigatorSetMeUpReducer>();
        services.Services.AddStatePulseService<TransitionInstigatorUnSetMeReducer>();

        services.Services.AddStatePulseService<ServerStartReducer>();
        services.Services.AddStatePulseService<FetchStartupParametersAction>();
        services.Services.AddStatePulseService<FetchStartupParametersDoneAction>();
        services.Services.AddStatePulseService<ServerGameInfoUpdateDoneAction>();
        services.Services.AddStatePulseService<ServerStartAction>();
        services.Services.AddStatePulseService<TransitionDoneAction>();
        services.Services.AddStatePulseService<ServerStatusUpdateDoneAction>();
        services.Services.AddStatePulseService<ServerStopAction>();
        services.Services.AddStatePulseService<UpdateStartupParameterAction>();
        services.Services.AddStatePulseService<UpdateStartupParameterDoneAction>();
        services.Services.AddStatePulseService<ServerGameInfoUpdateAction>();
        services.Services.AddStatePulseService<ServerGameInfoUpdateEffect>();
        services.Services.AddStatePulseService<FetchStartupParametersEffect>();
        services.Services.AddStatePulseService<ServerStartEffect>();
        services.Services.AddStatePulseService<ServerStopEffect>();
        services.Services.AddStatePulseService<UpdateStartupParameterEffect>();
        services.Services.AddStatePulseService<FetchStartupParametersDoneReducer>();
        services.Services.AddStatePulseService<ServerGameInfoUpdatedReducer>();
        services.Services.AddStatePulseService<TransitionDoneReducer>();
        services.Services.AddStatePulseService<ServerStatusUpdateDoneReducer>();
        services.Services.AddStatePulseService<ServerStopReducer>();
        services.CrossCircuitServices.AddStatePulseService<GameInfoState>();
        services.CrossCircuitServices.AddStatePulseService<ServerState>();

        services.Services.AddStatePulseService<ServerStatusUpdateAction>();
        services.Services.AddStatePulseService<ServerStatusUpdateEffect>();

        services.Services.AddMedihaterRequestHandler<GetServerStatusQuery, GetServerStatusHandler, ServerInfoResponse?>();
        services.Services.AddMedihaterRequestHandler<GetStartupParametersQuery, GetStartupParametersHandler, Dictionary<string, string>>();
        services.Services.AddMedihaterRequestHandler<ExecRestartServerCommand, ExecRestartServerHandler>();
        services.Services.AddMedihaterRequestHandler<ExecStartServerCommand, ExecStartServerHandler>();
        services.Services.AddMedihaterRequestHandler<ExecStopServerCommand, ExecStopServerHandler>();
        services.Services.AddMedihaterRequestHandler<ExecUpdateStartupParameterCommand, ExecUpdateStartupParameterHandler>();
        services.Services.AddMedihaterRequestHandler<GetGameInfoQuery, GetGameInfoHandler, GameInfoResponse?>();
        services.Services.AddMedihaterRequestHandler<GetRawGameInfoQuery, GetRawGameInfoHandler, string?>();


        if (isMaster)
        {
            services.Services.AddMasterStateFileWatcherService<FetchStartupParametersAction>(
                c =>
                {
                    string path = c.GetUserConfigBase(LifecycleKeys.MODULE_NAME, LinuxGameServerKeys.USERNAME);
                    Console.WriteLine(path);
                    return path;
                },
                USER_DEF_STARTUP_PARAM_FILE,
                [FileWatchEvents.Any]);

        }
    }
    public static async Task RuntimeLifecycleInitializer(this IServiceProvider serviceProvider, bool isMaster)
    {
        serviceProvider.LoadWatcher<FetchStartupParametersAction>();
    }

}
