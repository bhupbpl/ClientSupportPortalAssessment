namespace ClientSupportPortal.Models;

public class Ticket
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public string? RequesterName { get; set; }

    public string? RequesterEmail { get; set; }

    public string? Status { get; set; }

    public string? Priority { get; set; }

    public string? Category { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime LastUpdatedDate { get; set; }

    public string? AssignedTo { get; set; }
}
