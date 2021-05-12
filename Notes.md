
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
