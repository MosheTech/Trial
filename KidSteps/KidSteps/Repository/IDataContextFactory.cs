using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KidSteps.Repository
{
    public interface IDataContextFactory
    {
        Models.KidStepsDataContext Context { get; }
        void SaveAll();
    }
}