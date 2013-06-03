using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trainee_Manager.Model
{
    class Docent
    {
        private string _naam;
        private string _email;
        private string _adres;
        private string _postcode;

        private int _vrije_uren;
        private int _periode;
        private int _rating;

        List<string> kenmerken;
        List<string> VoorkeurBedrijven;

        List<int> VoorkeurStages;

        public string Naam { get; set; }
        public string Email { get; set; }
        public string Adres { get; set; }
        public string Postcode { get; set; }

        public int Vrije_uren   { get; set; }
        public int Periode      { get; set; }
        public int Rating       { get; set; }


    }
}
