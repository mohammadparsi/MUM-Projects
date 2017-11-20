using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcEshop.Infrastructure
{
    public static class MyStaticClass
    {
        private static MvcEshop.Models.DFEntities _context;

        static MyStaticClass()
        {
            _context = new MvcEshop.Models.DFEntities();
        }

        private static void UpdateContext()
        {
            _context = new MvcEshop.Models.DFEntities();
        }

        public static bool DoSomething(int id)
    {
        using (var context = _context)
        {
            var result = (from x in context.table.where(p=>p.id == id) select x).FirstOrDefault();
        }
    }
    }
}