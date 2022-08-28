using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;
using ENTBarCodeAPI.Models;

namespace ENTBarCodeAPI.DAL
{
    public class BarCodeDAL
    {
        string connString = ConfigurationManager.ConnectionStrings["BarCodeConnectionString"].ToString();

        //Get all Barcode
        public List<Barcode> GetAllBarcodes()
        {
            List<Barcode> barcodelist = new List<Barcode>();

            using (SqlConnection connection = new SqlConnection(connString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "usp_GetAllBarCode";
                SqlDataAdapter sqlDa = new SqlDataAdapter(command);
                DataTable dtBarcode = new DataTable();

                connection.Open();
                sqlDa.Fill(dtBarcode);
                connection.Close();
                foreach (DataRow dr in dtBarcode.Rows)
                {
                    barcodelist.Add(new Barcode
                    {
                        BarcodeId = Convert.ToInt32(dr["BarcodeId"]),
                        BarcodeInitial = dr["BarcodeInitial"].ToString(),
                        BarcodeDescription = dr["BarcodeDescription"].ToString(),
                        BarcodePackNo = Convert.ToInt32(dr["BarcodePackNo"]),
                        BarcodeCreatedDate = Convert.ToDateTime(dr["BarcodeCreatedDate"]),
                        //BarcodeCreatedDate= Convert.ToDateTime(dr["BarcodeCreateDate"]),
                        BarcodeActive = Convert.ToInt32(dr["BarcodeActive"])
                    }); ;
                }
            }
            return barcodelist;
        }

        //Insert BarCode
        public bool InsertBarCode(Barcode barcode)
        {
            int id = 0;
            using (SqlConnection connection = new SqlConnection(connString))
            {
                //SqlCommand command = new SqlCommand("usp_InsertBarCodeMaster", connection);
                SqlCommand command = new SqlCommand("usp_InsertBarCodeMaster_1", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@BarcodeInitial", barcode.BarcodeInitial);
                command.Parameters.AddWithValue("@BarcodeDescription", barcode.BarcodeDescription);
                command.Parameters.AddWithValue("@BarcodePackNo", barcode.BarcodePackNo);
              //  command.Parameters.AddWithValue("@BarcodeCreatedDate", barcode.BarcodeCreateDate);
                command.Parameters.AddWithValue("@BarcodeCreatedDate", barcode.BarcodeCreatedDate);
                command.Parameters.AddWithValue("@BarcodeActive", barcode.BarcodeActive);
                connection.Open();
                id = command.ExecuteNonQuery();              
                connection.Close();
            }
            if (id > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //GET  BARCODE by using BarCodeId
        public List<Barcode> GetBarcodeByID(int BarcodeId)
        {
            List<Barcode> barcodelist = new List<Barcode>();

            using (SqlConnection connection = new SqlConnection(connString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "usp_GetBarCodeByID";
                command.Parameters.AddWithValue("@BarcodeId", BarcodeId);
                SqlDataAdapter sqlDa = new SqlDataAdapter(command);
                DataTable dtBarcode = new DataTable();

                connection.Open();
                sqlDa.Fill(dtBarcode);
                connection.Close();
                foreach (DataRow dr in dtBarcode.Rows)
                {
                    barcodelist.Add(new Barcode
                    {
                        BarcodeId = Convert.ToInt32(dr["BarcodeId"]),
                        BarcodeInitial = dr["BarcodeInitial"].ToString(),
                        BarcodeDescription = dr["BarcodeDescription"].ToString(),
                        BarcodePackNo = Convert.ToInt32(dr["BarcodePackNo"]),
                        BarcodeCreatedDate = Convert.ToDateTime(dr["BarcodeCreatedDate"]),
                       // BarcodeCreatedDate = Convert.ToDateTime(dr["BarcodeCreateDate"]),
                        BarcodeActive = Convert.ToInt32(dr["BarcodeActive"])
                    });
                }
            }
            return barcodelist;
        }

        // Updating BarCode 
        public bool UpdateBarCode(Barcode barcode)
        {
            int i = 0;
            using (SqlConnection connection = new SqlConnection(connString))
            {
                SqlCommand command = new SqlCommand("usp_UpdateBarCodeMaster", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@BarcodeId", barcode.BarcodeId);
                command.Parameters.AddWithValue("@BarcodeInitial", barcode.BarcodeInitial);
                command.Parameters.AddWithValue("@BarcodeDescription", barcode.BarcodeDescription);
                command.Parameters.AddWithValue("@BarcodePackNo", barcode.BarcodePackNo);
                command.Parameters.AddWithValue("@BarcodeCreatedDate", barcode.BarcodeCreatedDate);
                //command.Parameters.AddWithValue("@BarcodeCreatedDate", barcode.BarcodeCreateDate);
                command.Parameters.AddWithValue("@BarcodeActive", barcode.BarcodeActive);
                connection.Open();
                i = command.ExecuteNonQuery();
                connection.Close();
            }
            if (i > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //Delete Bar Code
        public string DeleteBarCode(int barcodeid)
        {
            string result = "";
            using (SqlConnection connection = new SqlConnection(connString))
            {
                SqlCommand command = new SqlCommand("usp_DeleteBarCode", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@barcodeid", barcodeid);
                command.Parameters.Add("@OUTPUTMESSAGE", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                connection.Open();
                command.ExecuteNonQuery();
                result = command.Parameters["@OUTPUTMESSAGE"].Value.ToString();
                connection.Close();
            }
            return result;

        }
    }
}