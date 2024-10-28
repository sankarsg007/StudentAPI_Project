using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentAPI.Models;
using System.Data.SqlClient;

namespace StudentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {

        private readonly string connectionString;

        public StudentController(IConfiguration configuration)
        {
            connectionString = configuration["ConnectionStrings:DefaultConnection"] ?? "";
        }


        /// <summary>
        /// Used to register new student
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        [HttpPost("CreateStudent")]
        public IActionResult CreateStudent([FromBody] Student student)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "INSERT INTO StudentsAPI (FirstName, LastName, Email) VALUES (@FirstName, @LastName, @Email)";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@FirstName", student.FirstName);
                        command.Parameters.AddWithValue("@LastName", student.LastName);
                        command.Parameters.AddWithValue("@Email", student.Email);

                        command.ExecuteNonQuery();
                    }
                }
                return Ok("Student created successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// used to fetch all the student details
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllStudents")]
        public IActionResult GetAllStudents()
        {
            List<Student> students = new List<Student>();

            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT * FROM StudentsAPI";

                    using (var command = new SqlCommand(query, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Student student = new Student
                                {
                                    StudentID = reader.GetInt32(0),
                                    FirstName = reader.GetString(1),
                                    LastName = reader.GetString(2),
                                    Email = reader.GetString(3)
                                };

                                students.Add(student);
                            }
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("StudentsAPI", "There is an exception...");
                return BadRequest(ModelState);
            }

            return Ok(students);
        }

        /// <summary>
        /// used to fetch a particular student information using id
        /// </summary>
        /// <param name="StudentID"></param>
        /// <returns></returns>
        [HttpGet("{StudentID}")]
        public IActionResult GetStudent(int StudentID)
        {
            Student student = new Student();
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT * FROM StudentsAPI WHERE StudentID = @StudentID";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@StudentID", StudentID);

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                Console.WriteLine("Student found.");
                                student.StudentID = reader.GetInt32(0);
                                student.FirstName = reader.GetString(1);
                                student.LastName = reader.GetString(2);
                                student.Email = reader.GetString(3);
                            }
                            else
                            {
                                return NotFound($"Student with ID {StudentID} not found.");
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("StudentsAPI", "There is an exception...");
                return BadRequest(ModelState);
            }

            return Ok(student);
        }

        /// <summary>
        /// used to update students information
        /// </summary>
        /// <param name="StudentID"></param>
        /// <param name="student"></param>
        /// <returns></returns>
        [HttpPut("{StudentID}")]
        public IActionResult UpdateStudent(int StudentID, Student student) 
        {
            try
            {
                using (var connection = new SqlConnection(connectionString)) 
                {
                    connection.Open();

                    string query = "UPDATE StudentsAPI SET FirstName = @FirstName, LastName = @LastName, Email = @Email WHERE StudentID = @StudentID";

                    using (var command = new SqlCommand(query, connection)) 
                    {
                        command.Parameters.AddWithValue("@StudentID", StudentID); // Add StudentID parameter
                        command.Parameters.AddWithValue("@FirstName", student.FirstName);
                        command.Parameters.AddWithValue("@LastName", student.LastName);
                        command.Parameters.AddWithValue("@Email", student.Email);

                        command.ExecuteNonQuery();
                    }
                }

            }
            catch (Exception ex) 
            {
                ModelState.AddModelError("StudentsAPI", "There is an exception...");
                return BadRequest(ModelState);
            }

            return Ok();
        }


        /// <summary>
        /// used to delete a particular student record
        /// </summary>
        /// <param name="StudentID"></param>
        /// <returns></returns>
        [HttpDelete("{StudentID}")]
        public IActionResult DeleteStudent(int StudentID) 
        {
            if (StudentID <= 0)
            {
                return BadRequest("Invalid StudentID.");
            }
            try
            {
                using (var connection = new SqlConnection(connectionString)) 
                {
                    connection.Open();

                    string query = "DELETE FROM StudentsAPI WHERE StudentID = @StudentID";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@StudentID", StudentID);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected == 0)
                        {
                            return NotFound($"Student with ID {StudentID} not found.");
                        }
                    }
                }
            }
            catch (Exception ex) 
            {
                ModelState.AddModelError("StudentsAPI", "There is an exception...");
                return BadRequest(ModelState);
            }

            return NoContent();  // 204 No Content on successful deletion
        }
    }

}
