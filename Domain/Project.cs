namespace Domain;

public class Project
{
    public int Id { get; set; }

    public string Name { get; set; }

    public ICollection<Assignation> Assignation { get; set; }
}