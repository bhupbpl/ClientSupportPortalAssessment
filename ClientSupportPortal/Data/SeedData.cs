using ClientSupportPortal.Models;

namespace ClientSupportPortal.Data;

public static class SeedData
{
    public static void Initialize(AppDbContext context)
    {
        if (context.Tickets.Any())
        {
            return;
        }

        var statuses = TicketConstants.Status.AllValues;
        var priorities = TicketConstants.Priority.AllValues;
        var categories = TicketConstants.Category.AllValues;
        var assignees = new string?[]
        {
            "Amelia Roberts",
            "Daniel Price",
            "Nina Shah",
            "Marcus Green",
            "Olivia Hart",
            null,
            null,
            null
        };
        var requesters = new[]
        {
            ("Aisha Khan", "aisha.khan@contoso.example"),
            ("Ben Carter", "ben.carter@northwind.example"),
            ("Claire Morgan", "claire.morgan@fabrikam.example"),
            ("David Wilson", "david.wilson@adatum.example"),
            ("Emma Lewis", "emma.lewis@contoso.example"),
            ("Farah Ahmed", "farah.ahmed@northwind.example"),
            ("George Taylor", "george.taylor@fabrikam.example"),
            ("Hannah Scott", "hannah.scott@adatum.example"),
            ("Ibrahim Yusuf", "ibrahim.yusuf@contoso.example"),
            ("Julia Brown", "julia.brown@northwind.example"),
            ("Kieran Murphy", "kieran.murphy@fabrikam.example"),
            ("Lily Evans", "lily.evans@adatum.example"),
            ("Mohammed Ali", "mohammed.ali@contoso.example"),
            ("Natalie Reed", "natalie.reed@northwind.example"),
            ("Oscar Hughes", "oscar.hughes@fabrikam.example"),
            ("Priya Patel", "priya.patel@adatum.example")
        };
        var titleTemplates = new[]
        {
            "Unable to access payroll report",
            "Password reset link expired",
            "Customer record needs correcting",
            "New starter requires access",
            "Dashboard takes too long to load",
            "Export to finance system failed",
            "Report totals do not match spreadsheet",
            "Account locked after password change",
            "Incorrect department shown on profile",
            "Supplier feed has missing rows",
            "Request to remove old user access",
            "Monthly report page times out",
            "Cannot download attachment",
            "Search results show duplicate client records",
            "Invoice status not updating",
            "Manager approval email not received"
        };
        var descriptionTemplates = new[]
        {
            "User reported the issue after the morning processing run.",
            "The same task worked last week but now fails for several staff members.",
            "Support team attempted a workaround but the record still needs checking.",
            "The requester needs this resolved before the next reporting deadline.",
            "This appears intermittent and has been reported by more than one user.",
            "The client has asked for a quick review before the next supplier meeting.",
            "Internal support has made a manual adjustment and wants the system checked.",
            "The problem was escalated after the requester could not complete their task."
        };

        var random = new Random(420);
        var tickets = new List<Ticket>();
        var startDate = DateTime.Today.AddDays(-180);

        for (var i = 1; i <= 220; i++)
        {
            var requester = requesters[random.Next(requesters.Length)];
            var createdDate = startDate.AddDays(random.Next(0, 180)).AddHours(random.Next(8, 18)).AddMinutes(random.Next(0, 60));
            var status = statuses[WeightedIndex(random, 45, 35, 20)];
            var priority = priorities[WeightedIndex(random, 35, 40, 18, 7)];
            var assignedTo = status == TicketConstants.Status.Open && random.Next(100) < 45 ? null : assignees[random.Next(assignees.Length)];

            tickets.Add(new Ticket
            {
                Title = titleTemplates[random.Next(titleTemplates.Length)],
                Description = descriptionTemplates[random.Next(descriptionTemplates.Length)],
                RequesterName = requester.Item1,
                RequesterEmail = requester.Item2,
                Status = status,
                Priority = priority,
                Category = categories[random.Next(categories.Length)],
                CreatedDate = createdDate,
                LastUpdatedDate = createdDate.AddHours(random.Next(1, 240)),
                AssignedTo = assignedTo
            });
        }

        context.Tickets.AddRange(tickets);
        context.SaveChanges();
    }

    private static int WeightedIndex(Random random, params int[] weights)
    {
        var total = weights.Sum();
        var value = random.Next(total);
        var running = 0;

        for (var i = 0; i < weights.Length; i++)
        {
            running += weights[i];
            if (value < running)
            {
                return i;
            }
        }

        return weights.Length - 1;
    }
}
