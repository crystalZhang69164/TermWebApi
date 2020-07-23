using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System.Data;
using System.Data.SqlClient;
using Utilities;

namespace TermWebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/service/PaymentGateway")]
    public class TermController : Controller
    {
        // GET: api/Term
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value3", "value2" };
        }

        // GET: api/Term/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Term
        //[HttpPost]
        //public void Post([FromBody]string value)
        //{
        //}

        // PUT: api/Term/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        //[HttpPost] 

        //public string Post(string major)
        //{
        //    return "This is right:" + major;
        //}

        [HttpPost("CreateVirtualWallet")]
        public int CreateVirtualWallet(String MerchantId, String ApiKey, String CardType, String CardNumber)
        {


            DBConnect objDB = new DBConnect();
            SqlCommand objCommand = new SqlCommand();

            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "TP_CreateWallet";


            objCommand.Parameters.AddWithValue("@cardNumber", CardNumber);
            objCommand.Parameters.AddWithValue("@cardType", CardType);
            objCommand.Parameters.AddWithValue("@balance", 0);
            SqlParameter output = new SqlParameter("@virtualWalletId", 0);

            output.Direction = ParameterDirection.Output;
            objCommand.Parameters.Add(output);
            objDB.DoUpdateUsingCmdObj(objCommand);
            int virtualwallet;
            virtualwallet = int.Parse(objCommand.Parameters["@virtualWalletId"].Value.ToString());
            return virtualwallet;
        }

        [HttpPost("ProcessPayment")]
        public bool ProcessPayment(int CustomerWalletId, int RestaurantWalletId, float Amount, String type, int MerchantId, String ApiKey)
        {
            DBConnect objDB = new DBConnect();
            SqlCommand objCommand = new SqlCommand();

            if (type == "Refund")
            {
                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.CommandText = "TP_Refund";

                objCommand.Parameters.AddWithValue("@customerWalletId", CustomerWalletId);
                objCommand.Parameters.AddWithValue("@restaurantId", RestaurantWalletId);
                objCommand.Parameters.AddWithValue("@amount", Amount);
                objCommand.Parameters.AddWithValue("@type", type);
                objCommand.Parameters.AddWithValue("@date", DateTime.Now);

                objDB.DoUpdateUsingCmdObj(objCommand);

                return true;
            }
            if (type == "Payment")
            {
                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.CommandText = "TP_Payemnt";

                objCommand.Parameters.AddWithValue("@customerWalletId", CustomerWalletId);
                objCommand.Parameters.AddWithValue("@restaurantId", RestaurantWalletId);
                objCommand.Parameters.AddWithValue("@amount", Amount);
                objCommand.Parameters.AddWithValue("@type", type);
                objCommand.Parameters.AddWithValue("@date", DateTime.Now);

                objDB.DoUpdateUsingCmdObj(objCommand);

                return true;
            }
            return false;
        }
        [HttpPut("UpdatePaymentAccount")]
        public void UpdatePaymentAccount(int VirtualWalletId, String MerchantId, String ApiKey, String CardType, String CardNumber)
        {
            DBConnect objDB = new DBConnect();
            SqlCommand objCommand = new SqlCommand();

            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "TP_UpdateAccount";

            objCommand.Parameters.AddWithValue("@virtualWalletId", VirtualWalletId);
            objCommand.Parameters.AddWithValue("@cardType", CardType);
            objCommand.Parameters.AddWithValue("@cardNumber", CardNumber);

            objDB.DoUpdateUsingCmdObj(objCommand);

        }
        [HttpPut("FundAccount")]
        public void FundAccount(int VirtualWalletId, float Amount, int MerchantId, String ApiKey)
        {
            DBConnect objDB = new DBConnect();
            SqlCommand objCommand = new SqlCommand();

            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "TP_FundAccount";

            objCommand.Parameters.AddWithValue("@virtualWalletId", VirtualWalletId);
            objCommand.Parameters.AddWithValue("@amount", Amount);

            objDB.DoUpdateUsingCmdObj(objCommand);
        }

        [HttpGet("GetTransactions/{VirtualWalletId}/{MerchantId}/{apiKey}")]
        public List<Transactions> GetTransactions(int VirtualWalletId, int MerchantId, string apiKey)
        {
            List<Transactions> transactionList = new List<Transactions>();
            DBConnect objDB = new DBConnect();
            SqlCommand objCommand = new SqlCommand();

            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "TP_GetTransaction";

            objCommand.Parameters.AddWithValue("@virtualWalletId", VirtualWalletId);
            DataSet myData = objDB.GetDataSetUsingCmdObj(objCommand);

            objDB.DoUpdateUsingCmdObj(objCommand);

            Transactions transactions = new Transactions();

            foreach (DataRow record in myData.Tables[0].Rows)
            {
                transactions = new Transactions();
                transactions.Amount = float.Parse(record["Amount"].ToString());
                transactions.Date = DateTime.Parse(record["Date"].ToString());
                transactions.Type = record["type"].ToString();
                transactions.CustId = int.Parse(record["CustomerWalletId"].ToString());
                transactions.RestId = int.Parse(record["RestaurantWalletId"].ToString());
                transactionList.Add(transactions);
            }

            return transactionList;
        }
    }
}
