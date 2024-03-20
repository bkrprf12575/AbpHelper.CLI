using EasyAbp.AbpHelper.Core.Commands;
using EasyAbp.AbpHelper.Core.Commands.Generate.Crud;
using EasyAbp.AbpHelper.Core.Steps.Common;
using Elsa;
using Elsa.Activities.ControlFlow.Activities;
using Elsa.Scripting.JavaScript;
using Elsa.Services;

namespace EasyAbp.AbpHelper.Core.Workflow.Generate.Crud;

public static class EntityConstantsGenerationWorkflow
{
    public static IActivityBuilder AddEntityConstantsGenerationWorkflow(this IActivityBuilder builder)
    {
        return builder
              .Then<IfElse>(
              step => step.ConditionExpression = new JavaScriptExpression<bool>($"{CommandConsts.OptionVariableName}.{nameof(CrudCommandOption.SkipEntityConstants)}"),
              ifElse =>
              {
                  ifElse.When(OutcomeNames.False)
                      .Then<GroupGenerationStep>(
                          step =>
                          {
                              /* Generate constants files */
                              step.GroupName = "Domain";
                              step.TargetDirectory = new JavaScriptExpression<string>(VariableNames.AspNetCoreDir);
                          }
                      )
                      .Then("EntityConstructors")
                      ;
                  ifElse.When(OutcomeNames.True)
                      .Then("EntityConstructors")
                      ;
              });
    }
}
