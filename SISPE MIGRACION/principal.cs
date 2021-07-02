using SISPE_MIGRACION.codigo.baseDatos;
using SISPE_MIGRACION.formularios;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION
{
    static class principal
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            //**********************************************************
            //**       ICONOS PARA EL SISTEMAS PÁGINA                 **
            //**   https://icons8.com/icon/new-icons/office           **
            //**********************************************************

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            string host = SISPE_MIGRACION.Properties.Resources.servidor;
            string usuario = SISPE_MIGRACION.Properties.Resources.usuario;
            string password = SISPE_MIGRACION.Properties.Resources.password;
            string database = SISPE_MIGRACION.Properties.Resources.baseDatos;
            string port = SISPE_MIGRACION.Properties.Resources.puerto;


            //host = "ec2-23-21-160-38.compute-1.amazonaws.com";
            //usuario = "hwvzppntjiviyu";
            //password = "8ec67b7ca03d1e00ba4ac06dc6cdf97e148da0ddc2b7bb69523dec23cdde5256";
            //database = "ddboilk04tmcso";
            //SSL Mode = Require; Trust Server Certificate = true
            string queryConexion = string.Format("Host={0};Username={1};Password={2};Database={3};port={4}", host,usuario,password,database,port);
            
            

            globales.datosConexion = queryConexion;

            if (baseDatos.realizarConexion(queryConexion))
            {
                
                
                
                
                try
                {
                    
                    string ip = principal.obtenerIp();
                    string[] ipArr = ip.Split('.');
                    string ultimonumero = ipArr[3];
                    if (ultimonumero == "241" || ultimonumero == "198" || ultimonumero == "148" || ultimonumero == "199")
                    {
                        if (host.Contains("192.168.100."))
                        {
                            MessageBox.Show("Cuidado se esta trabajando con la base de datos servidor 192.168.100.101", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                    else {
                        if (host.Contains("10.172.23.")) {
                            MessageBox.Show("Base de datos incorrecta, verificar con sistemas..", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                    }
                }
                catch {
                    MessageBox.Show("Atención el servidor 192.168.100.102 donde se encuentra las aportaciones perdidas del año 2011 al 2015 no se encuentra disponible, puede continuar su flujo de trabajo con normalidad pero no se podrá consultar información de esa base de datos","Aviso",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }

                                
                //Application.Run(new subirinstalador());
               Application.Run(new login());
            }
        }


        //Método para obtener una ip
        public static string obtenerIp()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No existe adaptador de red");
        }
    }
}
