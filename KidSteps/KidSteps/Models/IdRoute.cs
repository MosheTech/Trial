using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KidSteps.Models
{
    public class IdRoute
    {
        private IdRoute(string id)
        {
            this.id = id;
        }

        //public static IdRoute Create(int id)
        //{
        //    IdRoute retVal = new IdRoute(id.ToString());
        //    return retVal;
        //}

        public static IdRoute Create(object id)
        {
            return new IdRoute(id.ToString());
        }

        public readonly string id;
    }
}