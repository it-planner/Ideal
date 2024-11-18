using Ideal.Core.Common.Extensions;
using System;
using System.ComponentModel;
using Xunit;

namespace Ideal.Core.Common.Test
{
    //正常枚举
    internal enum StatusEnum
    {
        [Description("正常")]
        Normal = 0,

        [Description("待机")]
        Standby = 1,

        [Description("离线")]
        Offline = 2,

        Online = 3,

        Fault = 4,
    }

    //位标志枚举
    [Flags]
    internal enum TypeFlagsEnum
    {
        [Description("Http协议")]
        Http = 1,

        [Description("Udp协议")]
        Udp = 2,

        [Description("Http协议,Udp协议")]
        HttpAndUdp = 3,

        [Description("Tcp协议")]
        Tcp = 4,

        [Description("Mqtt,Http协议")]
        Mqtt = 8,
    }

    /// <summary>
    /// 枚举相关扩展方法（string类型）
    /// </summary>
    public partial class EnumUnitTest
    {
        #region 根据枚举名称转换

        [Fact]
        public void ToEnumByName()
        {
            //正常枚举名称字符串，成功转换成枚举
            var status = "Standby".ToEnumByName<StatusEnum>();
            Assert.Equal(StatusEnum.Standby, status);

            //不存在的枚举名称字符串，返回空
            var isStatusNull = "StandbyNull".ToEnumByName<StatusEnum>();
            Assert.Null(isStatusNull);

            //整数类型的字符串，返回空
            var isStatusNullInt = "3".ToEnumByName<StatusEnum>();
            Assert.Null(isStatusNullInt);

            //正常位标志枚举名称字符串，成功转换成枚举
            var flags = "HttpAndUdp".ToEnumByName<TypeFlagsEnum>();
            Assert.Equal(TypeFlagsEnum.HttpAndUdp, flags);

            //不存在的位标志枚举名称字符串，返回空
            var isFlagsNull = "HttpAndUdpNull".ToEnumByName<TypeFlagsEnum>();
            Assert.Null(isFlagsNull);

            //正常的位标志枚举名称组合字符串，成功转换成枚举
            var flagsGroup = "Http,Tcp".ToEnumByName<TypeFlagsEnum>();
            Assert.Equal(TypeFlagsEnum.Http | TypeFlagsEnum.Tcp, flagsGroup);

            //不存在的位标志枚举名称组合字符串，返回空
            var isFlagsGroupNull = "Http,Tcp,Null".ToEnumByName<TypeFlagsEnum>();
            Assert.Null(isFlagsGroupNull);
        }

        [Fact]
        public void ToEnumOrDefaultByName()
        {
            //正常枚举名称字符串，成功转换成枚举
            var status = "Standby".ToEnumOrDefaultByName(StatusEnum.Normal);
            Assert.Equal(StatusEnum.Standby, status);

            //不存在的枚举名称字符串，返回指定默认值
            var statusDefault = "StandbyNull".ToEnumOrDefaultByName(StatusEnum.Standby);
            Assert.Equal(StatusEnum.Standby, statusDefault);
        }

        [Fact]
        public void ToEnumValueByName()
        {
            //正常枚举名称字符串，成功转换成枚举值
            var status = "Standby".ToEnumValueByName<StatusEnum>();
            Assert.Equal(1, status);

            //不存在的枚举名称字符串，返回空
            var isStatusNull = "StandbyNull".ToEnumValueByName<StatusEnum>();
            Assert.Null(isStatusNull);

            //正常位标志枚举名称字符串，成功转换成枚举值
            var flags = "HttpAndUdp".ToEnumValueByName<TypeFlagsEnum>();
            Assert.Equal(3, flags);

            //正常的位标志枚举名称组合字符串，成功转换成枚举值
            var flagsGroup = "Http,Udp".ToEnumValueByName<TypeFlagsEnum>();
            Assert.Equal(3, flagsGroup);

            //不存在的位标志枚举名称字符串，返回空
            var isFlagsNull = "HttpUdp".ToEnumValueByName<TypeFlagsEnum>();
            Assert.Null(isFlagsNull);
        }

        [Fact]
        public void ToEnumValueByNameGeneric()
        {
            //正常枚举名称字符串，成功转换成枚举值
            var status = "Standby".ToEnumValueByName<StatusEnum, byte>();
            Assert.Equal((byte)1, status);

            //正常位标志枚举名称字符串，成功转换成枚举值
            var flags = "HttpAndUdp".ToEnumValueByName<TypeFlagsEnum, long>();
            Assert.Equal(3, flags);

            //double不是有效的枚举类型
            var exception = Assert.Throws<InvalidOperationException>(() => "HttpAndUdp".ToEnumValueByName<TypeFlagsEnum, double>());
            Assert.Equal("TValue must be of type byte, sbyte, short, ushort, int, uint, long, or ulong.", exception.Message);
        }

        [Fact]
        public void ToEnumValueOrDefaultByName()
        {
            //正常枚举名称字符串，成功转换成枚举值
            var status = "Standby".ToEnumValueOrDefaultByName<StatusEnum>(2);
            Assert.Equal(1, status);

            //不存在的枚举名称字符串，返回指定默认值
            var statusDefault = "StandbyNull".ToEnumValueOrDefaultByName<StatusEnum>(2);
            Assert.Equal(2, statusDefault);
        }

        [Fact]
        public void ToEnumValueOrDefaultByNameGeneric()
        {
            //正常枚举名称字符串，成功转换成枚举值
            var status = "Standby".ToEnumValueOrDefaultByName<StatusEnum, byte>(2);
            Assert.Equal((byte)1, status);

            //正常枚举名称字符串，成功转换成枚举值
            var status1 = "Standby".ToEnumValueOrDefaultByName<StatusEnum, long>(2);
            Assert.Equal(1, status1);

            //不存在的枚举名称字符串，返回指定默认值
            var statusDefault = "StandbyNull".ToEnumValueOrDefaultByName<StatusEnum, long>(2);
            Assert.Equal(2, statusDefault);
        }

        [Fact]
        public void ToEnumDescByName()
        {
            //正常位标志枚举名称字符串，成功转换成枚举描述
            var flags = "HttpAndUdp".ToEnumDescByName<TypeFlagsEnum>();
            Assert.Equal("Http协议,Udp协议", flags);

            //正常的位标志枚举名称组合字符串，组合项存在，成功转换成枚举描述
            var flagsGroup = "Http,Udp".ToEnumDescByName<TypeFlagsEnum>();
            Assert.Equal("Http协议,Udp协议", flagsGroup);

            //正常的位标志枚举名称组合字符串，组合项不存在，成功转换成枚举描述
            var flagsGroup1 = "Http,Tcp".ToEnumDescByName<TypeFlagsEnum>();
            Assert.Equal("Http协议,Tcp协议", flagsGroup1);
        }

        [Fact]
        public void ToEnumDescOrDefaultByName()
        {
            //正常枚举名称字符串，成功转换成枚举描述
            var status = "Standby".ToEnumDescOrDefaultByName<StatusEnum>("测试");
            Assert.Equal("待机", status);

            //不存在的枚举名称字符串，返回指定默认枚举描述
            var statusDefault = "StandbyNull".ToEnumDescOrDefaultByName<StatusEnum>("测试");
            Assert.Equal("测试", statusDefault);
        }
        #endregion

        #region 根据枚举描述转换

        [Fact]
        public void ToEnumByDesc()
        {
            //正常枚举描述字符串，成功转换成枚举
            var status = "待机".ToEnumByDesc<StatusEnum>();
            Assert.Equal(StatusEnum.Standby, status);

            //如果枚举项没有枚举描述，则枚举名称字符串，成功转换成枚举
            var statusNotDesc = "Online".ToEnumByDesc<StatusEnum>();
            Assert.Equal(StatusEnum.Online, statusNotDesc);

            //不存在的枚举描述字符串，返回空
            var isStatusNull = "待机无".ToEnumByDesc<StatusEnum>();
            Assert.Null(isStatusNull);

            //正常位标志枚举描述字符串，成功转换成枚举
            var flags = "Http协议,Udp协议".ToEnumByDesc<TypeFlagsEnum>();
            Assert.Equal(TypeFlagsEnum.HttpAndUdp, flags);

            //不存在的位标志枚举描述字符串转换，返回空
            var isFlagsNull = "Http协议Udp协议".ToEnumByDesc<TypeFlagsEnum>();
            Assert.Null(isFlagsNull);

            //正常的位标志枚举名称字符串，但是包含组合分隔符[,]，成功转换成枚举描述
            var flags1 = "Mqtt,Http协议".ToEnumByDesc<TypeFlagsEnum>();
            Assert.Equal(TypeFlagsEnum.Mqtt, flags1);

            //不是完全正常的位标志枚举名称组合字符串，包含了正确项，返回空
            var flags2 = "Mqtt2,Http协议".ToEnumByDesc<TypeFlagsEnum>();
            Assert.Null(flags2);
        }

        [Fact]
        public void ToEnumOrDefaultByDesc()
        {
            //正常枚举描述字符串，成功转换成枚举
            var status = "待机".ToEnumOrDefaultByDesc(StatusEnum.Offline);
            Assert.Equal(StatusEnum.Standby, status);

            //不存在的枚举描述字符串，返回指定默认值
            var statusDefault = "待机无".ToEnumOrDefaultByDesc(StatusEnum.Offline);
            Assert.Equal(StatusEnum.Offline, statusDefault);
        }

        [Fact]
        public void ToEnumValueByDesc()
        {
            //正常枚举名称字符串，成功转换成枚举值
            var status = "待机".ToEnumValueByDesc<StatusEnum>();
            Assert.Equal(1, status);

            //不存在的枚举名称字符串，返回空
            var isStatusNull = "待机Null".ToEnumValueByDesc<StatusEnum>();
            Assert.Null(isStatusNull);

            //正常位标志枚举名称字符串，成功转换成枚举值
            var flags = "Http协议,Udp协议".ToEnumValueByDesc<TypeFlagsEnum>();
            Assert.Equal(3, flags);

            //正常的位标志枚举名称组合字符串，成功转换成枚举值
            var flagsGroup = "Http协议,Udp协议".ToEnumValueByDesc<TypeFlagsEnum>();
            Assert.Equal(3, flagsGroup);

            //不存在的位标志枚举名称字符串，返回空
            var isFlagsNull = "HttpUdp协议".ToEnumValueByDesc<TypeFlagsEnum>();
            Assert.Null(isFlagsNull);
        }

        [Fact]
        public void ToEnumValueByDescGeneric()
        {
            //正常枚举名称字符串，成功转换成枚举值
            var status = "待机".ToEnumValueByDesc<StatusEnum, byte>();
            Assert.Equal((byte)1, status);

            //正常位标志枚举名称字符串，成功转换成枚举值
            var flags = "Http协议,Udp协议".ToEnumValueByDesc<TypeFlagsEnum, long>();
            Assert.Equal(3, flags);
        }

        [Fact]
        public void ToEnumValueOrDefaultByDesc()
        {
            //正常枚举名称字符串，成功转换成枚举值
            var status = "待机".ToEnumValueOrDefaultByDesc<StatusEnum>(2);
            Assert.Equal(1, status);

            //不存在的枚举名称字符串，返回指定默认值
            var statusDefault = "待机Null".ToEnumValueOrDefaultByDesc<StatusEnum>(2);
            Assert.Equal(2, statusDefault);
        }

        [Fact]
        public void ToEnumValueOrDefaultByDescGeneric()
        {
            //正常枚举名称字符串，成功转换成枚举值
            var status = "待机".ToEnumValueOrDefaultByDesc<StatusEnum, byte>(2);
            Assert.Equal((byte)1, status);

            //不存在的枚举名称字符串，返回指定默认值
            var statusDefault = "待机Null".ToEnumValueOrDefaultByDesc<StatusEnum, long>(2);
            Assert.Equal(2, statusDefault);
        }

        [Fact]
        public void ToEnumNameByDesc()
        {
            //正常位标志枚举名称字符串，成功转换成枚举描述
            var flags = "Http协议,Udp协议".ToEnumNameByDesc<TypeFlagsEnum>();
            Assert.Equal("HttpAndUdp", flags);

            ////正常的位标志枚举名称组合字符串，组合项存在，成功转换成枚举描述
            //var flagsGroup = "Http协议,Udp协议".ToEnumNameByDesc<TypeFlagsEnum>();
            //Assert.Equal("Http,Udp", flagsGroup);

            //正常的位标志枚举名称组合字符串，组合项不存在，成功转换成枚举描述
            var flagsGroup1 = "Http协议,Tcp协议".ToEnumNameByDesc<TypeFlagsEnum>();
            //注意：逗号后面有一个空格，这个是官方控制的
            Assert.Equal("Http, Tcp", flagsGroup1);

        }

        [Fact]
        public void ToEnumNameOrDefaultByDesc()
        {
            //正常枚举名称字符串，成功转换成枚举描述
            var status = "待机".ToEnumNameOrDefaultByDesc<StatusEnum>("测试");
            Assert.Equal("Standby", status);

            //不存在的枚举名称字符串，返回指定默认枚举描述
            var statusDefault = "待机Null".ToEnumNameOrDefaultByDesc<StatusEnum>("测试");
            Assert.Equal("测试", statusDefault);
        }
        #endregion
    }

    public partial class EnumUnitTest
    {
        [Fact]
        public void ToEnumByValue()
        {
            //正常枚举值，成功转换成枚举
            var status = 1.ToEnumByValue<StatusEnum>();
            Assert.Equal(StatusEnum.Standby, status);

            //不存在的枚举值，但是可以通过枚举项按位或合并得到，返回空
            var isStatusNull = 5.ToEnumByValue<StatusEnum>();
            Assert.Null(isStatusNull);

            //不存在的枚举值，也不可以通过枚举项按位或合并得到，返回空
            var isStatusNull1 = 8.ToEnumByValue<StatusEnum>();
            Assert.Null(isStatusNull1);

            //正常位标志枚举值，成功转换成枚举
            var flags = 3.ToEnumByValue<TypeFlagsEnum>();
            Assert.Equal(TypeFlagsEnum.HttpAndUdp, flags);

            //不存在的枚举值，但是可以通过枚举项按位或合并得到，成功转换成枚举
            var flagsGroup = 5.ToEnumByValue<TypeFlagsEnum>();
            Assert.Equal(TypeFlagsEnum.Http | TypeFlagsEnum.Tcp, flagsGroup);

            //不存在的枚举值，也不可以通过枚举项按位或合并得到，返回空
            var isFlagsNull = 81.ToEnumByValue<TypeFlagsEnum>();
            Assert.Null(isFlagsNull);
        }

        [Fact]
        public void ToEnumOrDefaultByValue()
        {
            //正常枚举值，成功转换成枚举
            var status = 1.ToEnumOrDefaultByValue(StatusEnum.Offline);
            Assert.Equal(StatusEnum.Standby, status);

            //不存在的枚举值，返回指定默认枚举
            var statusDefault = 5.ToEnumOrDefaultByValue(StatusEnum.Offline);
            Assert.Equal(StatusEnum.Offline, statusDefault);
        }

        [Fact]
        public void ToEnumNameByValue()
        {
            //正常枚举值，成功转换成枚举名称
            var status = 1.ToEnumNameByValue<StatusEnum>();
            Assert.Equal("Standby", status);

            //不存在的枚举值，返回空
            var isStatusNull = 10.ToEnumNameByValue<StatusEnum>();
            Assert.Null(isStatusNull);

            //正常位标志枚举值，成功转换成枚举名称
            var flags = 3.ToEnumNameByValue<TypeFlagsEnum>();
            Assert.Equal("HttpAndUdp", flags);

            //不存在的位标志枚举值，返回空
            var isFlagsNull = 20.ToEnumNameByValue<TypeFlagsEnum>();
            Assert.Null(isFlagsNull);
        }

        [Fact]
        public void ToEnumNameOrDefaultByValue()
        {
            //正常枚举值，成功转换成枚举名称
            var status = 1.ToEnumNameOrDefaultByValue<StatusEnum>("离线");
            Assert.Equal("Standby", status);

            //不存在的枚举名值，返回指定默认枚举名称
            var statusDefault = 12.ToEnumNameOrDefaultByValue<StatusEnum>("离线");
            Assert.Equal("离线", statusDefault);
        }

        [Fact]
        public void ToEnumDescByValue()
        {
            //正常位标志枚举值，成功转换成枚举描述
            var flags = 3.ToEnumDescByValue<TypeFlagsEnum>();
            Assert.Equal("Http协议,Udp协议", flags);

            //正常的位标志枚举值，组合项不存在，成功转换成枚举描述
            var flagsGroup1 = 5.ToEnumDescByValue<TypeFlagsEnum>();
            Assert.Equal("Http协议,Tcp协议", flagsGroup1);
        }

        [Fact]
        public void ToEnumDescOrDefaultByValue()
        {
            //正常枚举值，成功转换成枚举描述
            var status = 1.ToEnumDescOrDefaultByValue<StatusEnum>("测试");
            Assert.Equal("待机", status);

            //不存在的枚举值，返回指定默认枚举描述
            var statusDefault = 11.ToEnumDescOrDefaultByValue<StatusEnum>("测试");
            Assert.Equal("测试", statusDefault);
        }

    }

    public partial class EnumUnitTest
    {
        [Fact]
        public void ToEnumDesc()
        {
            //正常枚举项，成功转换成枚举描述
            var status = StatusEnum.Offline.ToEnumDesc();
            Assert.Equal("离线", status);

            //正常枚举项，成功转换成枚举描述，没有枚举描述则为枚举名称
            var status1 = StatusEnum.Online.ToEnumDesc();
            Assert.Equal("Online", status1);

            //正常位标志枚举项，成功转换成枚举描述
            var flags = TypeFlagsEnum.Udp.ToEnumDesc();
            Assert.Equal("Udp协议", flags);

            //正常位标志枚举项组合，成功转换成枚举描述
            var flags_default = (TypeFlagsEnum.Http | TypeFlagsEnum.Tcp).ToEnumDesc();
            Assert.Equal("Http协议,Tcp协议", flags_default);
        }


        [Fact]
        public void ToEnumValue()
        {
            //正常枚举项，成功转换成枚举值
            var status = StatusEnum.Offline.ToEnumValue();
            Assert.Equal(2, status);

            //正常位标志枚举项组合，成功转换成枚举值
            var flags = (TypeFlagsEnum.Http | TypeFlagsEnum.Tcp).ToEnumValue();
            Assert.Equal(5, flags);
        }

        [Fact]
        public void ToEnumValueGeneric()
        {
            //正常枚举项，成功转换成指定类型枚举值
            var status1 = StatusEnum.Offline.ToEnumValue<sbyte>();
            Assert.Equal(2, status1);

            //正常枚举项，成功转换成指定类型枚举值
            var status2 = StatusEnum.Offline.ToEnumValue<long>();
            Assert.Equal(2, status2);


            //正常位标志枚举项组合，成功转换成指定类型枚举值
            var flags1 = (TypeFlagsEnum.Http | TypeFlagsEnum.Tcp).ToEnumValue<short>();
            Assert.Equal(5, flags1);

            //double不是有效的枚举类型
            var exception = Assert.Throws<InvalidOperationException>(() => TypeFlagsEnum.HttpAndUdp.ToEnumValue<DateTime>());
            Assert.Equal("TValue must be of type byte, sbyte, short, ushort, int, uint, long, or ulong.", exception.Message);
        }

        [Fact]
        public void ToEnumNameDescs()
        {
            //正常枚举，成功转换成键值对(枚举描述-枚举名称)
            var status1 = typeof(StatusEnum).ToEnumNameDescs();
            Assert.Equal(new Dictionary<string, string> {
                { "Normal","正常"},
                { "Standby","待机"},
                { "Offline","离线"},
                { "Online","Online"},
                { "Fault","Fault"},
            }, status1);


            //正常位标志枚举，成功转换成键值对(枚举描述-枚举名称)
            var flags = typeof(TypeFlagsEnum).ToEnumNameDescs();
            Assert.Equal(new Dictionary<string, string> {
                { "Http","Http协议"},
                { "Udp","Udp协议"},
                { "HttpAndUdp","Http协议,Udp协议"},
                { "Tcp","Tcp协议"},
                { "Mqtt","Mqtt,Http协议"},
            }, flags);


            //double不是有效的枚举类型
            var exception = Assert.Throws<InvalidOperationException>(() => typeof(double).ToEnumNameDescs());
            Assert.Equal("Type must be an enum type.", exception.Message);
        }

        [Fact]
        public void ToEnumDescNames()
        {
            //正常枚举，成功转换成键值对(枚举描述-枚举名称)
            var status1 = typeof(StatusEnum).ToEnumDescNames();
            Assert.Equal(new Dictionary<string, string> {
                { "正常","Normal"},
                { "待机","Standby"},
                { "离线","Offline"},
                { "Online","Online"},
                { "Fault","Fault"},
            }, status1);


            //正常位标志枚举，成功转换成键值对(枚举描述-枚举名称)
            var flags = typeof(TypeFlagsEnum).ToEnumDescNames();
            Assert.Equal(new Dictionary<string, string> {
                { "Http协议","Http"},
                { "Udp协议","Udp"},
                { "Http协议,Udp协议","HttpAndUdp"},
                { "Tcp协议","Tcp"},
                { "Mqtt,Http协议","Mqtt"},
            }, flags);
        }

        [Fact]
        public void ToEnumValueDescs()
        {
            //正常枚举，成功转换成键值对(枚举值-枚举描述)
            var status1 = typeof(StatusEnum).ToEnumValueDescs();
            Assert.Equal(new Dictionary<int, string> {
                { 0,"正常"},
                { 1,"待机"},
                { 2,"离线"},
                { 3,"Online"},
                { 4,"Fault"},
            }, status1);


            //正常位标志枚举，成功转换成键值对(枚举值-枚举描述)
            var flags = typeof(TypeFlagsEnum).ToEnumValueDescs();
            Assert.Equal(new Dictionary<int, string> {
                { 1,"Http协议"},
                { 2,"Udp协议"},
                { 3,"Http协议,Udp协议"},
                { 4,"Tcp协议"},
                { 8,"Mqtt,Http协议"},
            }, flags);
        }

        [Fact]
        public void ToEnumValueDescsGeneric()
        {
            //正常枚举，成功转换成键值对(枚举值-枚举描述)
            var status1 = typeof(StatusEnum).ToEnumValueDescs<ulong>();
            Assert.Equal(new Dictionary<ulong, string> {
                { 0,"正常"},
                { 1,"待机"},
                { 2,"离线"},
                { 3,"Online"},
                { 4,"Fault"},
            }, status1);


            //正常位标志枚举，成功转换成键值对(枚举值-枚举描述)
            var flags = typeof(TypeFlagsEnum).ToEnumValueDescs<ushort>();
            Assert.Equal(new Dictionary<ushort, string> {
                { 1,"Http协议"},
                { 2,"Udp协议"},
                { 3,"Http协议,Udp协议"},
                { 4,"Tcp协议"},
                { 8,"Mqtt,Http协议"},
            }, flags);
        }

        [Fact]
        public void ToEnumDescValues()
        {
            //正常枚举，成功转换成键值对(枚举描述-枚举值)
            var status1 = typeof(StatusEnum).ToEnumDescValues();
            Assert.Equal(new Dictionary<string, int> {
                { "正常",0},
                { "待机",1},
                { "离线",2},
                { "Online",3},
                { "Fault",4},
            }, status1);


            //正常位标志枚举，成功转换成键值对(枚举描述-枚举值)
            var flags = typeof(TypeFlagsEnum).ToEnumDescValues();
            Assert.Equal(new Dictionary<string, int> {
                { "Http协议",1},
                { "Udp协议",2},
                { "Http协议,Udp协议",3},
                { "Tcp协议",4},
                { "Mqtt,Http协议",8},
            }, flags);
        }

        [Fact]
        public void ToEnumDescValuesGeneric()
        {
            //正常枚举，成功转换成键值对(枚举描述-枚举值)
            var status1 = typeof(StatusEnum).ToEnumDescValues<sbyte>();
            Assert.Equal(new Dictionary<string, sbyte> {
                { "正常",0},
                { "待机",1},
                { "离线",2},
                { "Online",3},
                { "Fault",4},
            }, status1);


            //正常位标志枚举，成功转换成键值对(枚举描述-枚举值)
            var flags = typeof(TypeFlagsEnum).ToEnumDescValues<short>();
            Assert.Equal(new Dictionary<string, short> {
                { "Http协议",1},
                { "Udp协议",2},
                { "Http协议,Udp协议",3},
                { "Tcp协议",4},
                { "Mqtt,Http协议",8},
            }, flags);
        }

        [Fact]
        public void GetEnumNameValues()
        {
            //正常枚举，成功转换成键值对(枚举名称-枚举值)
            var status1 = typeof(StatusEnum).GetEnumNameValues();
            Assert.Equal(new Dictionary<string, int> {
                { "Normal",0},
                { "Standby", 1 },
                { "Offline", 2 },
                { "Online", 3 },
                { "Fault", 4 },
            }, status1);


            //正常位标志枚举，成功转换成键值对(枚举名称-枚举值)
            var flags = typeof(TypeFlagsEnum).GetEnumNameValues();
            Assert.Equal(new Dictionary<string, int> {
                { "Http",1},
                { "Udp", 2 },
                { "HttpAndUdp", 3 },
                { "Tcp", 4 },
                { "Mqtt", 8 },
            }, flags);
        }

        [Fact]
        public void GetEnumNameValuesGeneric()
        {
            //正常枚举，成功转换成键值对(枚举名称-枚举值)
            var status1 = typeof(StatusEnum).GetEnumNameValues<uint>();
            Assert.Equal(new Dictionary<string, uint> {
                { "Normal",0},
                { "Standby", 1 },
                { "Offline", 2 },
                { "Online", 3 },
                { "Fault", 4 },
            }, status1);


            //正常位标志枚举，成功转换成键值对(枚举名称-枚举值)
            var flags = typeof(TypeFlagsEnum).GetEnumNameValues<ulong>();
            Assert.Equal(new Dictionary<string, ulong> {
                { "Http",1},
                { "Udp", 2 },
                { "HttpAndUdp", 3 },
                { "Tcp", 4 },
                { "Mqtt", 8 },
            }, flags);
        }

        [Fact]
        public void GetEnumValueNames()
        {
            //正常枚举，成功转换成键值对(枚举值-枚举名称)
            var status1 = typeof(StatusEnum).GetEnumValueNames();
            Assert.Equal(new Dictionary<int, string> {
                { 0,"Normal"},
                { 1,"Standby"},
                { 2,"Offline"},
                { 3,"Online"},
                { 4,"Fault"},
            }, status1);


            //正常位标志枚举，成功转换成键值对(枚举值-枚举名称)
            var flags = typeof(TypeFlagsEnum).GetEnumValueNames();
            Assert.Equal(new Dictionary<int, string> {
                { 1,"Http"},
                { 2,"Udp"},
                { 3,"HttpAndUdp"},
                { 4,"Tcp"},
                { 8,"Mqtt"},
            }, flags);
        }

        [Fact]
        public void GetEnumValueNamesGeneric()
        {
            //正常枚举，成功转换成键值对(枚举值-枚举名称)
            var status1 = typeof(StatusEnum).GetEnumValueNames<byte>();
            Assert.Equal(new Dictionary<byte, string> {
                { 0,"Normal"},
                { 1,"Standby"},
                { 2,"Offline"},
                { 3,"Online"},
                { 4,"Fault"},
            }, status1);


            //正常位标志枚举，成功转换成键值对(枚举值-枚举名称)
            var flags = typeof(TypeFlagsEnum).GetEnumValueNames<long>();
            Assert.Equal(new Dictionary<long, string> {
                { 1,"Http"},
                { 2,"Udp"},
                { 3,"HttpAndUdp"},
                { 4,"Tcp"},
                { 8,"Mqtt"},
            }, flags);
        }
    }
}
