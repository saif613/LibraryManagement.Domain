using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace LibraryManagement.Domain.ValueObject;

public class Email
{
    private static readonly Regex EmailRegex = new(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled | RegexOptions.CultureInvariant);
    public Email(string value = "") => Value = value;
    public string Value { get; private init; }

    public static Email Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentNullException("Email is required", nameof(value));
        }

        var normalized = value.Trim().ToLowerInvariant();
        if (!EmailRegex.IsMatch(normalized))
        {
            throw new ArgumentException("Email format is invlid", nameof(value));
        }

        return new Email(normalized);
    }

    public override bool Equals(object? obj) => obj is Email email && Value.Equals(email.Value, StringComparison.InvariantCultureIgnoreCase);
    public override int GetHashCode() => Value.ToLowerInvariant().GetHashCode();
    public override string ToString() => Value;
}

