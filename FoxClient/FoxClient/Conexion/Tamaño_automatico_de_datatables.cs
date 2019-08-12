using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Forms;
using System.Drawing;

namespace FoxClient.Conexion
{
    class Tamaño_automatico_de_datatables
    {
        public static void Multilinea(ref DataGridView List)
        {
            //Las filas se adaptaran segun el texto para que no hayan problemas con el tamaño
            List.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            List.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            List.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            List.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            //cabeceras
            List.EnableHeadersVisualStyles = false;
            DataGridViewCellStyle styCabeceras = new DataGridViewCellStyle();
            //color
            styCabeceras.BackColor = System.Drawing.Color.White;
            styCabeceras.ForeColor = System.Drawing.Color.Black;
            //tipo de letra
            styCabeceras.Font = new Font ("Segoe UI",10,FontStyle.Bold);
            List.ColumnHeadersDefaultCellStyle = styCabeceras;
        }
    }
}
