﻿using BibliotecaApi.Validaciones;
using System.ComponentModel.DataAnnotations;

namespace BibliotecaApi.Entidades
{
    public class Autor : IValidatableObject
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(10, ErrorMessage = "El campo {0} debe tener {1} caracteres o menos ")]
        //[PrimeraLetraMayuscula]
        public required string Nombre { get; set; }
        public List<Libro> Libros { get; set; } = new List<Libro>();

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!string.IsNullOrEmpty(Nombre))
            {
                var primeraLetra = Nombre[0].ToString();
                if (primeraLetra != primeraLetra.ToUpper() && Edad >= 18)
                {
                    yield return new ValidationResult("La primera letra debe ser mayuscula - por modelo",
                      new string[] { nameof(Nombre) });
                }
            }
        }

        //[Range(18,100)]
        public int Edad { get; set; }

        //[CreditCard]
        //public string? TarjetaDeCredito { get; set; }

        //[Url]
        //public string? URL { get; set; }
    }


}
