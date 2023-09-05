using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BaseCommonControls.ExcelExport
{
    public class ExportExcel
    {

        public void ExportToExcel(DataGridView dgv)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture("en-US");


            // creating Excel Application
            Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
            
            // creating new WorkBook within Excel application
            Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);

            // creating new Excelsheet in workbook
            Microsoft.Office.Interop.Excel._Worksheet worksheet = null;

            // see the excel sheet behind the program
            app.Visible = true;

            // get the reference of first sheet. By default its name is Sheet1.
            // store its reference to worksheet
            //   worksheet = (Microsoft.Office.Interop.Excel._Worksheet)workbook.Sheets["Sheet1"];
            worksheet =  (Microsoft.Office.Interop.Excel._Worksheet)workbook.ActiveSheet;


            List<string> AvailableColumns = new List<string>();
            foreach (DataGridViewColumn c in dgv.Columns)
            {
                if (!c.Visible || c.Name == "colchkbx") continue;
                
                AvailableColumns.Add(c.HeaderText);
            }

      
      // storing header part in Excel
            for (int i = 0; i < AvailableColumns.Count; i++)
                worksheet.Cells[1, i + 1] = AvailableColumns[i];

            int nbRows = dgv.Rows.Count;

            string destrange = (((char)((int)'A' + dgv.Columns.Count - 1)).ToString() + (nbRows + 1)).ToString();
            Microsoft.Office.Interop.Excel.Range Destination = worksheet.get_Range("A2", destrange);


            object[,] datas = new object[nbRows, dgv.Columns.Count];


            for (int j = 0; j < AvailableColumns.Count; j++)
            {
                DataGridViewColumn selectedcol = null;

                foreach (DataGridViewColumn c in dgv.Columns)
                    if (c.HeaderText == AvailableColumns[j])
                        selectedcol = c;

                int k = 0;
                for (int i = 0; i < nbRows; i++)
                    if (dgv.Rows[i].Cells[dgv.ColumnCount - 1].GetType() != typeof(DataGridViewCheckBoxCell))
                    {
                        if (dgv.Rows[i].Cells[selectedcol.Name].Value != null)
                        {
                            if (dgv.Rows[i].Cells[selectedcol.Name].Value.GetType() == typeof(DateTime))
                                datas[k, j] = ((DateTime)dgv.Rows[i].Cells[selectedcol.Name].Value).ToShortDateString();
                            else
                                datas[k, j] = dgv.Rows[i].Cells[selectedcol.Name].Value.ToString();
                            k++;
                        }
                    }
            }

            Destination.set_Value(Type.Missing, datas);

        }
    }
}
