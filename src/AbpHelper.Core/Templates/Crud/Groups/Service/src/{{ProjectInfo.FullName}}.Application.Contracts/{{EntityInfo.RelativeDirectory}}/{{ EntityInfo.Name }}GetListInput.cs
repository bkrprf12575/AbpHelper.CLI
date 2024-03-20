{{- SKIP_GENERATE = Option.SkipGetListInputDto -}}
using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace {{ EntityInfo.Namespace }};

{{~ if EntityInfo.Document | !string.whitespace ~}}
/// <summary>
/// 【{{ EntityInfo.Document }}】查询参数
/// </summary>
{{~ end ~}}
[Serializable]
public class {{ EntityInfo.Name }}GetListInput : PagedAndSortedResultRequestDto
{
    {{~ for prop in EntityInfo.Properties ~}}
    {{~ if prop | abp.is_ignore_property || string.starts_with prop.Type "IList<"; continue; end ~}}
    {{~ if prop.Document | !string.whitespace ~}}
    /// <summary>
    /// 【{{ prop.Document }}】精准查询
    /// </summary>
    {{~ end ~}} 
    {{~ if !Option.SkipLocalization && Option.SkipViewModel ~}}
    [DisplayName("{{ EntityInfo.Name + prop.Name}}")]
    {{~ end ~}}
    [StringLength({{ EntityInfo.Name }}Constants.MaxLength.{{ prop.Name }})]
    public {{ prop.Type}}{{- if !string.ends_with prop.Type "?"; "?"; end}} {{ prop.Name }} { get; set; }

    {{~ if prop.Type | string.contains "string" ~}}
    {{~ if prop.Document | !string.whitespace ~}}
    /// <summary>
    /// 【{{ prop.Document }}】模糊查询
    /// </summary>
    {{~ end ~}} 
    {{~ if !Option.SkipLocalization && Option.SkipViewModel ~}}
    [DisplayName("{{ EntityInfo.Name + prop.Name}}")]
    {{~ end ~}}    
    [StringLength({{ EntityInfo.Name }}Constants.MaxLength.{{ prop.Name }})]
    public {{ prop.Type}}{{- if !string.ends_with prop.Type "?"; "?"; end}} {{ prop.Name }}Contain { get; set; }
    {{~ if !for.last ~}}
    {{~ end ~}}

    {{~ end ~}}
    {{~ end ~}}
}