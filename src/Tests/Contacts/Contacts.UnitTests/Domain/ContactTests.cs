using System;
using System.Linq;
using Services.Contacts.Domain.Aggregates.Contact;
using Services.Contacts.Domain.Aggregates.Contact.Events;
using Services.Contacts.Domain.Aggregates.Contact.ValueObjects;
using Services.Contacts.Domain.Enums;
using Services.Contacts.Domain.Exceptions;
using Services.Contacts.Domain.Factories;
using Shouldly;
using Xunit;

namespace Contacts.UnitTests.Domain;

public class ContactTests
{
    [Fact]
    public void
        AddField_Throws_ContactFieldAlreadyExistsException_When_There_Is_Already_A_Field_With_The_Same_Value_And_Type()
    {
        //ARRANGE
        var packingList = GetContact();
        packingList.AddField(new ContactField("STTTek", ContactFieldType.Company));

        //ACT
        var exception =
            Record.Exception(() => packingList.AddField(new ContactField("STTTek", ContactFieldType.Company)));

        //ASSERT
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<ContactFieldAlreadyExistsException>();
    }

    [Fact]
    public void AddField_Adds_ContactFieldAdded_Domain_Event_When_Succeeded()
    {
        //ARRANGE
        var contact = GetContact();

        //ACT
        var exception =
            Record.Exception(() => contact.AddField(new ContactField("istanbul", ContactFieldType.Location)));

        //ASSERT
        exception.ShouldBeNull();
        contact.Events.Count().ShouldBe(1);

        var @event = contact.Events.FirstOrDefault() as ContactFieldAdded;

        @event.ShouldNotBeNull();
        @event.Fields.All(f => f.Value == "istanbul".ToUpperInvariant()).ShouldBe(true);
    }


    #region ARRANGE

    private Contact GetContact()
    {
        var packingList = _factory.Create(Guid.NewGuid(), "Furkan", "Bozkurt");
        packingList.ClearEvents();
        return packingList;
    }

    private readonly IContactFactory _factory;

    public ContactTests()
    {
        _factory = new ContactFactory();
    }

    #endregion
}