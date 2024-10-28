using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Student_Con
{
    internal class Program
    {
        private static readonly HttpClient _httpClient = new HttpClient();

        static async Task Main(string[] args)
        {
            _httpClient.BaseAddress = new Uri("https://localhost:7105/api/");
            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("Choose an option:");
                Console.WriteLine("1. Register Student");
                Console.WriteLine("2. Update Student");
                Console.WriteLine("3. Delete Student");
                Console.WriteLine("4. Get All Students");
                Console.WriteLine("5. Get Student by ID");
                Console.WriteLine("6. Exit");
                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        await RegisterStudent();
                        break;
                    case "2":
                        await UpdateStudent();
                        break;
                    case "3":
                        await DeleteStudent();
                        break;
                    case "4":
                        await GetAllStudents();
                        break;
                    case "5":
                        await GetStudentById();
                        break;
                    case "6":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        /// <summary>
        /// Register new student
        /// </summary>
        /// <returns></returns>
        private static async Task RegisterStudent()
        {
            Console.WriteLine("Register Student");

            Console.Write("Enter First Name: ");
            string firstName = Console.ReadLine();

            Console.Write("Enter Last Name: ");
            string lastName = Console.ReadLine();

            Console.Write("Enter Email: ");
            string email = Console.ReadLine();

            var student = new
            {
                StudentID = 0,
                FirstName = firstName,
                LastName = lastName,
                Email = email
            };
            string jsonString = JsonSerializer.Serialize(student);
            var content = new StringContent(jsonString, Encoding.UTF8, "application/json");

            try
            {
                var response = await _httpClient.PostAsync("Student/CreateStudent", content);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Registration successful!");
                }
                else
                {
                    string errorMessage = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Registration failed. Error: {errorMessage}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }


        /// <summary>
        /// Update existing student using studentID
        /// </summary>
        /// <returns></returns>
        private static async Task UpdateStudent()
        {
            Console.WriteLine("Update Student");

            Console.Write("Enter Student ID: ");
            int studentId = int.Parse(Console.ReadLine());

            Console.Write("Enter First Name: ");
            string firstName = Console.ReadLine();

            Console.Write("Enter Last Name: ");
            string lastName = Console.ReadLine();

            Console.Write("Enter Email: ");
            string email = Console.ReadLine();

            var student = new
            {
                StudentID = studentId,
                FirstName = firstName,
                LastName = lastName,
                Email = email
            };
            string jsonString = JsonSerializer.Serialize(student);
            var content = new StringContent(jsonString, Encoding.UTF8, "application/json");

            try
            {
                var response = await _httpClient.PutAsync($"Student/{studentId}", content);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Update successful!");
                }
                else
                {
                    string errorMessage = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Update failed. Error: {errorMessage}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }


        /// <summary>
        /// Delete existing student using studentID
        /// </summary>
        /// <returns></returns>
        private static async Task DeleteStudent()
        {
            Console.WriteLine("Delete Student");

            Console.Write("Enter Student ID: ");
            int studentId = int.Parse(Console.ReadLine());

            try
            {
                var response = await _httpClient.DeleteAsync($"Student/{studentId}");

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Deletion successful!");
                }
                else
                {
                    string errorMessage = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Deletion failed. Error: {errorMessage}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        /// <summary>
        /// Displays all the students
        /// </summary>
        /// <returns></returns>
        private static async Task GetAllStudents()
        {
            Console.WriteLine("Get All Students");

            try
            {
                var response = await _httpClient.GetAsync("Student/GetAllStudents");

                if (response.IsSuccessStatusCode)
                {
                    var students = await response.Content.ReadFromJsonAsync<List<Student>>();
                    if (students != null)
                    {
                        foreach (var student in students)
                        {
                            Console.WriteLine($"ID: {student.StudentID}, Name: {student.FirstName} {student.LastName}, Email: {student.Email}");
                        }
                    }
                }
                else
                {
                    string errorMessage = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Failed to retrieve students. Error: {errorMessage}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        /// <summary>
        /// Display a particular student information
        /// </summary>
        /// <returns></returns>
        private static async Task GetStudentById()
        {
            Console.Write("Enter Student ID to retrieve: ");
            int studentId;

            // Validate input
            if (!int.TryParse(Console.ReadLine(), out studentId) || studentId <= 0)
            {
                Console.WriteLine("Invalid Student ID. Please enter a positive integer.");
                return;
            }

            var response = await _httpClient.GetAsync($"Student/{studentId}");

            if (response.IsSuccessStatusCode)
            {
                var student = await response.Content.ReadFromJsonAsync<Student>();
                Console.WriteLine($"Student found: {student.FirstName} {student.LastName}, Email: {student.Email}");
            }
            else
            {
                string errorMessage = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Failed to retrieve student. Error: {errorMessage}");
            }
        }

    }



    public class Student
    {
        public int StudentID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}

