using Dima.Core.Primitives;
using Dima.Core.Primitives.Result;

namespace Dima.UnitTests.Core.Primitives.Results;

public class FailureResultTests
{
    [Fact]
    public void CreateFailureResultWithError()
    {
        var error = new Error("error", "error message");
        var result = Result.Failure(error);

        result.IsSuccess.Should().BeFalse();
        result.Status.Should().Be(ResultStatus.Failure);
        result.Errors.Should().ContainEquivalentOf(error);
    }

    [Fact]
    public void CreateFailureResultWithListOfErrors()
    {
        var errors = new List<Error>{
            new("error1", "error message"),
            new("error2", "error message")
        };

        var result = Result.Failure([.. errors]);

        result.IsSuccess.Should().BeFalse();
        result.Status.Should().Be(ResultStatus.Failure);

        foreach (var error in errors)
        {
            result.Errors.Should().ContainEquivalentOf(error);
        }
    }
}
