using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KidSteps.Repository
{
    public class FamilyRepository : Repository<Models.Family>
    {
        public FamilyRepository(KidStepsContextFactory factory)
            : base(factory)
        {
        }
    }
}