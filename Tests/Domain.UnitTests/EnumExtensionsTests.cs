using Xunit;
using Domain.Enums;

public class EnumExtensionsTests
{
    [Fact]
    public void GetDisplayName_Should_Return_DisplayAttribute_When_Present()
    {
        // Act
        var result = TodoStatus.InProgress.GetDisplayName();

        // Assert
        Assert.Equal("In-Progress", result);
    }

    [Fact]
    public void GetDisplayName_Should_Return_EnumName_When_DisplayAttribute_Missing()
    {
        // Act
        var result = TodoStatus.Completed.GetDisplayName();

        // Assert
        Assert.Equal("Done", result);
    }

    [Fact]
    public void GetEnumFromDisplayName_Should_Return_Correct_Enum()
    {
        // Act
        var result = EnumExtensions.GetEnumFromDisplayName<TodoStatus>("Done");

        // Assert
        Assert.Equal(TodoStatus.Completed, result);
    }

    [Fact]
    public void GetEnumFromDisplayName_Should_Return_Correct_Enum2()
    {
        // Act
        var result = EnumExtensions.GetEnumFromDisplayName<TodoStatus>("In-Progress");

        // Assert
        Assert.Equal(TodoStatus.InProgress, result);
    }

    [Fact]
    public void GetEnumFromDisplayName_Should_Return_Null_When_Not_Found()
    {
        // Act
        var result = EnumExtensions.GetEnumFromDisplayName<TodoStatus>("Invalid Value");

        // Assert
        Assert.Null(result);
    }
}