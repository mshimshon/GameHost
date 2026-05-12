using CoreMap;
using GameHost.Features.Lifecycle.Domain.Entites;
using GameHost.Features.Lifecycle.Domain.Enums;

namespace GameHost.Features.Lifecycle.Infrastructure.Services.Contracts.Mapping;

public class RelatedToResponseToConstraintTypeEntity : ICoreMapHandler<GameStartupParameterRelatedToResponse, GameConfigParameterConstraintTypeEntity>
{
    public GameConfigParameterConstraintTypeEntity Handler(GameStartupParameterRelatedToResponse data, ICoreMap alsoMap)
        => new GameConfigParameterConstraintTypeEntity()
        {
            Constraint = data.Constraint.ToLower() switch
            {
                "equals" => ConfigParameterConstraintType.Equals,
                "greaterthan" => ConfigParameterConstraintType.GreaterThan,
                "greaterthanorequal" => ConfigParameterConstraintType.GreaterThanOrEqual,
                "lessthan" => ConfigParameterConstraintType.LessThan,
                "lessthanorequal" => ConfigParameterConstraintType.LessThanOrEqual,
                _ => ConfigParameterConstraintType.NotEquals
            },
            Key = data.Key,
            Message = data.Key
        };
}
