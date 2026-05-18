using LunaticPanel.Core.Abstraction.Widgets;
using StatePulse.Net;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GameHost.Features.Debugging.Web.Pages;

internal class DebuggingViewModel : WidgetViewModelBase, IDebuggingViewModel
{
    private readonly IServiceProvider _serviceProvider;
    private JsonSerializerOptions _serializerOptions = new()
    {
        WriteIndented = true,
        PropertyNameCaseInsensitive = true,
        ReferenceHandler = ReferenceHandler.IgnoreCycles
    };
    public List<Type> States { get; set; } = new();
    public Type? SelectedType { get; set; }
    public string CurrentPrint { get; set; } = "No State Selected";
    public DebuggingViewModel(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    protected override void OnViewModelInitialized()
    {
        Scan();
    }
    private void Scan()
    {
        var asm = Assembly.GetExecutingAssembly();

        States = asm.GetTypes()
               .Where(t => t.IsClass && !t.IsAbstract)
               .Where(t =>
                   typeof(IStateFeature).IsAssignableFrom(t) ||
                   typeof(IStateFeatureSingleton).IsAssignableFrom(t))
               .ToList();

    }

    public string GetStateFor(Type type)
    {
        var accessorType = typeof(IStateAccessor<>).MakeGenericType(type);
        var accessor = _serviceProvider.GetService(accessorType);
        var stateProp = accessorType.GetProperty("State");
        var stateValue = stateProp?.GetValue(accessor);
        return JsonSerializer.Serialize(stateValue, _serializerOptions);
    }

    public Task ChangeSelection(Type selected)
    {
        SelectedType = selected;
        CurrentPrint = GetStateFor(SelectedType);
        UpdateChanges();
        return Task.CompletedTask;
    }
}
