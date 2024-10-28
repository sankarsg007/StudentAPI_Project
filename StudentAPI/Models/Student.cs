using System.ComponentModel.DataAnnotations;

namespace StudentAPI.Models
{
    public class Student
    {
        /// <summary>
        /// student id (identity(1,1))
        /// </summary>
        public int StudentID { get; set; }

        /// <summary>
        /// FirstName
        /// </summary>
        [Required]
        public string FirstName { get; set; }

        /// <summary>
        /// LastName
        /// </summary>
        [Required]
        public string LastName { get; set; }

        /// <summary>
        /// Email must contain @, . 
        /// </summary>
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
