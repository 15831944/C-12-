using Common.Entities;
using DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLHelper
{
    public class BLLTree
    {
        DataBaseContext dbcontext = new DataBaseContext();
        //NavContext navcontext = new NavContext();
        public int GetTreeNavID(string NodeID)
        {
            return dbcontext.TreeNavContext.Where(tr => tr.NodeID == NodeID).FirstOrDefault().TreeNavID;
        }

        public List<ToolBar> GetToolBarsByTree(int treeNavID)
        {
            return dbcontext.TreeNavContext.Find(treeNavID).ToolBars.ToList();
        }

        public List<TreeNav> GetTops()
        {
            //return dbcontext.RoleContext.Find(roleID).TreeNavs.Where(tn => tn.ParentID == 0).ToList();

            return dbcontext.TreeNavContext.Where(tn => tn.ParentID == 0).ToList();
        }
        public int GetTopsCount()
        {
            try
            {
                //return dbcontext.RoleContext.Find(roleID).TreeNavs.Where(tn => tn.ParentID == 0).Count();
                return dbcontext.TreeNavContext.Where(tn => tn.ParentID == 0).Count();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
