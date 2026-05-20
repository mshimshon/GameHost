using GameHost.Features.Mods.Application.Mediator.Commands;
using GameHost.Features.Mods.Application.Mediator.Commands.Handlers;
using GameHost.Features.Mods.Application.Mediator.Queries;
using GameHost.Features.Mods.Application.Mediator.Queries.Handlers;
using GameHost.Features.Mods.Application.Payloads.Responses;
using GameHost.Features.Mods.Application.Pulses.Actions;
using GameHost.Features.Mods.Application.Pulses.Effects;
using GameHost.Features.Mods.Application.Pulses.Reducers;
using GameHost.Features.Mods.Application.Pulses.States;
using MedihatR;
using Microsoft.Extensions.DependencyInjection;
using StatePulse.Net;

namespace GameHost.Features.Mods.Application;

public static class ModsApplicationServiceRegsiterExt
{
    public static void RegisterApplicationServices(this IServiceCollection services)
    {
        services.AddStatePulseService<MonitoredModListFolderUpdateAction>();
        services.AddStatePulseService<MonitoredModListFolderUpdateEffect>();
        services.AddStatePulseService<CloseModListAction>();
        services.AddStatePulseService<CloseModListReducer>();

        services.AddStatePulseService<LoadModFeatureReducer>();
        services.AddStatePulseService<LoadModFeatureDoneReducer>();
        services.AddStatePulseService<LoadModFeatureEffect>();
        services.AddStatePulseService<LoadModFeatureAction>();
        services.AddStatePulseService<LoadModFeatureDoneAction>();

        services.AddStatePulseService<UpdateCurrentModListAction>();
        services.AddStatePulseService<UpdateCurrentModListDoneAction>();
        services.AddStatePulseService<UpdateCurrentModListEffect>();
        services.AddStatePulseService<UpdateCurrentModListDoneReducer>();
        services.AddStatePulseService<UpdateCurrentModlistReducer>();

        services.AddStatePulseService<GetCurrentModListAction>();
        services.AddStatePulseService<GetCurrentModListDoneAction>();
        services.AddStatePulseService<GetCurrentModListEffect>();
        services.AddStatePulseService<GetCurrentModListReducer>();
        services.AddStatePulseService<GetCurrentModListDoneReducer>();

        services.AddStatePulseService<GetModListAction>();
        services.AddStatePulseService<GetModListDoneAction>();
        services.AddStatePulseService<GetModListEffect>();
        services.AddStatePulseService<GetModListDoneReducer>();
        services.AddStatePulseService<GetModListReducer>();

        services.AddStatePulseService<CreateModListAction>();
        services.AddStatePulseService<CreateModListDoneAction>();
        services.AddStatePulseService<CreateModListEffect>();
        services.AddStatePulseService<CreateModListReducer>();
        services.AddStatePulseService<CreateModListDoneReducer>();

        services.AddStatePulseService<GetAvailableModListAction>();
        services.AddStatePulseService<GetAvailableModListDoneAction>();
        services.AddStatePulseService<GetAvailableModListEffect>();
        services.AddStatePulseService<GetAvailableModListReducer>();
        services.AddStatePulseService<GetAvailableModListDoneReducer>();


        services.AddStatePulseService<LoadModListSchematicAction>();
        services.AddStatePulseService<LoadModListSchematicDoneAction>();
        services.AddStatePulseService<LoadModListSchematicEffect>();
        services.AddStatePulseService<LoadModListSchematicReducer>();
        services.AddStatePulseService<LoadModListSchematicDoneReducer>();

        services.AddStatePulseService<DeleteModListAction>();
        services.AddStatePulseService<DeleteModListDoneAction>();
        services.AddStatePulseService<DeleteModListEffect>();
        services.AddStatePulseService<DeleteModListReducer>();
        services.AddStatePulseService<DeleteModListDoneReducer>();


        services.AddStatePulseService<SaveModListAction>();
        services.AddStatePulseService<SaveModListDoneAction>();
        services.AddStatePulseService<SaveModListEffect>();
        services.AddStatePulseService<SaveModListReducer>();
        services.AddStatePulseService<SaveModListDoneReducer>();

        services.AddStatePulseService<ModListState>();
        services.AddStatePulseService<ModListLocalState>();

        services.AddMedihaterRequestHandler<GetModListQuery, GetModListHandler, ModListResponse?>();
        services.AddMedihaterRequestHandler<CreateModListCommand, CreateModListHandler>();
        services.AddMedihaterRequestHandler<GetAllModListQuery, GetAllModListHandler, ICollection<ModListDescriptorResponse>>();
        services.AddMedihaterRequestHandler<GetModSchematicQuery, GetModSchematicHandler, ICollection<PartSchematicResponse>?>();
        services.AddMedihaterRequestHandler<DeleteModListCommand, DeleteModListHandler>();
        services.AddMedihaterRequestHandler<SaveModListCommand, SaveModListHandler>();
        services.AddMedihaterRequestHandler<UpdateCurrentModlistCommand, UpdateCurrentModListHandler>();
        services.AddMedihaterRequestHandler<GetCurrentModListQuery, GetCurrentModListHandler, Guid?>();
        services.AddMedihaterRequestHandler<GetModFeatureQuery, GetModFeatureHandler, ModFeatureResponse?>();



    }
}
