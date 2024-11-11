using System;
using System.Globalization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Ideal.Core.Common.Extensions
{

    /// <summary>
    /// 时间相关扩展方法（long类型）
    /// </summary>
    public static partial class DateTimeExtension
    {
        #region 转本地日期时间
        /// <summary>
        /// 时间戳（秒）转本地时间
        /// </summary> 
        /// <param name="timestamp">时间戳（秒）</param>
        /// <returns>本地时间</returns>
        public static TimeOnly ToLocalTimeTimeBySeconds(this long timestamp)
        {
            var dt = timestamp.ToLocalTimeDateTimeBySeconds();
            return TimeOnly.FromDateTime(dt);
        }

        /// <summary>
        /// 时间戳（毫秒）转本地时间
        /// </summary> 
        /// <param name="timestamp">时间戳（毫秒）</param>
        /// <returns>本地时间</returns>
        public static TimeOnly ToLocalTimeTimeByMilliseconds(this long timestamp)
        {
            var dt = timestamp.ToLocalTimeDateTimeByMilliseconds();
            return TimeOnly.FromDateTime(dt);
        }

        /// <summary>
        /// 时间戳（秒）转本地日期
        /// </summary> 
        /// <param name="timestamp">时间戳（秒）</param>
        /// <returns>本地日期</returns>
        public static DateOnly ToLocalTimeDateBySeconds(this long timestamp)
        {
            var dt = timestamp.ToLocalTimeDateTimeBySeconds();
            return DateOnly.FromDateTime(dt);
        }

        /// <summary>
        /// 时间戳（毫秒）转本地日期
        /// </summary> 
        /// <param name="timestamp">时间戳（毫秒）</param>
        /// <returns>本地日期</returns>
        public static DateOnly ToLocalTimeDateByMilliseconds(this long timestamp)
        {
            var dt = timestamp.ToLocalTimeDateTimeByMilliseconds();
            return DateOnly.FromDateTime(dt);
        }

        /// <summary>
        /// 时间戳（秒）转本地日期时间
        /// </summary>
        /// <param name="timestamp">时间戳（秒）</param>
        /// <returns>本地日期时间</returns>
        public static DateTime ToLocalTimeDateTimeBySeconds(this long timestamp)
        {
            var dto = DateTimeOffset.FromUnixTimeSeconds(timestamp);
            return dto.ToLocalTime().DateTime;
        }

        /// <summary>
        /// 时间戳（毫秒）转本地日期时间
        /// </summary> 
        /// <param name="timestamp">时间戳（毫秒）</param>
        /// <returns>本地日期时间</returns>
        public static DateTime ToLocalTimeDateTimeByMilliseconds(this long timestamp)
        {
            var dto = DateTimeOffset.FromUnixTimeMilliseconds(timestamp);
            return dto.ToLocalTime().DateTime;
        }
        #endregion

        #region 转UTC日期时间
        /// <summary>
        /// 时间戳（秒）转UTC时间
        /// </summary> 
        /// <param name="timestamp">时间戳（秒）</param>
        /// <returns>UTC时间</returns>
        public static TimeOnly ToUniversalTimeTimeBySeconds(this long timestamp)
        {
            var dt = timestamp.ToUniversalTimeDateTimeBySeconds();
            return TimeOnly.FromDateTime(dt);
        }

        /// <summary>
        /// 时间戳（毫秒）转UTC时间
        /// </summary> 
        /// <param name="timestamp">时间戳（毫秒）</param>
        /// <returns>UTC时间</returns>
        public static TimeOnly ToUniversalTimeTimeByMilliseconds(this long timestamp)
        {
            var dt = timestamp.ToUniversalTimeDateTimeByMilliseconds();
            return TimeOnly.FromDateTime(dt);
        }

        /// <summary>
        /// 时间戳（秒）转UTC日期
        /// </summary> 
        /// <param name="timestamp">时间戳（秒）</param>
        /// <returns>UTC日期</returns>
        public static DateOnly ToUniversalTimeDateBySeconds(this long timestamp)
        {
            var dt = timestamp.ToUniversalTimeDateTimeBySeconds();
            return DateOnly.FromDateTime(dt);
        }

        /// <summary>
        /// 时间戳（毫秒）转UTC日期
        /// </summary> 
        /// <param name="timestamp">时间戳（毫秒）</param>
        /// <returns>UTC日期</returns>
        public static DateOnly ToUniversalTimeDateByMilliseconds(this long timestamp)
        {
            var dt = timestamp.ToUniversalTimeDateTimeByMilliseconds();
            return DateOnly.FromDateTime(dt);
        }

        /// <summary>
        /// 时间戳（秒）转UTC日期时间
        /// </summary> 
        /// <param name="timestamp">时间戳（秒）</param>
        /// <returns>UTC日期时间</returns>
        public static DateTime ToUniversalTimeDateTimeBySeconds(this long timestamp)
        {
            var dto = DateTimeOffset.FromUnixTimeSeconds(timestamp);
            return dto.ToUniversalTime().DateTime;
        }

        /// <summary>
        /// 时间戳（毫秒）转UTC日期时间
        /// </summary> 
        /// <param name="timestamp">时间戳（毫秒）</param>
        /// <returns>UTC日期时间</returns>
        public static DateTime ToUniversalTimeDateTimeByMilliseconds(this long timestamp)
        {
            var dto = DateTimeOffset.FromUnixTimeMilliseconds(timestamp);
            return dto.ToUniversalTime().DateTime;
        }
        #endregion
    }

    /// <summary>
    /// 时间相关扩展方法（string类型）
    /// </summary>
    public static partial class DateTimeExtension
    {
        /// <summary>
        /// 字符串转日期时间，转换失败则返回空
        /// </summary>
        /// <param name="source">需转换的字符串</param>
        /// <returns>日期时间</returns>
        public static DateTime? ToDateTime(this string source)
        {
            if (DateTime.TryParse(source, CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
            {
                return date;
            }

            return default;
        }

        /// <summary>
        /// 字符串转日期时间，转换失败则返回默认值
        /// </summary>
        /// <param name="source">需转换的字符串</param>
        /// <param name="dateTime">默认日期时间</param>
        /// <returns>日期时间</returns>
        public static DateTime ToDateTimeOrDefault(this string source, DateTime dateTime)
        {
            if (DateTime.TryParse(source, CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
            {
                return date;
            }

            return dateTime;
        }

        /// <summary>
        /// 字符串转日期，转换失败则返回空
        /// </summary>
        /// <param name="source">需转换的字符串</param>
        /// <returns>日期</returns>
        public static DateOnly? ToDateOnly(this string source)
        {
            if (DateOnly.TryParse(source, CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
            {
                return date;
            }

            return default;
        }

        /// <summary>
        /// 字符串转日期，转换失败则返回默认日期
        /// </summary>
        /// <param name="source">需转换的字符串</param>
        /// <param name="dateOnly">默认日期</param>
        /// <returns>日期</returns>
        public static DateOnly ToDateOnlyOrDefault(this string source, DateOnly dateOnly)
        {
            if (DateOnly.TryParse(source, CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
            {
                return date;
            }

            return dateOnly;
        }

        /// <summary>
        /// 字符串转时间，转换失败则返回空
        /// </summary>
        /// <param name="source">需转换的字符串</param>
        /// <returns>时间</returns>
        public static TimeOnly? ToTimeOnly(this string source)
        {
            if (TimeOnly.TryParse(source, CultureInfo.InvariantCulture, DateTimeStyles.None, out var time))
            {
                return time;
            }

            return default;
        }

        /// <summary>
        /// 字符串转时间，转换失败则返回默认时间
        /// </summary>
        /// <param name="source">需转换的字符串</param>
        /// <param name="timeOnly">默认时间</param>
        /// <returns>时间</returns>
        public static TimeOnly ToTimeOnlyOrDefault(this string source, TimeOnly timeOnly)
        {
            if (TimeOnly.TryParse(source, CultureInfo.InvariantCulture, DateTimeStyles.None, out var time))
            {
                return time;
            }

            return timeOnly;
        }
    }

    /// <summary>
    /// 时间相关扩展方法（DateTime类型）
    /// </summary>
    public static partial class DateTimeExtension
    {
        #region 转换
        /// <summary>
        /// 日期时间转时间戳（秒）
        /// </summary>
        /// <param name="dateTime">日期时间</param>
        /// <returns>时间戳（秒）</returns>
        public static long ToUnixTimestampBySeconds(this DateTime dateTime)
        {
            var dto = new DateTimeOffset(dateTime);
            return dto.ToUnixTimeSeconds();
        }

        /// <summary>
        /// 日期时间转时间戳（毫秒）
        /// </summary> 
        /// <param name="dateTime">日期时间</param>
        /// <returns>时间戳（毫秒）</returns>
        public static long ToUnixTimestampByMilliseconds(this DateTime dateTime)
        {
            var dto = new DateTimeOffset(dateTime);
            return dto.ToUnixTimeMilliseconds();
        }

        /// <summary>
        /// 日期时间中日期部分+时间转日期时间
        /// </summary>
        /// <param name="dateTime">日期时间</param>
        /// <param name="timeOnly">时间</param>
        /// <returns>日期时间</returns>
        public static DateTime ToDateTime(this DateTime dateTime, TimeOnly timeOnly)
        {
            return DateOnly.FromDateTime(dateTime).ToDateTime(timeOnly);
        }

        /// <summary>
        /// 日期+日期时间中时间部分转为日期时间
        /// </summary>
        /// <param name="dateTime">日期时间</param>
        /// <param name="dateOnly">日期</param>
        /// <returns>日期时间</returns>
        public static DateTime ToDateTime(this DateTime dateTime, DateOnly dateOnly)
        {
            return dateOnly.ToDateTime(TimeOnly.FromDateTime(dateTime));
        }

        /// <summary>
        /// 日期时间转日期，保留日期时间中日期部分
        /// </summary>
        /// <param name="dateTime">日期时间</param>
        /// <returns日期></returns>
        public static DateOnly ToDateOnly(this DateTime dateTime)
        {
            return DateOnly.FromDateTime(dateTime);
        }

        /// <summary>
        /// 日期时间转时间，保留日期时间中时间部分
        /// </summary>
        /// <param name="dateTime">日期时间</param>
        /// <returns>时间</returns>
        public static TimeOnly ToTimeOnly(this DateTime dateTime)
        {
            return TimeOnly.FromDateTime(dateTime);
        }
        #endregion

        #region 获取 天相关

        /// <summary>
        /// 获取当天的开始时间
        /// </summary>
        /// <param name="dateTime">当前日期时间</param>
        /// <returns>日期 + 00:00:00</returns>
        public static DateTime GetStartDateTimeOfDay(this DateTime dateTime)
        {
            return dateTime.Date;
        }

        /// <summary>
        /// 获取当天的结束时间
        /// </summary>
        /// <param name="dateTime">当前日期时间</param>
        /// <returns>日期 + 23:59:59 9999999</returns>
        public static DateTime GetEndDateTimeOfDay(this DateTime dateTime)
        {
            return dateTime.Date.AddDays(1).AddTicks(-1);
        }

        #endregion

        #region 获取 周相关

        /// <summary>
        /// 判断当前日期是否是周末
        /// </summary>
        /// <param name="dateTime">当前日期时间</param>
        /// <returns>是否周末</returns>
        public static bool IsWeekend(this DateTime dateTime)
        {
            return dateTime.DayOfWeek == DayOfWeek.Saturday || dateTime.DayOfWeek == DayOfWeek.Sunday;
        }

        /// <summary>
        /// 获取当前日期所在周的第一天（周一）
        /// </summary>
        /// <param name="dateTime">当前日期时间</param>
        /// <returns>日期时间</returns>
        public static DateTime GetFirstDayDateTimeOfWeek(this DateTime dateTime)
        {
            //0 = Sunday 周日, 1 = Monday 周一, ..., 6 = Saturday 周六
            //首先获取当前日期星期枚举值，然后计算其和周一枚举值差值
            //结果+7，保证结果为正数
            //结果%7，保证结果在0-6之间，对于一周七天，从而表示要回退多少天到周一
            //+7 %7 巧妙的把周日当7处理，最后再转为0
            var diff = ((int)dateTime.DayOfWeek - (int)DayOfWeek.Monday + 7) % 7;
            return dateTime.AddDays(-diff).Date;
        }

        /// <summary>
        /// 获取当前日期所在周的最后一天（周日）
        /// </summary>
        /// <param name="dateTime">当前日期时间</param>
        /// <returns>日期</returns>
        public static DateTime GetLastDayDateTimeOfWeek(this DateTime dateTime)
        {
            //0 = Sunday 周日, 1 = Monday 周一, ..., 6 = Saturday 周六
            //首先计算还差几天到周日
            //结果%7，保证结果在0-6之间
            //当周日时dateTime.DayOfWeek为0，（7-0）% 7 = 0
            //巧妙的把周日当7处理，最后再转为0
            var diff = (7 - (int)dateTime.DayOfWeek) % 7;
            return dateTime.AddDays(diff).Date;
        }

        /// <summary>
        /// 获取当前日期上一个指定星期几
        /// </summary>
        /// <param name="dateTime">当前日期时间</param>
        /// <param name="dayOfWeek">指定星期几</param>
        /// <returns></returns>
        public static DateTime GetPreviousDayDateTimeOfWeek(this DateTime dateTime, DayOfWeek dayOfWeek)
        {
            //计算当前日期与目标星期几相差天数
            var diff = ((int)dateTime.DayOfWeek - (int)dayOfWeek + 7) % 7;
            //如果相差0天表示当前日期和目标星期几相同，需要改为7
            diff = diff == 0 ? 7 : diff;
            return dateTime.AddDays(-diff).Date;
        }

        /// <summary>
        /// 获取当前日期下一个最近指定星期几
        /// </summary>
        /// <param name="dateTime">当前日期时间</param>
        /// <param name="dayOfWeek">指定星期几</param>
        /// <returns>日期</returns>
        public static DateTime GetNextDayDateTimeOfWeek(this DateTime dateTime, DayOfWeek dayOfWeek)
        {
            //计算目标日期与当月最后一天相差天数
            var diff = ((int)dayOfWeek - (int)dateTime.DayOfWeek + 7) % 7;
            //如果相差0天表示当前日期和目标星期几相同，需要改为7
            diff = diff == 0 ? 7 : diff;
            return dateTime.AddDays(diff).Date;
        }
        #endregion

        #region 获取 月相关

        /// <summary>
        /// 获取当前日期是其所在月的第几周
        /// </summary>
        /// <param name="dateTime">当前日期时间</param>
        /// <returns>周数</returns>
        public static int GetWeekOfMonth(this DateTime dateTime)
        {
            //获取当前日期所在月的第一天
            var firstDayOfMonth = dateTime.GetFirstDayDateTimeOfMonth();
            //首先设定周一为一周的开始
            //计算当前月第一天与周一相差天数
            //即第一周如果不满一周还差多少天
            var diff = ((int)firstDayOfMonth.DayOfWeek - (int)DayOfWeek.Monday + 7) % 7;
            //用第一周不满的差值加上当前日期的天数之和计算当前为当月第几周
            //然后计算 总天数/7的商，如果有余数则再加1
            //公式为：n/7 + (n%7 > 0 ? 1 : 0)
            //上面公式可以简化为 (n+6)/7
            return (diff + dateTime.Day + 6) / 7;
        }

        /// <summary>
        /// 获取当前日期所在月份的周数
        /// </summary>
        /// <param name="dateTime">当前日期时间</param>
        /// <returns>周数</returns>
        public static int GetWeeksInMonth(this DateTime dateTime)
        {
            //获取当前日期所在月的第一天
            var firstDayOfMonth = dateTime.GetFirstDayDateTimeOfMonth();
            //获取当前日期所在月的最后一天
            var lastDayOfMonth = dateTime.GetLastDayDateTimeOfMonth();

            //获取当月第一天在全年中的周数
            var firstWeek = firstDayOfMonth.GetWeekOfYear();
            //获取当月最后一天在全年中的周数
            var lastWeek = lastDayOfMonth.GetWeekOfYear();

            return lastWeek - firstWeek + 1;
        }

        /// <summary>
        /// 获取当前日期所在月的第一天
        /// </summary>
        /// <param name="dateTime">当前日期时间</param>
        /// <returns>日期</returns>
        public static DateTime GetFirstDayDateTimeOfMonth(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, 1, 0, 0, 0, 0, DateTimeKind.Local);
        }

        /// <summary>
        /// 获取当前日期所在月的最后一天
        /// </summary>
        /// <param name="dateTime">当前日期时间</param>
        /// <returns>日期</returns>
        public static DateTime GetLastDayDateTimeOfMonth(this DateTime dateTime)
        {
            //获取当前月的总天数
            var days = DateTime.DaysInMonth(dateTime.Year, dateTime.Month);
            return new DateTime(dateTime.Year, dateTime.Month, days, 0, 0, 0, 0, DateTimeKind.Local);
        }

        /// <summary>
        /// 获取当前日期所在月的第一个指定星期几
        /// </summary>
        /// <param name="dateTime">当前日期时间</param>
        /// <param name="dayOfWeek">指定星期几</param>
        /// <returns>日期</returns>
        public static DateTime GetFirstDayOfWeekDateTimeInMonth(this DateTime dateTime, DayOfWeek dayOfWeek)
        {
            //获取当前日期所在月的第一天
            var firstDayOfMonth = dateTime.GetFirstDayDateTimeOfMonth();
            //计算目标日期与当月第一天相差天数
            var diff = ((int)dayOfWeek - (int)firstDayOfMonth.DayOfWeek + 7) % 7;
            return firstDayOfMonth.AddDays(diff);
        }

        /// <summary>
        /// 获取当前日期所在月的最后一个指定星期几
        /// </summary>
        /// <param name="dateTime">当前日期时间</param>
        /// <param name="dayOfWeek">指定星期几</param>
        /// <returns>日期</returns>
        public static DateTime GetLastDayOfWeekDateTimeInMonth(this DateTime dateTime, DayOfWeek dayOfWeek)
        {
            //获取当前日期所在月的最后一天
            var lastDayOfMonth = dateTime.GetLastDayDateTimeOfMonth();
            //计算目标日期与当月最后一天相差天数
            var diff = ((int)lastDayOfMonth.DayOfWeek - (int)dayOfWeek + 7) % 7;
            return lastDayOfMonth.AddDays(-diff);
        }
        #endregion

        #region 获取 季相关

        /// <summary>
        /// 获取当前日期所在季度
        /// </summary>
        /// <param name="dateTime">当前日期时间</param>
        /// <returns>1，2，3，4</returns>
        public static int GetQuarter(this DateTime dateTime)
        {
            return (dateTime.Month - 1) / 3 + 1;
        }

        /// <summary>
        /// 获取当前日期所在季度的第一天
        /// </summary>
        /// <param name="dateTime">当前日期时间</param>
        /// <returns>日期</returns>
        public static DateTime GetFirstDayDateTimeOfQuarter(this DateTime dateTime)
        {
            //计算当前日期所在季度的起始月
            var firstMonth = (dateTime.Month - 1) / 3 * 3 + 1;
            return new DateTime(dateTime.Year, firstMonth, 1, 0, 0, 0, 0, DateTimeKind.Local);
        }

        /// <summary>
        /// 获取当前日期所在季度的最后一天
        /// </summary>
        /// <param name="dateTime">当前日期时间</param>
        /// <returns>日期</returns>
        public static DateTime GetLastDayDateTimeOfQuarter(this DateTime dateTime)
        {
            //计算当前日期所在季度的最后月
            var lastMonth = (dateTime.Month + 2) / 3 * 3;
            //获取当前月的总天数
            var days = DateTime.DaysInMonth(dateTime.Year, lastMonth);
            return new DateTime(dateTime.Year, lastMonth, days, 0, 0, 0, 0, DateTimeKind.Local);
        }

        #endregion

        #region 获取 年相关
        /// <summary>
        /// 判断当前日期所在年是否是闰年
        /// </summary>
        /// <param name="dateTime">当前日期时间</param>
        /// <returns>是否闰年</returns>
        public static bool IsLeapYear(this DateTime dateTime)
        {
            return DateTime.IsLeapYear(dateTime.Year);
        }

        /// <summary>
        /// 获取当前日期是其所在年的第几周（ISO 8601 标准）
        /// </summary>
        /// <param name="dateTime">当前日期时间</param>
        /// <returns>周数</returns>
        public static int GetWeekOfYear(this DateTime dateTime)
        {
            var currentCulture = CultureInfo.CurrentCulture;
            return currentCulture.Calendar.GetWeekOfYear(dateTime, currentCulture.DateTimeFormat.CalendarWeekRule, currentCulture.DateTimeFormat.FirstDayOfWeek);
        }

        /// <summary>
        /// 获取当前日期所在年的第一天
        /// </summary>
        /// <param name="dateTime">当前日期时间</param>
        /// <returns>日期</returns>
        public static DateTime GetFirstDayDateTimeOfYear(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, 1, 1, 0, 0, 0, 0, DateTimeKind.Local);
        }

        /// <summary>
        /// 获取当前日期所在年的最后一天
        /// </summary>
        /// <param name="dateTime">当前日期时间</param>
        /// <returns>日期</returns>
        public static DateTime GetLastDayDateTimeOfYear(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, 12, 31, 0, 0, 0, 0, DateTimeKind.Local);
        }
        #endregion
    }

    /// <summary>
    /// 时间相关扩展方法（TimeOnly、DateOnly类型）
    /// </summary>
    public static partial class DateTimeExtension
    {
        /// <summary>
        /// 时间转日期时间，默认使用系统当前日期+时间转为日期时间格式
        /// </summary>
        /// <param name="timeOnly">时间</param>
        /// <returns>日期时间</returns>
        public static DateTime ToDateTime(this TimeOnly timeOnly)
        {
            return DateOnly.FromDateTime(DateTime.Now).ToDateTime(timeOnly);
        }

        /// <summary>
        /// 日期+时间转为日期时间
        /// </summary>
        /// <param name="timeOnly">时间</param>
        /// <param name="dateOnly">日期</param>
        /// <returns>日期时间</returns>
        public static DateTime ToDateTime(this TimeOnly timeOnly, DateOnly dateOnly)
        {
            return dateOnly.ToDateTime(timeOnly);
        }

        /// <summary>
        /// 日期时间中日期部分+时间转日期时间
        /// </summary>
        /// <param name="timeOnly">时间</param>
        /// <param name="dateTime">日期时间</param>
        /// <returns>日期时间</returns>
        public static DateTime ToDateTime(this TimeOnly timeOnly, DateTime dateTime)
        {
            return DateOnly.FromDateTime(dateTime).ToDateTime(timeOnly);
        }

        /// <summary>
        /// 日期转日期时间，日期+默认使用系统当前时间转为日期时间格式
        /// </summary>
        /// <param name="dateOnly">日期</param>
        /// <returns>日期时间</returns>
        public static DateTime ToDateTime(this DateOnly dateOnly)
        {
            return dateOnly.ToDateTime(TimeOnly.FromDateTime(DateTime.Now));
        }

        /// <summary>
        /// 日期+日期时间中时间部分转日期时间
        /// </summary>
        /// <param name="dateOnly">日期</param>
        /// <param name="dateTime">日期时间</param>
        /// <returns>日期时间</returns>
        public static DateTime ToDateTime(this DateOnly dateOnly, DateTime dateTime)
        {
            return dateOnly.ToDateTime(TimeOnly.FromDateTime(dateTime));
        }
    }
}













