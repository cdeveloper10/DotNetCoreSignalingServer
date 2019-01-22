using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Model.Signaling
{
    public class Candidate
    {
        private string _usernameFragment;
        private int _sdpMLineIndex;
        private string _candidate;
        private string _sdpMid;

        public string candidate
        {
            get { return _candidate; }
            set { _candidate = value; }
        }

        public string sdpMid
        {
            get { return _sdpMid; }
            set { _sdpMid = value; }
        }

        public int sdpMLineIndex
        {
            get { return _sdpMLineIndex; }
            set { _sdpMLineIndex = value; }
        }

        public string usernameFragment
        {
            get { return _usernameFragment; }
            set { _usernameFragment = value; }
        }
    }

    public class CandidateObject
    {
        private string _name;
        private Candidate _candidate;
        private string _type;
        private string _owner;

        public string type
        {
            get { return _type; }
            set { _type = value; }
        }

        public Candidate candidate
        {
            get { return _candidate; }
            set { _candidate = value; }
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

    public class CandidateResult
    {
        private string _type;
        private Candidate _candidate;
        private string _name;

        public string type
        {
            get { return _type; }
            set { _type = value; }
        }

        public Candidate candidate
        {
            get { return _candidate; }
            set { _candidate = value; }
        }
    }
}
