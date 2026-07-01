namespace ClientSupportPortal.Models;

/// <summary>
/// Defines allowed values and validation rules for ticket properties.
/// </summary>
public static class TicketConstants
{
    /// <summary>
    /// Valid ticket status values.
    /// </summary>
    public static class Status
    {
        public const string Open = "Open";
        public const string InProgress = "In Progress";
        public const string Closed = "Closed";

        /// <summary>
        /// All valid status values.
        /// </summary>
        public static readonly string[] AllValues = [Open, InProgress, Closed];

        /// <summary>
        /// Checks if a status value is valid.
        /// </summary>
        public static bool IsValid(string? status) => 
            !string.IsNullOrWhiteSpace(status) && AllValues.Contains(status);
    }

    /// <summary>
    /// Valid ticket priority values.
    /// </summary>
    public static class Priority
    {
        public const string Low = "Low";
        public const string Medium = "Medium";
        public const string High = "High";
        public const string Critical = "Critical";

        /// <summary>
        /// All valid priority values.
        /// </summary>
        public static readonly string[] AllValues = [Low, Medium, High, Critical];

        /// <summary>
        /// Checks if a priority value is valid.
        /// </summary>
        public static bool IsValid(string? priority) => 
            !string.IsNullOrWhiteSpace(priority) && AllValues.Contains(priority);
    }

    /// <summary>
    /// Valid ticket category values.
    /// </summary>
    public static class Category
    {
        public const string LoginIssue = "Login Issue";
        public const string Reporting = "Reporting";
        public const string DataCorrection = "Data Correction";
        public const string AccessRequest = "Access Request";
        public const string Performance = "Performance";
        public const string Integration = "Integration";
        public const string GeneralSupport = "General Support";

        /// <summary>
        /// All valid category values.
        /// </summary>
        public static readonly string[] AllValues = 
        [
            LoginIssue,
            Reporting,
            DataCorrection,
            AccessRequest,
            Performance,
            Integration,
            GeneralSupport
        ];

        /// <summary>
        /// Checks if a category value is valid.
        /// </summary>
        public static bool IsValid(string? category) => 
            !string.IsNullOrWhiteSpace(category) && AllValues.Contains(category);
    }
}
