using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trainee_Manager.Model
{
    public class Bedrijf
    {
        private string _naam;
        private string _plaats;
        private string _postcode;
        private string _straat;
        private int _bedrijf_id;

        public int Bedrijf_id
        {
            get { return _bedrijf_id; }
            set { _bedrijf_id = value; }
        }

        public string Naam { get; set; }
        public string Plaats { get; set; }
        public string Postcode { get; set; }
        public string Straat { get; set; }

        public Bedrijf()
        {

        }

    }
}
