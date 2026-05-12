using GameHost.Core.Features;
using GameHost.Features.Mods.Application.Pulses.Actions;
using GameHost.Features.Mods.Infrastructure;
using GameHost.Features.Mods.Web.Components;
using GameHost.Features.Mods.Web.Components.Dialogs;
using GameHost.Features.Mods.Web.Components.Dialogs.ViewModels;
using GameHost.Features.Mods.Web.Components.ViewModels;
using GameHost.Features.Mods.Web.Hooks.UI.Components;
using GameHost.Features.Mods.Web.Hooks.UI.Components.ViewModels;
using GameHost.Features.Mods.Web.Pages;
using GameHost.Features.Mods.Web.Pages.ViewModels;
using GameHost.Kernel.Abstractions.Extensions;
using GameHost.Kernel.Abstractions.Services.ActionFileWatcher.Enums;
using GameHost.Kernel.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace GameHost.Features.Mods;

public static class ModServiceRegistrationExt
{
    public static void AddModFeatureServices(this IServiceCollection services, bool isMaster)
    {
        services.AddScoped<IModListSelectorViewModel, ModListSelectorViewModel>();
        services.AddScoped<IWidgetModlistSelectorViewModel, WidgetModlistSelectorViewModel>();
        services.AddScoped<IModListEditorViewModel, ModListEditorViewModel>();
        services.AddScoped<IModListExplorerViewModel, ModListExplorerViewModel>();
        services.AddScoped<IModListFilesViewModel, ModListFilesViewModel>();
        services.AddScoped<IModListToolbarViewModel, ModListToolbarViewModel>();
        services.AddScoped<IWidgetModListWorkspaceViewModel, WidgetModListWorkspaceViewModel>();
        services.AddScoped<IModListHomeViewModel, ModListHomeViewModel>();
        services.AddTransient<ICreateModListDialogViewModel, CreateModListDialogViewModel>();
        services.AddTransient<IModListEditorCreateModDialogViewModel, ModListEditorCreateModDialogViewModel>();
        services.RegisterModInfrastructureServices();
        if (isMaster)
        {
            services.AddMasterStateFileWatcherService<MonitoredModListFolderUpdateAction>(
            c =>
            {
                return c.GetUserConfigBase(ModListKeys.MODULE_NAME, [ModListKeys.USER_SAVED_MODLIST_FOLDER_NAME], LinuxGameServerKeys.USERNAME);
            },
            "",
            [FileWatchEvents.Any]);
        }

    }

    public static async Task RuntimeModInitializer(this IServiceProvider serviceProvider, bool isMaster)
    {
        if (isMaster)
        {
            serviceProvider.LoadWatcher<MonitoredModListFolderUpdateAction>();
            await serviceProvider.DispatchAction<GetAvailableModListAction>();
            await serviceProvider.DispatchAction<GetCurrentModListAction>();
        }
    }
}
