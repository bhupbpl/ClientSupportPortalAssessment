using System.ComponentModel.DataAnnotations;

namespace ClientSupportPortal.Models.Validation;

/// <summary>
/// Validates that a ticket status value is one of the allowed values.
/// </summary>
public class ValidStatusAttribute : ValidationAttribute
{
    public ValidStatusAttribute()
    {
        ErrorMessage = "Status must be one of: " + string.Join(", ", TicketConstants.Status.AllValues);
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null)
        {
            return new ValidationResult("Status is required.");
        }

        var status = value.ToString();

        if (TicketConstants.Status.IsValid(status))
        {
            return ValidationResult.Success;
        }

        return new ValidationResult(ErrorMessage);
    }
}

/// <summary>
/// Validates that a ticket priority value is one of the allowed values.
/// </summary>
public class ValidPriorityAttribute : ValidationAttribute
{
    public ValidPriorityAttribute()
    {
        ErrorMessage = "Priority must be one of: " + string.Join(", ", TicketConstants.Priority.AllValues);
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null)
        {
            return new ValidationResult("Priority is required.");
        }

        var priority = value.ToString();

        if (TicketConstants.Priority.IsValid(priority))
        {
            return ValidationResult.Success;
        }

        return new ValidationResult(ErrorMessage);
    }
}

/// <summary>
/// Validates that a ticket category value is one of the allowed values or null.
/// </summary>
public class ValidCategoryAttribute : ValidationAttribute
{
    public ValidCategoryAttribute()
    {
        ErrorMessage = "Category must be one of: " + string.Join(", ", TicketConstants.Category.AllValues);
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        // Category is optional, so null or empty is valid
        if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
        {
            return ValidationResult.Success;
        }

        var category = value.ToString();

        if (TicketConstants.Category.IsValid(category))
        {
            return ValidationResult.Success;
        }

        return new ValidationResult(ErrorMessage);
    }
}
