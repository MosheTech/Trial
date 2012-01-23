using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KidSteps.Repository
{
    public class RelationshipRepository : Repository<Models.Relationship>
    {
        
        public RelationshipRepository(KidStepsContextFactory factory)
            : base(factory)
        {
        }

        public IEnumerable<Models.Relationship> GetRelationshipsOf(Models.Family family, Models.Member member)
        {
            return
                from rel in _dataContextFactory.Context.Relationships
                where rel.Family.Equals(family) && rel.Member.Equals(member)
                select rel;
        }

        public void Add(Models.Family family, Models.Member member, Models.RelationshipType isA, Models.Member of)
        {
            family.Relationships.Add(
                new Models.Relationship() 
                { 
                    Member = member, 
                    RelationshipType = (int)isA, 
                    Member1 = of 
                });
        }
    }
}