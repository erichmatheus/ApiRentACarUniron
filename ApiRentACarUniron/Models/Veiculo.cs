using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiRentACarUniron.Models
{
    public class Veiculo
    {
        public int Id { get; set; }
        [Required]
        public string Marca { get; set; }
        [Required]
        public string Modelo { get; set; }
        public int Ano { get; set; }
        public double Quilometragem { get; set; }
    }
}
