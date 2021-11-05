using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CertificadoAcreenciaWeb.Models
{
    public class ProveedorDataModel
    {
        public string Mes { get; set; }
        public string Dia { get; set; }
        public string Hora { get; set; }
        public string Clie { get; set; }
        public string Esta { get; set; }
        public string Prac { get; set; }
        public long IdProveedor { get; set; }

        [Display(Name = "TipoProveedor")]
        [Required(ErrorMessage = "Este campo es requerido.")] 
        public string TipoProveedor { get; set; }

        [Display(Name = "RUC / Cédula")]
        [Required(ErrorMessage = "Este campo es requerido.")]
        [StringLength(15, ErrorMessage = "Longitud entre 6 y 13 caracteres.",
                      MinimumLength = 6)]
        [DataType(DataType.Text)]
        public string Ruc { get; set; }

        public string NombreProveedor { get; set; }

        [Display(Name = "Correo electrónico")]
        [Required(ErrorMessage = "Este campo es requerido.")]
        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*",
            ErrorMessage = "Dirección de Correo electrónico incorrecta.")]
        [StringLength(100, ErrorMessage = "Longitud máxima 100")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Telefono")]
        [Required(ErrorMessage = "Este campo es requerido.")]       
        [DataType(DataType.PhoneNumber)]
        public string Telefono1 { get; set; }

        [Display(Name = "Telefono")]
        [Required(ErrorMessage = "Este campo es requerido.")]
        [DataType(DataType.PhoneNumber)]
        public string Telefono2 { get; set; }
        
        [Display(Name = "Celular")]
        [Required(ErrorMessage = "Este campo es requerido.")]
        [DataType(DataType.PhoneNumber)]
        public string Celular { get; set; }

        [Display(Name = "Dirección")]
        [Required(ErrorMessage = "Este campo es requerido.")]
        [DataType(DataType.PhoneNumber)]
        public string Direccion { get; set; }

        public string FechaHoraRegistro { get; set; }
        public string TipoCliente { get; set; }

        public string EstadoProveedor { get; set; }
        public ICollection<DetalleFacturasDataModel> DetalleFactura { get; set; }
        public ICollection<Agendamiento> Agendamiento { get; set; }
        public string Id { get; set; }

 

        public ProveedorDataModel()
        {
            this.DetalleFactura = new HashSet<DetalleFacturasDataModel>();
            this.Agendamiento = new HashSet<Agendamiento>();

        }

       
    }
}