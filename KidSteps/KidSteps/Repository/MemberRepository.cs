using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KidSteps.Repository
{
    public class MemberRepository : Repository<Models.Member>
    {
        public MemberRepository(KidStepsContextFactory factory)
            : base(factory)
        {
        }

        public IEnumerable<Models.Member> GetByFamily(Models.Family family)
        {
            return
                family.MemberFamilies.Select(rel => rel.Member);
        }

        
    }
}