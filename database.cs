using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;


namespace Pince_a_croute
{
    public  class database
    {
        public static void connection(string datetime, string FileFullPath)
        {

            //datetime = DateTime.Now.ToString("yyyyMMddHHmm");
            //var SQLConnection = new SqlConnection
            //{
            //    ConnectionString =
            //    @"Data Source = .; Database =RFID_SURAL_2 ;Integrated Security=SSPI"
            //};

            var query3 = @"
SELECT 
        D.[detection_id]
      , D.[reader_uwb_id]
      , D.[tag_id]
      , D.[tag_temperature]
      , D.[distance]
      , D.[Emplacement]
      , D.[insert_timestamp]
  
FROM[dbo].[noovelia_kencee_detection] as D
  
INNER JOIN dbo.noovelia_kencee_antenne as A
ON A.Reader_uwb_id=D.reader_uwb_id 

INNER JOIN dbo.noovelia_kencee_balise as B
ON B.Fonction='PINCE À CROUTE' and A.fonction='PINCE À CROUTE' WHERE  B.Location_ID=A.Location_ID    
ORDER BY detection_id desc";
            var SQLConnection = new SqlConnection();
            SQLConnection.ConnectionString =
                 @"Data Source = ABI-SMT-SQL-CL1.apm.alcoa.com; Database =SMART DFRM ;Integrated Security=SSPI";
        
            
            
           
            var query = @"SELECT
       D.[detection_id]
      , D.[reader_uwb_id]
      , D.[tag_id]
      , D.[tag_temperature]
      , D.[distance]
      , [Emplacement]
      , D.[insert_timestamp]  
  
  FROM [ABI-MES-SQL-CL1.APM.ALCOA.COM].[RFID_SURAL].[dbo].[noovelia_kencee_detection] as D
  
INNER JOIN [ABI-MES-QA.APM.ALCOA.COM].[RFID_SURAL_2].dbo.noovelia_kencee_antenne as A
ON A.Reader_uwb_id=D.reader_uwb_id 

INNER JOIN [ABI-MES-QA.APM.ALCOA.COM].[RFID_SURAL_2].dbo.noovelia_kencee_balise as B
ON B.Fonction='PINCE À CROUTE' and A.fonction='PINCE À CROUTE' WHERE [insert_timestamp]>='2022-08-19'
and B.Nom_Emplacement=A.Nom_Emplacement    
ORDER BY Emplacement ,detection_id desc";

            var cmd = new SqlCommand(query, SQLConnection);
            try
            {
                SQLConnection.Open();
                var d_table = new DataTable();
                if (d_table != null)
                {
                    Debug.Assert(d_table != null, nameof(d_table) + " != null");
                    d_table.Load(cmd.ExecuteReader());
                    WriteLine(d_table);

                    SQLConnection.Close();

                    //Prepare the file path 


                    StreamWriter sw = null;
                    sw = new StreamWriter(FileFullPath, false);
                    //// Write the Header Row to File
                    var ColumnCount = d_table.Columns.Count;
                    for (var ic = 0; ic < ColumnCount; ic++)
                    {
                        sw.Write(d_table.Columns[ic]);
                        if (ic < ColumnCount - 1) sw.Write(';');
                    }

                    WriteLine("50%");
                    sw.Write(sw.NewLine);

                    //// Write All Rows to the File
                    foreach (DataRow dr in d_table.Rows)
                    {
                        for (var ir = 0; ir < ColumnCount; ir++)
                        {
                            if (!Convert.IsDBNull(dr[ir])) sw.Write(dr[ir].ToString());
                            if (ir < ColumnCount - 1) sw.Write(';');
                        }

                        sw.Write(sw.NewLine);
                    }

                    WriteLine("Fichier creer");
                    sw.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                TextWriter wt = new StreamWriter(@"O:Temporaire/Ismael/Programclasserrors.txt");

                wt.WriteLine(e.Message);

                wt.Close();
            }
        }
    }
    
    }

