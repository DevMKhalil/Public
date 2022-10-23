# App description

This App Is An ECommerce App include Item List and Item Detail And Shopping Cart 

User Can View Item Details And Add Item To The Shopping Cart

# structure of the application

Application Sections :

    1 - Header : a folder has header component that Shows in The Header And Navigate between Pages

    2 - item-list : a folder has item-list Component that shows all items 
                    and the child component item-list-item is the single item component 
                    and item Service

    3 - item-details : a folder has item-details Component that shows the item in details 

    4 - shopping-cart : has shopping-cart component that shows The shoping cart page 
                    and the child component customer-info that shows the customer Data
                    and the child component shopping-cart-item is the single shopping cart item
                    and shopping cart service

    5 - shared : a folder has Item Model that used in all application

    6 - not-found : a folder has not-found Component that apprear when the route is wronge or leade to nothing

    7 - confirmation-page : a folder has confirmation-page component that shows after the customer check out the order

    8 - app-routing : a file has app-routing Module wich has links that route to previouse components

# ECommerceApp

This project was generated with [Angular CLI](https://github.com/angular/angular-cli) version 14.2.6.

## Development server

Run `ng serve` for a dev server. Navigate to `http://localhost:4200/`. The application will automatically reload if you change any of the source files.

## Code scaffolding

Run `ng generate component component-name` to generate a new component. You can also use `ng generate directive|pipe|service|class|guard|interface|enum|module`.

## Build

Run `ng build` to build the project. The build artifacts will be stored in the `dist/` directory.

## Running unit tests

Run `ng test` to execute the unit tests via [Karma](https://karma-runner.github.io).

## Running end-to-end tests

Run `ng e2e` to execute the end-to-end tests via a platform of your choice. To use this command, you need to first add a package that implements end-to-end testing capabilities.

## Further help

To get more help on the Angular CLI use `ng help` or go check out the [Angular CLI Overview and Command Reference](https://angular.io/cli) page.
