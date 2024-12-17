using Dima.Core.Primitives.Result;

namespace Dima.UnitTests.Core.Primitives.Results;

public class SuccessResultTests
{
    [Fact]
    public void CreateSuccessResult()
    {
        var result = Result.Success();

        result.IsSuccess.Should().BeTrue();

        result.Status.Should().Be(ResultStatus.Ok);
    }
}
