using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace {{ EntityInfo.Namespace }};

public sealed class {{ EntityInfo.Name }}AppServiceTests : {{ ProjectInfo.Name }}ApplicationTestBase<{{ ProjectInfo.Name }}ApplicationTestModule>
{
    private readonly I{{ EntityInfo.Name }}AppService _{{ EntityInfo.Name | abp.camel_case }}AppService;

    public {{ EntityInfo.Name }}AppServiceTests()
    {
        _{{ EntityInfo.Name | abp.camel_case }}AppService = GetRequiredService<I{{ EntityInfo.Name }}AppService>();
    }
}

