using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CsharpRecordInANutshell.Tests;

/// <summary>
/// https://enterprisecraftsmanship.com/posts/csharp-records-value-objects/
/// </summary>
public class Csharp9RecordsAsDDDValueObjectsTests
{

    public record RecordAddress(string Street, string ZipCode);
    public class ClassAddress
    {
        public ClassAddress(string street, string zipCode)
        {
            Street = street;
            ZipCode = zipCode;
        }

        public string Street { get; init; }
        public string ZipCode { get; init; }
    }

    [Fact]
    public void Records_introduice()
    {
        var address1 = new RecordAddress("1234 Main St", "20012");
        var address2 = new RecordAddress("1234 Main St", "20012");       

        address1.Equals(address2).Should().BeTrue();
        (address1 == address2).Should().BeTrue();


        var caddress1 = new ClassAddress("1234 Main St", "20012");
        var caddress2 = new ClassAddress("1234 Main St", "20012");

        caddress1.Equals(caddress2).Should().BeFalse();
        (caddress1 == caddress2).Should().BeFalse();

    }

}
