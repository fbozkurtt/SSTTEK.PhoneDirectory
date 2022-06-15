using Services.Contacts.Domain.Aggregates.Contact.Events;
using Services.Contacts.Domain.Aggregates.Contact.ValueObjects;
using Services.Contacts.Domain.Enums;
using Services.Contacts.Domain.Exceptions;
using Shared.Abstractions.Domain;

namespace Services.Contacts.Domain.Aggregates.Contact;

public class Contact : AggregateRoot<ContactId>
{
    // public ContactId Id { get; private set; }

    private ContactFirstName _firstName = null!;
    private ContactLastName? _lastName;
    private readonly List<ContactField> _fields = new();

    private Contact()
    {
    }

    private Contact(ContactId id, ContactFirstName firstName, ContactLastName? lastName, List<ContactField> fields)
        : this(id, firstName, lastName)
    {
        _fields = fields;
    }

    internal Contact(ContactId id, ContactFirstName firstName, ContactLastName? lastName)
    {
        Id = id;
        _firstName = firstName;
        _lastName = lastName;
    }

    public void AddField(ContactField field)
    {
        var alreadyExists = _fields.Contains(field);

        if (alreadyExists)
        {
            throw new ContactFieldAlreadyExistsException(field);
        }

        _fields.Add(field);
        AddEvent(new ContactFieldAdded(field, _fields));
    }

    public void RemoveField(string value, ContactFieldType type)
    {
        var field = GetField(value, type);
        _fields.Remove(field);
        AddEvent(new ContactFieldRemoved(field, _fields));
    }

    public void SetFirstName(ContactFirstName firstName)
    {
        _firstName = firstName;
    }

    public void SetLastName(ContactLastName? lastName)
    {
        _lastName = lastName;
    }

    private ContactField GetField(string value, ContactFieldType type)
    {
        var field = _fields.SingleOrDefault(f => f.Value == value.ToUpperInvariant() && f.Type == type);

        if (field is null)
        {
            throw new ContactFieldNotFoundException(value, type);
        }

        return field;
    }

    public IEnumerable<ContactField> GetFields()
    {
        return _fields;
    }
}