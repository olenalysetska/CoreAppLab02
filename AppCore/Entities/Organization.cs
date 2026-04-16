namespace AppCore.Entities;

public class Organization : Contact
{
    public string Name { get; set; } = string.Empty;
    public OrganizationType Type { get; set; }
    public string? OrganizationKRS { get; set; }  // było: KRS
    public string? Website { get; set; }
    public string? Mission { get; set; }

    public List<Person> Members { get; set; } = new();
    public Person? PrimaryContact { get; set; }
}