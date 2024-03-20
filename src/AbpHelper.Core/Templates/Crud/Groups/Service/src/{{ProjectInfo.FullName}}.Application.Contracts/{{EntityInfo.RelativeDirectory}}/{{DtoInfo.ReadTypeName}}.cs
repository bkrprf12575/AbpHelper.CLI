using System;
using Volo.Abp.Application.Dtos;

#pragma warning disable CS8618 

namespace {{ EntityInfo.Namespace }};

{{~ if EntityInfo.Document | !string.whitespace ~}}
/// <summary>
/// 【{{ EntityInfo.Document }}】信息
/// </summary>
{{~ end ~}}
[Serializable]
public class {{ DtoInfo.ReadTypeName }} : {{ EntityInfo.BaseType | string.replace "AggregateRoot" "Entity"}}Dto{{ if EntityInfo.PrimaryKey }}<{{ EntityInfo.PrimaryKey}}>{{ end }}
{
    {{~ for prop in EntityInfo.Properties ~}}
    {{~ if prop | abp.is_ignore_property; continue; end ~}}
    {{~ if prop.Document| !string.whitespace ~}}
    /// <summary>
    /// {{ prop.Document }}
    /// </summary>
    {{~ end ~}} 
    public {{ prop.Type}} {{ prop.Name }} { get; set; }
    {{~ if !for.last ~}}

    {{~ end ~}}
    {{~ end ~}}
}