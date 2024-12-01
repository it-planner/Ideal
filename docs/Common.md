
# 日期时间扩展
主要包括时间戳、字符串与日期时间、日期、时间之间转换，以及特殊日期时间的获取。

## 时间戳转日期时间
### 转本地日期时间
~~~
//时间戳（秒）转本地时间
public static TimeOnly ToLocalTimeTimeBySeconds(this long timestamp)

//时间戳（毫秒）转本地时间
public static TimeOnly ToLocalTimeTimeByMilliseconds(this long timestamp)

//时间戳（秒）转本地日期
public static DateOnly ToLocalTimeDateBySeconds(this long timestamp)

//时间戳（毫秒）转本地日期
public static DateOnly ToLocalTimeDateByMilliseconds(this long timestamp)

//时间戳（秒）转本地日期时间
public static DateTime ToLocalTimeDateTimeBySeconds(this long timestamp)

//时间戳（毫秒）转本地日期时间
public static DateTime ToLocalTimeDateTimeByMilliseconds(this long timestamp)
~~~
### 转UTC日期时间
~~~
//时间戳（秒）转UTC时间
public static TimeOnly ToUniversalTimeTimeBySeconds(this long timestamp)
        
//时间戳（毫秒）转UTC时间
public static TimeOnly ToUniversalTimeTimeByMilliseconds(this long timestamp)
        
//时间戳（秒）转UTC日期
public static DateOnly ToUniversalTimeDateBySeconds(this long timestamp)
        
//时间戳（毫秒）转UTC日期
public static DateOnly ToUniversalTimeDateByMilliseconds(this long timestamp)
        
//时间戳（秒）转UTC日期时间
public static DateTime ToUniversalTimeDateTimeBySeconds(this long timestamp)
        
//时间戳（毫秒）转UTC日期时间
public static DateTime ToUniversalTimeDateTimeByMilliseconds(this long timestamp)
~~~

## 字符串转日期时间
~~~
//字符串转日期时间，转换失败则返回空
public static DateTime? ToDateTime(this string source)
        
//字符串转日期时间，转换失败则返回默认值
public static DateTime ToDateTimeOrDefault(this string source, DateTime dateTime)
        
//字符串转日期，转换失败则返回空
public static DateOnly? ToDateOnly(this string source)
        
//字符串转日期，转换失败则返回默认日期
public static DateOnly ToDateOnlyOrDefault(this string source, DateOnly dateOnly)
        
//字符串转时间，转换失败则返回空
public static TimeOnly? ToTimeOnly(this string source)
        
//字符串转时间，转换失败则返回默认时间
public static TimeOnly ToTimeOnlyOrDefault(this string source, TimeOnly timeOnly)
~~~

## 日期时间操作
### 转换
~~~
//日期时间转时间戳（秒）
public static long ToUnixTimestampBySeconds(this DateTime dateTime)
        
//日期时间转时间戳（毫秒）
public static long ToUnixTimestampByMilliseconds(this DateTime dateTime)
        
//日期时间中日期部分+时间转日期时间
public static DateTime ToDateTime(this DateTime dateTime, TimeOnly timeOnly)
        
//日期+日期时间中时间部分转为日期时间
public static DateTime ToDateTime(this DateTime dateTime, DateOnly dateOnly)
        
//日期时间转日期，保留日期时间中日期部分
public static DateOnly ToDateOnly(this DateTime dateTime)
        
//日期时间转时间，保留日期时间中时间部分
public static TimeOnly ToTimeOnly(this DateTime dateTime)
~~~
### 获取 天相关
~~~
//获取当天的开始时间
public static DateTime GetStartDateTimeOfDay(this DateTime dateTime)
        
//获取当天的结束时间
public static DateTime GetEndDateTimeOfDay(this DateTime dateTime)
~~~
### 获取 周相关
~~~
//判断当前日期是否是周末
public static bool IsWeekend(this DateTime dateTime)
        
//获取当前日期所在周的第一天（周一）
public static DateTime GetFirstDayDateTimeOfWeek(this DateTime dateTime)
        
//获取当前日期所在周的最后一天（周日）
public static DateTime GetLastDayDateTimeOfWeek(this DateTime dateTime)
        
//获取当前日期上一个指定星期几
public static DateTime GetPreviousDayDateTimeOfWeek(this DateTime dateTime, DayOfWeek dayOfWeek)
        
//获取当前日期下一个最近指定星期几
public static DateTime GetNextDayDateTimeOfWeek(this DateTime dateTime, DayOfWeek dayOfWeek)
~~~
### 获取 月相关
~~~
//获取当前日期是其所在月的第几周
public static int GetWeekOfMonth(this DateTime dateTime)
        
//获取当前日期所在月份的周数
public static int GetWeeksInMonth(this DateTime dateTime)
        
//获取当前日期所在月的第一天
public static DateTime GetFirstDayDateTimeOfMonth(this DateTime dateTime)
        
//获取当前日期所在月的最后一天
public static DateTime GetLastDayDateTimeOfMonth(this DateTime dateTime)
        
//获取当前日期所在月的第一个指定星期几
public static DateTime GetFirstDayOfWeekDateTimeInMonth(this DateTime dateTime, DayOfWeek dayOfWeek)
        
//获取当前日期所在月的最后一个指定星期几
public static DateTime GetLastDayOfWeekDateTimeInMonth(this DateTime dateTime, DayOfWeek dayOfWeek)
~~~
### 获取 季相关
~~~
//获取当前日期所在季度
public static int GetQuarter(this DateTime dateTime)

//获取当前日期所在季度的第一天
public static DateTime GetFirstDayDateTimeOfQuarter(this DateTime dateTime)

//获取当前日期所在季度的最后一天
public static DateTime GetLastDayDateTimeOfQuarter(this DateTime dateTime)
~~~
### 获取 年相关
~~~
//判断当前日期所在年是否是闰年
public static bool IsLeapYear(this DateTime dateTime)

//获取当前日期是其所在年的第几周（ISO 8601 标准）
public static int GetWeekOfYear(this DateTime dateTime)

//获取当前日期所在年的第一天
public static DateTime GetFirstDayDateTimeOfYear(this DateTime dateTime)

//获取当前日期所在年的最后一天
public static DateTime GetLastDayDateTimeOfYear(this DateTime dateTime)
~~~

## 日期、时间转日期时间
~~~
//时间转日期时间，默认使用系统当前日期+时间转为日期时间格式
public static DateTime ToDateTime(this TimeOnly timeOnly)

//日期+时间转为日期时间
public static DateTime ToDateTime(this TimeOnly timeOnly, DateOnly dateOnly)

//日期时间中日期部分+时间转日期时间
public static DateTime ToDateTime(this TimeOnly timeOnly, DateTime dateTime)

//日期转日期时间，日期+默认使用系统当前时间转为日期时间格式
public static DateTime ToDateTime(this DateOnly dateOnly)

//日期+日期时间中时间部分转日期时间
public static DateTime ToDateTime(this DateOnly dateOnly, DateTime dateTime)
~~~
# 枚举扩展
主要包括枚举值、枚举名称、枚举描述之间相互转换及获取。
## 枚举名称转换
~~~
//根据枚举名称转换成枚举，转换失败则返回空
var status = "Standby".ToEnumByName<StatusEnum>();

//根据枚举名称转换成枚举，转换失败则返回默认枚举
var status = "Standby".ToEnumOrDefaultByName(StatusEnum.Normal);)

//根据枚举名称转换成枚举值，转换失败则返回空
 var status = "Standby".ToEnumValueByName<StatusEnum>();

//根据枚举名称转换成枚举值，转换失败则返回空
var status = "Standby".ToEnumValueByName<StatusEnum, byte>();

//根据枚举名称转换成枚举值，转换失败则返回默认枚举值
var status = "Standby".ToEnumValueOrDefaultByName<StatusEnum>(2);

//根据枚举名称转换成枚举值，转换失败则返回默认枚举值
var status = "Standby".ToEnumValueOrDefaultByName<StatusEnum, byte>(2);

//根据枚举名称转换成枚举描述，转换失败则返回空
var status = "Standby".ToEnumDescByName<StatusEnum>();

//根据枚举名称转换成枚举描述，转换失败则返回默认枚举描述
var status = "Standby".ToEnumDescOrDefaultByName<StatusEnum>("测试");
~~~
## 枚举描述转换
~~~
//根据枚举描述转换成枚举，转换失败返回空
var status = "待机".ToEnumByDesc<StatusEnum>();

//根据枚举描述转换成枚举，转换失败返回默认枚举
var status = "待机".ToEnumOrDefaultByDesc(StatusEnum.Offline);

//根据枚举描述转换成枚举值，转换失败则返回空
var status = "待机".ToEnumValueByDesc<StatusEnum>();

//根据枚举描述转换成枚举值，转换失败则返回空
var status = "待机".ToEnumValueByDesc<StatusEnum, byte>();

//根据枚举描述转换成枚举值，转换失败则返回默认枚举值
var status = "待机".ToEnumValueOrDefaultByDesc<StatusEnum>(2);

//根据枚举名称转换成枚举值，转换失败则返回默认枚举值
var status = "待机".ToEnumValueOrDefaultByDesc<StatusEnum, byte>(2);

//根据枚举描述转换成枚举名称，转换失败则返回空
var status = "待机".ToEnumNameByDesc<StatusEnum>();

//根据枚举描述转换成枚举名称，转换失败则返回默认枚举描述
var status = "待机".ToEnumNameOrDefaultByDesc<StatusEnum>("测试");
~~~
## 枚举值转换
支持sbyte、byte、short、ushort、int、uint、long、ulong八种类型，每种类型支持的方法相同，下面以int为例。
~~~
//根据枚举值转换成枚举，转换失败则返回空
var status = 1.ToEnumByValue<StatusEnum>();

//根据枚举值转换成枚举，转换失败则返回默认枚举
var status = 1.ToEnumOrDefaultByValue(StatusEnum.Offline);

//根据枚举值转换成枚举名称，转换失败则返回空
var status = 1.ToEnumNameByValue<StatusEnum>();

//根据枚举值转换成枚举名称，转换失败则返回默认枚举名称
var status = 1.ToEnumNameOrDefaultByValue<StatusEnum>("离线");

//根据枚举值转换成枚举描述，转换失败则返回空
var status = 1.ToEnumDescByValue<StatusEnum>();

//根据枚举值转换成枚举描述，转换失败则返回默认枚举描述
var status = 1.ToEnumDescOrDefaultByValue<StatusEnum>("测试");
~~~
## 枚举项转换
~~~
//枚举项转枚举值
var status = StatusEnum.Offline.ToEnumValue();
var flags = (TypeFlagsEnum.Http | TypeFlagsEnum.Tcp).ToEnumValue();

//枚举项转枚举值
var status = StatusEnum.Offline.ToEnumValue<sbyte>();
var flags = (TypeFlagsEnum.Http | TypeFlagsEnum.Tcp).ToEnumValue<short>();

//枚举项转枚举描述(Descripion)。
//支持位域，如果是位域组合值，多个值按分隔符[,]组合。
var status = StatusEnum.Offline.ToEnumDesc();
var flags = (TypeFlagsEnum.Http | TypeFlagsEnum.Tcp).ToEnumDesc();
~~~
## 枚举信息获取
~~~
//获取枚举值+枚举名称
var status = typeof(StatusEnum).GetEnumValueNames();

//获取枚举值+枚举名称
var status = typeof(StatusEnum).GetEnumValueNames<byte>();

//获取枚举值+枚举描述
var status = typeof(StatusEnum).ToEnumValueDescs();

//获取枚举值+枚举描述
var status = typeof(StatusEnum).ToEnumValueDescs<ulong>();

//获取枚举名称+枚举值
var status = typeof(StatusEnum).GetEnumNameValues();

//获取枚举名称+枚举值
var status = typeof(StatusEnum).GetEnumNameValues<uint>();

//获取枚举名称+枚举描述
var status = typeof(StatusEnum).ToEnumNameDescs();

//获取枚举描述+枚举值
var status = typeof(StatusEnum).ToEnumDescValues();

//获取枚举描述+枚举值
var status = typeof(StatusEnum).ToEnumDescValues<sbyte>();

//获取枚举描述+枚举名称
var status = typeof(StatusEnum).ToEnumDescNames();
~~~

# 表格扩展
主要包括表格的快速创建，对象表格互相转换等。

## 创建表格
### 根据列名-类型键值对创建表格
~~~
var table = TableHelper.Create(["A", "B"]);
~~~

### 根据列名数组创建表格
~~~
var columns = new Dictionary<string, Type>
            {
                { "A", typeof(string) },
                { "B", typeof(int) }
            };

var table = TableHelper.Create(columns);
~~~

### 根据类创建表格
类既可以是类，也可以是结构体，并且只支持属性并不支持字段。
如果属性指定Description特性，则特性值用于和表格列名关联，否则使用属性名称和表格列名关联。
~~~
public struct Student<T>
{
    [Description("标识")]
    public string Id { get; set; }
    [Description("姓名")]
    public string Name { get; set; }
    public T Age { get; set; }
}

var table = TableHelper.Create<Student<double>>();
~~~

## 转换
### 把表格转换为对象集合
~~~
var students = TableHelper.ToModels<Student<double>>(table);
~~~

### 把对象集合转为表格
~~~
var table = TableHelper.ToDataTable<Student<double>>(students, "学生表");
~~~

### 把一维数组作为一列转换为表格
~~~
var table = TableHelper.ToDataTableWithColumnArray<string>(["A", "B"], "学生表");
~~~

### 把一维数组作为一行转换为表格
~~~
var table = TableHelper.ToDataTableWithRowArray<string>(["A", "B"]);
~~~

### 行列转置
~~~
var table = TableHelper.Transpose(originalTable, true);
~~~