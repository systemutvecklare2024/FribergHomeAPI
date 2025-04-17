# Models

## RealEstateAgency
### Properties
- Name : string
- Presentation : string
- Logo : string

## RealEstateAgent
### Properties
- RealEstateAgency : RealEstateAgency
- FirstName : string
- LastName : string
- Email : string 
- PhoneNumber : string
- Image : string
### Relations
- RealEstateAgency


## Property
### Properties
- Category : PropertyTypes
- Address : Address
- Muncipality : Muncipality
- ListingPrice : decmial
- LivingSpace : double
- SecondaryArea : double
- LotSize : double
- Description : string
- NumberOfRooms : int
- MonthlyFee : decimal
- OperationalCostPerYear : decimal
- YearBuilt : int
- More stuff maybe?
	- Views? - meta maybe?
	- IsPublished
	- PublishDate?
	- Sold?
	- Sold Date?
	- CreatedAt
	- UpdatedAt

### PropertyTypes
- Condo
- TownHouse
- House
- VacationHouse
### Relations
- RealEstateAgent
- Muncipality


## Muncipality
### Properties
- Name