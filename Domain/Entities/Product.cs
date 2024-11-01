using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(256)")]
        public string Name { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }

        [Column(TypeName = "nvarchar(400)")]
        public string Description { get; set; }

        [Required]
        [Column(TypeName = "int")] // Cambia esta línea para eliminar el tamaño
        public int Stock { get; set; }

        public bool ProductAvaible { get; set; } = true;

        [Required]
        [RegularExpression(@"^[A-Za-z]{3}\d{3}$", ErrorMessage = "El código debe estar formado por tres letras seguidas de tres números.")]
        [Column(TypeName = "nvarchar(6)")] 
        public string code {get;set;}
    }
}
