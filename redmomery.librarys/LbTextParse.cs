using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLRedmomery;
using redmomery.Model;
using redmomery.Common;
using redmomery.command;
//本模型只适合与顺序记叙文，并且因为采用的是百度的地理编码，所以对于外文的识别不怎么样。
namespace redmomery.librarys
{
    public class LbTextParse
    {
        public static List<trajectory> parseLbstored(LB_INFO lb)
        {
            return parseLbstored(lb.ID,lb.LBexperience);
        }
        public static List<trajectory> parseLbstored(int lbID, string lbtext)
        {

            List<trajectory> list = new List<trajectory>();
            List<Text_result> temp1 = LBText.parsetext(lbtext);
            List<Text_result> initlist = LBText.mergeresult((List<Text_result>)temp1);
            List<Time_result> temp2 = LBText.ChangeCp(initlist);
            List<Time_result> temp3 = LBText.Removevilable((List<Time_result>)temp2);
            List<Time_result> show = temp3 as List<Time_result>;
            List<Time_result> show6 = LBText.ExtractTime(show); show = show6;
            List<Time_result> show4 = LBText.Removevilable(show); show = show4;
            List<Time_result> show5 = LBText.reckonTime(show); show = show5;
            List<T_LocalText> show7 = LBText.ExtractLocalName(show);
            List<Res_T_LocalText> show8 = LBText.ExtractContent(show7);
            List<Res_T_LocalText> show9 = LBText.mergeLocal(show8);
            List<Text_trcajectory> show10 = LBText.uniquelocal(show9);
            List<Text_trcajectory> temp = show10;
            for (int j = 0; j < temp.Count; j++)
            {
                trajectory newtra = new trajectory();
                newtra.LBID = lbID;
                newtra.Local = temp[j].address == null ? "null" : temp[j].address;
                string temps = temp[j].time.IndexOf("年") >= 0 ?
                temp[j].time.Replace("9999-12-30-", "").Replace("年", "-").Replace("月", "-").Replace("日", "-").ToString() :
                temp[j].time.Replace("年", "-").Replace("月", "-").Replace("日", "-").ToString();
                DateTime dt = new DateTime();
                try
                {
                    dt = DateTime.ParseExact(temps, "yyyy-MM-dd-", null);
                }
                catch
                {
                    try
                    {
                        dt = DateTime.ParseExact(temps, "yyyy-MM-", null);
                    }
                    catch
                    {
                        try
                        {
                            dt = DateTime.ParseExact(temps, "yyyy-", null);
                        }
                        catch
                        {
                            dt = DateTime.ParseExact("9999-12-30-", "yyyy-MM-dd-", null);
                        }
                    }
                }
                newtra.T_time = dt;
                newtra.Timetext = temp[j].time;
                newtra.x = temp[j].xy.lng.ToString();
                newtra.y = temp[j].xy.lat.ToString();
                newtra.isCurrent = temp[j].iscurent;
                newtra.context = temp[j].context;
                list.Add(newtra);
            }
            return list;
        }
    }
    public class LBText
    {
        //常用变量
        private const string NLpath = @"D:\题库系统\github\team\redmomery\NLRedmomery\bin\Debug\";
        public const string userDirs = NLpath + @"\output\userC.txt";
        public static List<Text_trcajectory> parseText(string text)
        {
            List<Text_trcajectory> result = new List<Text_trcajectory>();
            object temp = LBText.parsetext(text);
            temp = LBText.mergeresult((List<Text_result>)temp);
       
            return result;
        }
        //数据预处理
        public static List<string> ReadNameForm(string path)
        {
            string s = redmomery.command.createlog.readTextFrompath(path);
            string[] slist = s.Split(',','，');
            List<string> result = new List<string>();
            result.AddRange(slist);
            return result;
        }
        public static List<Text_result> parsetext(string text)
        {
            List<string> bookname = Extractbookname(text);
            List<Text_result> resl = new List<Text_result>();
            Text_result[] results = null;
            NLPIR_ICTCLAS_C nlpir = new NLPIR_ICTCLAS_C();
            //这个主要是用来处理其中的书名
            for (int i = 0; i < bookname.Count; i++)
            {
                nlpir.AddUserWord(bookname[i] + "\t" + "n");
            }
            List<string> Exin = ReadNameForm(userDirs);
            for (int i = 0; i < Exin.Count; i++)
            {
                nlpir.AddUserWord(Exin[i] + "\t" + "n");
            }
            int count = nlpir.GetParagraphProcessAWordCount(text);
            result_t[] res = nlpir.ParagraphProcessAW(count);
            byte[] bytes = System.Text.Encoding.Default.GetBytes(text);
            //对于一些名词的提取
            
            //下面将对应的数据进行转换
            results = new Text_result[count];
            for (int i = 0; i < results.Length; i++)
            {
                results[i] = new Text_result();
                results[i].text = Encoding.Default.GetString(bytes, res[i].start, res[i].length);
                results[i].res = res[i];
            }
            resl.AddRange(results);
            return resl;
        }
        #region 规则  -----------------这几条规则是按照时间为主线进行编写的，因此这里更加侧重事件发生的时间，对于地点就进行了模糊化处理
        //规则一：当两个时间在一起时，可以认为同一个时间词
        public static List<Text_result> mergeresult(List<Text_result> temp)
        {
            List<Text_result> result = new List<Text_result>();
            //采用队列的方案
            for (int i = 0; i < temp.Count; i++)
            {
                Text_result ttemp = temp[i];
                //当读到为时间属性是，就去比较队列前一项
                if (temp[i].res.sPos == "t")
                {
                    if (result.Count > 0 && result[result.Count - 1].res.sPos == "t")
                    {
                        //取出当前队列的前一项
                        Text_result rtemp = result[result.Count - 1];
                        result.RemoveAt(result.Count - 1);
                        rtemp.text += ttemp.text;//将两个内容一起比较
                        result.Add(rtemp);
                        continue;
                    }
                }
                result.Add(ttemp);
            }
            return result;
        }
        //规则二：提取书名
        public static List<string> Extractbookname(string s1)
        {
            List<string> bookname = new List<string>();
            for (int i = 0; i < s1.Length; i++)
            {
                if (s1[i].ToString() == "》")
                {
                    int starts = s1.LastIndexOf("《", i);
                    if (starts >= 0)
                    {
                        StringBuilder st = new StringBuilder();
                        st.Append(s1.Substring(starts, i - starts + 1));
                        bookname.Add(st.ToString());
                    }
                }

            }
            return bookname;

        }
        //规则三：一般一句话的开始，就是"" 或者 "\n" “\r” 。也就是说当遇到 \n \r 的时候，可以直接进行提取这一段
        public static List<Time_result> ChangeCp(List<Text_result> initlist)
        {
            List<Time_result> result = new List<Time_result>();
            
            List<Text_result> line = new List<Text_result>();//临时队列，用来进行提取数据
            for (int i = 0; i < initlist.Count; i++)
            {
                Text_result temp = initlist[i];
                if (temp.text == "\n" || temp.text == "\r")//当遇到着几种情况时直接就将放在列表中的数据提取出来
                { 
                  //这就情况下就需要进行对于处于列表中的时间词检索出来  
                    line.Add(temp);
                    Time_result newtime = new Time_result();
                    for (int j = 0; j < line.Count; j++)
                    {
                        if (line[j].res.sPos == "t")
                        {
                            newtime.time = line[j];//这里假设第一个时间就是我们需要的，起码可以满足大部分的提取需求,这里我就不想还有该率判断
                            break;
                        }
                    }
                    newtime.timelist = line;
                    line = new List<Text_result>();
                    result.Add(newtime);
                    continue;
                }
                if (temp.res.sPos == "t") //就将对对应的时间提取出来，直接将处于列表中，最近的一个 。 为根基进行提取
                {
                    //这里需要注意，只能提取最近的一个。 时间段
                    Time_result newtime = new Time_result();
                    for (int j = line.Count-1; j >=0 ; j--)
                    {
                        if (line[j].text == "。")//表示找到对应的时间词语，开始进行提取
                        {
                            List<Text_result> linetemp = line.GetRange(0,j+1);
                            for (int t = 0; t < linetemp.Count; t++)
                            {
                                if (linetemp[t].res.sPos == "t")
                                {
                                    newtime.time = linetemp[t];
                                    break;
                                }
                            }
                            newtime.timelist = linetemp;
                           //开始进行数据的处理提交
                            line.RemoveRange(0,j+1);
                            break;
                        }
                    }
                    //将当前的时间词语添加列表中,为下词提取做好准备
                    line.Add(temp);
                    result.Add(newtime);
                    continue;
                }

                if (i == initlist.Count - 1)//当处于结尾的时候，无论就将列表中的内容全部提取出来
                {
                    line.Add(initlist[i]);//将最后的一个元素加入列表中
                    Time_result newtime = new Time_result();
                    for (int j = 0; j < line.Count; j++)
                    {
                        if(line[j].res.sPos=="t")
                        {
                            newtime.time = line[j];//这里假设第一个时间就是我们需要的，起码可以满足大部分的提取需求
                            break;
                        }
                    }
                    newtime.timelist = line;
                    result.Add(newtime);
                }
                line.Add(temp);
                continue;
            }

            return result;
        }
        //规则四:对于在记录中，没有对应的词语记录，需要进行删除
        public static List<Time_result> Removevilable(List<Time_result> temp)
        {
            List<Time_result> result = new List<Time_result>();
            for (int i = 0; i < temp.Count; i++)
            {
                if (temp[i].timelist.Count > 0)
                {
                    result.Add(temp[i]);
                }
            }
            return result;
        }
        //规则五：对于时间记录中，没有年，只有月份的情况或者日期的情况，就将上一条记录中的日期顺延到此日期节点上
        public static List<Time_result> reckonTime(List<Time_result> temp)
        {
            List<Time_result> result = new List<Time_result>();
            for (int i = 0; i < temp.Count; i++)
            {
                Time_result ttemp = temp[i];
                if (i == 0)//后面可能还有对应这条判断，那个只不过是为了预防产生错误异常，进行必要的处理，可以不予理会
                {   //注意这里，只是为了预防第一条记录没有时间，就没有了推算的起始点，所以这里注意，只是给了一个时间，并没有修改原文中的内容
                    if (ttemp.time == null || ttemp.time.text == null || ttemp.time.text == "")
                    {
                        //如果这一条记录中没有时间对象，就给其附上时间，将后一条记录的时间，赋值给他,
                        for (int j = 0; j < temp.Count; j++)
                        {
                            if (temp[j].time != null && temp[j].time.text != null && temp[j].time.text != "")
                            {
                                ttemp.time = temp[j].time;
                            }
                        }
                    }
                    result.Add(ttemp);
                    continue;
                }
                //首先判断有没有年份
                int y = ttemp.time.text.IndexOf("年")>=0?100:0;
                int m = ttemp.time.text.IndexOf("月")>=0?10:0;
                int d = ttemp.time.text.IndexOf("日") >= 0 ? 1 : 0;
                int ymd = y + m + d;
               //若是第一条数据没有时间，就以往后第一条时间作为处理
                switch (ymd)
                {
                    case 111://这种情况就是三者皆有，就不要处理，直接进栈
                        result.Add(ttemp);
                        continue;
                    case 110: //这种情况表明也是有时间的，也是直接进栈
                        result.Add(ttemp);
                        continue;
                    case 101://这种情况并他有年 和日期 (这种东西的出现，简直反人类，也不要处理，直接就扔进列表中，将其中的日期删除)
                        int yy = ttemp.time.text.IndexOf("年");
                        ttemp.time.text = ttemp.time.text.Substring(0,yy+1);
                        result.Add(ttemp);
                        continue;
                    case 100://表明这个是只有年份，不予处理
                        result.Add(ttemp);
                        continue;
                    case 11: //这种表明没有年份，需要进行处理，将前一条记录中的时间进行提取年份
                         //如果是第二条记录，也不要处理
                        if (result.Count == 0)
                        {
                            result.Add(ttemp);
                            continue;
                        }
                        string yt = result[result.Count - 1].time.text.Substring(0,result[result.Count-1].time.text.IndexOf("年")+1);
                        ttemp.time.text=ttemp.time.text.Insert(0,yt);
                        //这里还要注意，需要将插入的词语，放到时间序列中
                        Text_result ytt = new Text_result();
                        ytt.text = yt;
                        ytt.res.sPos = "t";
                        ttemp.timelist.Insert(0,ytt);
                        result.Add(ttemp);
                        continue;
                    case 10://这种情况表明只有月份，没有年份，处理同上
                         if (result.Count == 0)
                        {
                            result.Add(ttemp);
                            continue;
                        }
                        string yt1 = result[result.Count - 1].time.text.Substring(0,result[result.Count-1].time.text.IndexOf("年")+1);
                        ttemp.time.text=ttemp.time.text.Insert(0,yt1);
                        //这里还要注意，需要将插入的词语，放到时间序列中
                        Text_result ytt1 = new Text_result();
                        ytt1.text = yt1;
                        ytt1.res.sPos = "t";
                        ttemp.timelist.Insert(0,ytt1);
                        result.Add(ttemp);
                        continue;
                    case 1://这种情况，表示只有日期没有年月，这里，也进行处理，需要更具前一条数据进行处理，若是前一条数据没有
                           if (result.Count == 0)
                        {
                            result.Add(ttemp);
                            continue;
                        }
                           if (result[result.Count - 1].time.text.IndexOf("月") < 0)
                           { 
                             //这里就直接进行合并吧，
                               if (i == 0)
                               {
                                   result.Add(ttemp);
                                   continue;
                               }
                               else//如果不是第一条记录这里就需要处理
                               {
                                   result[result.Count - 1].timelist.AddRange(ttemp.timelist);//当这一条记录的时间添加到末尾
                                   continue;
                               }
                             
                           }
                        string yt2 = result[result.Count - 1].time.text.Substring(0,result[result.Count-1].time.text.IndexOf("月")+1);
                        ttemp.time.text=ttemp.time.text.Insert(0,yt2);
                        //这里还要注意，需要将插入的词语，放到时间序列中
                        Text_result ytt2 = new Text_result();
                        ytt2.text = yt2;
                        ytt2.res.sPos = "t";
                        ttemp.timelist.Insert(0,ytt2);
                        result.Add(ttemp);
                        continue;

                    default:
                        {
                           //若是三者都没有，那就只能进行合并到上一条数据中
                            if (i == 0)
                            {
                                result.Add(ttemp);
                                continue;
                            }
                            else//如果不是第一条记录这里就需要处理
                            {
                                result[result.Count - 1].timelist.AddRange(ttemp.timelist);//当这一条记录的时间添加到末尾
                                continue;
                            }
                         
                        }
                }
               
                
            }
            return result;
        }
        //规则六：对于没有具体的时间，比如，一些特殊的时间词 比如：会后，今后之类的，需要将它并到上一条记录中（这条规则在第规则四之前使用）
        public static List<Time_result> ExtractTime(List<Time_result> temp)
        {
            //这里同样采用队列的方式进行处理比较简单和方便
            List<Time_result> result = new List<Time_result>();
            for (int i = 0; i < temp.Count; i++)
            {
                Time_result ttemp = temp[i];

                if (ttemp.time == null || ttemp.time.text==null||ttemp.time.text == "")
                { 
                  //这种情况表示其需要时间的限制,直接将其并到上一条记录中
                    if (i == 0)
                    {
                        result.Add(ttemp);
                       
                        continue;
                    }
                    else//如果不是第一条记录这里就需要处理
                    {
                        result[result.Count - 1].timelist.AddRange(ttemp.timelist);//当这一条记录的时间添加到末尾
                       
                        continue;
                    }
                }
                if (ttemp.time.text.IndexOf("年") >= 0 || ttemp.time.text.IndexOf("月") >= 0 || ttemp.time.text.IndexOf("日") >= 0)
                {
                    //三者任有一，不予处理直接添加队列中
                        result.Add(ttemp);
                       
                        continue;
                }
                if (ttemp.time.text.IndexOf("年") < 0 && ttemp.time.text.IndexOf("月") < 0 && ttemp.time.text.IndexOf("日") < 0)
                { 
                   //这三个时间都没有,需要处理，处理方法，直接将其和上一一条记录进行合并
                    //如果是第一条记录，就不要处理(虽然概率很小，但是这个是属于极大的问题处理)
                    if (i == 0)
                    {
                        result.Add(ttemp);
                     
                        continue;
                    }
                    else//如果不是第一条记录这里就需要处理
                    {
                        result[result.Count - 1].timelist.AddRange(ttemp.timelist);//当这一条记录的时间添加到末尾
                      
                        continue;
                    }
                }
            }
            return result;
        }
        //规则七：对于其中地名开始进行提取,遍历地名,但是不对地名进行处理
        public static List<T_LocalText>  ExtractLocalName(List<Time_result> temp)
        {
            List<T_LocalText> result = new List<T_LocalText>();
            for (int i = 0; i < temp.Count; i++)
            {
                T_LocalText ltemp = new T_LocalText();
                Time_result ttemp = temp[i];
                ltemp.Time = ttemp.time;
                ltemp.iscurrent = 2;
                ltemp.res = ttemp.timelist;
                for (int jq = 0; jq < ttemp.timelist.Count; jq++)
                {
                    if (ttemp.timelist[jq].res.sPos == "ns" || ttemp.timelist[jq].res.sPos == "nsf")
                    {
                        ltemp.local.Add(ttemp.timelist[jq]);
                    }
                }
                result.Add(ltemp);
            }
            return result;
        }
        //规则八：对于这一步开始将记录中的分词元祖转化为字符串，
        public static List<Res_T_LocalText> ExtractContent(List<T_LocalText> temp)
        {
            List<Res_T_LocalText> result = new List<Res_T_LocalText>();
            for (int i = 0; i < temp.Count; i++)
            {
                T_LocalText ttemp = temp[i];
                Res_T_LocalText restemp = new Res_T_LocalText();
                restemp.iscurrent = 5;
                restemp.time = ttemp.Time.text;
                //开始将地名提取和赋值
                for (int j = 0; j < temp[i].local.Count; j++)
                {
                    if (temp[i].local[j] != null && temp[i].local[j].text != null )
                    {
                        restemp.local.Add(temp[i].local[j].text);
                    }
                }
                //下面开始进行内容的整合
                restemp.context=string.Empty;
                for (int j = 0; j < temp[i].res.Count; j++)
                {
                    restemp.context += temp[i].res[j].text;
                }
                result.Add(restemp);
            }
            return result;
        }
       //规则九:这一规则是基于一个建设前提建立的：即，当用户没有明确的指出地点的变更的时候，就意味着对象没有进行地点位移，所以这条规则，就是将所有的地点为空的记录，全部并到前一条记录中
        public static List<Res_T_LocalText> mergeLocal(List<Res_T_LocalText> temp)
        {
            List<Res_T_LocalText> result = new List<Res_T_LocalText>();
            for (int i = 0; i < temp.Count; i++)
            {
                Res_T_LocalText ttemp=temp[i];
                
                if (temp[i].local.Count == 0)
                {
                    if (i == 0)//如果这是第一条数据，这里需要特殊处理
                    { 
                      //首先以后面的第一条地名数据作为本记录到地名
                        for (int j = 0; j < temp.Count; j++)
                        {
                            if (temp[i].local.Count > 0)
                            {
                                ttemp.local = temp[i].local;
                            }
                        }
                        result.Add(ttemp);
                        continue; 
                    }

                  //表示这个里面没有地名数据，那将这个对应的数据合并到前一条数据上
                    result[result.Count - 1].context += ttemp.context;
                    continue;
                }
                result.Add(ttemp);
            }
                return result;
        }
        //规则十：目的是为了保证的地名的唯一性，注意这一步，需要云平台的地理编码进行配合，所以运算速度可能会变慢。
        public static List<Text_trcajectory> uniquelocal(List<Res_T_LocalText> temp)
        {
            List<Text_trcajectory> result = new List<Text_trcajectory>();
          //小规则1：地名等级比较小为准，对于一些特殊的地名，比如日本，法国，巴黎，这里由于是国内，所以无法识别
            for (int i = 0; i < temp.Count; i++)
            {
                Text_trcajectory restemp = new Text_trcajectory();
                restemp.time=temp[i].time;
                restemp.context = temp[i].context;
                Res_t_locals res = new Res_t_locals();
                for (int j = 0; j < temp[i].local.Count; j++)
                {
                    baiduGeocodingaddress obj = redmomery.command.Geocodingcommand.getGeocodingByAddressobject(temp[i].local[j]);
                    if (obj.status != 0&&obj.result==null)
                    {
                        restemp.iscurent = 6;//极度危险
                    }
                    
                    if (j == 0)
                    {
                        res.addressname = temp[i].local[j];
                        res.local = obj;//这里若是为空，不用管
                        continue;
                    }
                    //如果不是第一条数据，就需要进行比较
                    if (res.local != null && res.local.result != null&&res.local.result.level!=null)
                    {
                        if (obj.result != null)
                        {
                            if (levelscore(res.local) < levelscore(obj))
                            {
                                res.addressname = temp[i].local[j];
                                res.local = obj;
                                restemp.iscurent = 4;
                            }
                        }
                    }

                }
                restemp.address = res.addressname;
                restemp.xy = res.local.result == null ? null : res.local.result.location == null ? null : res.local.result.location;
                result.Add(restemp);
            }
            return result;
        }
        #endregion 
        public static double levelscore(baiduGeocodingaddress ba)
        {
            double mitemp = 0;
            switch (ba.result.level)
            {
                case "国家": mitemp = 0.2; break;
                case "省": mitemp = 0.4; break;
                case "城市": mitemp = 0.6; break;
                case "区县": mitemp = 0.8; break;
                case "村庄": mitemp = 1; break;
                default: mitemp = 0; break;
            }
            return mitemp;
        }
        public static float distancestatic(baiduGeocodingaddress ba, baiducoordinate Gcenter)
        {
            float distance = float.MaxValue;
            distance = (ba.result.location.lng - Gcenter.lng) * (ba.result.location.lng - Gcenter.lng) + (ba.result.location.lat - Gcenter.lat) * (ba.result.location.lat - Gcenter.lat);
            return distance;
        }

        //调试用过得方法
        public static void outmid(List<Text_result> temp)
        {
            for (int i = 0; i < temp.Count; i++)
            {
                Console.Write(temp[i].text);
            }
            Console.WriteLine();
            Console.WriteLine();
        }
    }

}



namespace redmomery.librarys
{
    public class Text_trcajectory
    {
        public string time;
        public string address;
        public baiducoordinate xy = new baiducoordinate();
        public string context;
        public int iscurent;
    }
}
namespace redmomery.librarys
{
    public class Time_result
    {
        public Text_result time = new Text_result();//若为null开头非时间词
        public List<Text_result> timelist = new List<Text_result>();//表示表示这个时间段，所对应的时间词切分结果
    }
    public class T_LocalText//提取时间，和地点词  第二次处理
    {
        public Text_result Time;//表示时间
        public List<Text_result> local = new List<Text_result>();//表示地点
        public List<Text_result> res = new List<Text_result>();
        public int iscurrent = 0;
    }
    public class Res_T_LocalText //提取时间和内容 去除对应对的分词属性之后结果 第三次处理
    {
        public string time;//时间
        public List<string> local = new List<string>();//地点 
        public string context;//内容
        public int iscurrent = 0;
    }
    public class Res_t_localtext //给地点 进行坐标处理之后的第四次处理
    {
        public string Time;
        public List<Res_t_locals> locals = new List<Res_t_locals>();
        public string context;
        public int iscurrent = 0;
    }
    public class Res_t_locals //地点赋值
    {
        public string addressname;
        public baiduGeocodingaddress local;
    }
    public class Text_result//分词第一次处理
    {
        public string text;
        public result_t res;
    }

}