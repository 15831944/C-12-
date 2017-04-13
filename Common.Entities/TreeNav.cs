using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Entities
{
   public  class TreeNav
   {
       public int TreeNavID { get; set; }
       public string NodeID { get; set; }
       public string NodeText { get; set; }
       public int ParentID { get; set; }
       public string EnablePostBack { get; set; }
       public string CommandArgument { get; set; }
       public string SingleClickExpand { get; set; }
       public int Rank { get; set; }
       public string TabText { get; set; }     
       public virtual ICollection<ToolBar> ToolBars { get; set; }

    }
}
