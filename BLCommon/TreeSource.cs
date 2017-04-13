using DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace BLCommon
{
  public  class TreeSource
    {
      DataBaseContext dbcontext = new DataBaseContext();

      public XmlDocument GetRoleTreeSource()
      {
          string initTreeCommand = "select * into #tamp from ( SELECT TreeNavID,[NodeID] ,NodeText as Text,[ParentID],[EnablePostBack],[CommandArgument],[SingleClickExpand] ,[Rank] ,[TabText] FROM  TreeNavs )aa SELECT NodeID, A.Text,EnablePostBack,CommandArgument,SingleClickExpand, (select NodeID,B.Text ,EnablePostBack,CommandArgument,SingleClickExpand, (select NodeID,C.Text,EnablePostBack,CommandArgument,SingleClickExpand from #temp C where parentID = B.TreeNavID FOR XML RAW('TreeNode'), TYPE) from #tamp B where parentID = A.TreeNavID FOR XML RAW('TreeNode'), TYPE) FROM #tamp A where parentID =0 FOR XML RAW('TreeNode'), TYPE,ROOT('Tree') drop table #tamp";     
          var xmlstring = dbcontext.Database.SqlQuery<string>(initTreeCommand).FirstOrDefault();
          if (xmlstring != null)
          {
              XmlDocument doc = new XmlDocument();
              doc.LoadXml(xmlstring);
              return doc;
          }
          return null;
      }

      public XmlDocument GetCheckTreeSource()
      {
          string initTreeCommand= "select * into #tamp from (SELECT TreeNavID,[NodeID] ,[NodeText] as Text,[ParentID],[EnablePostBack],[CommandArgument],[SingleClickExpand] ,[Rank] ,[TabText] ,'True' as Checked FROM  TreeNavs)aa SELECT NodeID,A.Text,'True' as AutoPostBack,'True' as EnablePostBack,Checked,'True' as Expanded, 'True' as EnableCheckBox, (select NodeID,B.Text,'True' as AutoPostBack,'True' as EnablePostBack,Checked, 'True' as EnableCheckBox, (select NodeID,C.Text,'True' as AutoPostBack,'True' as EnablePostBack,Checked, 'True' as EnableCheckBox from #tamp C where parentID = B.TreeNavID FOR XML RAW('TreeNode'), TYPE) from #temp B where parentID = A.TreeNavID FOR XML RAW('TreeNode'), TYPE) FROM #tamp A where parentID =0 FOR XML RAW('TreeNode'), TYPE,ROOT('Tree')drop table #tamp";

          var xmlstring = dbcontext.Database.SqlQuery<string>(initTreeCommand).FirstOrDefault();
          if (xmlstring != null)
          {
              XmlDocument doc = new XmlDocument();
              doc.LoadXml(xmlstring);
              return doc;
          }
          return null;
      }


      public XmlDocument GetRoleTreeSource(int parentID)
      {
          string initTreeCommand = "select * into #tamp from ( SELECT TreeNavID,[NodeID] ,NodeText as Text,[ParentID],[EnablePostBack],[CommandArgument],[SingleClickExpand] ,[Rank] ,[TabText] FROM  TreeNavs )aa SELECT NodeID, A.Text,EnablePostBack,CommandArgument,SingleClickExpand, (select NodeID,B.Text ,EnablePostBack,CommandArgument,SingleClickExpand, (select NodeID,C.Text,EnablePostBack,CommandArgument,SingleClickExpand from #tamp C where parentID = B.TreeNavID FOR XML RAW('TreeNode'), TYPE) from #tamp B where parentID = A.TreeNavID FOR XML RAW('TreeNode'), TYPE) FROM #tamp A where parentID ="+parentID+" FOR XML RAW('TreeNode'), TYPE,ROOT('Tree') drop table #tamp";
          var xmlstring = dbcontext.Database.SqlQuery<string>(initTreeCommand).FirstOrDefault();
          if (xmlstring != null)
          {
              XmlDocument doc = new XmlDocument();
              doc.LoadXml(xmlstring);
              return doc;
          }
          return null;
      }

     

    }
}
