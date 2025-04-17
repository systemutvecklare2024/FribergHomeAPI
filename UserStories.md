# 🧑‍💼 User Stories – Friberg Real Estate Marketplace

## Visitor (Spekulant)

### 🔍 Search & View Listings
- **As a visitor**, I want to view all available properties so that I can browse what's on the market.
- **As a visitor**, I want to filter properties by municipality so that I can narrow my search.
- **As a visitor**, I want to see a list of properties with key info (address, price, area, etc.) so I can quickly evaluate options.
- **As a visitor**, I want to click on a listing to view full property details so that I can make informed decisions.

---

## Real Estate Agent (Mäklare)

### 👤 Registration & Authentication
- **As an agent**, I want to register an account so that I can manage property listings.
- **As an agent**, I want to log in to access my personal dashboard and tools.
- **As an agent**, I want to log out to keep my account secure.

### 🏠 Manage My Listings
- **As an agent**, I want to see an overview of my active listings so that I can track my work.
- **As an agent**, I want to add a new property listing so that it becomes available to the public.
- **As an agent**, I want to edit property details so that I can correct or update info.
- **As an agent**, I want to delete a listing I no longer want published.

### 👤 Manage My Profile
- **As an agent**, I want to update my personal information (name, contact, image) so that it is accurate on the site.
- **As an agent**, I want to be associated with a brokerage so that listings reflect who I work for.

---

## 🏢 System Features

### 🏠 Property Data
- **As a system**, I want to store and manage property data including:  
  - Category (Apartment, Townhouse, Villa, Vacation Home)  
  - Address, Municipality  
  - Starting price  
  - Living area, Secondary area, Lot size  
  - Description, Number of rooms  
  - Monthly fee (if any), Annual operating cost  
  - Year built  
  - 1–40 images  
  - Assigned agent and brokerage  

### 👥 Agents & Brokerages
- **As a system**, I want to manage agents, each belonging to a brokerage.
- **As a system**, I want to manage brokerage data including name, presentation, and logo.

---

## 🔒 Security & Identity

- **As a system**, I want to secure API endpoints using Identity so only authorized agents can manage listings.
- **As a system**, I want to support custom agent classes inheriting from IdentityUser to store extra fields like first name, last name, and brokerage.

---

## 📦 Seeded Data

- **As a system**, I want to seed some brokerages and agents by default so the app can be demoed without needing full registration logic implemented.

---

## 💻 Developer Goals

- **As a developer**, I want the API and frontend to be separate projects so they can be deployed independently.
- **As a developer**, I want to use Git and branches for version control and milestone snapshots.
- **As a developer**, I want to first build and test the API using Postman before connecting the frontend.
- **As a developer**, I want to delay implementing Identity until the API and frontend are stable.

