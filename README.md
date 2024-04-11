Instruction of ToDo List:

Application has an interface which contains:
1. Calendar: allows user to click on the specific day and get the list of tasks (if any) assigned to that specific day.
2. ListView: allows user to see list of tasks and also double click on the specific record and display options like:
  a) Edit task
  b) Remove task - requires a confirmation
  c) Cancel - closes form
4. Two Buttons:
  a) GetAllTasks - gets the whole list of all tasks from the database
  b) Add Task - displays a new windows which allows user to add a new task to database.

In the case of adding a new task and editing task, all data (Task Title, Task Description and Task Day) must be provided. Otherwise user will get a MessageBox error.

Unit tests must be executed one-by-one (not execute all). There are 12 unit tests and each test returns positive result.

Note: If user uses the calendar and the wants to click on the button, user must click on the button two times because calendar must be "unclicked" ("unfocused").

