using System;
using EasyAbp.AbpHelper.Core.Commands.Generate.Crud;
using EasyAbp.AbpHelper.Core.Models;
using Elsa.Results;
using Elsa.Services.Models;
using Microsoft.Extensions.Logging;

namespace EasyAbp.AbpHelper.Core.Steps.Abp
{
    public class BuildDtoInfoStep : Step
    {
        protected override ActivityExecutionResult OnExecute(WorkflowExecutionContext context)
        {
            var entityInfo = context.GetVariable<EntityInfo>("EntityInfo");
            var option = context.GetVariable<object>("Option") as CrudCommandOption;

            try
            {
                string[] actionNames = { string.Empty, string.Empty, string.Empty};                

                if (option is { SeparateDto: true })
                {
                    actionNames[1] = "Create";
                    actionNames[2] = "Update";
                }
                else
                {
                    actionNames[1] = "CreateUpdate";
                    actionNames[2] = actionNames[1];
                }

                var typeNames = new string[actionNames.Length];

                var useEntityPrefix = option is { EntityPrefixDto: true };
                var dtoSubfix = option?.DtoSuffix ?? "Dto";

                var readTypeName = typeNames[0] = $"{entityInfo.Name}Dto";

                for (var i = 1; i < typeNames.Length; i++)
                {
                    typeNames[i] = useEntityPrefix
                        ? $"{entityInfo.Name}{actionNames[i]}{dtoSubfix}"
                        : $"{actionNames[i]}{entityInfo.Name}{dtoSubfix}";
                }
                
                var dtoInfo = new DtoInfo(readTypeName, typeNames[1], typeNames[2]);
               
                context.SetLastResult(dtoInfo);
                context.SetVariable("DtoInfo", dtoInfo);
                LogOutput(() => dtoInfo);

                return Done();
            }
            catch (Exception e)
            {
                Logger.LogError(e, "Building DTO info failed.");
                if (e is ParseException pe)
                    foreach (var error in pe.Errors)
                        Logger.LogError(error);
                throw;
            }
        }
    }
}