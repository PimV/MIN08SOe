using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trainee_Manager.Controller;

namespace Trainee_Manager.Model
{
    class StageOpdracht
    {
        private Boolean _afstudeerOpdracht;

        public Boolean AfstudeerOpdracht
        {
            get { return _afstudeerOpdracht; }
            set { _afstudeerOpdracht = value; }
        }

        private Boolean _EPS;

        public Boolean EPS
        {
            get { return _EPS; }
            set { _EPS = value; }
        }

        private int _stageID;
        private int _periode;

        private Bedrijf bedrijf;

        public Bedrijf Bedrijf
        {
            get { return bedrijf; }
            set { bedrijf = value; }
        }

        private Student student1;

        public Student Student1
        {
            get { return student1; }
            set { student1 = value; }
        }
        private Student student2;

        public Student Student2
        {
            get { return student2; }
            set { student2 = value; }
        }

        public List<string> kenmerken;

        public List<string> Kenmerken
        {
            get { return kenmerken; }
            set { kenmerken = value; }
        }

        public int StageID { get; set; }
        public int Periode { get; set; }

        public StageOpdracht(Student student1, Bedrijf bedrijf)
        {
            this.student1 = student1;
            this.bedrijf = bedrijf;

            kenmerken = new List<string>();
        }

        public StageOpdracht(Student student1, Student student2, Bedrijf bedrijf)
        {
            this.student1 = student1;
            this.student2 = student2;
            this.bedrijf = bedrijf;

            kenmerken = new List<string>();
        }

        public StageOpdracht()
        {
            kenmerken = new List<string>();
        }
    }
}
