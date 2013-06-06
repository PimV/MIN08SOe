using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trainee_Manager.Model
{
    public class Docent
    {
        private string _naam;
        private string _email;
        private string _adres;
        private string _postcode;

        private int _vrije_uren;
        private int _periode;
        private int _rating;
        private int _id;
        private int _tijdvrij;

        public List<string> kenmerken;
        public List<string> VoorkeurBedrijven;
        public List<int> VoorkeurStages;
        public List<KeyValuePair<int, string>> Vrije_uren;

        public string Naam { get; set; }
        public string Email { get; set; }
        public string Adres { get; set; }
        public string Postcode { get; set; }

        public int Periode { get; set; }
        public int Rating { get; set; }
        public int Id { get; set; }
        public int Tijdvrij { get; set; }

        public Docent()
        {
            kenmerken = new List<string>();
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
