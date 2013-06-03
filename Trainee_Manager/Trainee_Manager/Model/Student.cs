using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trainee_Manager.Model
{
    class Student
    {
        private int _studentid;
        private int _studentnummer;

        private string _naam;

        public int Studentid { get; set; }
        public int Studentnummer { get; set; }

        public string Naam { get; set; }

        public Student(int studentid, int studentnummer, String studentnaam)
        {
            Studentid = studentid;
            Studentnummer = studentnummer;
            Naam = studentnaam;
        }



    }
}
