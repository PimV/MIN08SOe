﻿using System;
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

        public RatingCalculator Calc
        {
            get { return calc; }
            set { calc = value; }
        }
        private InstructorRatingList listView;
        private DocentList docList;


        public RatingController() 
        {
           // this.listView = listView;
        }

        public void KoppelDocent(int docID, int stageID, int newTimeAvailable)
        {
            
            DatabaseConnection.commandEdit("CALL procedure_koppel_docent()");
        }

        public void setList(InstructorRatingList listView)
        {
            this.listView = listView;
            if (Calc != null)
            {
                this.listView.test(Calc.Docenten.DocentenList);
            }
        }

        public void setCalc(RatingCalculator calc)
        {
            this.Calc = calc;
            Calc.Docenten.SortList();
        }

        public void setImporter(RatingGegevensImporter importer)
        {
            this.importer = importer;
        }

    }
}
