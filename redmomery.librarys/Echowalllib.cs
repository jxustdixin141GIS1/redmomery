using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using redmomery.Model;
using redmomery.DAL;
using NLRedmomery;
using System.Data.Spatial;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Types;
namespace redmomery.librarys
{
     public   class Echowalllib
    {
         public static Echowall Addechowall(string text)
         {
             Echowall newechowall = new Echowall();
             newechowall.text = text;
             newechowall.FTime = DateTime.Now;
             int id = (new EchowallDAL()).AddNew(newechowall);
             Echowall result = (new EchowallDAL()).Get(id);
             return result;
         }
         public static List<Echowall> getAllEchowall()
         {
             List<Echowall> result = (new EchowallDAL()).ListAll() as List<Echowall>;
             return result;
         }
    }
    

}
