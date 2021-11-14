using FluentAssertions;
using System;
using Xunit;

namespace CsharpRecordInANutshell.Tests;

/// <summary>
/// https://devblogs.microsoft.com/dotnet/c-9-0-on-the-record/
/// </summary>
public class Csharp9OnTheRecordTests
{
    public class Person
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }

    [Fact]
    public void Class_is_mutable()
    {
        var person = new Person { FirstName = "Harold", LastName = "Triggerman" };

        person.LastName = "Quail";

        person.LastName.Should().Be("Quail");
    }

    public class InitAccessorPerson
    {
        public string? FirstName { get; init; }
        public string? LastName { get; init; }
    }

    [Fact]
    public void Init_accessor_introduces()
    {
        var person = new InitAccessorPerson { FirstName = "Harold", LastName = "Triggerman" };

        //person.LastName = "Quail": // error
    }

    public class InitAccessorAndReadOnlyFieldsPerson
    {
        private readonly string firstName = "<unknown>";
        private readonly string lastName = "<unknown>";

        public string FirstName
        {
            get => firstName;
            init => firstName = (value ?? throw new ArgumentNullException(nameof(FirstName)));
        }

        public string LastName
        {
            get => lastName;
            init => lastName = (value ?? throw new ArgumentNullException(nameof(LastName)));
        }
    }

    [Fact]
    public void Init_accessor_and_readonly_fields_introduce()
    {
        var person = new InitAccessorAndReadOnlyFieldsPerson { FirstName = "Harold", LastName = "Triggerman" };
    }

    public record RecordPerson
    {
        public string? FirstName { get; init; }
        public string? LastName { get; init; }
    }

    [Fact]
    public void Record_is_immutable_like_a_value()
    {
        var person = new RecordPerson { FirstName = "Harold", LastName = "Triggerman" };
        var otherPerson = person with { LastName = "Quail" };

        ReferenceEquals(person, otherPerson).Should().BeFalse();
    }

    [Fact]
    public void Record_is_immutable_like_a_value2()
    {
        var person = new RecordPerson { FirstName = "Harold", LastName = "Quail" };
        var otherPerson = new RecordPerson { FirstName = "Harold", LastName = "Quail" };

        ReferenceEquals(person, otherPerson).Should().BeFalse();

        person.GetHashCode().Equals(otherPerson.GetHashCode()).Should().BeTrue();

        (person == otherPerson).Should().BeTrue();

        person.Equals(otherPerson).Should().BeTrue();
    }
}