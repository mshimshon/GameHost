using CoreMap;
using GameHost.Features.Lifecycle.Domain.Entites;
using GameHost.Features.Lifecycle.Domain.ValueObjects;

namespace GameHost.Features.Lifecycle.Infrastructure.Services.Contracts.Mapping;

public class ValidationResponseToValidationEntity : ICoreMapHandler<GameStartupParameterValidationResponse, GameConfigParameterValidationEntity>
{
    public GameConfigParameterValidationEntity Handler(GameStartupParameterValidationResponse data, ICoreMap alsoMap)
        => new()
        {
            Max = data.Max,
            Min = data.Min,
            MaxLength = data.MaxLength,
            MinLength = data.MinLength,
            UnitPrefix = data.UnitPrefix,
            UnitSuffix = data.UnitSuffix,
            PatternValidation = data.Pattern,
            AllowedValues = data.AllowedValues != default ? alsoMap.MapEach(data.AllowedValues).To<ConfigParameterAllowedValue>() : default
        };
}
