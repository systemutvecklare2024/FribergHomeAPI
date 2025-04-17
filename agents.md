## 📦 Class Diagram (Backend Model)

```plaintext
┌────────────────────┐
│      User          │
├────────────────────┤
│ Guid Id            │
│ string FirstName   │
│ string LastName    │
│ string Email       │
│ string PasswordHash│
│ string Role        │ ◄─── "Agent", "Customer", "Admin"
└────────────────────┘
         │ 1
         │
         ▼
┌────────────────────┐
│   AgentProfile     │
├────────────────────┤
│ Guid UserId        │ (FK to User)
│ Guid AgencyId      │
│ string Bio         │
│ string Phone       │
│ string ProfileImage│
│ int NumberOfSales  │
└────────────────────┘
         │
         ▼
┌────────────────────┐
│      Agency        │
├────────────────────┤
│ Guid Id            │
│ string Name        │
│ string LogoUrl     │
│ string Presentation│
└────────────────────┘
```