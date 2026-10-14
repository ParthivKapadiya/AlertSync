# AlertSync — TCIE-2 submission

**Course:** CE545 Enterprise Computing Through .NET Frameworks  
**Semester:** CE/IT 5th, 2026-27  
**Component:** TCIE-2 (20 marks)  
**Project:** AlertSync — Smart Reminder and Digital Asset Management System

## Team and work split

| Member | Role | TCIE-2 work |
| --- | --- | --- |
| Parthiv Kapadiya | Team lead / developer | Shared layout, purple-and-white visual system, navigation, Entity Framework schema and migration |
| Krish Raiyani | Developer | Reminder screens, calendar, upcoming deadlines, completed tasks, categories |
| Nandish Dholariya | Tester | Document locker, family members, profile, settings, help, and the check that every screen links to the next |

## Layout creation (10 marks)

The app is an ASP.NET Core MVC site on .NET 9. One dashboard layout is used after sign-in screens. Login, register, and forgot password use a separate layout.

Sidebar order:

1. Dashboard
2. My Tasks
3. Calendar
4. Upcoming Deadlines
5. Completed Tasks
6. Categories
7. Document Locker
8. Family Members
9. Notifications
10. Profile
11. Settings
12. Help and About
13. Logout

Every item opens a working screen. Dashboard counts link to the matching list. A calendar event opens reminder details. A family card opens that member's documents. Forms use labels, required fields, and server-side checks.

Sample cards are there so the arrangement can be seen. They are not database rows. A valid submit says the form passed and that nothing was stored.

## Database design (5 marks)

SQLite through Entity Framework Core. The migration `InitialCreate` builds the database on startup.

### Tables

| Table | Purpose |
| --- | --- |
| AspNetUsers | Account, including FullName and CreatedAt. Password hash stays in the Identity column. |
| AspNetRoles, AspNetUserRoles, AspNetUserClaims, AspNetUserLogins, AspNetUserTokens, AspNetRoleClaims | Identity support tables |
| UserPreferences | One settings row per user: time zone and in-app notifications |
| Categories | User-owned reminder groups. Name is unique per user. |
| Reminders | Title, description, due time, priority, lead time, repeat, completion |
| ReminderAlerts | One notify time per reminder. Unique on reminder + notify time. |
| UserNotifications | Inbox row. Unique on the alert that created it. |
| FamilyMembers | People used to group documents. Not login accounts. |
| UserDocuments | File metadata. Null family member means a personal document. |

### Normalization choices

- A reminder stores `CategoryId`, not a copy of the category description.
- A document stores `FamilyMemberId`. The person's name is only on `FamilyMembers`.
- Overdue is not a column. `IsCompleted` is stored. Overdue means not completed and `DueAt` is in the past.
- `ReminderAlerts` does not copy `UserId`. The user comes from the reminder.
- Settings are not columns on the login row. `UserPreferences` is one-to-one with the user.
- The unique alert index is the plan for stopping duplicate notifications.

### Delete rules

- Deleting a user removes that user's reminders, categories, alerts, notifications, family members, documents, and settings.
- Deleting a category is blocked while a reminder uses it (`Restrict`).
- Deleting a family member is blocked while a document points at them (`Restrict`).
- Deleting a reminder removes its alerts and notifications.
- The link from an alert to its notification uses `SetNull`, so there is only one cascade path from the reminder. That keeps a later move to SQL Server possible.

### CRUD plan

| Entity | Create | Read | Update | Delete |
| --- | --- | --- | --- | --- |
| ApplicationUser | Register | Profile | Edit profile, change password | Not offered |
| UserPreference | With the account | Settings | Settings form | With the user |
| Category | Categories page | Categories and reminder form | Rename | Only when unused |
| Reminder | Add reminder | Tasks, details, calendar, upcoming, completed, dashboard | Edit, mark completed | Confirm, then delete |
| ReminderAlert | When a reminder is saved | Scheduler only | Mark processed | With the reminder |
| UserNotification | When an alert is due | Notifications | Mark read | Delete one |
| FamilyMember | Add member | Family list and document filter | Edit | Blocked while documents remain |
| UserDocument | Upload | Locker, details, download | Metadata only | Row and private file together |

The final submission will enforce that a user can only touch their own rows. Files will be stored outside `wwwroot` under a generated name.

## Voice viva notes

- TCIE-2 shows the screens and the schema. It does not sign anyone in or save rows.
- Dashboard numbers are counted from the same sample list the tables use.
- Status on screen uses: completed, otherwise overdue if the due time has passed, otherwise pending.
- SignalR and email are not in this submission. A closed browser cannot receive a live push.

## How to run

See `README.md` in the AlertSync folder. Start at http://localhost:5182 and open Help, then Database design, during the viva.
