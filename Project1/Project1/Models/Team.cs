﻿using Project_1.Models;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Project1.Models
{
    public class Team : Auditable, IValidatableObject
    {

        public int ID { get; set; }

        [Display(Name = "Team Name")]
        [Required(ErrorMessage = "You cannot leave the team name blank.")]
        [StringLength(70, ErrorMessage = "Team name cannot be more than 70 characters long.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "You cannot leave the Budget blank.")]
        [Range(500.0, 10000.0, ErrorMessage = "Budget must be between $500 and $10,000.")]
        [DataType(DataType.Currency)]
        public double Budget { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {

            if (Name[0] == 'X' || Name[0] == 'F' || Name[0] == 'S')
            {
                yield return new ValidationResult("Team names are not allowed to start with the letters X, F, or S.", new[] { "Name" });
            }
        }
    }
}
