using EasyAbp.AbpHelper.Core;
using Volo.Abp.Modularity;

namespace EasyAbp.AbpHelper
{
    [DependsOn(typeof(AbpHelperCoreModule))]
    public class AbpHelperModule : AbpModule
    {
    }
}