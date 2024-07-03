using EasyAbp.AbpHelper.Core.Steps.Abp.ModificationCreatorSteps.CSharp;
using EasyAbp.AbpHelper.Core.Steps.Common;
using Elsa.Scripting.JavaScript;
using Elsa.Services;

namespace EasyAbp.AbpHelper.Core.Workflow.Generate.Crud;

public static class EntityErrorCodesGenerationWorkflow
{
    public static IActivityBuilder AddEntityErrorCodesGenerationWorkflow(this IActivityBuilder builder)
    {
        return builder
                .Then<FileFinderStep>(
                    step => step.SearchFileName = new JavaScriptExpression<string>("`*${ProjectInfo.Name}ErrorCodes.cs`")
                )
                .Then<EntityErrorCodesStep>()
                .Then<FileModifierStep>()
            ;
    }
}
