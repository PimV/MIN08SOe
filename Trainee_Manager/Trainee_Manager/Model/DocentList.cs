using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trainee_Manager.Model
{
    class DocentList
    {
         List<Docent> DocentenList;
         List<Docent> SortedDocentList;

         public DocentList()
         {
             DocentenList = new List<Docent>();
         }

        public void SortList()
        {
            SortedDocentList = DocentenList.OrderBy(o => o.Rating).ToList();
        }

    }
}
