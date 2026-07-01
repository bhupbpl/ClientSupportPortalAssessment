using System.ComponentModel.DataAnnotations;
using ClientSupportPortal.Models.Validation;

namespace ClientSupportPortal.Models;

public class Ticket
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Title is required")]
    [StringLength(200, ErrorMessage = "Title cannot exceed 200 characters")]
    public string? Title { get; set; }

    [Required(ErrorMessage = "Description is required")]
    [StringLength(2000, ErrorMessage = "Description cannot exceed 2000 characters")]
    public string? Description { get; set; }

    [Required(ErrorMessage = "Requester name is required")]
    [StringLength(100)]
    public string? RequesterName { get; set; }

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    public string? RequesterEmail { get; set; }

    [Required(ErrorMessage = "Status is required")]
    [ValidStatus]
    public string? Status { get; set; }

    [Required(ErrorMessage = "Priority is required")]
    [ValidPriority]
    public string? Priority { get; set; }

    [ValidCategory]
    public string? Category { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? LastUpdatedDate { get; set; }

    public string? AssignedTo { get; set; }
}
