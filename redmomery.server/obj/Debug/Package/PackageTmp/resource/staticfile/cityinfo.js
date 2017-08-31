/// <reference path="various.js" />
data = [{name:'威海市',value:2},{name:'临沂市',value:3},{name:'济宁市',value:1},{name:'青岛市',value:5},{name:'淄博市',value:2},{name:'枣庄市',value:2},{name:'聊城市',value:1},{name:'滨州市',value:1},{name:'烟台市',value:1},{name:'邯郸市',value:3},{name:'济南市',value:3},{name:'葫芦岛市',value:5},{name:'菏泽市',value:2},{name:'郑州市',value:1},{name:'泰安市',value:1},{name:'巴彦淖尔市',value:1},{name:'天水市',value:1},{name:'乌鲁木齐市',value:3},{name:'塔城地区',value:1},{name:'海东地区',value:1},{name:'玉树藏族自治州',value:1},{name:'乌海市',value:1},{name:'扬州市',value:1},{name:'抚顺市',value:4},{name:'沈阳市',value:16},{name:'鞍山市',value:3},{name:'盘锦市',value:1},{name:'哈尔滨市',value:3},{name:'鸡西市',value:1},{name:'辽阳市',value:1},{name:'丹东市',value:3},{name:'黑河市',value:1},{name:'大连市',value:1},{name:'齐齐哈尔市',value:1},{name:'本溪市',value:1},{name:'鹤岗市',value:1},{name:'铁岭市',value:3},{name:'锦州市',value:1},{name:'承德市',value:1},{name:'衡水市',value:1},{name:'石家庄市',value:3},{name:'保定市',value:4},{name:'秦皇岛市',value:1},{name:'沧州市',value:3},{name:'新余市',value:2},{name:'萍乡市',value:25},{name:'赣州市',value:13},{name:'上饶市',value:34},{name:'南昌市',value:13},{name:'吉安市',value:13},{name:'九江市',value:18},{name:'上海市',value:2},{name:'抚州市',value:13},{name:'宜春市',value:7},{name:'景德镇市',value:1},{name:'鹰潭市',value:2},{name:'福州市',value:20},{name:'漳州市',value:18},{name:'泉州市',value:8},{name:'三明市',value:4},{name:'龙岩市',value:13},{name:'莆田市',value:12},{name:'厦门市',value:7},{name:'宁德市',value:7},{name:'南平市',value:12},{name:'肇庆市',value:2},{name:'广州市',value:11},{name:'韶关市',value:3},{name:'云浮市',value:9},{name:'佛山市',value:2},{name:'咸阳市',value:1},{name:'梅州市',value:16},{name:'东莞市',value:1},{name:'茂名市',value:6},{name:'河源市',value:1},{name:'湛江市',value:1},{name:'江门市',value:1},{name:'深圳市',value:3},{name:'潮州市',value:9},{name:'汕头市',value:21},{name:'揭阳市',value:3},{name:'阳江市',value:2},{name:'惠州市',value:3},{name:'大理白族自治州',value:44},{name:'德宏傣族景颇族自治州',value:15},{name:'保山市',value:31},{name:'楚雄彝族自治州',value:5},{name:'玉溪市',value:1},{name:'昆明市',value:3},{name:'荆州市',value:17},{name:'随州市',value:2},{name:'十堰市',value:8},{name:'宜昌市',value:41},{name:'襄阳市',value:8},{name:'黄石市',value:2},{name:'武汉市',value:10},{name:'仙桃市',value:4},{name:'荆门市',value:5},{name:'潜江市',value:2},{name:'黄冈市',value:1},{name:'恩施土家族苗族自治州',value:1},{name:'合肥市',value:8},{name:'阜阳市',value:57},{name:'六安市',value:3},{name:'亳州市',value:7},{name:'宣城市',value:9},{name:'淮南市',value:3},{name:'安庆市',value:6},{name:'芜湖市',value:1},{name:'蚌埠市',value:1},{name:'孝感市',value:1},{name:'金华市',value:14},{name:'马鞍山市',value:1},{name:'黄南藏族自治州',value:1},{name:'温州市',value:10},{name:'宁波市',value:15},{name:'台州市',value:19},{name:'绍兴市',value:15},{name:'杭州市',value:12},{name:'嘉兴市',value:5},{name:'衢州市',value:4},{name:'湖州市',value:4},{name:'舟山市',value:1},{name:'丽水市',value:2},{name:'梧州市',value:14},{name:'玉林市',value:12},{name:'贵港市',value:33},{name:'柳州市',value:4},{name:'南宁市',value:13},{name:'来宾市',value:12},{name:'北海市',value:1},{name:'百色市',value:1},{name:'桂林市',value:2},{name:'防城港市',value:1},{name:'钦州市',value:4},{name:'崇左市',value:1},{name:'贺州市',value:2},{name:'北京市',value:19},{name:'廊坊市',value:1},{name:'天津市',value:5},{name:'唐山市',value:1},{name:'徐州市',value:3}];
var geoCoordMap = {'威海市':[122.09395836580605,37.52878708125143],'临沂市':[118.34076823661057,35.07240907439113],'济宁市':[116.60079762482256,35.40212166433135],'青岛市':[120.38442818368189,36.10521490127382],'淄博市':[118.05913427786938,36.80468485421204],'枣庄市':[117.27930538329689,34.80788307838604],'聊城市':[115.98686913929108,36.455828514727979],'滨州市':[117.96829241452845,37.405313941825898],'烟台市':[121.30955503008511,37.53656156285964],'邯郸市':[114.4826939323422,36.60930792847117],'济南市':[117.02496706629023,36.68278472716141],'葫芦岛市':[120.86075764475588,40.743029881317507],'菏泽市':[115.46335977452752,35.26244049607468],'郑州市':[113.64964384986449,34.756610064140257],'泰安市':[117.08941491713698,36.18807775894815],'巴彦淖尔市':[107.42380671968002,40.76917990242912],'天水市':[105.73693162285642,34.58431941886869],'乌鲁木齐市':[87.56498774111579,43.84038034721766],'塔城地区':[82.9748805837443,46.758683629680358],'海东地区':[102.08520698745453,36.51761016768607],'玉树藏族自治州':[97.01331613741361,33.00623990972234],'乌海市':[106.83199909716231,39.683177006785239],'扬州市':[119.42777755116976,32.40850525456844],'抚顺市':[123.92981976705382,41.8773038295909],'沈阳市':[123.43279092160505,41.808644783515749],'鞍山市':[123.0077633288837,41.118743682153468],'盘锦市':[122.07322781023007,41.14124802295616],'哈尔滨市':[126.65771685544611,45.7732246332393],'鸡西市':[130.94176727325067,45.32153988655108],'辽阳市':[123.17245120514733,41.27333926556943],'丹东市':[124.33854311476908,40.1290228266375],'黑河市':[127.50083029523519,50.250690090738277],'大连市':[121.59347778143519,38.94870993830429],'齐齐哈尔市':[123.9872889421725,47.34769981336638],'本溪市':[123.77806236979248,41.32583762664885],'鹤岗市':[130.29247205063454,47.33866590372683],'铁岭市':[123.85484961461595,42.29975701212545],'锦州市':[121.14774873823639,41.13087887591732],'承德市':[117.93382245583847,40.992521052457139],'衡水市':[115.68622865290866,37.74692904585666],'石家庄市':[114.52208184420766,38.048958314615457],'保定市':[115.49481016907626,38.88656454802744],'秦皇岛市':[119.6043676161184,39.945461565897577],'沧州市':[116.86380647644208,38.297615350325717],'新余市':[114.9471174167892,27.82232155862896],'萍乡市':[113.85991703300718,27.639544222952308],'赣州市':[114.9359090792838,25.84529553634676],'上饶市':[117.95546387714895,28.457622553937275],'南昌市':[115.89352754583604,28.689578000141148],'吉安市':[114.99203871092017,27.113847650157085],'九江市':[115.99984802155373,29.71963952612237],'上海市':[121.48789948569473,31.24916171001514],'抚州市':[116.36091886693136,27.954545170269936],'宜春市':[114.40003867155777,27.811129895842887],'景德镇市':[117.18652262527252,29.303562768448289],'鹰潭市':[117.03545018601243,28.241309597181968],'福州市':[119.33022110712668,26.04712549657293],'漳州市':[117.67620467894617,24.517064779808483],'泉州市':[118.60036234322938,24.901652383991104],'三明市':[117.64219393404412,26.270835279362193],'龙岩市':[117.01799673877318,25.07868543351518],'莆田市':[119.07773096395963,25.448450136734264],'厦门市':[118.10388604566382,24.489230612469233],'宁德市':[119.54208214971854,26.656527419158914],'南平市':[118.18188294866126,26.643626474197839],'肇庆市':[112.4796533699162,23.078663282928927],'广州市':[113.30764967515182,23.12004910207623],'韶关市':[113.5944611074356,24.80296031189189],'云浮市':[112.05094595864539,22.937975685537468],'佛山市':[113.1340256353934,23.035094840514384],'咸阳市':[108.70750927819563,34.34537299599856],'梅州市':[116.12640309836864,24.30457060603053],'东莞市':[113.76343399075656,23.043023815368238],'茂名市':[110.93124533067585,21.66822571882161],'河源市':[114.71372147587484,23.757250850469008],'湛江市':[110.36506726285033,21.257463103764317],'江门市':[113.07812534115365,22.57511678345104],'深圳市':[114.0259736573215,22.546053546205248],'潮州市':[116.63007599086392,23.661811676516505],'汕头市':[116.72865028834109,23.383908453269198],'揭阳市':[116.37950085538243,23.547999466926137],'阳江市':[111.97700975587391,21.87151730451877],'惠州市':[114.41065807997102,23.11353985240842],'大理白族自治州':[100.22367478928341,25.59689963942069],'德宏傣族景颇族自治州':[98.58943428740686,24.441239663007964],'保山市':[99.17799561327822,25.120489196190353],'楚雄彝族自治州':[101.52938223914012,25.0663556741861],'玉溪市':[102.54506789248257,24.370447134438427],'昆明市':[102.71460113878045,25.049153100453159],'荆州市':[112.24186580719135,30.33259052298605],'随州市':[113.37935836429192,31.717857608188575],'十堰市':[110.80122891676038,32.636994339468127],'宜昌市':[111.31098109196083,30.73275781802591],'襄阳市':[112.25009284837394,32.22916859153757],'黄石市':[115.05068316392397,30.21612712771406],'武汉市':[114.31620010268132,30.58108412692075],'仙桃市':[113.38744819357841,30.293966004922326],'荆门市':[112.21733029896677,31.04261120294864],'潜江市':[112.76876801685833,30.34311579260133],'黄冈市':[114.9066180465751,30.446108937901135],'恩施土家族苗族自治州':[109.49192330375213,30.285888316555604],'合肥市':[117.28269909168304,31.86694226068694],'阜阳市':[115.82093225904637,32.90121133056961],'六安市':[116.50525268298414,31.75555835519844],'亳州市':[115.78792824511814,33.87121056530193],'宣城市':[118.75209631098257,30.951642354295758],'淮南市':[117.01863886329463,32.64281182374795],'安庆市':[117.0587387721073,30.537897817381098],'芜湖市':[118.38410842322829,31.36601978754301],'蚌埠市':[117.35707986587582,32.92949890669797],'孝感市':[113.93573439207124,30.927954784200965],'金华市':[119.65257570368145,29.10289910539069],'马鞍山市':[118.515881846623,31.688528158880027],'黄南藏族自治州':[102.00760030834485,35.52285155172832],'温州市':[120.6906347337091,28.002837594041247],'宁波市':[121.57900597258933,29.885258965918056],'台州市':[121.44061293593727,28.668283285674368],'绍兴市':[120.59246738555155,30.00236458052847],'杭州市':[120.2193754157201,30.259244461536104],'嘉兴市':[120.76042769895746,30.77399223958183],'衢州市':[118.87584165150854,28.95691044753569],'湖州市':[120.13724316328173,30.877925155690897],'舟山市':[122.1698720983516,30.036010302553938],'丽水市':[119.92957584318849,28.4562995521443],'梧州市':[111.30547195007331,23.485394636734456],'玉林市':[110.15167631614493,22.643973608377278],'贵港市':[109.61370755657885,23.10337316440881],'柳州市':[109.42240181015115,24.329053352467225],'南宁市':[108.29723355586639,22.80649293560261],'来宾市':[109.23181650474223,23.741165926514819],'北海市':[109.12262791919324,21.472718235009688],'百色市':[106.63182140365118,23.901512367910116],'桂林市':[110.26092014748277,25.262901245955239],'防城港市':[108.35179115286083,21.61739847047181],'钦州市':[108.6387980564235,21.973350465312728],'崇左市':[107.35732203836824,22.415455296546438],'贺州市':[111.5525941788367,24.41105354711281],'北京市':[116.39564503787867,39.92998577808024],'廊坊市':[116.70360222263986,39.51861062508462],'天津市':[117.21081309155257,39.143929903310077],'唐山市':[118.18345059773411,39.6505309225366],'徐州市':[117.18810662317687,34.27155343109188]};
