using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trainee_Manager.Model;
using Trainee_Manager.View;

namespace Trainee_Manager.Controller
{
    public class RatingController
    {

        private RatingGegevensImporter importer;
        private RatingCalculator calc;
        private InstructorRatingList listView;
        private DocentList docList;


        public RatingController() 
        {
           // this.listView = listView;
        }

        public void setList(InstructorRatingList listView)
        {
            this.listView = listView;
            if (calc != null)
            {
                this.listView.test(calc.Docenten.DocentenList);
            }
        }

        public void setCalc(RatingCalculator calc)
        {
            this.calc = calc;
        }

        public void setImporter(RatingGegevensImporter importer)
        {
            this.importer = importer;
        }

    }
}
