# TodoList

Simple ASP.NET MVC app I created while following a tutorial.

## Requirements

- [x] Users should be able to Add, Delete, Update and Read from a database, using a SPA (single-page application).
- [x] The user should never be redirected to a new page.
- [x] You don't need a navigation bar. No menu is necessary since you'll only have one page.
- [x] Once you execute any operation, the todo-list needs to be updated accordingly.
- [x] Your data model is only one table with to-dos. You. might be tempted to create more complex data-models (categories of todos for example) but avoid that for now. We're focusing on the front-end.

## Features

- MSSQL database connection
   - The application uses SQL db connection to store and read information.
   
- UI upgraded with Bootstrap and JavaScript implemented for additional functions - JQuery, Ajax

![ui](https://user-images.githubusercontent.com/114943386/231151577-b5259aef-ef91-4a3b-a166-7fa0afacc272.png)

- When a record is added, two buttons are available - Update and Delete

![record_added](https://user-images.githubusercontent.com/114943386/231151928-e06d8c41-c093-4e61-9b4b-8e705f61a918.png)

- Updating a record inserts the current name into a input box and the button for previosly adding a record switches to update a record

![update_record](https://user-images.githubusercontent.com/114943386/231152296-c99cc3fd-deda-4003-bc82-ff803f98f933.png)

## Challenges

- ASP.NET Core MVC is by far the most difficult thing for me to understand at the moment. So much moving components that need to work together.
- Adding a JS to it did not help much but luckily the implementation wasn't the hardest.
- Understanding the use of some return types such as RedirectResult or JsonResult and why to use them was challenging.
- Building and ASP.NET Core MVC web app alone would be a big big challenge for me now.

So I will be definitely watching more tutorials, reading the right resources and learning more about this architecture.
