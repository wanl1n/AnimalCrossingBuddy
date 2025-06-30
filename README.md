Animal Crossing New Horizons Buddy

Leocario, Marcus
Que, Zachary
Valdecantos, Vito
Young, Kate

# Animal Crossing Buddy (GDINFMG)

**Animal Crossing Buddy** is an online tool built with **Unity** and powered by a **MySQL database backend**, designed to help players of *Animal Crossing: New Horizons* track critters, manage villager interactions, and plan their in-game days more efficiently.
This tool was developed as part of a two-phase machine project in database systems, combining strong backend design with a role-based, user-friendly interface in Unity.

## About the Tool

Animal Crossing: New Horizons features a wide variety of critters and villagers, each with different availability windows and birthdays. It can be overwhelming for players to keep track of everything manually.

**Animal Crossing Buddy** simplifies this by:
- Showing which **critters are currently catchable** based on the system's real-world time and in-game hemisphere
- **Tracking progress** on critter collection (e.g., caught vs. not caught)
- Listing **upcoming villager birthdays** so players never miss a celebration
- Providing a centralized place to **view and manage data** about your island

## Project Features

### Phase 1: Database Design
Initial work focused on designing a normalized relational database with full **CRUD** support and meaningful queries.

#### Core Entities:
- `Users`: Stores account info and preferences
- `Critters`: Tracks fish, bugs, and sea creatures, including spawn times and conditions
- `Villagers`: Contains data on names, birthdays, personalities, and house locations
- `UserCritterProgress`: Maps players to the critters they've caught or missed
- `UserVillagerFavorites`: Stores players’ favorite villagers or notes

#### Query Highlights:
- Basic `SELECT` projections for visible data
- Multi-table `JOIN` queries (e.g. user favorites with villager birthdays)
- Aggregate queries (e.g. % of critters caught)
- Subqueries for conditional or time-based data filters

Includes a simplified **ER diagram** showing relationships and constraints between all entities.

### Phase 2: System Implementation
Built using **Unity (C#)** for the frontend and **MySQL** for the backend.

#### Technologies:
- **Frontend**: Unity Engine with HTTP-based requests
- **Backend**: MySQL

#### Key System Features:
- **Role-Based Access**:
  - Regular Users: Can view, track, and update personal progress
  - Admin Users: Can add/edit critter/villager entries and manage user data
- **Live Critter Filters**: Displays what critters are catchable *right now*, based on time and date
- **Birthday Alerts**: Lists which villagers have birthdays today or soon
- **Inventory-Like Views**: Lets players check off which critters they've already caught
- **CRUD Operations** for all major entities, accessible through a clean, interactive Unity UI
