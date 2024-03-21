using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace {{ EntityInfo.Namespace }};

{{~ if EntityInfo.Document | !string.whitespace ~}}
/// <summary>
/// 【{{ EntityInfo.Document }}】领域服务
/// </summary>
{{~ end ~}}
public interface I{{ EntityInfo.Name }}Manager : IDomainService
{
    {{~ if EntityInfo.Document | !string.whitespace ~}}
    /// <summary>
    /// 创建【{{ EntityInfo.Document }}】信息
    /// </summary>
    {{~ for prop in EntityInfo.Properties ~}}
    /// <param name="{{ prop.Name | abp.camel_case }}">{{ prop.Document }}</param>
    {{~ end ~}}
    /// <returns>{{ EntityInfo.Document }}实例</returns>
    {{~ end ~}}
    public Task<{{ EntityInfo.Name }}> CreateAsync(
    {{~ for prop in EntityInfo.Properties ~}} 
        {{ prop.Type }}{{ if prop.IsNullable; ""; end }} {{ prop.Name | abp.camel_case }}{{ if prop.IsNullable; " = null"; end }}{{ if for.last | string.contains false; ","; end }}
    {{~ end ~}}
    );
}