namespace Domain;

public class Assignation
{
    public int Id { get; set; }

    public int MemberId { get; set; }
    public Member Member { get; set; }

    public int ProjectId { get; set; }
    public Project Project { get; set; }

    public string Role { get; set; }
}

