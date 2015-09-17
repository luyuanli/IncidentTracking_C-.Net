using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

// Added by MF
using LL_Lab6.Models;

namespace LL_Lab6.ViewModels
{
    public class RepositoryBase
    {
        public RepositoryBase()
        {
            dc = new MyDbContext();
            dc.Configuration.ProxyCreationEnabled = false;
            dc.Configuration.LazyLoadingEnabled = false;
        }

        protected MyDbContext dc;
    }
}