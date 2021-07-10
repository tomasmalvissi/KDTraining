using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KdTraining.Application.Models
{
    public class AutoModel
    {
        /// <summary>
        /// Nombre y Apellido del titular del vehiculo
        /// </summary>
        public string Titular { get; set; }
        /// <summary>
        /// Patente del vehiculo en formato: AAA000
        /// </summary>
        public string Patente { get; set; }
        /// <summary>
        /// Marca del vehiculo
        /// </summary>
        public string Marca { get; set; }
        /// <summary>
        /// Modelo de la marca del vehiculo
        /// </summary>
        public string Modelo { get; set; }
        /// <summary>
        /// Tipo de vehiculo. Ej: Auto/Camioneta/Camion
        /// </summary>
        public string Tipo { get; set; }

    }
}
