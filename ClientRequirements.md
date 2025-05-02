# Client-Api communications

## HomeController

### GET /
- List of Property with {address{street, muncipality}, id, price, livingspace, rooms, image, description excerpt}, Agency {name, logo}
- List of muncipalities
- Has a search form which leads to /Search with query parameters

### GET /Search ---- /Search?MuncipalityId=3&PropertyType=3

### GET /Agents

### GET /Support?

### GET /About


## AccountController

### POST /Account/Login
- Email, Password

### POST /Account/Register
- {FirstName, LastName, Email, Phone, ProfileImage, Agency}

### GET /Account/Profile
- {FirstName, LastName, Email, Phone, ProfileImage}
### POST /Account/Profile
- {FirstName, LastName, Email, Phone, ProfileImage}


## DashboardController

### GET /Index
- {NumberOfSold, NumberOfProperties, Views}


## AgenciesController

### GET /Agencies
- List of Agencies

### GET /Agencies/my
- {Name, Presentation, LogoUrl}
- List of Agents {profileimage, FirstName, LastName}
### POST /Agencies/my
- {Name, Presentation, LogoUrl}

### GET /Agencies/{id}/applications
 - MODEL: {Agency, Agent->ApiUser, ENUM status, created/updated}
 - DTO {asdfasdf, AgentId}
## POST /Agencies/{agencyId}/applications/{applicationId}
- {AgentId, Status}


## PropertiesController

### GET /Properties/Create
- List of Agents (from own agency)
- list of PropertyTypes
- list of muncipalities (Should it be its own endpoint? /muncipalities)

### POST /Properties/Create
- {PropertyType(int), BuildYear, Address, LivingSpace, Rooms, SecondaryArea, Lotsize, monthlyfee, operationalcostperyear, listingprice, description, list of imageurls, agentid }

### GET /Properties/Edit/1
- list of all muncipalities
- list of all agents
- {PropertyType(int), BuildYear, Address, LivingSpace, Rooms, SecondaryArea, Lotsize, monthlyfee, operationalcostperyear, listingprice, description, list of imageurls, agentid }

### POST /Property/Edit/1
- {PropertyType(int), BuildYear, Address, LivingSpace, Rooms, SecondaryArea, Lotsize, monthlyfee, operationalcostperyear, listingprice, description, list of imageurls, agentid }

### GET /Properties/my
- For filtering {muncipalities, propertytypes}
- List of properties {Id, image, address{street, city}, muncipality, listprice, rooms, livingspace, propertytype}

