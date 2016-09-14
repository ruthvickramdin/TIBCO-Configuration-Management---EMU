using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDFCreationApplication
{
    public static class Enhancements
    {
        #region Copy Transform files to SVN

        public static void DirSearch(string svnDirPath, string actualDirPath)
        {
            try
            {
                string localPath = actualDirPath + "\\";

                foreach (string d in Directory.GetDirectories(svnDirPath))
                {
                    foreach (string f in Directory.GetFiles(d))
                    {
                        //Console.WriteLine(f);

                        if (f.Contains(".transform"))
                        {

                            string test = f.Substring(f.LastIndexOf('\\'), f.Length - f.LastIndexOf('\\'));
                            test = test.Substring(1);
                            Console.WriteLine(test);
                            File.Copy(localPath + test, f, true);
                        }
                    }
                    DirSearch(d, localPath);
                }
            }
            catch (System.Exception excpt)
            {
                Console.WriteLine(excpt.Message);
            }
        }

        #endregion

        #region Update PDF Values

        public static void ReplaceOldWithNewValue(string pdfFilePath, string excelFilePath)
        {
            string conStr = string.Empty;
            string sheetName = string.Empty;
            string Extension = string.Empty; //Path.GetExtension(ExcelFilePath);
            string oldvalue = string.Empty;
            string newvalue = string.Empty;
            string EditableFilePath = pdfFilePath;
            //set the default value 

            string ExcelFilePath = excelFilePath;
            Extension = ".xlsx";

            try
            {
                switch (Extension)
                {
                    case ".xls": //Excel 97-03
                        conStr = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + ExcelFilePath + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=1\";";//"Provider=Microsoft.Jet.OLEDB.4.0;Data Source="+FilePath+";Extended Properties=Excel 8.0";

                        break;
                    case ".xlsx": //Excel 07
                        conStr = //"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FilePath + ";Extended Properties=Excel 12.0;HDR=Yes;IMEX=2";
                        @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + ExcelFilePath + ";Extended Properties=\"Excel 12.0 Xml;HDR=YES;IMEX=1\";";
                        break;
                }
                //Get the name of the First Sheet.
                using (OleDbConnection con = new OleDbConnection(conStr))
                {
                    using (OleDbCommand cmd = new OleDbCommand())
                    {
                        cmd.Connection = con;
                        con.Open();
                        DataTable dtExcelSchema = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                        sheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                        con.Close();
                    }
                }

                //read the all Excel file data into  datatable
                DataTable dt = new DataTable();
                using (OleDbConnection con = new OleDbConnection(conStr))
                {
                    using (OleDbCommand cmd = new OleDbCommand())
                    {
                        using (OleDbDataAdapter oda = new OleDbDataAdapter())
                        {
                            cmd.CommandText = "SELECT * From [" + sheetName + "]";
                            cmd.Connection = con;
                            con.Open();
                            oda.SelectCommand = cmd;
                            oda.Fill(dt);
                            con.Close();

                        }
                    }
                }

                // Read the file as one string. and replace the oldvalue with new value
                string text = System.IO.File.ReadAllText(EditableFilePath);

                foreach (DataRow dr in dt.Rows)
                {
                    oldvalue = dr[0].ToString();
                    if (text.Contains(oldvalue))
                    {
                        newvalue = dr[1].ToString();
                        text = text.Replace(oldvalue, newvalue);
                    }
                }

                System.IO.File.WriteAllText(EditableFilePath, text);
                Console.ReadLine();
            }

            finally
            {
            }//
        } 

        #endregion
    }
}
