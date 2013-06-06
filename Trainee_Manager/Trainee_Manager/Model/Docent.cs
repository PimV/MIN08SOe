using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trainee_Manager.Model
{
    public class Docent
    {
        public int Id { get; set; }
        public int Rating { get; set; }

        
        private string _naam;
        private string _email;
        private string _adres;
        private string _postcode;
        private string _kenmerken;

        private int _afstand;

        public int Afstand
        {
            get { return _afstand; }
            set { _afstand = value; }
        }

        private int _vrije_uren;
        private int _periode;
        private int _rating;
        private int _id;
        private int _tijdvrij;

        public List<string> kenmerkenlijst;
        public List<string> VoorkeurBedrijven;
        public List<int> VoorkeurStages;
        public List<KeyValuePair<int, string>> Vrije_uren;

        public string Naam { get; set; }
        public string Email { get; set; }
        public string Adres { get; set; }
        public string Postcode { get; set; }

        public string Kenmerken
        {
            get { return _kenmerken; }
            set { _kenmerken = value; }
        }

        public int Periode { get; set; } 
        
        public int Tijdvrij { get; set; }

        public Docent()
        {
            kenmerkenlijst = new List<string>();
            VoorkeurBedrijven = new List<string>();
            VoorkeurStages = new List<int>();

        }

        public void addVrijurenList(List<KeyValuePair<int, string>> vrijuren)
        {
            Vrije_uren = new List<KeyValuePair<int, string>>();

            Vrije_uren = vrijuren;
        }
    }
}
