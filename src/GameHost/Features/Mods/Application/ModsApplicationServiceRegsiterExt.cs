using GameHost.Features.Mods.Application.Mediator.Commands;
using GameHost.Features.Mods.Application.Mediator.Commands.Handlers;
using GameHost.Features.Mods.Application.Mediator.Queries;
using GameHost.Features.Mods.Application.Mediator.Queries.Handlers;
using GameHost.Features.Mods.Application.Payloads.Responses;
using GameHost.Features.Mods.Application.Pulses.Actions;
using GameHost.Features.Mods.Application.Pulses.Effects;
using GameHost.Features.Mods.Application.Pulses.Reducers;
using GameHost.Features.Mods.Application.Pulses.States;
using LunaticPanel.Core.Abstraction.DependencyInjection;
using MedihatR;
using StatePulse.Net;

namespace GameHost.Features.Mods.Application;

public static class ModsApplicationServiceRegsiterExt
{
    public static void RegisterApplicationServices(this IPluginServiceCollection services)
    {
        services.Services.AddStatePulseService<MonitoredModListFolderUpdateAction>();
        services.Services.AddStatePulseService<MonitoredModListFolderUpdateEffect>();
        services.Services.AddStatePulseService<CloseModListAction>();
        services.Services.AddStatePulseService<CloseModListReducer>();

        services.Services.AddStatePulseService<LoadModFeatureReducer>();
        services.Services.AddStatePulseService<LoadModFeatureDoneReducer>();
        services.Services.AddStatePulseService<LoadModFeatureEffect>();
        services.Services.AddStatePulseService<LoadModFeatureAction>();
        services.Services.AddStatePulseService<LoadModFeatureDoneAction>();

        services.Services.AddStatePulseService<UpdateCurrentModListAction>();
        services.Services.AddStatePulseService<UpdateCurrentModListDoneAction>();
        services.Services.AddStatePulseService<UpdateCurrentModListEffect>();
        services.Services.AddStatePulseService<UpdateCurrentModListDoneReducer>();
        services.Services.AddStatePulseService<UpdateCurrentModlistReducer>();

        services.Services.AddStatePulseService<GetCurrentModListAction>();
        services.Services.AddStatePulseService<GetCurrentModListDoneAction>();
        services.Services.AddStatePulseService<GetCurrentModListEffect>();
        services.Services.AddStatePulseService<GetCurrentModListReducer>();
        services.Services.AddStatePulseService<GetCurrentModListDoneReducer>();

        services.Services.AddStatePulseService<GetModListAction>();
        services.Services.AddStatePulseService<GetModListDoneAction>();
        services.Services.AddStatePulseService<GetModListEffect>();
        services.Services.AddStatePulseService<GetModListDoneReducer>();
        services.Services.AddStatePulseService<GetModListReducer>();

        services.Services.AddStatePulseService<CreateModListAction>();
        services.Services.AddStatePulseService<CreateModListDoneAction>();
        services.Services.AddStatePulseService<CreateModListEffect>();
        services.Services.AddStatePulseService<CreateModListReducer>();
        services.Services.AddStatePulseService<CreateModListDoneReducer>();

        services.Services.AddStatePulseService<GetAvailableModListAction>();
        services.Services.AddStatePulseService<GetAvailableModListDoneAction>();
        services.Services.AddStatePulseService<GetAvailableModListEffect>();
        services.Services.AddStatePulseService<GetAvailableModListReducer>();
        services.Services.AddStatePulseService<GetAvailableModListDoneReducer>();


        services.Services.AddStatePulseService<LoadModListSchematicAction>();
        services.Services.AddStatePulseService<LoadModListSchematicDoneAction>();
        services.Services.AddStatePulseService<LoadModListSchematicEffect>();
        services.Services.AddStatePulseService<LoadModListSchematicReducer>();
        services.Services.AddStatePulseService<LoadModListSchematicDoneReducer>();

        services.Services.AddStatePulseService<DeleteModListAction>();
        services.Services.AddStatePulseService<DeleteModListDoneAction>();
        services.Services.AddStatePulseService<DeleteModListEffect>();
        services.Services.AddStatePulseService<DeleteModListReducer>();
        services.Services.AddStatePulseService<DeleteModListDoneReducer>();


        services.Services.AddStatePulseService<SaveModListAction>();
        services.Services.AddStatePulseService<SaveModListDoneAction>();
        services.Services.AddStatePulseService<SaveModListEffect>();
        services.Services.AddStatePulseService<SaveModListReducer>();
        services.Services.AddStatePulseService<SaveModListDoneReducer>();

        services.CrossCircuitServices.AddStatePulseService<ModListState>();
        services.Services.AddStatePulseService<ModListLocalState>();

        services.Services.AddMedihaterRequestHandler<GetModListQuery, GetModListHandler, ModListResponse?>();
        services.Services.AddMedihaterRequestHandler<CreateModListCommand, CreateModListHandler>();
        services.Services.AddMedihaterRequestHandler<GetAllModListQuery, GetAllModListHandler, ICollection<ModListDescriptorResponse>>();
        services.Services.AddMedihaterRequestHandler<GetModSchematicQuery, GetModSchematicHandler, ICollection<PartSchematicResponse>?>();
        services.Services.AddMedihaterRequestHandler<DeleteModListCommand, DeleteModListHandler>();
        services.Services.AddMedihaterRequestHandler<SaveModListCommand, SaveModListHandler>();
        services.Services.AddMedihaterRequestHandler<UpdateCurrentModlistCommand, UpdateCurrentModListHandler>();
        services.Services.AddMedihaterRequestHandler<GetCurrentModListQuery, GetCurrentModListHandler, Guid?>();
        services.Services.AddMedihaterRequestHandler<GetModFeatureQuery, GetModFeatureHandler, ModFeatureResponse?>();



    }
}
