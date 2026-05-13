using GameHost.Core.Features;
using GameHost.Features.Lifecycle.Application.Mediator.Commands;
using GameHost.Features.Lifecycle.Application.Mediator.Commands.Handlers;
using GameHost.Features.Lifecycle.Application.Mediator.Queries;
using GameHost.Features.Lifecycle.Application.Mediator.Queries.Handlers;
using GameHost.Features.Lifecycle.Application.Payloads.Responses.Events.Mapping;
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
using MedihatR;
using Microsoft.Extensions.DependencyInjection;
using StatePulse.Net;

namespace GameHost.Features.Lifecycle;

public static class LifecycleServiceExt
{
    private const string USER_DEF_STARTUP_PARAM_FILE = "user_defined_startup_params.json";

    public static void AddLifecycleFeatureServices(this IServiceCollection services, bool isMaster)
    {
        services.AddScoped<IServerControlViewModel, ServerControlViewModel>();
        services.AddScoped<IStartupParameterViewModel, StartupParameterViewModel>();

        services.AddTransient<IStartupParameterFieldViewModel, StartupParameterFieldViewModel>();
        services.AddScoped<LifecycleServices>();
        services.AddScoped<ILifecycleServices>(sp => sp.GetRequiredService<LifecycleServices>());
        services.AddScoped<IGameInfoService>(sp => sp.GetRequiredService<LifecycleServices>());
        services.AddScoped<IStartupParameterService>(sp => sp.GetRequiredService<LifecycleServices>());

        services.AddStatePulseService<SpreadGameInfoStateMiddleware>();
        services.AddStatePulseService<ServerTransitionState>();
        services.AddStatePulseService<TransitionAction>();
        services.AddStatePulseService<TransitionEffect>();
        services.AddStatePulseService<TransitionInstigatorSetMeUpAction>();
        services.AddStatePulseService<TransitionInstigatorUnSetMeAction>();
        services.AddStatePulseService<TransitionCareAddAction>();
        services.AddStatePulseService<TransitionCareAddReducer>();
        services.AddStatePulseService<TransitionCareRemoveAction>();
        services.AddStatePulseService<TransitionCareRemoveReducer>();
        services.AddStatePulseService<TransitionInstigatorSetMeUpReducer>();
        services.AddStatePulseService<TransitionInstigatorUnSetMeReducer>();

        services.AddStatePulseService<ServerStartReducer>();
        services.AddStatePulseService<FetchStartupParametersAction>();
        services.AddStatePulseService<FetchStartupParametersDoneAction>();
        services.AddStatePulseService<ServerGameInfoUpdateDoneAction>();
        services.AddStatePulseService<ServerStartAction>();
        services.AddStatePulseService<TransitionDoneAction>();
        services.AddStatePulseService<ServerStatusUpdateDoneAction>();
        services.AddStatePulseService<ServerStopAction>();
        services.AddStatePulseService<UpdateStartupParameterAction>();
        services.AddStatePulseService<UpdateStartupParameterDoneAction>();
        services.AddStatePulseService<ServerGameInfoUpdateAction>();
        services.AddStatePulseService<ServerGameInfoUpdateEffect>();
        services.AddStatePulseService<FetchStartupParametersEffect>();
        services.AddStatePulseService<ServerStartEffect>();
        services.AddStatePulseService<ServerStopEffect>();
        services.AddStatePulseService<UpdateStartupParameterEffect>();
        services.AddStatePulseService<FetchStartupParametersDoneReducer>();
        services.AddStatePulseService<ServerGameInfoUpdatedReducer>();
        services.AddStatePulseService<TransitionDoneReducer>();
        services.AddStatePulseService<ServerStatusUpdateDoneReducer>();
        services.AddStatePulseService<ServerStopReducer>();
        services.AddStatePulseService<GameInfoState>();
        services.AddStatePulseService<ServerState>();

        services.AddStatePulseService<ServerStatusUpdateAction>();
        services.AddStatePulseService<ServerStatusUpdateEffect>();

        services.AddMedihaterRequestHandler<GetServerStatusQuery, GetServerStatusHandler, ServerInfoEntity?>();
        services.AddMedihaterRequestHandler<GetStartupParametersQuery, GetStartupParametersHandler, Dictionary<string, string>>();
        services.AddMedihaterRequestHandler<ExecRestartServerCommand, ExecRestartServerHandler>();
        services.AddMedihaterRequestHandler<ExecStartServerCommand, ExecStartServerHandler>();
        services.AddMedihaterRequestHandler<ExecStopServerCommand, ExecStopServerHandler>();
        services.AddMedihaterRequestHandler<ExecUpdateStartupParameterCommand, ExecUpdateStartupParameterHandler>();
        services.AddMedihaterRequestHandler<GetGameInfoQuery, GetGameInfoHandler, GameInfoEntity?>();
        services.AddMedihaterRequestHandler<GetRawGameInfoQuery, GetRawGameInfoHandler, string?>();

        services.AddCoreMapHandler<AllowedValueResponseToAllowedValueEntity>();
        services.AddCoreMapHandler<GameStartupParameterResponseToStartupParameter>();
        services.AddCoreMapHandler<GameStartupParamRespToGameStartupParamEntity>();
        services.AddCoreMapHandler<RelatedToResponseToConstraintTypeEntity>();
        services.AddCoreMapHandler<ValidationResponseToValidationEntity>();
        services.AddCoreMapHandler<GameInfoResponseToGameInfoEntity>();
        services.AddCoreMapHandler<StatusResponseToServerInfoEntity>();
        services.AddCoreMapHandler<PortInfoResponseToConnectionPort>();

        services.AddCoreMapHandler<ServerStateToServerStateTransitionResponse>();

        if (isMaster)
        {
            services.AddMasterStateFileWatcherService<FetchStartupParametersAction>(
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
