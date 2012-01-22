using System.Data.Linq;
namespace KidSteps.Repository
{
    class KidStepsContextFactory : IDataContextFactory
    {


        public DataContext Context
        {
            get { return new Models.KidStepsDataContext(); }
        }

        public void SaveAll()
        {
            throw new System.NotImplementedException();
        }
    }
}