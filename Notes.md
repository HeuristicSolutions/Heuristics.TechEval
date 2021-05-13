
# Brent's Notes

## Learning the application

- Familiarized myself with Code-Based migrations in EF and SQL Server Express LocalDB
- Played around with the app to see what it did

## Task 1 - Implement an "edit" screen for a Member

- Examined how members were being displayed (Razor w/ Member model being passed in)
- Examined what the "Add Member" button was doing
- Learned about Bootstrap JS Modal
- Shamelessly needed a refresher on passing data between views and EF
- Decided it would be cleaner to move the data access into a separate service
    - Refactored a bit to make this possible (removed the DB context from the members controller and consolidated NewMember/EditMember models)

## Task 2 - Validation

- Almost finished setting up model state validation -- I couldn't get the validation messages to show up in the partial views
- Added some basic jquery validation to both the New member and Edit member modal forms

## Refactoring/Testing

- Need to add some unit tests before going any further
- I'll need to mock the new MemberService. To make this easier, I need to do dependency injection 
  - Moq/NUnit/Unity.Mvc5
- Ran into an issue with a test that dealt with validating Model State

## Task 3 - Prevent duplicate email addresses

## Tips

When you want to start with a fresh DB again, just run these two commands in the package management console:

- `Update-Database -TargetMigration:0 -force` (destroy all tables and data)
- `Update-Database` (seed the DB)
