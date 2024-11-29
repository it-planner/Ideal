using Ideal.Core.Common.Extensions;
using System.ComponentModel;
using Xunit;

namespace Ideal.Core.Common.Test
{
    //����ö��
    internal enum StatusEnum
    {
        [Description("����")]
        Normal = 0,

        [Description("����")]
        Standby = 1,

        [Description("����")]
        Offline = 2,

        Online = 3,

        Fault = 4,
    }

    //λ��־ö��
    [Flags]
    internal enum TypeFlagsEnum
    {
        [Description("HttpЭ��")]
        Http = 1,

        [Description("UdpЭ��")]
        Udp = 2,

        [Description("HttpЭ��,UdpЭ��")]
        HttpAndUdp = 3,

        [Description("TcpЭ��")]
        Tcp = 4,

        [Description("Mqtt,HttpЭ��")]
        Mqtt = 8,
    }

    /// <summary>
    /// ö�������չ������string���ͣ�
    /// </summary>
    public partial class EnumUnitTest
    {
        #region ����ö������ת��

        [Fact]
        public void ToEnumByName()
        {
            //����ö�������ַ������ɹ�ת����ö��
            var status = "Standby".ToEnumByName<StatusEnum>();
            Assert.Equal(StatusEnum.Standby, status);

            //�����ڵ�ö�������ַ��������ؿ�
            var isStatusNull = "StandbyNull".ToEnumByName<StatusEnum>();
            Assert.Null(isStatusNull);

            //�������͵��ַ��������ؿ�
            var isStatusNullInt = "3".ToEnumByName<StatusEnum>();
            Assert.Null(isStatusNullInt);

            //����λ��־ö�������ַ������ɹ�ת����ö��
            var flags = "HttpAndUdp".ToEnumByName<TypeFlagsEnum>();
            Assert.Equal(TypeFlagsEnum.HttpAndUdp, flags);

            //�����ڵ�λ��־ö�������ַ��������ؿ�
            var isFlagsNull = "HttpAndUdpNull".ToEnumByName<TypeFlagsEnum>();
            Assert.Null(isFlagsNull);

            //������λ��־ö����������ַ������ɹ�ת����ö��
            var flagsGroup = "Http,Tcp".ToEnumByName<TypeFlagsEnum>();
            Assert.Equal(TypeFlagsEnum.Http | TypeFlagsEnum.Tcp, flagsGroup);

            //�����ڵ�λ��־ö����������ַ��������ؿ�
            var isFlagsGroupNull = "Http,Tcp,Null".ToEnumByName<TypeFlagsEnum>();
            Assert.Null(isFlagsGroupNull);
        }

        [Fact]
        public void ToEnumOrDefaultByName()
        {
            //����ö�������ַ������ɹ�ת����ö��
            var status = "Standby".ToEnumOrDefaultByName(StatusEnum.Normal);
            Assert.Equal(StatusEnum.Standby, status);

            //�����ڵ�ö�������ַ���������ָ��Ĭ��ֵ
            var statusDefault = "StandbyNull".ToEnumOrDefaultByName(StatusEnum.Standby);
            Assert.Equal(StatusEnum.Standby, statusDefault);
        }

        [Fact]
        public void ToEnumValueByName()
        {
            //����ö�������ַ������ɹ�ת����ö��ֵ
            var status = "Standby".ToEnumValueByName<StatusEnum>();
            Assert.Equal(1, status);

            //�����ڵ�ö�������ַ��������ؿ�
            var isStatusNull = "StandbyNull".ToEnumValueByName<StatusEnum>();
            Assert.Null(isStatusNull);

            //����λ��־ö�������ַ������ɹ�ת����ö��ֵ
            var flags = "HttpAndUdp".ToEnumValueByName<TypeFlagsEnum>();
            Assert.Equal(3, flags);

            //������λ��־ö����������ַ������ɹ�ת����ö��ֵ
            var flagsGroup = "Http,Udp".ToEnumValueByName<TypeFlagsEnum>();
            Assert.Equal(3, flagsGroup);

            //�����ڵ�λ��־ö�������ַ��������ؿ�
            var isFlagsNull = "HttpUdp".ToEnumValueByName<TypeFlagsEnum>();
            Assert.Null(isFlagsNull);
        }

        [Fact]
        public void ToEnumValueByNameGeneric()
        {
            //����ö�������ַ������ɹ�ת����ö��ֵ
            var status = "Standby".ToEnumValueByName<StatusEnum, byte>();
            Assert.Equal((byte)1, status);

            //����λ��־ö�������ַ������ɹ�ת����ö��ֵ
            var flags = "HttpAndUdp".ToEnumValueByName<TypeFlagsEnum, long>();
            Assert.Equal(3, flags);

            //double������Ч��ö������
            var exception = Assert.Throws<InvalidOperationException>(() => "HttpAndUdp".ToEnumValueByName<TypeFlagsEnum, double>());
            Assert.Equal("TValue must be of type byte, sbyte, short, ushort, int, uint, long, or ulong.", exception.Message);
        }

        [Fact]
        public void ToEnumValueOrDefaultByName()
        {
            //����ö�������ַ������ɹ�ת����ö��ֵ
            var status = "Standby".ToEnumValueOrDefaultByName<StatusEnum>(2);
            Assert.Equal(1, status);

            //�����ڵ�ö�������ַ���������ָ��Ĭ��ֵ
            var statusDefault = "StandbyNull".ToEnumValueOrDefaultByName<StatusEnum>(2);
            Assert.Equal(2, statusDefault);
        }

        [Fact]
        public void ToEnumValueOrDefaultByNameGeneric()
        {
            //����ö�������ַ������ɹ�ת����ö��ֵ
            var status = "Standby".ToEnumValueOrDefaultByName<StatusEnum, byte>(2);
            Assert.Equal((byte)1, status);

            //����ö�������ַ������ɹ�ת����ö��ֵ
            var status1 = "Standby".ToEnumValueOrDefaultByName<StatusEnum, long>(2);
            Assert.Equal(1, status1);

            //�����ڵ�ö�������ַ���������ָ��Ĭ��ֵ
            var statusDefault = "StandbyNull".ToEnumValueOrDefaultByName<StatusEnum, long>(2);
            Assert.Equal(2, statusDefault);
        }

        [Fact]
        public void ToEnumDescByName()
        {
            //����λ��־ö�������ַ������ɹ�ת����ö������
            var flags = "HttpAndUdp".ToEnumDescByName<TypeFlagsEnum>();
            Assert.Equal("HttpЭ��,UdpЭ��", flags);

            //������λ��־ö����������ַ������������ڣ��ɹ�ת����ö������
            var flagsGroup = "Http,Udp".ToEnumDescByName<TypeFlagsEnum>();
            Assert.Equal("HttpЭ��,UdpЭ��", flagsGroup);

            //������λ��־ö����������ַ������������ڣ��ɹ�ת����ö������
            var flagsGroup1 = "Http,Tcp".ToEnumDescByName<TypeFlagsEnum>();
            Assert.Equal("HttpЭ��,TcpЭ��", flagsGroup1);
        }

        [Fact]
        public void ToEnumDescOrDefaultByName()
        {
            //����ö�������ַ������ɹ�ת����ö������
            var status = "Standby".ToEnumDescOrDefaultByName<StatusEnum>("����");
            Assert.Equal("����", status);

            //�����ڵ�ö�������ַ���������ָ��Ĭ��ö������
            var statusDefault = "StandbyNull".ToEnumDescOrDefaultByName<StatusEnum>("����");
            Assert.Equal("����", statusDefault);
        }
        #endregion

        #region ����ö������ת��

        [Fact]
        public void ToEnumByDesc()
        {
            //����ö�������ַ������ɹ�ת����ö��
            var status = "����".ToEnumByDesc<StatusEnum>();
            Assert.Equal(StatusEnum.Standby, status);

            //���ö����û��ö����������ö�������ַ������ɹ�ת����ö��
            var statusNotDesc = "Online".ToEnumByDesc<StatusEnum>();
            Assert.Equal(StatusEnum.Online, statusNotDesc);

            //�����ڵ�ö�������ַ��������ؿ�
            var isStatusNull = "������".ToEnumByDesc<StatusEnum>();
            Assert.Null(isStatusNull);

            //����λ��־ö�������ַ������ɹ�ת����ö��
            var flags = "HttpЭ��,UdpЭ��".ToEnumByDesc<TypeFlagsEnum>();
            Assert.Equal(TypeFlagsEnum.HttpAndUdp, flags);

            //�����ڵ�λ��־ö�������ַ���ת�������ؿ�
            var isFlagsNull = "HttpЭ��UdpЭ��".ToEnumByDesc<TypeFlagsEnum>();
            Assert.Null(isFlagsNull);

            //������λ��־ö�������ַ��������ǰ�����Ϸָ���[,]���ɹ�ת����ö������
            var flags1 = "Mqtt,HttpЭ��".ToEnumByDesc<TypeFlagsEnum>();
            Assert.Equal(TypeFlagsEnum.Mqtt, flags1);

            //������ȫ������λ��־ö����������ַ�������������ȷ����ؿ�
            var flags2 = "Mqtt2,HttpЭ��".ToEnumByDesc<TypeFlagsEnum>();
            Assert.Null(flags2);
        }

        [Fact]
        public void ToEnumOrDefaultByDesc()
        {
            //����ö�������ַ������ɹ�ת����ö��
            var status = "����".ToEnumOrDefaultByDesc(StatusEnum.Offline);
            Assert.Equal(StatusEnum.Standby, status);

            //�����ڵ�ö�������ַ���������ָ��Ĭ��ֵ
            var statusDefault = "������".ToEnumOrDefaultByDesc(StatusEnum.Offline);
            Assert.Equal(StatusEnum.Offline, statusDefault);
        }

        [Fact]
        public void ToEnumValueByDesc()
        {
            //����ö�������ַ������ɹ�ת����ö��ֵ
            var status = "����".ToEnumValueByDesc<StatusEnum>();
            Assert.Equal(1, status);

            //�����ڵ�ö�������ַ��������ؿ�
            var isStatusNull = "����Null".ToEnumValueByDesc<StatusEnum>();
            Assert.Null(isStatusNull);

            //����λ��־ö�������ַ������ɹ�ת����ö��ֵ
            var flags = "HttpЭ��,UdpЭ��".ToEnumValueByDesc<TypeFlagsEnum>();
            Assert.Equal(3, flags);

            //������λ��־ö����������ַ������ɹ�ת����ö��ֵ
            var flagsGroup = "HttpЭ��,UdpЭ��".ToEnumValueByDesc<TypeFlagsEnum>();
            Assert.Equal(3, flagsGroup);

            //�����ڵ�λ��־ö�������ַ��������ؿ�
            var isFlagsNull = "HttpUdpЭ��".ToEnumValueByDesc<TypeFlagsEnum>();
            Assert.Null(isFlagsNull);
        }

        [Fact]
        public void ToEnumValueByDescGeneric()
        {
            //����ö�������ַ������ɹ�ת����ö��ֵ
            var status = "����".ToEnumValueByDesc<StatusEnum, byte>();
            Assert.Equal((byte)1, status);

            //����λ��־ö�������ַ������ɹ�ת����ö��ֵ
            var flags = "HttpЭ��,UdpЭ��".ToEnumValueByDesc<TypeFlagsEnum, long>();
            Assert.Equal(3, flags);
        }

        [Fact]
        public void ToEnumValueOrDefaultByDesc()
        {
            //����ö�������ַ������ɹ�ת����ö��ֵ
            var status = "����".ToEnumValueOrDefaultByDesc<StatusEnum>(2);
            Assert.Equal(1, status);

            //�����ڵ�ö�������ַ���������ָ��Ĭ��ֵ
            var statusDefault = "����Null".ToEnumValueOrDefaultByDesc<StatusEnum>(2);
            Assert.Equal(2, statusDefault);
        }

        [Fact]
        public void ToEnumValueOrDefaultByDescGeneric()
        {
            //����ö�������ַ������ɹ�ת����ö��ֵ
            var status = "����".ToEnumValueOrDefaultByDesc<StatusEnum, byte>(2);
            Assert.Equal((byte)1, status);

            //�����ڵ�ö�������ַ���������ָ��Ĭ��ֵ
            var statusDefault = "����Null".ToEnumValueOrDefaultByDesc<StatusEnum, long>(2);
            Assert.Equal(2, statusDefault);
        }

        [Fact]
        public void ToEnumNameByDesc()
        {
            //����λ��־ö�������ַ������ɹ�ת����ö������
            var flags = "HttpЭ��,UdpЭ��".ToEnumNameByDesc<TypeFlagsEnum>();
            Assert.Equal("HttpAndUdp", flags);

            ////������λ��־ö����������ַ������������ڣ��ɹ�ת����ö������
            //var flagsGroup = "HttpЭ��,UdpЭ��".ToEnumNameByDesc<TypeFlagsEnum>();
            //Assert.Equal("Http,Udp", flagsGroup);

            //������λ��־ö����������ַ������������ڣ��ɹ�ת����ö������
            var flagsGroup1 = "HttpЭ��,TcpЭ��".ToEnumNameByDesc<TypeFlagsEnum>();
            //ע�⣺���ź�����һ���ո�����ǹٷ����Ƶ�
            Assert.Equal("Http, Tcp", flagsGroup1);

        }

        [Fact]
        public void ToEnumNameOrDefaultByDesc()
        {
            //����ö�������ַ������ɹ�ת����ö������
            var status = "����".ToEnumNameOrDefaultByDesc<StatusEnum>("����");
            Assert.Equal("Standby", status);

            //�����ڵ�ö�������ַ���������ָ��Ĭ��ö������
            var statusDefault = "����Null".ToEnumNameOrDefaultByDesc<StatusEnum>("����");
            Assert.Equal("����", statusDefault);
        }
        #endregion
    }

    public partial class EnumUnitTest
    {
        [Fact]
        public void ToEnumByValue()
        {
            //����ö��ֵ���ɹ�ת����ö��
            var status = 1.ToEnumByValue<StatusEnum>();
            Assert.Equal(StatusEnum.Standby, status);

            //�����ڵ�ö��ֵ�����ǿ���ͨ��ö���λ��ϲ��õ������ؿ�
            var isStatusNull = 5.ToEnumByValue<StatusEnum>();
            Assert.Null(isStatusNull);

            //�����ڵ�ö��ֵ��Ҳ������ͨ��ö���λ��ϲ��õ������ؿ�
            var isStatusNull1 = 8.ToEnumByValue<StatusEnum>();
            Assert.Null(isStatusNull1);

            //����λ��־ö��ֵ���ɹ�ת����ö��
            var flags = 3.ToEnumByValue<TypeFlagsEnum>();
            Assert.Equal(TypeFlagsEnum.HttpAndUdp, flags);

            //�����ڵ�ö��ֵ�����ǿ���ͨ��ö���λ��ϲ��õ����ɹ�ת����ö��
            var flagsGroup = 5.ToEnumByValue<TypeFlagsEnum>();
            Assert.Equal(TypeFlagsEnum.Http | TypeFlagsEnum.Tcp, flagsGroup);

            //�����ڵ�ö��ֵ��Ҳ������ͨ��ö���λ��ϲ��õ������ؿ�
            var isFlagsNull = 81.ToEnumByValue<TypeFlagsEnum>();
            Assert.Null(isFlagsNull);
        }

        [Fact]
        public void ToEnumOrDefaultByValue()
        {
            //����ö��ֵ���ɹ�ת����ö��
            var status = 1.ToEnumOrDefaultByValue(StatusEnum.Offline);
            Assert.Equal(StatusEnum.Standby, status);

            //�����ڵ�ö��ֵ������ָ��Ĭ��ö��
            var statusDefault = 5.ToEnumOrDefaultByValue(StatusEnum.Offline);
            Assert.Equal(StatusEnum.Offline, statusDefault);
        }

        [Fact]
        public void ToEnumNameByValue()
        {
            //����ö��ֵ���ɹ�ת����ö������
            var status = 1.ToEnumNameByValue<StatusEnum>();
            Assert.Equal("Standby", status);

            //�����ڵ�ö��ֵ�����ؿ�
            var isStatusNull = 10.ToEnumNameByValue<StatusEnum>();
            Assert.Null(isStatusNull);

            //����λ��־ö��ֵ���ɹ�ת����ö������
            var flags = 3.ToEnumNameByValue<TypeFlagsEnum>();
            Assert.Equal("HttpAndUdp", flags);

            //�����ڵ�λ��־ö��ֵ�����ؿ�
            var isFlagsNull = 20.ToEnumNameByValue<TypeFlagsEnum>();
            Assert.Null(isFlagsNull);
        }

        [Fact]
        public void ToEnumNameOrDefaultByValue()
        {
            //����ö��ֵ���ɹ�ת����ö������
            var status = 1.ToEnumNameOrDefaultByValue<StatusEnum>("����");
            Assert.Equal("Standby", status);

            //�����ڵ�ö����ֵ������ָ��Ĭ��ö������
            var statusDefault = 12.ToEnumNameOrDefaultByValue<StatusEnum>("����");
            Assert.Equal("����", statusDefault);
        }

        [Fact]
        public void ToEnumDescByValue()
        {
            //����λ��־ö��ֵ���ɹ�ת����ö������
            var flags = 3.ToEnumDescByValue<TypeFlagsEnum>();
            Assert.Equal("HttpЭ��,UdpЭ��", flags);

            //������λ��־ö��ֵ���������ڣ��ɹ�ת����ö������
            var flagsGroup1 = 5.ToEnumDescByValue<TypeFlagsEnum>();
            Assert.Equal("HttpЭ��,TcpЭ��", flagsGroup1);
        }

        [Fact]
        public void ToEnumDescOrDefaultByValue()
        {
            //����ö��ֵ���ɹ�ת����ö������
            var status = 1.ToEnumDescOrDefaultByValue<StatusEnum>("����");
            Assert.Equal("����", status);

            //�����ڵ�ö��ֵ������ָ��Ĭ��ö������
            var statusDefault = 11.ToEnumDescOrDefaultByValue<StatusEnum>("����");
            Assert.Equal("����", statusDefault);
        }

    }

    public partial class EnumUnitTest
    {
        [Fact]
        public void ToEnumDesc()
        {
            //����ö����ɹ�ת����ö������
            var status = StatusEnum.Offline.ToEnumDesc();
            Assert.Equal("����", status);

            //����ö����ɹ�ת����ö��������û��ö��������Ϊö������
            var status1 = StatusEnum.Online.ToEnumDesc();
            Assert.Equal("Online", status1);

            //����λ��־ö����ɹ�ת����ö������
            var flags = TypeFlagsEnum.Udp.ToEnumDesc();
            Assert.Equal("UdpЭ��", flags);

            //����λ��־ö������ϣ��ɹ�ת����ö������
            var flags_default = (TypeFlagsEnum.Http | TypeFlagsEnum.Tcp).ToEnumDesc();
            Assert.Equal("HttpЭ��,TcpЭ��", flags_default);
        }


        [Fact]
        public void ToEnumValue()
        {
            //����ö����ɹ�ת����ö��ֵ
            var status = StatusEnum.Offline.ToEnumValue();
            Assert.Equal(2, status);

            //����λ��־ö������ϣ��ɹ�ת����ö��ֵ
            var flags = (TypeFlagsEnum.Http | TypeFlagsEnum.Tcp).ToEnumValue();
            Assert.Equal(5, flags);
        }

        [Fact]
        public void ToEnumValueGeneric()
        {
            //����ö����ɹ�ת����ָ������ö��ֵ
            var status1 = StatusEnum.Offline.ToEnumValue<sbyte>();
            Assert.Equal(2, status1);

            //����ö����ɹ�ת����ָ������ö��ֵ
            var status2 = StatusEnum.Offline.ToEnumValue<long>();
            Assert.Equal(2, status2);


            //����λ��־ö������ϣ��ɹ�ת����ָ������ö��ֵ
            var flags1 = (TypeFlagsEnum.Http | TypeFlagsEnum.Tcp).ToEnumValue<short>();
            Assert.Equal(5, flags1);

            //double������Ч��ö������
            var exception = Assert.Throws<InvalidOperationException>(() => TypeFlagsEnum.HttpAndUdp.ToEnumValue<DateTime>());
            Assert.Equal("TValue must be of type byte, sbyte, short, ushort, int, uint, long, or ulong.", exception.Message);
        }

        [Fact]
        public void ToEnumNameDescs()
        {
            //����ö�٣��ɹ�ת���ɼ�ֵ��(ö������-ö������)
            var status1 = typeof(StatusEnum).ToEnumNameDescs();
            Assert.Equal(new Dictionary<string, string> {
                { "Normal","����"},
                { "Standby","����"},
                { "Offline","����"},
                { "Online","Online"},
                { "Fault","Fault"},
            }, status1);


            //����λ��־ö�٣��ɹ�ת���ɼ�ֵ��(ö������-ö������)
            var flags = typeof(TypeFlagsEnum).ToEnumNameDescs();
            Assert.Equal(new Dictionary<string, string> {
                { "Http","HttpЭ��"},
                { "Udp","UdpЭ��"},
                { "HttpAndUdp","HttpЭ��,UdpЭ��"},
                { "Tcp","TcpЭ��"},
                { "Mqtt","Mqtt,HttpЭ��"},
            }, flags);


            //double������Ч��ö������
            var exception = Assert.Throws<InvalidOperationException>(() => typeof(double).ToEnumNameDescs());
            Assert.Equal("Type must be an enum type.", exception.Message);
        }

        [Fact]
        public void ToEnumDescNames()
        {
            //����ö�٣��ɹ�ת���ɼ�ֵ��(ö������-ö������)
            var status1 = typeof(StatusEnum).ToEnumDescNames();
            Assert.Equal(new Dictionary<string, string> {
                { "����","Normal"},
                { "����","Standby"},
                { "����","Offline"},
                { "Online","Online"},
                { "Fault","Fault"},
            }, status1);


            //����λ��־ö�٣��ɹ�ת���ɼ�ֵ��(ö������-ö������)
            var flags = typeof(TypeFlagsEnum).ToEnumDescNames();
            Assert.Equal(new Dictionary<string, string> {
                { "HttpЭ��","Http"},
                { "UdpЭ��","Udp"},
                { "HttpЭ��,UdpЭ��","HttpAndUdp"},
                { "TcpЭ��","Tcp"},
                { "Mqtt,HttpЭ��","Mqtt"},
            }, flags);
        }

        [Fact]
        public void ToEnumValueDescs()
        {
            //����ö�٣��ɹ�ת���ɼ�ֵ��(ö��ֵ-ö������)
            var status1 = typeof(StatusEnum).ToEnumValueDescs();
            Assert.Equal(new Dictionary<int, string> {
                { 0,"����"},
                { 1,"����"},
                { 2,"����"},
                { 3,"Online"},
                { 4,"Fault"},
            }, status1);


            //����λ��־ö�٣��ɹ�ת���ɼ�ֵ��(ö��ֵ-ö������)
            var flags = typeof(TypeFlagsEnum).ToEnumValueDescs();
            Assert.Equal(new Dictionary<int, string> {
                { 1,"HttpЭ��"},
                { 2,"UdpЭ��"},
                { 3,"HttpЭ��,UdpЭ��"},
                { 4,"TcpЭ��"},
                { 8,"Mqtt,HttpЭ��"},
            }, flags);
        }

        [Fact]
        public void ToEnumValueDescsGeneric()
        {
            //����ö�٣��ɹ�ת���ɼ�ֵ��(ö��ֵ-ö������)
            var status1 = typeof(StatusEnum).ToEnumValueDescs<ulong>();
            Assert.Equal(new Dictionary<ulong, string> {
                { 0,"����"},
                { 1,"����"},
                { 2,"����"},
                { 3,"Online"},
                { 4,"Fault"},
            }, status1);


            //����λ��־ö�٣��ɹ�ת���ɼ�ֵ��(ö��ֵ-ö������)
            var flags = typeof(TypeFlagsEnum).ToEnumValueDescs<ushort>();
            Assert.Equal(new Dictionary<ushort, string> {
                { 1,"HttpЭ��"},
                { 2,"UdpЭ��"},
                { 3,"HttpЭ��,UdpЭ��"},
                { 4,"TcpЭ��"},
                { 8,"Mqtt,HttpЭ��"},
            }, flags);
        }

        [Fact]
        public void ToEnumDescValues()
        {
            //����ö�٣��ɹ�ת���ɼ�ֵ��(ö������-ö��ֵ)
            var status1 = typeof(StatusEnum).ToEnumDescValues();
            Assert.Equal(new Dictionary<string, int> {
                { "����",0},
                { "����",1},
                { "����",2},
                { "Online",3},
                { "Fault",4},
            }, status1);


            //����λ��־ö�٣��ɹ�ת���ɼ�ֵ��(ö������-ö��ֵ)
            var flags = typeof(TypeFlagsEnum).ToEnumDescValues();
            Assert.Equal(new Dictionary<string, int> {
                { "HttpЭ��",1},
                { "UdpЭ��",2},
                { "HttpЭ��,UdpЭ��",3},
                { "TcpЭ��",4},
                { "Mqtt,HttpЭ��",8},
            }, flags);
        }

        [Fact]
        public void ToEnumDescValuesGeneric()
        {
            //����ö�٣��ɹ�ת���ɼ�ֵ��(ö������-ö��ֵ)
            var status1 = typeof(StatusEnum).ToEnumDescValues<sbyte>();
            Assert.Equal(new Dictionary<string, sbyte> {
                { "����",0},
                { "����",1},
                { "����",2},
                { "Online",3},
                { "Fault",4},
            }, status1);


            //����λ��־ö�٣��ɹ�ת���ɼ�ֵ��(ö������-ö��ֵ)
            var flags = typeof(TypeFlagsEnum).ToEnumDescValues<short>();
            Assert.Equal(new Dictionary<string, short> {
                { "HttpЭ��",1},
                { "UdpЭ��",2},
                { "HttpЭ��,UdpЭ��",3},
                { "TcpЭ��",4},
                { "Mqtt,HttpЭ��",8},
            }, flags);
        }

        [Fact]
        public void GetEnumNameValues()
        {
            //����ö�٣��ɹ�ת���ɼ�ֵ��(ö������-ö��ֵ)
            var status1 = typeof(StatusEnum).GetEnumNameValues();
            Assert.Equal(new Dictionary<string, int> {
                { "Normal",0},
                { "Standby", 1 },
                { "Offline", 2 },
                { "Online", 3 },
                { "Fault", 4 },
            }, status1);


            //����λ��־ö�٣��ɹ�ת���ɼ�ֵ��(ö������-ö��ֵ)
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
            //����ö�٣��ɹ�ת���ɼ�ֵ��(ö������-ö��ֵ)
            var status1 = typeof(StatusEnum).GetEnumNameValues<uint>();
            Assert.Equal(new Dictionary<string, uint> {
                { "Normal",0},
                { "Standby", 1 },
                { "Offline", 2 },
                { "Online", 3 },
                { "Fault", 4 },
            }, status1);


            //����λ��־ö�٣��ɹ�ת���ɼ�ֵ��(ö������-ö��ֵ)
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
            //����ö�٣��ɹ�ת���ɼ�ֵ��(ö��ֵ-ö������)
            var status1 = typeof(StatusEnum).GetEnumValueNames();
            Assert.Equal(new Dictionary<int, string> {
                { 0,"Normal"},
                { 1,"Standby"},
                { 2,"Offline"},
                { 3,"Online"},
                { 4,"Fault"},
            }, status1);


            //����λ��־ö�٣��ɹ�ת���ɼ�ֵ��(ö��ֵ-ö������)
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
            //����ö�٣��ɹ�ת���ɼ�ֵ��(ö��ֵ-ö������)
            var status1 = typeof(StatusEnum).GetEnumValueNames<byte>();
            Assert.Equal(new Dictionary<byte, string> {
                { 0,"Normal"},
                { 1,"Standby"},
                { 2,"Offline"},
                { 3,"Online"},
                { 4,"Fault"},
            }, status1);


            //����λ��־ö�٣��ɹ�ת���ɼ�ֵ��(ö��ֵ-ö������)
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
