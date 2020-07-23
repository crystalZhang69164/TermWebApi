using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TermWebApi
{
    public class Transactions
    {
        private string merchantid;
        private string apikey;
        private DateTime date;
        private float amount;
        private string type;
        private int custId;
        private int restId;


        public int CustId
        {
            get { return custId; }
            set { custId = value; }
        }
        public int RestId
        {
            get { return restId; }
            set { restId = value; }
        }
        public String MerchantId
        {
            get { return merchantid; }
            set { merchantid = value; }
        }

        public String ApiKey
        {
            get { return apikey; }
            set { apikey = value; }
        }

        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }

        public float Amount
        {
            get { return amount; }
            set { amount = value; }
        }

        public String Type
        {
            get { return type; }
            set { type = value; }
        }
    }
}
