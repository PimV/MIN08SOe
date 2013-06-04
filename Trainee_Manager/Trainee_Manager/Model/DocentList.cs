using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trainee_Manager.Model
{
    public class DocentList
    {
         List<Docent> docentenList;
         List<Docent> sortedDocentList;

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
