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
    public class DbContextClassStep : CSharpModificationCreatorStep
    {
        protected override IList<ModificationBuilder<CSharpSyntaxNode>> CreateModifications(WorkflowExecutionContext context, CompilationUnitSyntax rootUnit)
        {
            var model = context.GetVariable<object>("Model");
            var entityUsingText = context.GetVariable<string>("EntityUsingText");
            var templateDir = context.GetVariable<string>(VariableNames.TemplateDirectory);
            var dbContextPropertyText = TextGenerator.GenerateByTemplateName(templateDir, "DbContextClass_Property", model);

            return new List<ModificationBuilder<CSharpSyntaxNode>>
            {
                new InsertionBuilder<CSharpSyntaxNode>(
                    root => root.Descendants<UsingDirectiveSyntax>().Last().GetEndLine(),
                    entityUsingText,
                    InsertPosition.After,
                    root => root.DescendantsNotContain<UsingDirectiveSyntax>(entityUsingText)
                ),
                new InsertionBuilder<CSharpSyntaxNode>(
                    root => root.Descendants<ConstructorDeclarationSyntax>().Single().Identifier.GetStartLine() - 1,
                    dbContextPropertyText,
                    modifyCondition: root => root.DescendantsNotContain<PropertyDeclarationSyntax>(dbContextPropertyText)
                )
            };
        }

        public DbContextClassStep([NotNull] TextGenerator textGenerator) : base(textGenerator)
        {
        }
    }
}