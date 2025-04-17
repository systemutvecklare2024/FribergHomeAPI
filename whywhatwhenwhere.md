
## Question:
```
I have this client action to create a property
- GET /Properties/Create 
It needs the following information from the API
- List of Agents (from own agency)
- list of PropertyTypes
- list of muncipalities (Should it be its own endpoint? /muncipalities)

Should I make several calls to different endpoints, or should I create a specific endpoint to get all of the information?
```

## ChatGPT:
Great question—and a super common design decision! Both approaches are valid, but the best choice depends on how reusable and decoupled you want your data to be.

✅ Option 1: Multiple Specific Endpoints (Recommended for reusability)

GET /api/agents/agency         → returns agents from own agency
GET /api/propertytypes         → returns list of property types
GET /api/municipalities        → returns list of municipalities

Pros:
Highly reusable — other pages can use the same endpoints.
Easier to cache, version, and maintain separately.
Better aligns with REST principles (one resource per endpoint).
Makes it easy to update just one dataset without affecting the others.

Use Case:
If you might use /api/municipalities or /api/propertytypes on multiple forms (search, filters, etc.), then this is the better choice.

🤝 Option 2: Single Aggregated Endpoint

GET /api/properties/create-info
Response:
{
  "agents": [ ... ],
  "propertyTypes": [ ... ],
  "municipalities": [ ... ]
}

Pros:
One call, fewer round trips.

Clean and fast for one-off forms where all the data is always needed.

Cons:
Not very reusable outside of this specific page.
Changes in one part of the structure may affect unrelated pieces.
Harder to cache selectively.

🧠 Best Practice Suggestion (Hybrid)
Do both!

Use dedicated endpoints:

/api/propertytypes
/api/municipalities
/api/agents/agency or /api/agents?agencyId=X

For convenience, you can also create an optional:
/api/properties/create-info

This way, your frontend can decide:

"I already have municipalities from search, skip that call."

Or: "This form needs everything at once, just call create-info."

Would you like a quick DTO and endpoint example for /api/properties/create-info just in case?