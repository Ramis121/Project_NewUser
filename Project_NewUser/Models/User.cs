using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project_NewUser.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int age { get; set; }
    }
}
