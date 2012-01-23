using System.Data.Linq;
namespace KidSteps.Repository
{
    public class KidStepsContextFactory : IDataContextFactory
    {
        public KidStepsContextFactory()
        {
            _context = new Models.KidStepsDataContext();
        }

        public Models.KidStepsDataContext Context
        {
            get { return _context; }
        }

        public void SaveAll()
        {
            _context.SubmitChanges();
        }

        private Models.KidStepsDataContext _context;
    }
}