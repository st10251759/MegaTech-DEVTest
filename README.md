# DEVTEST Cameron MVC

A simple ASP.NET Core MVC application built for the Junior Software Developer Test. The app manages Salespeople, Customers, and Orders, and includes the SQL solutions for the written portion of the test.

## What is included

- `DEVTEST_Cameron_MVC` – the ASP.NET Core MVC (.NET 8) project
- `Question1to5SQLQuery.sql` – SQL solutions for Questions 1 to 5 of the test
- `DatabaseSchema_DevTest.sql` – script to create the DEVTEST database, tables, and sample data

## Requirements

- Visual Studio 2022 (or later) with the ASP.NET and web development workload
- .NET 8 SDK
- SQL Server or SQL Server LocalDB (included with Visual Studio)
- SQL Server Management Studio (SSMS), optional but useful for running the SQL scripts

## Cloning the project

```
git clone https://github.com/your-username/DEVTEST_Cameron_MVC.git
cd DEVTEST_Cameron_MVC
```

Open the solution file (`.sln`) in Visual Studio.

## Setting up the database

1. Open `DatabaseSchema_DevTest.sql` in SSMS.
2. Run the script against your SQL Server instance. This creates the `DEVTEST` database, the `Salespersons`, `Customers`, `Orders`, and `HighAchievers` tables, and inserts the sample data from the test paper.
3. Open `Question1to5SQLQuery.sql` in SSMS to view or run the SQL solutions for Questions 1 to 5.

## Configuring the connection string

Open `appsettings.json` and confirm the connection string points to your local database:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\camDB1;Database=DEVTEST;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

Change `(localdb)\camDB1` to match your own local SQL Server instance name if it is different.

## Running the project

1. Build the solution in Visual Studio (Build > Build Solution).
2. Press F5, or select **IIS Express** / the project's HTTPS profile, to run the app.
3. The app will open in your browser at the Home page, with navigation links to Orders, Customers, and Salespeople.

## A note on the database schema

This project was built against a local copy of the DEVTEST database, created using the schema in `DatabaseSchema_DevTest.sql`. The table names, column names, and data types in that script, and in the application's data access code, were set up to match this local schema.

If you point the application at a different DEVTEST server, for example the original test server, and that server's schema uses different table names, column names, or data types, the application may not run correctly or may throw errors. If this happens, the database schema on that server will need to be reviewed and the application's data access code adjusted to match it.

## Application walkthrough (Database First approach)

This project follows a database first approach. The database and its tables were built first, and the application's models, data access layer, and pages were then built to match the structure already in the database.

### 1. Building the database

The starting point was the `DEVTEST` database itself. The `DatabaseSchema_DevTest.sql` script creates:

- `Salespersons`, holding each salesperson's name, age, and salary
- `Customers`, holding each customer's name, city, and industry type
- `Orders`, holding the sales order number, order date, amount, and a link to both a customer and a salesperson through foreign keys
- `HighAchievers`, a table used to hold the names and ages of salespeople who qualify as high achievers

The foreign keys on the `Orders` table were set up so that every order must be linked to a valid, existing customer and a valid, existing salesperson. This keeps the data consistent and reflects how the tables relate to one another in the original test paper.

### 2. Loading sample data

Once the tables existed, the sample data from the test paper was inserted into `Salespersons`, `Customers`, and `Orders`, using the same IDs shown in the test paper so that the relationships between orders, customers, and salespeople matched exactly.

### 3. Answering the SQL questions

With the database and sample data in place, the five SQL questions from the test were written and run directly against it. These cover finding salespeople linked to specific customers, filtering orders by amount, finding customers with no orders, finding salespeople with multiple orders, and inserting qualifying salespeople into the `HighAchievers` table. All five are saved in `Question1to5SQLQuery.sql`.

### 4. Connecting the application to the existing database

With the database already built, the application side of the project was created to work against it. A data context class was set up to represent the database inside the application, and a model class was created for each table, with each model's properties matching the columns already defined in the database. Basic validation rules, such as required fields and length limits, were added to these models to prevent obviously invalid data from being submitted through the application.

### 5. Building the controllers

A controller was created for each of the three main tables: Orders, Customers, and Salespeople. Each controller was built to handle the same set of actions:

- viewing a list of all records
- viewing the full details of a single record
- adding a new record
- editing an existing record
- deleting a record, with a confirmation step first

The Orders controller also loads the related customer and salesperson names alongside each order, and provides dropdown lists of customers and salespeople when creating or editing an order, so a user selects from existing records rather than typing in raw ID numbers.

### 6. Building the views

Razor views were created for each controller action, using a simple and consistent blue color scheme throughout the application. The Customers and Salespeople detail pages also show a list of that customer's or salesperson's related orders, making use of the relationships already defined in the database.

### 7. Tying it together with navigation

Finally, the shared layout was updated with a navigation bar linking to the Home page, Orders, Customers, and Salespeople sections, and the Home page was built as a simple landing page linking into each section. This gives the whole application one consistent entry point and menu, while every page underneath it reads from and writes to the same DEVTEST database.
