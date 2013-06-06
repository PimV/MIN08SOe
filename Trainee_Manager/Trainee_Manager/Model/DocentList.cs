using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trainee_Manager.Model
{
    public class DocentList
    {
        private List<Docent> docentenList;

        public List<Docent> DocentenList
        {
            get { return docentenList; }
            set { docentenList = value; }
        }
        private List<Docent> sortedDocentList;

        public  List<Docent> SortedDocentList
        {
            get { return sortedDocentList; }
            set { sortedDocentList = value; }
        }

         public DocentList()
         {
             docentenList = new List<Docent>();
         }

        public void SortList()
        {
            sortedDocentList = docentenList.OrderBy(o => o.Rating).ToList();
        }

    }
}
