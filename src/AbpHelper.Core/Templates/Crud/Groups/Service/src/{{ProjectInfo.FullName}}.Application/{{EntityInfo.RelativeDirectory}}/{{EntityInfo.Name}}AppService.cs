{{-
if Option.SkipCustomRepository
    if EntityInfo.CompositeKeyName
        repositoryType = "IRepository<" + EntityInfo.Name + ">"
    else
        repositoryType = "IRepository<" + EntityInfo.Name + ", " + EntityInfo.PrimaryKey + ">"
    end
    repositoryName = "Repository"
else
    repositoryType = "I" + EntityInfo.Name + "Repository"
    repositoryName = "_repository"
end ~}}
using System;
{{~ if !EntityInfo.CompositeKeyName
    crudClassName = "CrudAppService"
else
    crudClassName = "AbstractKeyCrudAppService"
end ~}}
{{~ if EntityInfo.CompositeKeyName || !Option.SkipGetListInputDto~}}
using System.Linq;
using System.Threading.Tasks;
using Arim.Infrastructure.Inputs;
{{~ end -}}
{{~ if !Option.SkipPermissions
    permissionNamesPrefix = ProjectInfo.Name + "Permissions." + EntityInfo.Name
~}}
using {{ ProjectInfo.FullName }}.Permissions;
{{~ end ~}}
{{~ if Option.SkipGetListInputDto ~}}
using Volo.Abp.Application.Dtos;
{{~ end ~}}
using Volo.Abp.Application.Services;
{{~ if Option.SkipCustomRepository ~}}
using Volo.Abp.Domain.Repositories;
{{~ end ~}}

namespace {{ EntityInfo.Namespace }};

{{~
    if !Option.SkipGetListInputDto
        TGetListInput = EntityInfo.Name + "GetListInput"
    else
        TGetListInput = "PagedAndSortedResultRequestDto"
end ~}}
{{~ if EntityInfo.Document | !string.whitespace ~}}
/// <summary>
/// 【{{ EntityInfo.Document }}】应用服务  
/// </summary>
{{~ end ~}}
public class {{ EntityInfo.Name }}AppService(
    {{ repositoryType }} {{ EntityInfo.Name | abp.camel_case }}Repository,
    I{{ EntityInfo.Name }}Manager {{ EntityInfo.Name | abp.camel_case }}Manager)
    : {{ crudClassName }}<{{ EntityInfo.Name }}, {{ DtoInfo.ReadTypeName }}, {{ EntityInfo.PrimaryKey ?? EntityInfo.CompositeKeyName }}, {{TGetListInput}}, {{ DtoInfo.CreateTypeName }}, {{ DtoInfo.UpdateTypeName }}>({{ EntityInfo.Name | abp.camel_case }}Repository),
        I{{ EntityInfo.Name }}AppService
{
    {{~ if !Option.SkipPermissions ~}}
    protected override string GetPolicyName { get; set; } = {{ permissionNamesPrefix }}.Default;
    protected override string GetListPolicyName { get; set; } = {{ permissionNamesPrefix }}.Default;
    protected override string CreatePolicyName { get; set; } = {{ permissionNamesPrefix }}.Create;
    protected override string UpdatePolicyName { get; set; } = {{ permissionNamesPrefix }}.Update;
    protected override string DeletePolicyName { get; set; } = {{ permissionNamesPrefix }}.Delete;
    {{~ end ~}}
    {{~ if EntityInfo.CompositeKeyName ~}}

    protected override Task DeleteByIdAsync({{ EntityInfo.CompositeKeyName }} id)
    {
        return {{ repositoryName }}.DeleteAsync(e =>
        {{~ for prop in EntityInfo.CompositeKeys ~}}
            e.{{ prop.Name }} == id.{{ prop.Name}}{{ if !for.last}} &&{{end}}
        {{~ end ~}}
        );
    }

    protected override async Task<{{ EntityInfo.Name }}> GetEntityByIdAsync({{ EntityInfo.CompositeKeyName }} id)
    {
        return await AsyncExecuter.FirstOrDefaultAsync(
            (await {{ repositoryName }}.WithDetailsAsync()).Where(e =>
            {{~ for prop in EntityInfo.CompositeKeys ~}}
                e.{{ prop.Name }} == id.{{ prop.Name}}{{ if !for.last}} &&{{end}}
            {{~ end ~}}
            ));
    }

    protected override IQueryable<{{ EntityInfo.Name }}> ApplyDefaultSorting(IQueryable<{{ EntityInfo.Name }}> query)
    {
        return query.OrderBy(e => e.{{ EntityInfo.CompositeKeys[0].Name }});
    }
    {{~ end ~}}

    {{~ if EntityInfo.Document | !string.whitespace ~}}
    /// <summary>
    /// 创建【{{ EntityInfo.Document }}】信息
    /// <param name="input">{{ EntityInfo.Document }}信息</param>
    /// </summary>
    /// <returns>{{ EntityInfo.Document }}实例</returns>
    {{~ end ~}}
    public override async Task<{{ EntityInfo.Name }}Dto> CreateAsync({{ EntityInfo.Name }}CreateInput input)
    {
        // 检查创建权限
        await CheckCreatePolicyAsync();

        // 创建{{ EntityInfo.Document }}
        var {{ EntityInfo.Name | abp.camel_case }} = await {{ EntityInfo.Name | abp.camel_case }}Manager.CreateAsync(
            {{~ for prop in EntityInfo.Properties ~}} 
            input.{{ prop.Name }}{{ if for.last | string.contains false; ","; end }}
            {{~ end ~}}
            );

        // 保存{{ EntityInfo.Document }}
        await {{ EntityInfo.Name | abp.camel_case }}Repository.InsertAsync({{ EntityInfo.Name | abp.camel_case }});

        // 返回{{ EntityInfo.Document }}信息
        return await MapToGetOutputDtoAsync({{ EntityInfo.Name | abp.camel_case }});
    }

    /// <summary>
    /// 删除【{{ EntityInfo.Document }}】信息（批量）
    /// </summary>
    /// <param name="input">批量删除信息</param>
    public async Task DeleteManyAsync(BulkDeleteInput<Guid> input)
    {
        // 检查删除权限
        await CheckDeletePolicyAsync();

        // 删除{{ EntityInfo.Document }}信息
        await {{ EntityInfo.Name | abp.camel_case }}Repository.DeleteManyAsync(input.Items);
    }

    {{~ if !Option.SkipGetListInputDto ~}}
    /// <summary>
    /// 创建查询过滤条件
    /// </summary>
    /// <param name="input">查询参数</param>
    /// <returns>查询语句</returns>
    protected override async Task<IQueryable<{{ EntityInfo.Name }}>> CreateFilteredQueryAsync({{ EntityInfo.Name }}GetListInput input)
    {
        return (await base.CreateFilteredQueryAsync(input))
        {{~ for prop in EntityInfo.Properties ~}}
            {{~ if (prop | abp.is_ignore_property) ; continue; end ~}}
                {{~ if prop.Type | string.contains "string" ~}}
                .WhereIf(!input.{{ prop.Name }}.IsNullOrWhiteSpace(), x => x.{{ prop.Name }} == input.{{ prop.Name }}!)
                .WhereIf(!input.{{ prop.Name }}Contain.IsNullOrWhiteSpace(), x => x.{{ prop.Name }} != null && x.{{ prop.Name }}.Contains(input.{{ prop.Name }}Contain!))
                {{~ else ~}}
                .WhereIf(input.{{ prop.Name }}.HasValue, x => x.{{ prop.Name }} == input.{{ prop.Name }}!.Value)
                {{~ end ~}}

        {{~ end ~}}
        ;
    }
    {{~ end ~}}
}
