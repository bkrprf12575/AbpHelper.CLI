using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using {{ EntityInfo.Namespace }}.Exceptions;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace {{ EntityInfo.Namespace }};

{{~ if EntityInfo.Document | !string.whitespace ~}}
/// <summary>
/// 【{{ EntityInfo.Document }}】领域服务
/// </summary>
{{~ end ~}}
public class {{ EntityInfo.Name }}Manager(
    I{{ EntityInfo.Name }}Repository {{ EntityInfo.Name | abp.camel_case }}Repository)
    : DomainService,
        I{{ EntityInfo.Name }}Manager
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
    public async Task<{{ EntityInfo.Name }}> CreateAsync(
    {{~ for prop in EntityInfo.Properties ~}} 
        {{ if prop.IsNullable == false; "[Required]"; end }}{{ prop.Type }} {{ prop.Name | abp.camel_case }}{{ if prop.IsNullable; " = null"; end }}{{ if for.last | string.contains false; ","; end }}
    {{~ end ~}}
    )
    {
        // 检查{{ EntityInfo.Document }}是否已存在
        //if (await {{ EntityInfo.Name | abp.camel_case }}Repository.AnyAsync(x => x. == ))
        //{
        //    throw new {{ EntityInfo.Name }}AlreadyExistException();
        //}

        // 创建{{ EntityInfo.Document }}
        var {{ EntityInfo.Name | abp.camel_case }} = new {{ EntityInfo.Name }}(
            GuidGenerator.Create(),
            {{~ for prop in EntityInfo.Properties ~}} 
            {{ prop.Name | abp.camel_case }}{{ if for.last | string.contains false; ","; end }}
            {{~ end ~}}
            );

        // 返回{{ EntityInfo.Document }}
        return {{ EntityInfo.Name | abp.camel_case }};
    }
}