using System.Collections.Generic;
using System.Linq;
using EasyAbp.AbpHelper.Core.Extensions;
using EasyAbp.AbpHelper.Core.Generator;
using EasyAbp.AbpHelper.Core.Models;
using EasyAbp.AbpHelper.Core.Workflow;
using Elsa.Services.Models;
using JetBrains.Annotations;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace EasyAbp.AbpHelper.Core.Steps.Abp.ModificationCreatorSteps.CSharp
{
    public class EntityErrorCodesStep : CSharpModificationCreatorStep
    {
        protected override IList<ModificationBuilder<CSharpSyntaxNode>> CreateModifications(WorkflowExecutionContext context, CompilationUnitSyntax rootUnit)
        {
            var model = context.GetVariable<object>("Model");
            var projectInfo = context.GetVariable<ProjectInfo>("ProjectInfo");

            string templateDir = context.GetVariable<string>(VariableNames.TemplateDirectory);
            string contents = TextGenerator.GenerateByTemplateName(templateDir, "EntityErrorCodes_Add", model);

            CSharpSyntaxNode Func(CSharpSyntaxNode root) => 
                root
                .Descendants<ClassDeclarationSyntax>()
                .First(node => node.ToString().Contains(projectInfo.Name + "ErrorCodes"));
            
            return new List<ModificationBuilder<CSharpSyntaxNode>>
            {
                new InsertionBuilder<CSharpSyntaxNode>(
                    root => Func(root).GetEndLine(),
                    contents,
                    modifyCondition: root => Func(root).NotContains(contents)
                ),
            };
        }

        public EntityErrorCodesStep([NotNull] TextGenerator textGenerator) : base(textGenerator)
        {
        }
    }
}