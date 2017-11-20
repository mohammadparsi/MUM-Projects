using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Infrastructure
{
    public class ExcellUtility
    {
        public static void ImportFromExcelToSqlServer(string filePath, string fileName, string sheetName, string tableName)
        {
            //string tsqlConnectionString = "Data Source=.; Initial Catalog=test3;Integrated Security=True";

            string excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties=Excel 12.0;";
            // Create Connection to Excel Workbook

            using (OleDbConnection connection =
                         new OleDbConnection(excelConnectionString))
            {


                OleDbCommand command = new OleDbCommand
                            (string.Format("Select * FROM [{0}$]", sheetName), connection);
                //OleDbCommand command1 = new OleDbCommand
                //            ("DELETE top(7) FROM [PERSON$]", connection);

                connection.Open();


                // Create DbDataReader to Data Worksheet `
                using (DbDataReader dr = command.ExecuteReader())
                {

                    // SQL Server Connection String 
                    string sqlConnectionString = "Data Source=.; Initial Catalog=DocumentFinder;Integrated Security=True";


                    // Bulk Copy to SQL Server 
                    using (SqlBulkCopy bulkCopy =
                               new SqlBulkCopy(sqlConnectionString))
                    {
                        bulkCopy.DestinationTableName = tableName;
                        bulkCopy.WriteToServer(dr);
                    }
                }

                //using (var sc = new SqlConnection(tsqlConnectionString))
                //using (var cmd = sc.CreateCommand())
                //{
                //    sc.Open();
                //    cmd.CommandText = "DELETE FROM BOOK  WHERE (Uri IS NULL) or uri=N'کتاب' or uri=N'اجباري' or uri=N'اجباری' or uri=N'كد کتاب' ";
                //    cmd.ExecuteNonQuery();
                //}

                //using (var sc = new SqlConnection(tsqlConnectionString))
                //using (var cmd = sc.CreateCommand())
                //{
                //    string strCommandText = string.Format("UPDATE BOOK SET f1= N'{0}' where f1 is null", fileName);
                //    sc.Open();
                //    cmd.CommandText = strCommandText;
                //    cmd.ExecuteNonQuery();
                //}

                //using (var sc = new SqlConnection(tsqlConnectionString))
                //using (var cmd = sc.CreateCommand())
                //{
                //    sc.Open();
                //    //cmd.CommandText = "Update BOOK Set clasIdGrupTitle = editionNum where clasIdGrupTitle=N'كاغذی و الكترونیكی' or clasIdGrupTitle=N'الكترونیكی' or clasIdGrupTitle=N'كاغذی'; Update BOOK Set clasIdGrupTitle= replace(clasIdGrupTitle, N'علوم پزشکی', N'علوم پزشكی'); Update BOOK Set clasIdGrupTitle = replace(clasIdGrupTitle, N'ي', N'ی'); Update BOOK Set clasIdGrupTitle = replace(clasIdGrupTitle, N'فنی و مهندسی', N'فنی مهندسی'); DELETE from BOOK where clasIdGrupTitle= N'فرمت انتشار'; ";
                //    cmd.CommandText = "delete from BOOK where clasIdGrupTitle=N'clasIdGrupTitle'; Update BOOK Set clasIdGrupTitle= replace(clasIdGrupTitle, N'علوم پزشکی', N'علوم پزشكی'); Update BOOK Set clasIdGrupTitle = replace(clasIdGrupTitle, N'ي', N'ی'); Update BOOK Set clasIdGrupTitle = replace(clasIdGrupTitle, N'فنی و مهندسی', N'فنی مهندسی'); DELETE from BOOK where clasIdGrupTitle= N'فرمت انتشار'; ";
                //    cmd.ExecuteNonQuery();
                //}

            }
        }

    }
}