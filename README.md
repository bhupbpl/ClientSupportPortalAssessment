# DSP .NET Developer Assessment

## Overview

Thank you for taking part in the DSP .NET developer assessment.

This exercise is designed to reflect the type of work involved in the role. You will be working with a small existing .NET application that has been inherited from a previous supplier. The application runs, but the client has reported several issues and is concerned that recent quick fixes have made the system harder to maintain.

You will have **60 minutes** to improve the application as much as you reasonably can.

We are not expecting you to fix everything. We are interested in how you approach the task, what you choose to prioritise, and how clearly you can explain your decisions.

You may use documentation, search engines, or AI tools if you would normally use them during development. However, you must be able to understand, explain, and justify any code you add or change.

---

## Client Scenario

A new client has come to DSP after a poor experience with a previous development company.

They have a small internal support portal used by their staff to track support tickets. The application was originally built in .NET and worked reasonably well, but over time several quick fixes have been made by different people. Some of these changes appear to have introduced new problems.

The client does not currently have the budget for a full rewrite. Their priority is to stabilise the current application, fix the most visible issues, and make practical improvements that give them the most value in the time available.

They would like DSP to review the application, resolve the highest-priority issues where possible, and identify anything that should be addressed later.

---

## Application Summary

The application is a simple support ticket portal.

It allows users to:

- View support tickets
- Search and filter tickets
- Create new tickets
- Edit existing tickets
- View ticket details

The project uses:

- .NET 10
- ASP.NET Core Razor Pages
- Entity Framework Core
- SQLite
- Bootstrap

The SQLite database should be created and seeded automatically when the application runs. No separate database server should be required.

---

## Reported Client Issues

The client has reported the following issues:

1. The ticket search does not always return the correct results.
2. Closed tickets sometimes appear when users are trying to view open tickets.
3. Invalid priority or status values can be saved.
4. The ticket list feels slower than expected when there are more records.
5. High-priority tickets are not easy to identify in the list.
6. The code has become harder to maintain after several quick fixes.

You do not need to complete every item. Please prioritise the work you think will deliver the most value to the client within the available time.

---

## Your Task

You have **60 minutes** to improve the application as much as you reasonably can.

You may choose to:

- Fix bugs
- Improve validation
- Improve performance
- Improve maintainability
- Improve the user interface
- Refactor code where appropriate
- Add comments only where they genuinely help explain the code

Please avoid rewriting the whole application. The client has a limited budget and is looking for practical, safe, incremental improvements.

We are more interested in your judgement and approach than in seeing every issue fully completed.

---

## Returning Your Work

At the end of the 60 minutes, please return your completed work using the submission method provided by DSP.

Submission details, including upload links, will be provided separately by email.

Your submission should include:

1. A zipped copy of your completed project
2. A short note covering:
   - What you changed
   - Anything you did not have time to complete
   - Any assumptions you made

If returning a zipped copy, please exclude build output and local IDE folders where possible, such as:

- `bin`
- `obj`
- `.vs`

Please name your files clearly, for example:

- `YourName-DotNetAssessment.zip`
- `YourName-Notes.txt`

---

## Notes to Include With Your Submission

Please include a short note with your submission covering the following:

1. What you changed
2. Anything you did not have time to complete
3. Any assumptions you made

This does not need to be long. A short bullet-point summary is fine.

---

## Review Process

After the assessment, DSP will review your submitted work.

Selected candidates may be invited to a follow-up review call. In that call, we may ask you to walk through your changes, explain your approach, and discuss how you would continue improving the application.

---

## Running the Application

Open the project in your preferred development environment, such as Visual Studio, Visual Studio Code, or Rider.

You should be able to run the application locally using Visual Studio or from the command line.

From the command line, you can usually run:

```bash
dotnet restore
dotnet run
```

The application uses SQLite, and the local database should be created and seeded automatically when the app starts.

If you need to reset the local data, you can delete the local SQLite database file and run the application again.

---

## Important Notes

- You are not expected to fix everything.
- You should prioritise the changes you believe matter most.
- You may use AI tools, documentation, or search engines if you would normally use them.
- You must be able to understand and explain any code you add or change.
- Please focus on practical improvements rather than a full rewrite.
