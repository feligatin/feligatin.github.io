using CertificadoAcreenciaWeb.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Threading.Tasks;
using Rotativa;

namespace CertificadoAcreenciaWeb.Controllers
{
    public class HomeController : Controller
    {
        
        ProveedorFacturaAD logica;
 

        public ActionResult Index()
        {
            ViewBag.ListaTipoProveedor = ListTipoProveedor();
            

            return View();
       

        }

        [HttpPost]
        public ActionResult Contact(ProveedorDataModel model)
        {
            var employee = model.IdProveedor;
            return View(employee);
        }
        

        public void Guardar(ProveedorDataModel model)
        {

        }
        public ActionResult About(ProveedorDataModel model)
        {
            ViewBag.Message = "Your application description page.";

            return View(model.IdProveedor);
        }
        

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        private List<SelectListItem> ListTipoProveedor()
        {
            List<SelectListItem> list = new List<SelectListItem>{
                new SelectListItem {Selected = true, Text = "Seleccione", Value = "0"},
                new SelectListItem {Selected = false, Text = "Cliente Nacional", Value = "1"},
                new SelectListItem {Selected = false, Text = "Cliente Internacional", Value = "2"},
                new SelectListItem {Selected = false, Text = "Proveedor Nacional", Value = "3"},
                new SelectListItem {Selected = false, Text = "Proveedor Internacional", Value = "4"}
                };
            return list;
        }

        private void EnviaMensajeCorreo(ProveedorDataModel model)
        {
            string correoDe,  coreoPara,  asunto,  mensajeDetalle;
            // string to = "taekps@tame.com.ec, keops.tame@tame.com.ec";
            correoDe = "felipem.serrano@tame.com.ec";
            coreoPara = model.Email;
            asunto = "Cita Para Tame";
            mensajeDetalle = "Su cita es para: "+ model.Mes +" "+  model.Hora;
           /* try
            {
                System.Net.Mail.MailMessage correo = new System.Net.Mail.MailMessage();
                correo.To.Add(coreoPara);
                correo.From = new System.Net.Mail.MailAddress(correoDe);
                correo.Subject = asunto;
                correo.SubjectEncoding = System.Text.Encoding.UTF8;

                correo.Body = mensajeDetalle;
                correo.BodyEncoding = System.Text.Encoding.UTF8;
                correo.IsBodyHtml = false;
                System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient
                {
                    Host = "10.1.1.11",
                    Credentials = new System.Net.NetworkCredential("12345", "lguairacaja400"),
                    EnableSsl = false
                };
                smtp.Send(correo);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            */
        }

        public JsonResult BuscaProveedor(string ruc)
        {
            logica = new ProveedorFacturaAD();
            List<ProveedorDataModel> listarProveedor = new List<ProveedorDataModel>();
            listarProveedor = logica.BuscarProveedorExiste(ruc);
            return Json(listarProveedor, JsonRequestBehavior.AllowGet);
        }

        public JsonResult BuscaMes(String mes)
        {
            logica = new ProveedorFacturaAD();
            List <ProveedorDataModel> lsTipoProveedor2 = new List<ProveedorDataModel>();
            lsTipoProveedor2 = logica.BuscarMesExiste(mes);
            return Json(lsTipoProveedor2, JsonRequestBehavior.AllowGet); 
            
        }
        public JsonResult Buscadia(ProveedorDataModel model)
        {
            logica = new ProveedorFacturaAD();
            List<ProveedorDataModel> lsTipoProveedor3 = new List<ProveedorDataModel>();
            lsTipoProveedor3 = logica.BuscarDiaExiste(model);
            return Json(lsTipoProveedor3, JsonRequestBehavior.AllowGet);

        }

        public JsonResult Reagendar(string ruc)
        {
            logica = new ProveedorFacturaAD();
            List<ProveedorDataModel> listarProveedor = new List<ProveedorDataModel>();
            listarProveedor = logica.BuscarProveedorExiste(ruc);
            return Json(listarProveedor, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GrabarProveedor(ProveedorDataModel model)
        {
            TicketView(model);
            logica = new ProveedorFacturaAD();
            string mensaje = "";
            model.FechaHoraRegistro = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString();
            model.EstadoProveedor = "REG";
            logica.FechaCitaRecepcionDocumentos();
            EnviaMensajeCorreo(model);
            if (logica.GrabraProveedor(model).IdProveedor > 0)
            {

                mensaje = "OK";
            }
            return Json(mensaje, JsonRequestBehavior.AllowGet);
           
        }

        public ActionResult Print()
        {
            var report = new Rotativa.ActionAsPdf("Resumen");
            return report;
        }
      
        public ActionResult Resumen(String valor, String valor2)
        {
            ViewData["Nombre"] = valor;
            ViewBag.nombre = valor2;
            return View();
        }
        [HttpPost]
        public ActionResult TicketView(ProveedorDataModel model)
        {
            IronPdf.Installation.TempFolderPath = $@"/irontemp/";
            IronPdf.Installation.LinuxAndDockerDependenciesAutoConfig = true;
            var html = "Resumen";
            var ironPdfRender = new IronPdf.HtmlToPdf();
            var pdfDoc = ironPdfRender.RenderHtmlAsPdf(html);
            var path = Path.Combine(
                  Directory.GetCurrentDirectory(), "wwwroot");
            //var images = pdfDoc.RasterizeToImageFiles($@"{path}\thumbnail_*.jpg", 100, 80);
            return File(pdfDoc.Stream.ToArray(), "application/pdf");
        }
    }
}