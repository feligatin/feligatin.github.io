using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CertificadoAcreenciaWeb.Models
{
    public class DetalleFacturasDataModel
    {
        public long IdProveedor { get; set; }
        public  string NumeroFactura { get; set; }
        public string DetalleFactura { get; set; }
        public decimal ValorFactura { get; set; }
    }
}