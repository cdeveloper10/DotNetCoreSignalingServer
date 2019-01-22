using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalingServer.Model.Signaling
{
    public class Answer
    {
        private string _sdp;
        private string _type;

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

    public class AnswerObject
    {
        private string _owner;
        private string _name;
        private Answer _answer;
        private string _type;

        public string type
        {
            get { return _type; }
            set { _type = value; }
        }

        public Answer answer
        {
            get { return _answer; }
            set { _answer = value; }
        }

        public string name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string owner
        {
            get { return _owner; }
            set { _owner = value; }
        }
    }
}
