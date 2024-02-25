# Country Explorer Application

Welcome to the Country Explorer application! This repository contains both the Back-End (BE) and Front-End (FE) applications.

## Project Structure

- **BE (InnovaLab.CountryExplorer):** This folder contains the Back-End application.
- **FE (innova-lab-country-explorer):** This folder contains the Front-End application.

## Description

This is a simple 'country explorer' application based on .NET 8 that displays a listing of country information. The application leverages a Country Explorer API to retrieve formatted country information from the third-party API [Restcountries](https://restcountries.com/).

## Back-End (BE) Application

- The Back-End is a .NET 8 application that exposes an API to retrieve country information.
- To run the Back-End application please open folder `InnovaLab.CountryExplorer` with VS/VS Code (need creation of own launch files) and press Run.
- The server is hosted at `http://localhost:5247`.
- Explore the API documentation at `http://localhost:5247/swagger`.

## Front-End (FE) Application

- The Front-End is a React application.
- To run the Front-End application, use the command: `npm start`.
- The application is hosted at `http://localhost:3000`.
- It displays a table of all countries with columns in the order: Name, Capital, Currency, Language, Region.
- Country names are clickable and lead to a details page.
- The details page shows additional information, including the country flag image and a link to a map.
- The details page includes a link back to the countries listing page.

## Styling

- The Front-End is a Single Page Application (SPA) using the Bootstrap library for styling.
- The application is responsive for desktop screens.

## How to Run

1. **Back-End:**
   - Navigate to the BE folder (`InnovaLab.CountryExplorer/src/InnovaLab.CountryExplorer.Application`).
   - Run the command: `dotnet run`.

2. **Front-End:**
   - Navigate to the FE folder (`innova-lab-country-explorer`).
   - Run the command: `npm start`.

## Dependencies

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Node.js (LTS)](https://nodejs.org/)