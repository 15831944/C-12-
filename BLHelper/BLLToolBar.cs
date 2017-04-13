using Common.Entities;
using DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLHelper
{
    public class BLLToolBar
    {
        DataBaseContext dbcontext = new DataBaseContext();

        public List<ToolBar> FindAll()
        {
            return dbcontext.ToolBarContext.ToList();
        }

        //public List<ToolBar> FindAllByRole(int roleID, string PageBH)
        //{
        //    //return dbcontext.RoleContext.Find(roleID).ToolBars.Where(tb => (tb.Isvisible == true && tb.WebBM == PageBH)).ToList();
        //}

        //public List<ToolBar> FindAllByRoleAndTree(int roleID, int TreeID)
        //{
        //   // return dbcontext.RoleContext.Find(roleID).ToolBars.Where(ut => ut.TreeNav.TreeNavID == TreeID).ToList();
        //    //return dbcontext.RoleContext.Find(roleID).ToolBars.Where(tb => (tb.Isvisible == true && tb.WebBM == PageBH)).ToList();
        //}

        //public List<ToolBar> FindAllByRole(int roleID)
        //{
        //    return dbcontext.RoleContext.Find(roleID).ToolBars.Where(tb => (tb.Isvisible == true)).ToList();
        //}

        public List<ToolBar> FindAll(int[] ToolBarIDArray)
        {
            return dbcontext.ToolBarContext.Where(tb => ToolBarIDArray.Contains(tb.ToolBarID)).ToList();
        }

        ////public void DeleteRoleToolBars(int roleID, List<ToolBar> tbs)
        ////{
        ////    for (int i = 0; i < tbs.Count; i++)
        ////    {
        ////        dbcontext.RoleContext.Find(roleID).ToolBars.Remove(tbs[i]);
        ////    }

        ////    dbcontext.SaveChanges();
        ////}

        //public void RoleToolBarsAdd(int roleID, List<ToolBar> tbs)
        //{
        //    for (int i = 0; i < tbs.Count; i++)
        //    {
        //        dbcontext.RoleContext.Find(roleID).ToolBars.Add(tbs[i]);
        //    }
        //    dbcontext.SaveChanges();
        //}
    }
}
