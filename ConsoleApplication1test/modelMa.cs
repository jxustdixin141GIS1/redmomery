using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using redmomery.DAL;
using redmomery.Model;
using System.Net;
using System.Data.Spatial;
using System.Data.SqlTypes;
using PanGu;
using PanGu.Framework;
using PanGu.Dict;
using PanGu.HighLight;
using PanGu.Match;
using PanGu.Setting;
namespace ConsoleApplication1test
{
   public  class modelMa
    {
       //为了处理多线程进行线程之间的使用说明
       int index = 0;
       class Lbexpre
       {
         public string ID;
         public string text;
       }
       List<redmomery.Model.LB_INFO> lblist = new List<LB_INFO>();
       List<Lbexpre> LB_library = new List<Lbexpre>();
       List<WordInfo> wordinfos = new List<WordInfo>();
       public void modelmain()
       {
           premanage();
       }
       //下面开始进行数据的分析，所有的程序全部都是private ，进行程序封装
         //--------------------开始进行分词统计-------------------
       void ceshi()
       { 
        //这个方法用来进行数据的统计。用来对于词语进行分词处理，并将其中的地点名词提取出来，进行统计。
           //并将其中时间词和前面的词语提取出来，进行数据的二次判别,用来考虑是否需要进行时间词典的生成。
       }

          ///------------------数据提取代码-----------------------
       void premanage()
       {
           LB_INFODAL dal = new LB_INFODAL();
           lblist = dal.ListAll() as List<redmomery.Model.LB_INFO>;
           //开始进行多线程处理
           int theatcount = lblist.Count / 50;
           Thread[] ts = new Thread[theatcount + 1];
           for (int i = 0; i < ts.Length; i++)
           {
               ts[i] = new Thread(Extract);
               ts[i].Start();
           }
           while (LB_library.Count != lblist.Count)
           {
               Console.Write("\r线程程序执行中...");
           }
           Console.WriteLine("数据提取完成,开始提取数据");
           string result = "";
           for (int i = 0; i < LB_library.Count; i++)
           {
               result += LB_library[i].text + "\n\r";
           }
           redmomery.command.createlog.createlogs(result);
           Console.WriteLine("数据提取完成。");
       }
       void Extract()
       {
           int sign = index;
           index++;
           //开始针对临时文件进行处理
           int count = 0;
           List<Lbexpre> temp = new List<Lbexpre>();
           for (int i = sign * 50; i < lblist.Count && count < 50; i++)
           {
               Lbexpre lb = new Lbexpre();
               lb.ID = lblist[i].ID.ToString().Trim();
               lb.text = lblist[i].LBexperience.ToString().Trim();
               temp.Add(lb);
               count++;
           }
           bool isok = false;
           while(!isok)
           { 
             Monitor.Enter(LB_library);
              try
              {
               LB_library.AddRange(temp);
               isok = true;
              }
               finally
              {
               Monitor.Exit(LB_library);
               }  
           }
           Console.WriteLine(LB_library.Count.ToString());
       }
      
    }
}
