using Common.Entities;
using DataBase;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLHelper
{
    public  class BLLFundingSet
    {
        DataBaseContext dbcontext = new DataBaseContext();
       
        //更新比例
        public bool Update(string projectnature, string proname, string proportion)
        {
            try
            {
                FundProportionSet newfundset = FindFundset(projectnature, proname);
                newfundset.Proportion = proportion;
                dbcontext.SaveChanges();
                return true;
            }
            catch (System.Data.SqlClient.SqlException e)
            {
                throw e;
            }
        }
        
       //根据项目性质找比例
        public string FindProportion(string projectnature, string proname)
        {
            List<FundProportionSet> list = new List<FundProportionSet>();
            list = dbcontext.FundProportionSetContext.Where(a => a.ProjectNature == projectnature && a.ProportionName == proname).ToList();
            if (list.Count() != 0)
            {
                if (list.FirstOrDefault().Proportion  != null)
                {
                    return list.FirstOrDefault().Proportion.ToString ();
                }
                else
                {
                    return "" ;
                }
            }
            else
            {
                return "";
            }
        }

        //根据项目性质找比例
        public FundProportionSet FindFundset(string projectnature, string proname)
        {
            List<FundProportionSet> list = new List<FundProportionSet>();
            list = dbcontext.FundProportionSetContext.Where(a => a.ProjectNature == projectnature && a.ProportionName == proname).ToList();
            if (list.Count() != 0)
            {
                return list.FirstOrDefault();
            }
            else
            {
                return null;
            }
        }
    }
}
