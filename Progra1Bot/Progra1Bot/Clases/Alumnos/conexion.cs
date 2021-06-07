using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data.Sql;
using System.Data.SqlClient;

namespace Progra1Bot.Clases
{
    class clsConexion
    {
        
        private string servidor = "DESKTOP-03L4M4P\\SQLEXPRESS";    
        private string nombre  = "Accesorios"; 
       
        private string datos = null; 
        private bool iniciado = false;
        public long ChatId { get; private set; }
        SqlConnection conexionBD;
        SqlDataReader reader;
        public clsConexion(long ChatId)
        {
            this.ChatId = ChatId;
            this.iniciar();
        }
        private void iniciar()
        {
            if (!iniciado)
            {
                //Crearemos la cadena de conexión concatenando las variables
                string cadenaConexion = "Database=" + nombre + "; Data Source=" + servidor + "; User Id="; /*+ usuario + "; Password=" + password + "";*/
                //Instancia para conexión a MySQL, recibe la cadena de conexión
                conexionBD = new SqlConnection(cadenaConexion);
                this.iniciado = true;
            }
        }
        private DataTable Consultitas(string consulta)
        {

            try
            {
                SqlCommand comando = new SqlCommand(consulta); 
                comando.Connection = conexionBD; //Establece la SqlConnection utilizada por esta instancia de SqlCommand
                conexionBD.Open(); //Abre la conexión

                DataTable dt = new DataTable();
                dt.Load(comando.ExecuteReader());
                return dt;
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                DataTable dt = new DataTable();
                return dt;
            }
            finally
            {
                conexionBD.Close(); //Cierra la conexión a SQL
            }
        }
        public DataTable Orden()
        {
            string consulta = $"select * from posiciones where idchat={this.ChatId} limit 1";
            Console.WriteLine(consulta);
            return this.Consultitas(consulta);
        }


    }
}  