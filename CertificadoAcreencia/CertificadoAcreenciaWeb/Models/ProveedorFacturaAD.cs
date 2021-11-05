using System;
using System.Collections.Generic;
//using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Globalization;

namespace CertificadoAcreenciaWeb.Models
{
    public class ProveedorFacturaAD
    {
        ServicioWebAS400.Service1 servicoDB;
        const string biblioteca = "TAMSDAT";
        public ProveedorFacturaAD() { }

        public List<ProveedorDataModel> BuscarProveedorExiste(string codigoRuc)
        {
            List<ProveedorDataModel> listarProveedor = new List<ProveedorDataModel>();
            DataSet dsProveedor = new DataSet();
            servicoDB = new ServicioWebAS400.Service1();
            try
            {
                dsProveedor = servicoDB.SelectPuroDB2DataSET("SELECT FCTC02 AS RUC, FCMRAZ AS RAZON_SOCIAL, FCMDIR AS DIRECCION,  FCMTEL AS TELEFONO1, FCMTE1 AS TELEFONO2, FCMTE2 AS CELULAR, FCMEMA AS EMAIL FROM TAMEFIN.FCTAR7 LEFT JOIN TAMEFIN.FCMAR1 ON(FCTC02 = FCMCO1) WHERE FCMCO1 = '" + codigoRuc.Trim() + "'");
                
                foreach (DataRow item in dsProveedor.Tables[0].Rows)
                {

                    ProveedorDataModel proveedor = new ProveedorDataModel
                    {
                        Ruc = item.Field<string>("FCPRU1").Trim(),
                        NombreProveedor = item.Field<string>("FCPT02").Trim(),
                        Direccion = item.Field<string>("FCPDIR").Trim(),
                        Telefono1 = item.Field<string>("FCPTEL").Trim(),
                        Celular = item.Field<string>("FCPCEL").Trim(),
                        Email = item.Field<string>("FCPC15").Trim()
                        
                    };
                    listarProveedor.Add(proveedor);
                }
                dsProveedor = servicoDB.SelectPuroDB2DataSET("SELECT FCPID1, FCPT02, FCPRU1 , FCPNO1, FCPC15, FCPTEL, FCPCEL, FCPDIR, FCPF17, FCPE01 FROM  TAMSDAT.FCPA12 WHERE FCPRU1= '" + codigoRuc.Trim() + "'");
                foreach (DataRow item in dsProveedor.Tables[0].Rows)
                {
                    ProveedorDataModel proveedor = new ProveedorDataModel
                    {
                        Ruc = item.Field<string>("FCPRU1").Trim(),
                        NombreProveedor = item.Field<string>("FCPT02").Trim(),
                        Direccion = item.Field<string>("FCPDIR").Trim(),
                        Telefono1 = item.Field<string>("FCPTEL").Trim(),
                        Celular = item.Field<string>("FCPCEL").Trim(),
                        Email = item.Field<string>("FCPC15").Trim()
                    };
                    listarProveedor.Add(proveedor);
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listarProveedor;
        }
        public List<ProveedorDataModel> BuscarMesExiste(String mes)
            {
            List<ProveedorDataModel> listaragen = new List<ProveedorDataModel>();
            DataSet dsMes = new DataSet();
            servicoDB = new ServicioWebAS400.Service1();
            string format = "yyyy-mm-dd";
            DateTime dateTime = DateTime.ParseExact(mes, format, CultureInfo.InvariantCulture);
            
            string x = dateTime.ToString("dd/mm/yyyy");
            Console.WriteLine(x);
            try
            {
                

                dsMes = servicoDB.SelectPuroDB2DataSET("select FCHF14, FCHH12 from TAMSDAT.FCHA49 where FCHE05 = 0 and FCHF14='"+ x +"' group by FCHF14, FCHH12 order by FCHF14, FCHH12 asc");
                foreach (DataRow item in dsMes.Tables[0].Rows)
                {
                    ProveedorDataModel agen = new ProveedorDataModel
                    {
                        Hora = item.Field<string>("FCHH12").Trim(),
                    };
                    listaragen.Add(agen);
                }

                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listaragen;
        }
        public List<ProveedorDataModel> BuscarDiaExiste(ProveedorDataModel proveedor)
        {
           
            List<ProveedorDataModel> listaragen = new List<ProveedorDataModel>();
            DataSet dsMes = new DataSet();
            servicoDB = new ServicioWebAS400.Service1();
            try
            {

                dsMes = servicoDB.SelectPuroDB2DataSET("SELECT * FROM TAMSDAT.FCCA96 WHERE FCCE20='0' and FCCDIA=" +proveedor.Mes);
                foreach (DataRow item in dsMes.Tables[0].Rows)
                {
                    ProveedorDataModel agen = new ProveedorDataModel
                    {
                        Dia = item.Field<string>("FCCDIA").Trim()
                    };
                    agen.Dia = item.Field<string>("FCCDIA").Trim();
                    listaragen.Add(agen);
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listaragen;
        }



        //sECENCIAL..
        public long SecuencialProveedor()
        {
            servicoDB = new ServicioWebAS400.Service1();
            long secuencial = 0;
            DataSet dsSecuncial = new DataSet();
            try
            {
                dsSecuncial = servicoDB.SelectPuroDB2DataSET("SELECT IFNULL(MAX(FCPID1), 0) + 1 AS SECUENCIAL  FROM  "+  biblioteca + ".FCPA12");
                foreach (DataRow row in dsSecuncial.Tables[0].Rows)
                {
                    secuencial = long.Parse(row.Field<decimal>("SECUENCIAL").ToString());                   
                }
                
            }
            catch(Exception ex) 
            {
                throw ex;
            }
            return secuencial;
        }

        public ProveedorDataModel GrabraProveedor (ProveedorDataModel proveedor)
        {
            servicoDB = new ServicioWebAS400.Service1();
            DataSet tiempo = new DataSet();
            ProveedorDataModel modelProveedor = new ProveedorDataModel();
            
            string sqlInsertar = "INSERT INTO " + biblioteca + ".FCPA12" + "(FCPID1, FCPT02, FCPRU1, FCPNO1, FCPC15, FCPTEL, FCPCEL, FCPDIR, FCPF17, FCPE01, FCPF21, FCPT03 ) VALUES(";            
            StringBuilder stbDetalle = new StringBuilder();
            stbDetalle.Append("INSERT INTO " + biblioteca + ".FCFAR8 (FCFIDP, FCFNU6, FCFC05, FCFV23) VALUES" + Environment.NewLine);
            string sqlDetalle = "";     
            string sqlcita = "";
            string sqlInsertarDetalle = string.Empty;
            string format = "yyyy-mm-dd";
            DateTime dateTime = DateTime.ParseExact(proveedor.Mes, format, CultureInfo.InvariantCulture);

            string x = dateTime.ToString("dd/mm/yyyy");
            try
            {
                proveedor.IdProveedor = SecuencialProveedor();
                sqlInsertar = sqlInsertar + proveedor.IdProveedor + ", '" + proveedor.TipoProveedor + "', '"+ proveedor.Ruc + "', '" + proveedor.NombreProveedor + "', '" + proveedor.Email + "', '" + proveedor.Telefono1 + "', '" + proveedor.Celular + "', '" + proveedor.Direccion + "', '" + proveedor.FechaHoraRegistro + "','" + proveedor.EstadoProveedor + "','" + proveedor.Mes + "','" + proveedor.TipoCliente + "')";

                sqlcita = "Update TAMSDAT.FCHA49 SET" + " FCHE05='1' WHERE FCHF14='" + x  + "' and FCHH12='"+ proveedor.Hora + "' fetch first 1 rows only";
                servicoDB.SelectPuroDB2DataSET(sqlcita);
                if (servicoDB.EliminarPuro(sqlInsertar))
                {
                    foreach (var item in proveedor.DetalleFactura)
                    {
                        string valorTotal = item.ValorFactura.ToString().Replace(",", ".");
                        stbDetalle.Append("(" + proveedor.IdProveedor + ", '" + item.NumeroFactura + "', '" + item.DetalleFactura + "', " + valorTotal + ")," + Environment.NewLine);

                        }
                    sqlInsertarDetalle = string.Empty;
                    sqlInsertarDetalle = stbDetalle.ToString().TrimEnd();
                    sqlDetalle = sqlInsertarDetalle.Substring(0, sqlInsertarDetalle.Length - 1);
                    if (servicoDB.EliminarPuro(sqlDetalle))
                    {
                        modelProveedor = DatosProveedor(proveedor.IdProveedor);
                    }
                    
                }
                


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return modelProveedor;
        }

        public ProveedorDataModel DatosProveedor(long idProveedor)
        {
            servicoDB = new ServicioWebAS400.Service1();            
            DataSet dsProveedor = new DataSet();
            DataSet dsFacturas = new DataSet();
            ProveedorDataModel model = new ProveedorDataModel();
            try
            {
                dsProveedor = servicoDB.SelectPuroDB2DataSET("SELECT FCPID1, FCPT02, FCPRU1, FCPNO1, FCPC15, FCPTEL, FCPCEL, FCPDIR, FCPF17, FCPE01 FROM  " + biblioteca + ".FCPA12 WHERE FCPID1 = " + idProveedor);

                dsFacturas = servicoDB.SelectPuroDB2DataSET("SELECT FCFIDP, FCFNU6, FCFC05, FCFV23 FROM  " + biblioteca + ".FCFAR8 WHERE FCFIDP = " + idProveedor);
                foreach (DataRow row in dsProveedor.Tables[0].Rows)
                {
                    model.IdProveedor = long.Parse(row.Field<decimal>("FCPID1").ToString());
                    model.TipoProveedor = row.Field<string>("FCPT02");
                    model.Ruc = row.Field<string>("FCPRU1").Trim();
                    model.NombreProveedor = row.Field<string>("FCPNO1").Trim();
                    model.Email = row.Field<string>("FCPC15").Trim();                    
                    model.Telefono1 = row.Field<string>("FCPTEL").Trim();
                    model.Celular = row.Field<string>("FCPCEL").Trim();
                    model.Direccion = row.Field<string>("FCPDIR").Trim();
                    model.FechaHoraRegistro = row.Field<string>("FCPF17").Trim();
                    model.EstadoProveedor = row.Field<string>("FCPE01").Trim();
                }

                foreach (DataRow row in dsFacturas.Tables[0].Rows)
                {
                    DetalleFacturasDataModel detalleFactura = new DetalleFacturasDataModel
                    {
                        IdProveedor = long.Parse(row.Field<decimal>("FCFIDP").ToString()),
                        NumeroFactura = row.Field<string>("FCFNU6"),
                        DetalleFactura = row.Field<string>("FCFC05"),
                        ValorFactura = row.Field<decimal>("FCFV23")
                    };
                    model.DetalleFactura.Add(detalleFactura);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return model;
        }



        public string FechaCitaRecepcionDocumentos()
        {
            string fechaCita = string.Empty;
            servicoDB = new ServicioWebAS400.Service1();


            try
            {
                fechaCita = servicoDB.SelectPuro("select max(FCPF17) from " + biblioteca + ".FCPA12");

                DateTime myDate = DateTime.Now;
                myDate = myDate.AddMinutes(30);

                fechaCita = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            }
            catch 
            {
                fechaCita = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            }
            return fechaCita;
        }
    }

}