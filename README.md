# Student Details Management System

## Project Overview

The **Student Details Management System** is a robust application designed to efficiently manage student information. This project provides a user-friendly interface for administrators to perform various operations related to student data, including creating, updating, deleting, and fetching student details. The system is built using ASP.NET Core and leverages a SQL Server database for data storage.

## Features

- **Create Student**: Easily add new student records to the system with essential details such as first name, last name, and email.
  
- **Update Student Details**: Modify existing student information. Administrators can update any details of a student, ensuring that records are always up to date.

- **Delete Student**: Remove a student record from the system when it's no longer needed. This feature ensures that the database remains clean and relevant.

- **Fetch All Students**: Retrieve a list of all registered students in the system. This feature provides a comprehensive overview of the student body.

- **Fetch Particular Student**: Access the details of a specific student by entering their unique ID. This functionality is essential for quickly retrieving information about individual students.

## Technologies Used

- **Backend**: ASP.NET Core
- **Database**: SQL Server
- **API Communication**: HTTP Client for API calls
- **Serialization**: JSON for data interchange

## Usage

Once the application is running, users can interact with the system through the provided interface or API endpoints. The following endpoints are available:

- **POST /Student/CreateStudent**: Create a new student.
- **PUT /Student/UpdateStudent/{StudentID}**: Update details for an existing student.
- **DELETE /Student/DeleteStudent/{StudentID}**: Delete a student record.
- **GET /Student/GetAllStudents**: Retrieve a list of all students.
- **GET /Student/GetStudent/{StudentID}**: Fetch details for a specific student.

## Conclusion

The Student Details Management System is a comprehensive solution for managing student information efficiently. It provides essential CRUD (Create, Read, Update, Delete) functionalities, making it easier for administrators to maintain accurate student records.
