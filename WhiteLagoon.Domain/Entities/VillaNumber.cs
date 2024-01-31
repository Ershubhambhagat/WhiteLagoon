using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WhiteLagoon.Domain.Entities
{
    public class VillaNumber
    {
        //Primary key without  without default Identity Column
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display (Name ="Villa Number  ")]
        public int Villa_Number { get; set; }

        //Foreign Key Relation 
        [ForeignKey("Villa")]
        public int VillaId { get; set; }
        [ValidateNever]
        public Villa villa { get; set; }
        public string? SpecialDetails { get; set; }
    }
}
