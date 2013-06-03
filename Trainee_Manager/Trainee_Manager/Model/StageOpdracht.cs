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

        private int _stageID;
        private int _periode;

        private Bedrijf bedrijf;

        private Student student1;
        private Student student2;

        List<string> kenmerken;

        public int StageID { get; set; }
        public int Periode { get; set; }

        public StageOpdracht(Student student1, Student student2, Bedrijf bedrijf)
        {
            this.student1 = student1;
            this.student2 = student2;
            this.bedrijf = bedrijf;

            kenmerken = new List<string>();
        }
    }
}
