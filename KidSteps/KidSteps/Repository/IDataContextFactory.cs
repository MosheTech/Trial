using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KidSteps.Repository
{
    public interface IDataContextFactory
    {
        System.Data.Linq.DataContext Context { get; }
        void SaveAll();
    }
}