using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace PhoneBook.Models;

public partial class Contact
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public bool Validate()
    {
        string pattern = @"^(\+7|8)?\d{10}$";
        return Regex.IsMatch(Phone, pattern);
    }
}
