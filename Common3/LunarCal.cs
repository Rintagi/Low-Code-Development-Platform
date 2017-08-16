namespace RO.Common3
{
    using System;
    using System.Text;
    using System.Globalization;
    using System.Collections;
    using System.Collections.Generic;

    public class LunarCal
    {
        private static String[] ChinaYear = new String[]{ 
            "甲子","乙丑","丙寅","丁卯","戊辰","己巳","庚午","辛未","壬申","癸酉","甲戌","乙亥",
            "丙子","丁丑","戊寅","己卯","庚辰","辛巳","壬午","癸未","甲申","乙酉","丙戌","丁亥",
            "戊子","己丑","庚寅","辛卯","壬辰","癸巳","甲午","己未","丙申","丁酉","戊戌","己亥",
            "庚子","辛丑","壬寅","癸卯","甲辰","乙巳","丙午","丁未","戊申","己酉","庚戌","辛亥",
            "壬子","癸丑","甲寅","乙卯","丙辰","丁巳","戊午","己未","庚申","辛酉","壬戌","癸亥"
        };
        private static String[] ChinaMonth = new String[]{  
            "元", "二", "三", "四", "五", "六", "七", "八", "九", "十", "十一", "腊" 
        };
        private static String[] ChinaDay = new String[]{
            "初一", "初二", "初三", "初四", "初五", "初六", "初七", "初八", "初九", "初十", 
            "十一", "十二","十三", "十四" ,"十五", "十六","十七", "十八" ,"十九", "二十", 
            "廿一", "廿二","廿三", "廿四" ,"廿五", "廿六","廿七", "廿八" ,"廿九", "三十"
        };
        private static String[] ChinaZodiac = new String[] { 
            "鼠", "牛", "虎", "兔", "龙", "蛇", "马", "羊", "猴", "鸡", "狗", "猪" 
        };

        private static Hashtable aChinaFestival_LC = new Hashtable();
        private static ChineseLunisolarCalendar aLunisolarCalendar_C = new ChineseLunisolarCalendar();
        private static Int32 aDay_Lc;

        static LunarCal()
        {
            aChinaFestival_LC.Add("腊三十", "除夕");
            aChinaFestival_LC.Add("元初一", "春节");
            aChinaFestival_LC.Add("元十五", "元宵");
            aChinaFestival_LC.Add("四十六", "清明");
            aChinaFestival_LC.Add("五初五", "端午");
            aChinaFestival_LC.Add("七初七", "七夕");
            aChinaFestival_LC.Add("九初九", "重阳");
            aChinaFestival_LC.Add("七十五", "中元");
            aChinaFestival_LC.Add("八十五", "中秋");
            aChinaFestival_LC.Add("腊初八", "腊八");
        }

        public static String GetLCDateForChina(DateTime CurrentDateTime)
        {
            if (CurrentDateTime > aLunisolarCalendar_C.MaxSupportedDateTime || CurrentDateTime < aLunisolarCalendar_C.MinSupportedDateTime)
            {
                return "";
            }
            string yy = LunarCal.GetLCYearForChina(CurrentDateTime);
            string mm = LunarCal.GetLCMonthForChina(CurrentDateTime);
            string dd = LunarCal.GetLCDayForChina(CurrentDateTime, false, false);
            string zz = LunarCal.GetLCYearZodiac(CurrentDateTime);
            return yy + "年 " + mm + "月 " + dd;
        }

        public static String GetLCDateTimeForChina(DateTime CurrentDateTime)
        {
            if (CurrentDateTime > aLunisolarCalendar_C.MaxSupportedDateTime || CurrentDateTime < aLunisolarCalendar_C.MinSupportedDateTime)
            {
                return "";
            }
            return GetLCDateForChina(CurrentDateTime) + " " + CurrentDateTime.ToShortTimeString().Replace("AM", "上午").Replace("PM", "下午");
        }

        /// <summary>
        /// 农历年
        /// </summary>
        /// <param name="CurrentDateTime">当前日期</param>
        /// <returns></returns>
        public static String GetLCYearForChina(DateTime CurrentDateTime)
        {
            if (CurrentDateTime > aLunisolarCalendar_C.MaxSupportedDateTime || CurrentDateTime < aLunisolarCalendar_C.MinSupportedDateTime)
            {
                return "";
            }
            Int32 iYear = aLunisolarCalendar_C.GetYear(CurrentDateTime);
            iYear = (iYear - 4) % 60;
            return ChinaYear[iYear];
        }

        /// <summary>
        /// 农历月
        /// </summary>
        /// <param name="CurrentDateTime">当前日期</param>
        /// <returns></returns>
        public static String GetLCMonthForChina(DateTime CurrentDateTime)
        {
            if (CurrentDateTime > aLunisolarCalendar_C.MaxSupportedDateTime || CurrentDateTime < aLunisolarCalendar_C.MinSupportedDateTime)
            {
                return "";
            }
            Int32 iMonth = aLunisolarCalendar_C.GetMonth(CurrentDateTime);
            Int32 iYear = aLunisolarCalendar_C.GetYear(CurrentDateTime);
            Boolean iLeapYear = aLunisolarCalendar_C.IsLeapYear(iYear);
            if (iLeapYear)
            {
                Int32 iLeapMonth = aLunisolarCalendar_C.GetLeapMonth(CurrentDateTime.Year);
                if (iMonth >= iLeapMonth)
                    iMonth--;
            }
            return ChinaMonth[iMonth - 1];
        }

        /* 农历日 */
        /// </summary>
        /// <param name="CurrentDateTime">当前日期</param>
        /// <param name="IfRetMonthInFistDay">如果为月份的第一天是否返回月份字符串</param>
        /// <param name="IfRetFestival">是否返回节日（比月份优先返回）</param>
        /// <returns></returns>
        public static String GetLCDayForChina(DateTime CurrentDateTime, Boolean IfRetMonthInFistDay, Boolean IfRetFestival)
        {
            if (CurrentDateTime > aLunisolarCalendar_C.MaxSupportedDateTime || CurrentDateTime < aLunisolarCalendar_C.MinSupportedDateTime)
            {
                return "";
            }
            String iMonthStr;
            aDay_Lc = aLunisolarCalendar_C.GetDayOfMonth(CurrentDateTime);
            if (IfRetFestival)
            {
                iMonthStr = GetLCMonthForChina(CurrentDateTime);
                if (aChinaFestival_LC.Contains(iMonthStr + ChinaDay[aDay_Lc - 1]))
                    return aChinaFestival_LC[iMonthStr + ChinaDay[aDay_Lc - 1]].ToString();
            }
            if ((aDay_Lc == 1) && IfRetMonthInFistDay)
            {
                iMonthStr = GetLCMonthForChina(CurrentDateTime);
                if (aLunisolarCalendar_C.IsLeapMonth(CurrentDateTime.Year, CurrentDateTime.Month))
                    return String.Format("闰{0}月", iMonthStr);
                else
                    return String.Format("{0}月", iMonthStr); ;
            }
            return ChinaDay[aDay_Lc - 1];
        }

        /// <summary>
        /// 取指定年的生肖
        /// </summary>
        /// <param name="CurrentDateTime">指定年的日期</param>
        /// <returns></returns>
        public static String GetLCYearZodiac(DateTime CurrentDateTime)
        {
            if (CurrentDateTime > aLunisolarCalendar_C.MaxSupportedDateTime || CurrentDateTime < aLunisolarCalendar_C.MinSupportedDateTime)
            {
                return "";
            }
            Int32 iYear = aLunisolarCalendar_C.GetSexagenaryYear(CurrentDateTime);
            iYear = aLunisolarCalendar_C.GetTerrestrialBranch(iYear);
            return ChinaZodiac[iYear - 1];
        }
    }
}