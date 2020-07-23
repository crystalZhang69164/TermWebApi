using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TermWebApi
{
    
    public class VirtualWallet
    {
        private string merchantid;
        private string apikey;
        private string cardnumber;
        private string cardtype;
        private float balance;

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

        public String CardNumber
        {
            get { return cardnumber; }
            set { cardnumber = value; }
        }

        public String CardType
        {
            get { return cardtype; }
            set { cardtype = value; }
        }

        public float Balance
        {
            get { return balance; }
            set { balance = value; }
        }
    }
}
