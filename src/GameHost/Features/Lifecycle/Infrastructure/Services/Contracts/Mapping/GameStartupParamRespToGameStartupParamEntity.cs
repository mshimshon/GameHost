using CoreMap;
using GameHost.Features.Lifecycle.Domain.Entites;
using GameHost.Features.Lifecycle.Domain.ValueObjects;

namespace GameHost.Features.Lifecycle.Infrastructure.Services.Contracts.Mapping;

public class GameStartupParamRespToGameStartupParamEntity : ICoreMapHandler<GameStartupParameterResponse, GameConfigParamaterEntity>
{
    public GameConfigParamaterEntity Handler(GameStartupParameterResponse data, ICoreMap alsoMap)
        => new()
        {
            Category = data.Category,
            DefaultValue = data.DefaultValue,
            Description = data.Description,
            Editable = data.Editable,
            Label = data.Label,
            Required = data.Required,
            Warning = data.Warning,
            RelatedTo = data.RelatedTo != default ? alsoMap.Map(data.RelatedTo).To<GameConfigParameterConstraintTypeEntity>() : default,
            Validation = data.Validation != default ? alsoMap.Map(data.Validation).To<GameConfigParameterValidationEntity>() : default,
            Key = alsoMap.Map(data).To<ConfigParameter>()
        };
}
