using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KidSteps.Repository
{
    public class MemberRepository : Repository<Models.Member>
    {
        public IEnumerable<Models.Member> GetByFamily(Models.Family family)
        {
            return
                family.MemberFamilies.Select(rel => rel.Member);
        }

        public IEnumerable<Models.Relationship> GetRelationshipsOf(Models.Member member)
        {
            return _dataContextFactory.Context.Relationships.Where(
                r => member.Equals(r.Member));
        }
    }
}