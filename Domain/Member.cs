﻿namespace Domain;

public class Member
{
    public int Id { get; set; }

    public string FullName { get; set; }

    public string Email { get; set; }

    public ICollection<Assignation> Assignations { get; set; }
}