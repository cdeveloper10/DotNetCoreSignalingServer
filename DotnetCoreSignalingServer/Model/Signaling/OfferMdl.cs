using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Model.Signaling
{
    public class Offer
    {
        private string _type;
        private string _sdp;

        public string type
        {
            get { return _type; }
            set { _type = value; }
        }

        public string sdp
        {
            get { return _sdp; }
            set { _sdp = value; }
        }
    }

    public class OfferObject
    {
        private string _type;
        private Offer _offer;
        private string _name;
        private string _owner;

        public string type
        {
            get { return _type; }
            set { _type = value; }
        }

        public Offer offer
        {
            get { return _offer; }
            set { _offer = value; }
        }

        public string name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string Owner
        {
            get { return _owner; }
            set { _owner = value; }
        }
    }

    public class OfferResult
    {
        private string _type;
        private Offer _offer;
        private string _name;

        public string type
        {
            get { return _type; }
            set { _type = value; }
        }

        public Offer offer
        {
            get { return _offer; }
            set { _offer = value; }
        }

        public string name
        {
            get { return _name; }
            set { _name = value; }
        }
    }
}
