using Ideal.Core.Common.Extensions;
using Xunit;

namespace Ideal.Core.Common.Test
{
    /// <summary>
    /// ʱ�������չ������Ԫ���ԣ�long���ͣ�
    /// </summary>
    public partial class DateTimeUnitTest
    {
        [Fact]
        public void ToDateTime_Long()
        {
            var abc = 123L;
            var dd = abc.ToLocalTimeTimeBySeconds();
        }
    }

    /// <summary>
    /// ʱ�������չ������Ԫ���ԣ�string���ͣ�
    /// </summary>
    public partial class DateTimeUnitTest
    {
    }

    /// <summary>
    /// ʱ�������չ������Ԫ���ԣ�DateTime���ͣ�
    /// </summary>
    public partial class DateTimeUnitTest
    {
        [Fact]
        public void ToDateTime_DateTime()
        {
            var time1 = new TimeOnly(11, 11, 11);
            var dateTime1 = new DateTime(2024, 11, 11, 2, 2, 2);
            var date = dateTime1.ToDateTime(time1);
            Assert.Equal(new DateTime(dateTime1.Year, dateTime1.Month, dateTime1.Day, time1.Hour, time1.Minute, time1.Second), date);
        }

        [Fact]
        public void ToDateTime1_DateTime()
        {
            var date1 = new DateOnly(2024, 11, 11);
            var dateTime1 = new DateTime(2024, 11, 11, 2, 2, 2);
            var date = dateTime1.ToDateTime(date1);
            Assert.Equal(new DateTime(date1.Year, date1.Month, date1.Day, dateTime1.Hour, dateTime1.Minute, dateTime1.Second), date);
        }

        [Fact]
        public void ToDateOnly()
        {
            var dateTime1 = new DateTime(2024, 11, 11, 2, 2, 2);
            var date = dateTime1.ToDateOnly();
            Assert.Equal(new DateOnly(2024, 11, 11), date);
        }

        [Fact]
        public void ToTimeOnly()
        {
            var dateTime1 = new DateTime(2024, 11, 11, 2, 2, 2);
            var date = dateTime1.ToTimeOnly();
            Assert.Equal(new TimeOnly(2, 2, 2), date);
        }
    }

    /// <summary>
    /// ʱ�������չ������Ԫ���ԣ�TimeOnly��DateOnly���ͣ�
    /// </summary>
    public partial class DateTimeUnitTest
    {
        [Fact]
        public void ToDateTime_TimeOnly()
        {
            var now = DateTime.Now;
            var time1 = new TimeOnly(11, 11, 11);
            var date = time1.ToDateTime();
            Assert.Equal(new DateTime(now.Year, now.Month, now.Day, time1.Hour, time1.Minute, time1.Second), date);
        }

        [Fact]
        public void ToDateTime1_TimeOnly()
        {
            var now = DateTime.Now;
            var time1 = new TimeOnly(11, 11, 11);
            var date1 = new DateOnly(2024, 11, 11);
            var date = time1.ToDateTime(date1);
            Assert.Equal(new DateTime(date1.Year, date1.Month, date1.Day, time1.Hour, time1.Minute, time1.Second), date);
        }

        [Fact]
        public void ToDateTime2_TimeOnly()
        {
            var now = DateTime.Now;
            var time1 = new TimeOnly(11, 11, 11);
            var date1 = new DateTime(2024, 11, 11, 2, 2, 2);
            var date = time1.ToDateTime(date1);
            Assert.Equal(new DateTime(date1.Year, date1.Month, date1.Day, time1.Hour, time1.Minute, time1.Second), date);
        }

        [Fact]
        public void ToDateTime_DateOnly()
        {
            var now = DateTime.Now;
            var date1 = new DateOnly(2024, 11, 11);
            var date = date1.ToDateTime();
            Assert.Equal($"{date1:yyyy-MM-dd} {now:HH:mm:ss}", date.ToString("yyyy-MM-dd HH:mm:ss"));
        }

        [Fact]
        public void ToDateTime1_DateOnly()
        {
            var now = DateTime.Now;
            var date1 = new DateOnly(2024, 11, 11);
            var dateTime1 = new DateTime(2024, 11, 11, 2, 2, 2);
            var date = date1.ToDateTime(dateTime1);
            Assert.Equal(new DateTime(date1.Year, date1.Month, date1.Day, dateTime1.Hour, dateTime1.Minute, dateTime1.Second), date);
        }

        [Fact]
        public void IsWeekend()
        {
            var date1 = new DateTime(2024, 11, 1);
            var friday = date1.IsWeekend();
            Assert.False(friday);
            var date2 = new DateTime(2024, 11, 2);
            var saturday = date2.IsWeekend();
            Assert.True(saturday);
            var date3 = new DateTime(2024, 11, 3);
            var sunday = date3.IsWeekend();
            Assert.True(sunday);
            var date4 = new DateTime(2024, 11, 4);
            var monday = date4.IsWeekend();
            Assert.False(monday);
        }

        [Fact]
        public void IsLeapYear()
        {
            var date4 = new DateTime(2023, 11, 7);
            var thursday = date4.IsLeapYear();
            Assert.False(thursday);

            var date6 = new DateTime(2024, 11, 9);
            var saturday = date6.IsLeapYear();
            Assert.True(saturday);
        }

        [Fact]
        public void GetQuarter()
        {
            var date1 = new DateTime(2024, 1, 1);
            var q1 = date1.GetQuarter();
            Assert.Equal(1, q1);

            var date2 = new DateTime(2024, 5, 12);
            var q2 = date2.GetQuarter();
            Assert.Equal(2, q2);

            var date21 = new DateTime(2024, 6, 30);
            var q21 = date21.GetQuarter();
            Assert.Equal(2, q21);

            var date3 = new DateTime(2024, 7, 1);
            var q3 = date3.GetQuarter();
            Assert.Equal(3, q3);

            var date31 = new DateTime(2024, 9, 30);
            var q31 = date31.GetQuarter();
            Assert.Equal(3, q31);

            var date4 = new DateTime(2024, 12, 1);
            var q4 = date4.GetQuarter();
            Assert.Equal(4, q4);
        }

        [Fact]
        public void GetStartDateTimeOfDay()
        {
            var datetime = new DateTime(2024, 11, 7, 14, 10, 10);
            var start = datetime.GetStartDateTimeOfDay();
            Assert.Equal(new DateTime(2024, 11, 7), start);
        }

        [Fact]
        public void GetEndDateTimeOfDay()
        {
            var date4 = new DateTime(2024, 11, 7, 14, 10, 10);
            var end = date4.GetEndDateTimeOfDay();
            Assert.Equal("2024-11-07 23:59:59 9999999", end.ToString("yyyy-MM-dd HH:mm:ss fffffff"));
        }

        [Fact]
        public void GetFirstDayDateTimeOfWeek()
        {
            //��֤��ǰ���������壬����һ����һ���µ����
            var friday = new DateTime(2024, 11, 1, 14, 10, 10);
            var day_friday = friday.GetFirstDayDateTimeOfWeek();
            Assert.Equal(new DateTime(2024, 10, 28), day_friday);

            //��֤��ǰ���ھ�����һ�����
            var monday = new DateTime(2024, 11, 4, 4, 10, 10);
            var day_monday = monday.GetFirstDayDateTimeOfWeek();
            Assert.Equal(new DateTime(2024, 11, 4), day_monday);

            //��֤��ǰ���������ĵ����
            var thursday = new DateTime(2024, 11, 7, 4, 10, 10);
            var day_thursday = thursday.GetFirstDayDateTimeOfWeek();
            Assert.Equal(new DateTime(2024, 11, 4), day_thursday);

            //��֤��ǰ���������յ����
            var sunday = new DateTime(2024, 11, 10, 4, 10, 10);
            var day_sunday = sunday.GetFirstDayDateTimeOfWeek();
            Assert.Equal(new DateTime(2024, 11, 4), day_sunday);
        }

        [Fact]
        public void GetLastDayDateTimeOfWeek()
        {
            //��֤��ǰ����������������������һ���µ����
            var sunday = new DateTime(2024, 11, 30, 14, 10, 10);
            var day_sunday = sunday.GetLastDayDateTimeOfWeek();
            Assert.Equal(new DateTime(2024, 12, 1), day_sunday);

            //��֤��ǰ���ھ�����һ�����
            var monday = new DateTime(2024, 11, 4, 4, 10, 10);
            var day_monday = monday.GetLastDayDateTimeOfWeek();
            Assert.Equal(new DateTime(2024, 11, 10), day_monday);

            //��֤��ǰ���������ĵ����
            var thursday = new DateTime(2024, 11, 7, 4, 10, 10);
            var day_thursday = thursday.GetLastDayDateTimeOfWeek();
            Assert.Equal(new DateTime(2024, 11, 10), day_thursday);

            //��֤��ǰ���������յ����
            var sunday1 = new DateTime(2024, 11, 10, 4, 10, 10);
            var day_thursday1 = sunday1.GetLastDayDateTimeOfWeek();
            Assert.Equal(new DateTime(2024, 11, 10), day_thursday1);
        }

        [Fact]
        public void GetFirstDayOfMonth()
        {
            //Sunday = 0,
            //Monday = 1,
            //Tuesday = 2,
            //Wednesday = 3,
            //Thursday = 4,
            //Friday = 5,
            //Saturday = 6

            var date1 = new DateTime(2024, 11, 1, 14, 10, 10);
            var day1 = date1.GetFirstDayDateTimeOfMonth();
            Assert.Equal(new DateTime(2024, 11, 1), day1);
            var date17 = new DateTime(2024, 11, 17, 4, 10, 10);
            var day17 = date17.GetFirstDayDateTimeOfMonth();
            Assert.Equal(new DateTime(2024, 11, 1), day17);
            var date30 = new DateTime(2024, 11, 30, 4, 10, 10);
            var day30 = date30.GetFirstDayDateTimeOfMonth();
            Assert.Equal(new DateTime(2024, 11, 1), day30);
        }

        [Fact]
        public void GetLastDayOfMonth()
        {
            var date1 = new DateTime(2024, 11, 1, 14, 10, 10);
            var day1 = date1.GetLastDayDateTimeOfMonth();
            Assert.Equal(new DateTime(2024, 11, 30), day1);
            var date17 = new DateTime(2024, 11, 17, 4, 10, 10);
            var day17 = date17.GetLastDayDateTimeOfMonth();
            Assert.Equal(new DateTime(2024, 11, 30), day17);
            var date30 = new DateTime(2024, 11, 30, 4, 10, 10);
            var day30 = date30.GetLastDayDateTimeOfMonth();
            Assert.Equal(new DateTime(2024, 11, 30), day30);
        }

        [Fact]
        public void GetFirstDayDateTimeOfQuarter()
        {
            //һ�����ȵ�һ����ȡ��һ������
            var month1 = new DateTime(2024, 10, 1, 14, 10, 10);
            var day_month1 = month1.GetFirstDayDateTimeOfQuarter();
            Assert.Equal(new DateTime(2024, 10, 1), day_month1);

            //һ�����ȵڶ�����ȡ�м��һ������
            var month2 = new DateTime(2024, 11, 17, 4, 10, 10);
            var day_month2 = month2.GetFirstDayDateTimeOfQuarter();
            Assert.Equal(new DateTime(2024, 10, 1), day_month2);

            //һ�����ȵ�������ȡ���һ������
            var month3 = new DateTime(2024, 12, 31, 4, 10, 10);
            var day_month3 = month3.GetFirstDayDateTimeOfQuarter();
            Assert.Equal(new DateTime(2024, 10, 1), day_month3);
        }

        [Fact]
        public void GetLastDayDateTimeOfQuarter()
        {
            //һ�����ȵ�һ����ȡ��һ������
            var month1 = new DateTime(2024, 10, 1, 14, 10, 10);
            var day_month1 = month1.GetLastDayDateTimeOfQuarter();
            Assert.Equal(new DateTime(2024, 12, 31), day_month1);

            //һ�����ȵڶ�����ȡ�м��һ������
            var month2 = new DateTime(2024, 11, 17, 4, 10, 10);
            var day_month2 = month2.GetLastDayDateTimeOfQuarter();
            Assert.Equal(new DateTime(2024, 12, 31), day_month2);

            //һ�����ȵ�������ȡ���һ������
            var month3 = new DateTime(2024, 12, 31, 4, 10, 10);
            var day_month3 = month3.GetLastDayDateTimeOfQuarter();
            Assert.Equal(new DateTime(2024, 12, 31), day_month3);
        }

        [Fact]
        public void GetFirstDayOfYear()
        {
            //Sunday = 0,
            //Monday = 1,
            //Tuesday = 2,
            //Wednesday = 3,
            //Thursday = 4,
            //Friday = 5,
            //Saturday = 6

            var date1 = new DateTime(2024, 1, 1, 14, 10, 10);
            var day1 = date1.GetFirstDayDateTimeOfYear();
            Assert.Equal(new DateTime(2024, 1, 1), day1);
            var date17 = new DateTime(2024, 11, 17, 4, 10, 10);
            var day17 = date17.GetFirstDayDateTimeOfYear();
            Assert.Equal(new DateTime(2024, 1, 1), day17);
            var date30 = new DateTime(2024, 12, 31, 4, 10, 10);
            var day30 = date30.GetFirstDayDateTimeOfYear();
            Assert.Equal(new DateTime(2024, 1, 1), day30);
        }

        [Fact]
        public void GetLastDayDateTimeOfYear()
        {
            var date1 = new DateTime(2024, 1, 1, 14, 10, 10);
            var day1 = date1.GetLastDayDateTimeOfYear();
            Assert.Equal(new DateTime(2024, 12, 31), day1);
            var date17 = new DateTime(2024, 11, 17, 4, 10, 10);
            var day17 = date17.GetLastDayDateTimeOfYear();
            Assert.Equal(new DateTime(2024, 12, 31), day17);
            var date30 = new DateTime(2024, 12, 31, 4, 10, 10);
            var day30 = date30.GetLastDayDateTimeOfYear();
            Assert.Equal(new DateTime(2024, 12, 31), day30);
        }

        [Fact]
        public void GetFirstDayOfWeekDateTimeInMonth()
        {
            //��֤��ǰ���������壬����һ����һ�ܵ����
            var friday_monday = new DateTime(2024, 11, 1, 14, 10, 10);
            var day_friday_monday = friday_monday.GetFirstDayOfWeekDateTimeInMonth(DayOfWeek.Monday);
            Assert.Equal(new DateTime(2024, 11, 4), day_friday_monday);

            //��֤��ǰ�����Ǳ��µ�һ����һ�����
            var monday_monday = new DateTime(2024, 11, 4, 4, 10, 10);
            var day_monday_monday = monday_monday.GetFirstDayOfWeekDateTimeInMonth(DayOfWeek.Monday);
            Assert.Equal(new DateTime(2024, 11, 4), day_monday_monday);

            //��֤��ǰ���������գ������ڱ��µ�һ����һ֮������
            var sunday_monday = new DateTime(2024, 11, 30, 4, 10, 10);
            var day_sunday_monday = sunday_monday.GetFirstDayOfWeekDateTimeInMonth(DayOfWeek.Monday);
            Assert.Equal(new DateTime(2024, 11, 4), day_sunday_monday);


            //��֤��ǰ���������壬�����ڱ��µ�һ������֮ǰ�����
            var friday_sunday = new DateTime(2024, 11, 1, 14, 10, 10);
            var day_friday_sunday = friday_sunday.GetFirstDayOfWeekDateTimeInMonth(DayOfWeek.Sunday);
            Assert.Equal(new DateTime(2024, 11, 3), day_friday_sunday);

            //��֤��ǰ�����Ǳ��µ�һ�����յ����
            var sunday_sunday = new DateTime(2024, 11, 30, 4, 10, 10);
            var day_sunday_sunday = sunday_sunday.GetFirstDayOfWeekDateTimeInMonth(DayOfWeek.Sunday);
            Assert.Equal(new DateTime(2024, 11, 3), day_sunday_sunday);

            //��֤��ǰ��������һ�������ڱ��µ�һ������֮������
            var monday_sunday = new DateTime(2024, 11, 4, 4, 10, 10);
            var day_monday_sunday = monday_sunday.GetFirstDayOfWeekDateTimeInMonth(DayOfWeek.Sunday);
            Assert.Equal(new DateTime(2024, 11, 3), day_monday_sunday);
        }

        [Fact]
        public void GetLastDayOfWeekInMonthh()
        {
            var date1 = new DateTime(2024, 11, 1, 14, 10, 10);
            var day1 = date1.GetLastDayOfWeekDateTimeInMonth(DayOfWeek.Monday);
            Assert.Equal(new DateTime(2024, 11, 25), day1);
            var date17 = new DateTime(2024, 11, 4, 4, 10, 10);
            var day17 = date17.GetLastDayOfWeekDateTimeInMonth(DayOfWeek.Monday);
            Assert.Equal(new DateTime(2024, 11, 25), day17);
            var date30 = new DateTime(2024, 11, 30, 4, 10, 10);
            var day30 = date30.GetLastDayOfWeekDateTimeInMonth(DayOfWeek.Monday);
            Assert.Equal(new DateTime(2024, 11, 25), day30);


            var date11 = new DateTime(2024, 11, 1, 14, 10, 10);
            var day11 = date11.GetLastDayOfWeekDateTimeInMonth(DayOfWeek.Sunday);
            Assert.Equal(new DateTime(2024, 11, 24), day11);
            var date171 = new DateTime(2024, 11, 4, 4, 10, 10);
            var day171 = date171.GetLastDayOfWeekDateTimeInMonth(DayOfWeek.Sunday);
            Assert.Equal(new DateTime(2024, 11, 24), day171);
            var date301 = new DateTime(2024, 11, 30, 4, 10, 10);
            var day301 = date301.GetLastDayOfWeekDateTimeInMonth(DayOfWeek.Sunday);
            Assert.Equal(new DateTime(2024, 11, 24), day301);
        }

        [Fact]
        public void GetPreviousDayDateTimeOfWeek()
        {
            //��֤��ǰ��������һ������һ����һ����һ�µ����
            var monday = new DateTime(2024, 11, 1, 14, 10, 10);
            var day_monday = monday.GetPreviousDayDateTimeOfWeek(DayOfWeek.Monday);
            Assert.Equal(new DateTime(2024, 10, 28), day_monday);

            //��֤��ǰ��������һ������һ����һ�ڵ��µ����
            var monday1 = new DateTime(2024, 11, 25, 14, 10, 10);
            var day_monday1 = monday1.GetPreviousDayDateTimeOfWeek(DayOfWeek.Monday);
            Assert.Equal(new DateTime(2024, 11, 18), day_monday1);

            //��֤��ǰ���������գ�����һ����һ�ڵ��µ����
            var sunday = new DateTime(2024, 11, 24, 4, 10, 10);
            var day_sunday = sunday.GetPreviousDayDateTimeOfWeek(DayOfWeek.Monday);
            Assert.Equal(new DateTime(2024, 11, 18), day_sunday);

            //��֤��ǰ�����������������ǵ������һ������
            var saturday = new DateTime(2024, 11, 30, 4, 10, 10);
            var day_saturday = saturday.GetPreviousDayDateTimeOfWeek(DayOfWeek.Monday);
            Assert.Equal(new DateTime(2024, 11, 25), day_saturday);
        }


        [Fact]
        public void GetNextDayDateTimeOfWeek()
        {
            //Sunday = 0,
            //Monday = 1,
            //Tuesday = 2,
            //Wednesday = 3,
            //Thursday = 4,
            //Friday = 5,
            //Saturday = 6

            var date1 = new DateTime(2024, 11, 1, 14, 10, 10);
            var day1 = date1.GetNextDayDateTimeOfWeek(DayOfWeek.Monday);
            Assert.Equal(new DateTime(2024, 11, 4), day1);
            var date3 = new DateTime(2024, 11, 3, 14, 10, 10);
            var day3 = date3.GetNextDayDateTimeOfWeek(DayOfWeek.Monday);
            Assert.Equal(new DateTime(2024, 11, 4), day3);
            var date17 = new DateTime(2024, 11, 4, 4, 10, 10);
            var day17 = date17.GetNextDayDateTimeOfWeek(DayOfWeek.Monday);
            Assert.Equal(new DateTime(2024, 11, 11), day17);
            var date30 = new DateTime(2024, 11, 30, 4, 10, 10);
            var day30 = date30.GetNextDayDateTimeOfWeek(DayOfWeek.Monday);
            Assert.Equal(new DateTime(2024, 12, 2), day30);
        }

        [Fact]
        public void GetWeekOfMonth()
        {
            //��֤��ǰ���������壬���ǵ��µ�һ������
            var friday = new DateTime(2024, 11, 1, 14, 10, 10);
            var day_friday = friday.GetWeekOfMonth();
            Assert.Equal(1, day_friday);

            //��֤��ǰ���������գ����ڵ��µ�һ�ܵ����
            var sunday = new DateTime(2024, 11, 3, 14, 10, 10);
            var day_sunday = sunday.GetWeekOfMonth();
            Assert.Equal(1, day_sunday);

            //��֤��ǰ��������һ�����ڵ��µ����ܵ����
            var monday = new DateTime(2024, 11, 11, 4, 10, 10);
            var day_monday = monday.GetWeekOfMonth();
            Assert.Equal(3, day_monday);

            //��֤��ǰ���������գ����ڵ��µ����ܵ����
            var date17 = new DateTime(2024, 11, 17, 4, 10, 10);
            var day17 = date17.GetWeekOfMonth();
            Assert.Equal(3, day17);

            //��֤��ǰ���������������ǵ������һ������
            var sunday1 = new DateTime(2024, 11, 30, 4, 10, 10);
            var day_sunday1 = sunday1.GetWeekOfMonth();
            Assert.Equal(5, day_sunday1);
        }

        [Fact]
        public void GetWeekOfYear()
        {
            var date1 = new DateTime(2024, 11, 1, 14, 10, 10);
            var day1 = date1.GetWeekOfYear();
            Assert.Equal(44, day1);
            var date3 = new DateTime(2024, 11, 7, 14, 10, 10);
            var day3 = date3.GetWeekOfYear();
            Assert.Equal(45, day3);
            var date17 = new DateTime(2024, 11, 11, 4, 10, 10);
            var day17 = date17.GetWeekOfYear();
            Assert.Equal(46, day17);
            var date30 = new DateTime(2024, 11, 30, 4, 10, 10);
            var day30 = date30.GetWeekOfYear();
            Assert.Equal(48, day30);
        }
        [Fact]
        public void GetWeeksInMonth()
        {
            var date1 = new DateTime(2024, 9, 1, 14, 10, 10);
            var day1 = date1.GetWeeksInMonth();
            Assert.Equal(6, day1);
            var date3 = new DateTime(2024, 12, 7, 14, 10, 10);
            var day3 = date3.GetWeeksInMonth();
            Assert.Equal(6, day3);
            var date17 = new DateTime(2024, 10, 11, 4, 10, 10);
            var day17 = date17.GetWeeksInMonth();
            Assert.Equal(5, day17);
            var date30 = new DateTime(2024, 2, 29, 4, 10, 10);
            var day30 = date30.GetWeeksInMonth();
            Assert.Equal(5, day30);
        }
    }
}







