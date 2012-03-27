using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KidSteps.Models
{
    public class IdRoute
    {
        private IdRoute(int id)
        {
            this.id = id;
        }

        public static IdRoute Create(int id)
        {
            IdRoute retVal = new IdRoute(id);
            return retVal;
        }

        public static IdRoute Create(object id)
        {
            string idString = id.ToString();
            int intId = int.Parse(idString);
            return new IdRoute(intId);
        }

        public readonly int id;
    }
}