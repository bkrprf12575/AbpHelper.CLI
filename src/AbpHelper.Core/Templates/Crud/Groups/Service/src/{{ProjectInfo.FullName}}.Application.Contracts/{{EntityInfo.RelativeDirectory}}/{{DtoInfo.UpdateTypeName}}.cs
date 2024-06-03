{{- SKIP_GENERATE = DtoInfo.CreateTypeName == DtoInfo.UpdateTypeName -}}
using System;
{{~ if !Option.SkipLocalization && Option.SkipViewModel ~}}
using System.ComponentModel;
{{~ end ~}}
using System.ComponentModel.DataAnnotations;

namespace {{ EntityInfo.Namespace }};

{{~ if EntityInfo.Document | !string.whitespace ~}}
/// <summary>
/// {{ EntityInfo.Document }}
/// </summary>
{{~ end ~}}
[Serializable]
public class {{ DtoInfo.UpdateTypeName }}
{
    {{~ for prop in EntityInfo.Properties ~}}
    {{~ if prop | abp.is_ignore_property; continue; end ~}}
    {{~ if prop.Document| !string.whitespace ~}}
    /// <summary>
    /// {{ prop.Document }}
    /// </summary>
    {{~ end ~}} 
    {{~ if !Option.SkipLocalization && Option.SkipViewModel ~}}
    [DisplayName("{{ EntityInfo.Name + prop.Name}}")]
    {{~ end ~}}    
    {{~ if prop.IsNullable == false ~}}
    [Required]
    {{~ end ~}}
    {{~ if string.starts_with prop.Type "string" ~}}
    [StringLength({{ EntityInfo.Name }}Constants.MaxLength.{{ prop.Name }})]
    {{~ end ~}}
    public {{ prop.Type}} {{ prop.Name }} { get; set; }
    {{~ if !for.last ~}}

    {{~ end ~}}
    {{~ end ~}}
}