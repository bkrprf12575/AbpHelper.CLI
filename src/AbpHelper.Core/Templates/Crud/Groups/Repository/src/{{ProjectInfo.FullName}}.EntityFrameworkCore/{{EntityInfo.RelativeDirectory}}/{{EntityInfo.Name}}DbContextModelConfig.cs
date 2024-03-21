using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace {{ EntityInfo.Namespace }};

internal static class {{ EntityInfo.Name }}DbContextModelConfig
{
    {{~ if EntityInfo.Document | !string.whitespace ~}}
    /// <summary>
    /// 【{{ EntityInfo.Document }}】模型配置
    /// </summary>
    {{~ end ~}} 
    public static void Configure{{ EntityInfo.Name }}(this ModelBuilder builder)
    {
        builder.Entity<{{ EntityInfo.Name }}>(b =>
        {
            b.ToTable({{ ProjectInfo.Name }}DbProperties.DbTablePrefix + nameof({{ EntityInfo.Name }}), {{ ProjectInfo.Name }}DbProperties.DbSchema{{- if EntityInfo.Document | !string.whitespace; ", table => table.HasComment(\""+ EntityInfo.Document +"\")"; end}});
            b.ConfigureByConvention(); 

            // 属性
            {{~ for prop in EntityInfo.Properties ~}}
            b.Property(x => x.{{ prop.Name }}){{~ if prop.IsNullable ~}}.IsRequired(false){{~ else ~}}.IsRequired(){{~ end ~}}{{~ if prop.Type == "string" || prop.Type == "string?" ~}}.HasMaxLength({{ EntityInfo.Name }}Constants.MaxLength.{{ prop.Name }}){{~ end ~}}.HasComment("{{ prop.Document }}");
            {{~ end ~}}
        });
    }
}
