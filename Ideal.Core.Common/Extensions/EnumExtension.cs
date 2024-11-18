using System;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Reflection;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Ideal.Core.Common.Extensions
{
    /// <summary>
    /// 枚举相关扩展方法（string类型）
    /// </summary>
    public static partial class EnumExtension
    {
        #region 根据枚举名称转换
        /// <summary>
        /// 根据枚举名称转换成枚举，转换失败则返回空
        /// </summary>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        /// <param name="name">枚举名称</param>
        /// <returns>枚举</returns>
        public static TEnum? ToEnumByName<TEnum>(this string name)
            where TEnum : struct, Enum
        {
            //转换成功则返回结果，否则返回空
            if (Enum.TryParse<TEnum>(name, out var result))
            {
                //检查是否为有效的枚举名称字符串，
                if (Enum.IsDefined(typeof(TEnum), name))
                {
                    //返回枚举
                    return result;
                }
                else
                {
                    //计算是否为有效的位标志组合项
                    var isValidFlags = IsValidFlagsMask<ulong, TEnum>(result.ToEnumValue<ulong>());
                    //如果是有效的位标志组合项则返回枚举，否则返回空
                    return isValidFlags ? result : default(TEnum?);
                }
            }

            //返回空
            return default;
        }

        /// <summary>
        /// 根据枚举名称转换成枚举，转换失败则返回默认枚举
        /// </summary>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        /// <param name="name">枚举名称</param>
        /// <param name="defaultValue">默认枚举</param>
        /// <returns>枚举</returns>
        public static TEnum ToEnumOrDefaultByName<TEnum>(this string name, TEnum defaultValue)
            where TEnum : struct, Enum
        {
            //调用根据枚举名称转换成枚举方法
            var result = name.ToEnumByName<TEnum>();
            if (result.HasValue)
            {
                //返回枚举
                return result.Value;
            }

            //转换失败则返回默认枚举
            return defaultValue;
        }

        /// <summary>
        /// 根据枚举名称转换成枚举值，转换失败则返回空
        /// </summary>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        /// <param name="name">枚举名称</param>
        /// <returns>枚举值</returns>
        public static int? ToEnumValueByName<TEnum>(this string name)
            where TEnum : struct, Enum
        {
            //调用根据枚举名称转换成枚举方法
            var result = name.ToEnumByName<TEnum>();
            if (result.HasValue)
            {
                //返回枚举值
                return result.Value.ToEnumValue<int>();
            }

            //转换失败则返回空
            return default;
        }

        /// <summary>
        /// 根据枚举名称转换成枚举值，转换失败则返回空
        /// </summary>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        /// <typeparam name="TValue">枚举值类型</typeparam>
        /// <param name="name">枚举名称</param>
        /// <returns>枚举值</returns>
        /// <exception cref="InvalidOperationException">TValue必须是以下类型：byte, sbyte, short, ushort, int, uint, long, ulong.</exception>
        public static TValue? ToEnumValueByName<TEnum, TValue>(this string name)
            where TEnum : struct, Enum
            where TValue : struct
        {
            //断言值类型有效
            AssertValueTypeValid<TValue>();

            //调用根据枚举名称转换成枚举方法
            var result = name.ToEnumByName<TEnum>();
            if (result.HasValue)
            {
                //返回枚举值
                return result.Value.ToEnumValue<TValue>();
            }

            //转换失败则返回空
            return default;
        }

        /// <summary>
        /// 根据枚举名称转换成枚举值，转换失败则返回默认枚举值
        /// </summary>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        /// <param name="name">枚举名称</param>
        /// <param name="defaultValue">默认枚举值</param>
        /// <returns>枚举值</returns>
        public static int ToEnumValueOrDefaultByName<TEnum>(this string name, int defaultValue)
            where TEnum : struct, Enum
        {
            //根据枚举名称转换成枚举值
            var result = name.ToEnumValueByName<TEnum>();
            if (result.HasValue)
            {
                //返回枚举值
                return result.Value;
            }

            //转换失败则返回默认枚举值
            return defaultValue;
        }

        /// <summary>
        /// 根据枚举名称转换成枚举值，转换失败则返回默认枚举值
        /// </summary>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        /// <typeparam name="TValue">枚举值类型</typeparam>
        /// <param name="name">枚举名称</param>
        /// <param name="defaultValue">默认枚举值</param>
        /// <returns>枚举值</returns>
        /// <exception cref="InvalidOperationException">TValue必须是以下类型：byte, sbyte, short, ushort, int, uint, long, ulong.</exception>
        public static TValue? ToEnumValueOrDefaultByName<TEnum, TValue>(this string name, TValue defaultValue)
            where TEnum : struct, Enum
            where TValue : struct
        {
            //根据枚举名称转换成枚举值
            var result = name.ToEnumValueByName<TEnum, TValue>();
            if (result.HasValue)
            {
                //返回枚举值
                return result.Value;
            }

            //转换失败则返回默认枚举值
            return defaultValue;
        }

        /// <summary>
        /// 根据枚举名称转换成枚举描述，转换失败则返回空
        /// </summary>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        /// <param name="name">枚举名称</param>
        /// <returns>枚举值</returns>
        public static string? ToEnumDescByName<TEnum>(this string name)
            where TEnum : struct, Enum
        {
            //调用根据枚举名称转换成枚举方法
            var result = name.ToEnumByName<TEnum>();
            if (result.HasValue)
            {
                //返回枚举描述
                return result.Value.ToEnumDesc();
            }

            //转换失败则返回空
            return default;
        }

        /// <summary>
        /// 根据枚举名称转换成枚举描述，转换失败则返回默认枚举描述
        /// </summary>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        /// <param name="name">枚举名称</param>
        /// <param name="defaultValue">默认枚举描述</param>
        /// <returns>枚举值</returns>
        public static string ToEnumDescOrDefaultByName<TEnum>(this string name, string defaultValue)
            where TEnum : struct, Enum
        {
            //调用根据枚举名称转换成枚举描述方法
            var result = name.ToEnumDescByName<TEnum>();
            if (!string.IsNullOrWhiteSpace(result))
            {
                //返回枚举描述
                return result;
            }

            //转换失败则返回默认枚举描述
            return defaultValue;
        }
        #endregion

        #region 根据枚举描述转换
        /// <summary>
        /// 根据枚举描述转换成枚举，转换失败返回空
        /// </summary>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        /// <param name="description">枚举描述</param>
        /// <returns>枚举</returns>
        public static TEnum? ToEnumByDesc<TEnum>(this string description)
            where TEnum : struct, Enum
        {
            var type = typeof(TEnum);
            var info = GetEnumTypeInfo(type);
            //不是位标志枚举的情况处理
            if (!info.IsFlags)
            {
                return ToEnumDesc<TEnum>(description);
            }

            //是位标志枚举的情况处理
            //不是组合位的情况,本身可能就包含[,]
            var tenum = ToEnumDesc<TEnum>(description);
            if (tenum.HasValue)
            {
                return tenum;
            }

            //如果不包含[,]，则直接返回
            if (!description.Contains(','))
            {
                return default;
            }

            //是组合位的情况
            var names = description.Split(',');
            var values = Enum.GetValues(type);
            //记录有效枚举描述个数
            var count = 0;
            ulong mask = 0L;
            //变量枚举所有项
            foreach (var name in names)
            {
                foreach (Enum value in values)
                {
                    //取枚举项描述与目标描述相比较，相同则返回该枚举项
                    if (value.ToEnumDesc() == name)
                    {
                        //有效枚举个数加1
                        count++;
                        //将枚举值转为long类型
                        var valueLong = Convert.ToUInt64(value);
                        // 过滤掉负数或无效的值，规范的位标志枚举应该都为非负数
                        if (valueLong >= 0)
                        {
                            //合并枚举值至mask
                            mask |= valueLong;
                        }

                        break;
                    }
                }
            }

            //如果两者不相等，说明描述字符串不是一个有效组合项
            if (count != names.Length)
            {
                return default;
            }

            var underlyingType = Enum.GetUnderlyingType(type);
            if (underlyingType == typeof(byte))
            {
                return ((byte)mask).ToEnumByValue<TEnum>();
            }
            else if (underlyingType == typeof(sbyte))
            {
                return ((sbyte)mask).ToEnumByValue<TEnum>();
            }
            else if (underlyingType == typeof(short))
            {
                return ((short)mask).ToEnumByValue<TEnum>();
            }
            else if (underlyingType == typeof(ushort))
            {
                return ((ushort)mask).ToEnumByValue<TEnum>();
            }
            else if (underlyingType == typeof(int))
            {
                return ((int)mask).ToEnumByValue<TEnum>();
            }
            else if (underlyingType == typeof(uint))
            {
                return ((uint)mask).ToEnumByValue<TEnum>();
            }
            else if (underlyingType == typeof(long))
            {
                return ((long)mask).ToEnumByValue<TEnum>();
            }
            else if (underlyingType == typeof(ulong))
            {
                return mask.ToEnumByValue<TEnum>();
            }

            return default;
        }

        /// <summary>
        /// 根据枚举描述转换成枚举，转换失败返回默认枚举
        /// </summary>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        /// <param name="description">枚举描述</param>
        /// <param name="defaultValue">默认枚举</param>
        /// <returns>枚举</returns>
        public static TEnum? ToEnumOrDefaultByDesc<TEnum>(this string description, TEnum defaultValue)
            where TEnum : struct, Enum
        {
            //调用根据枚举描述转换成枚举方法
            var result = description.ToEnumByDesc<TEnum>();
            if (result.HasValue)
            {
                //返回枚举描述
                return result.Value;
            }

            //未查到匹配描述则返回默认值
            return defaultValue;
        }

        /// <summary>
        /// 根据枚举描述转换成枚举值，转换失败则返回空
        /// </summary>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        /// <param name="description">枚举描述</param>
        /// <returns>枚举值</returns>
        public static int? ToEnumValueByDesc<TEnum>(this string description)
            where TEnum : struct, Enum
        {
            //调用根据枚举描述转换成枚举方法
            var result = description.ToEnumByDesc<TEnum>();
            if (result.HasValue)
            {
                //返回枚举值
                return result.Value.ToEnumValue<int>();
            }

            //转换失败则返回空
            return default;
        }

        /// <summary>
        /// 根据枚举描述转换成枚举值，转换失败则返回空
        /// </summary>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        /// <typeparam name="TValue">枚举值类型</typeparam>
        /// <param name="description">枚举描述</param>
        /// <returns>枚举值</returns>
        /// <exception cref="InvalidOperationException">TValue必须是以下类型：byte, sbyte, short, ushort, int, uint, long, ulong.</exception>
        public static TValue? ToEnumValueByDesc<TEnum, TValue>(this string description)
            where TEnum : struct, Enum
            where TValue : struct
        {
            //断言值类型有效
            AssertValueTypeValid<TValue>();

            //调用根据枚举名称转换成枚举方法
            var result = description.ToEnumByDesc<TEnum>();
            if (result.HasValue)
            {
                //返回枚举值
                return result.Value.ToEnumValue<TValue>();
            }

            //转换失败则返回空
            return default;
        }

        /// <summary>
        /// 根据枚举描述转换成枚举值，转换失败则返回默认枚举值
        /// </summary>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        /// <param name="description">枚举描述</param>
        /// <param name="defaultValue">默认枚举值</param>
        /// <returns>枚举值</returns>
        public static int ToEnumValueOrDefaultByDesc<TEnum>(this string description, int defaultValue)
            where TEnum : struct, Enum
        {
            //根据枚举描述转换成枚举值
            var result = description.ToEnumValueByDesc<TEnum>();
            if (result.HasValue)
            {
                //返回枚举值
                return result.Value;
            }

            //转换失败则返回默认值
            return defaultValue;
        }

        /// <summary>
        /// 根据枚举名称转换成枚举值，转换失败则返回默认枚举值
        /// </summary>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        /// <typeparam name="TValue">枚举值类型</typeparam>
        /// <param name="description">枚举描述</param>
        /// <param name="defaultValue">默认枚举值</param>
        /// <returns>枚举值</returns>
        /// <exception cref="InvalidOperationException">TValue必须是以下类型：byte, sbyte, short, ushort, int, uint, long, ulong.</exception>
        public static TValue? ToEnumValueOrDefaultByDesc<TEnum, TValue>(this string description, TValue defaultValue)
            where TEnum : struct, Enum
            where TValue : struct
        {
            //根据枚举名称转换成枚举值
            var result = description.ToEnumValueByDesc<TEnum, TValue>();
            if (result.HasValue)
            {
                //返回枚举值
                return result.Value;
            }

            //转换失败则返回默认枚举值
            return defaultValue;
        }

        /// <summary>
        /// 根据枚举描述转换成枚举名称，转换失败则返回空
        /// </summary>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        /// <param name="description">枚举描述</param>
        /// <returns>枚举名称</returns>
        public static string? ToEnumNameByDesc<TEnum>(this string description)
            where TEnum : struct, Enum
        {
            //调用根据枚举描述转换成枚举方法
            var result = description.ToEnumByDesc<TEnum>();
            if (result.HasValue)
            {
                //转为枚举名称
                return result.Value.ToString();
            }

            //转换失败则返回空
            return default;
        }

        /// <summary>
        /// 根据枚举描述转换成枚举名称，转换失败则返回默认枚举描述
        /// </summary>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        /// <param name="description">枚举描述</param>
        /// <param name="defaultValue">默认枚举描述</param>
        /// <returns>枚举名称</returns>
        public static string ToEnumNameOrDefaultByDesc<TEnum>(this string description, string defaultValue)
            where TEnum : struct, Enum
        {
            //调用根据枚举描述转换成枚举名称方法
            var result = description.ToEnumNameByDesc<TEnum>();
            if (!string.IsNullOrWhiteSpace(result))
            {
                //返回枚举名称
                return result;
            }

            //转换失败则返回默认枚举名称
            return defaultValue;
        }
        #endregion
    }

    /// <summary>
    /// 枚举相关扩展方法（type类型）
    /// </summary>
    public static partial class EnumExtension
    {
        /// <summary>
        /// 枚举项转枚举值
        /// </summary>
        /// <param name="source">枚举项</param>
        /// <returns>枚举值</returns>
        public static int ToEnumValue(this Enum source)
        {
            return Convert.ToInt32(source);
        }

        /// <summary>
        /// 枚举项转枚举值
        /// </summary>
        /// <typeparam name="TValue">枚举值</typeparam>
        /// <param name="source">枚举项</param>
        /// <returns>枚举值</returns>
        /// <exception cref="InvalidOperationException">TValue必须是以下类型：byte, sbyte, short, ushort, int, uint, long, ulong.</exception>
        public static TValue ToEnumValue<TValue>(this Enum source) 
            where TValue : struct
        {
            //断言值类型有效
            AssertValueTypeValid<TValue>();

            // 获取枚举的底层类型（byte, sbyte, short, ushort, int, uint, long, ulong）
            var underlyingType = Enum.GetUnderlyingType(source.GetType());

            // 如果底层类型是 TValue 类型，则直接转换
            if (underlyingType == typeof(TValue))
            {
                return (TValue)(object)source;
            }

            // 否则，使用 Convert.ChangeType 来进行转换
            return (TValue)Convert.ChangeType(source, typeof(TValue));
        }

        /// <summary>
        /// 枚举项转枚举描述(Descripion)。
        /// 支持位域，如果是位域组合值，多个值按分隔符[,]组合。
        /// </summary>
        /// <param name="source">枚举项</param>
        /// <returns>枚举描述</returns>
        public static string ToEnumDesc(this Enum source)
        {
            //从缓存中获取枚举描述，如果缓存没有则计算后存入缓存并返回
            return _descs.GetOrAdd(source, (key) =>
            {
                //正常枚举为一个值，位标志枚举可能是多个值组合
                var names = key.ToString().Split(',');
                var res = new string[names.Length];
                var type = key.GetType();
                //循环处理每一个值
                for (var i = 0; i < names.Length; i++)
                {
                    //通过枚举名称获取枚举项信息
                    var field = type.GetField(names[i].Trim());
                    if (field == null)
                    {
                        continue;
                    }

                    //通过枚举项信息获取描述
                    res[i] = GetFieldDescription(field);
                }

                //拼接结果
                return string.Join(',', res);
            });
        }

        /// <summary>
        /// 获取枚举值+枚举名称
        /// </summary>
        ///<param name="type">枚举的类型</param>
        /// <returns>键值对(枚举值-枚举名称)</returns>
        /// <exception cref="InvalidOperationException">type必须是枚举类型</exception>
        public static Dictionary<int, string> GetEnumValueNames(this Type type)
        {
            return GetEnumValueNames<int>(type);
        }

        /// <summary>
        /// 获取枚举值+枚举名称
        /// </summary>
        /// <typeparam name="TValue">枚举值</typeparam>
        /// <param name="type">枚举的类型</param>
        /// <returns>键值对(枚举值-枚举名称)</returns>
        /// <exception cref="InvalidOperationException">type必须是枚举类型</exception>
        public static Dictionary<TValue, string> GetEnumValueNames<TValue>(this Type type)
            where TValue : struct
        {
            //断言值类型有效
            AssertValueTypeValid<TValue>();
            //根据type获取枚举类型基础信息
            var info = GetEnumTypeInfo(type);
            //通过枚举项集合转为目标类型
            return info.Items.ToDictionary(r => r.ToEnumValue<TValue>(), r => r.ToString());
        }

        /// <summary>
        /// 获取枚举值+枚举描述
        /// </summary>
        /// <param name="type">枚举类型</param>
        /// <returns>键值对(枚举值-枚举描述)</returns>
        /// <exception cref="InvalidOperationException">type必须是枚举类型</exception>
        public static Dictionary<int, string> ToEnumValueDescs(this Type type)
        {
            return ToEnumValueDescs<int>(type);
        }

        /// <summary>
        /// 获取枚举值+枚举描述
        /// </summary>
        /// <typeparam name="TValue">枚举值</typeparam>
        /// <param name="type">枚举类型</param>
        /// <returns>键值对(枚举值-枚举描述)</returns>
        /// <exception cref="InvalidOperationException">type必须是枚举类型</exception>
        /// <exception cref="InvalidOperationException">TValue必须是以下类型：byte, sbyte, short, ushort, int, uint, long, ulong.</exception>
        public static Dictionary<TValue, string> ToEnumValueDescs<TValue>(this Type type)
            where TValue : struct
        {
            //断言值类型有效
            AssertValueTypeValid<TValue>();
            //根据type获取枚举类型基础信息
            var info = GetEnumTypeInfo(type);
            //通过枚举项集合转为目标类型
            return info.Items.ToDictionary(r => r.ToEnumValue<TValue>(), r => r.ToEnumDesc());
        }

        /// <summary>
        /// 获取枚举名称+枚举值
        /// </summary>
        /// <param name="type">枚举类型</param>
        /// <returns>键值对(枚举名称-枚举值)</returns>
        /// <exception cref="InvalidOperationException">type必须是枚举类型</exception>
        public static Dictionary<string, int> GetEnumNameValues(this Type type)
        {
            return GetEnumNameValues<int>(type);
        }

        /// <summary>
        /// 获取枚举名称+枚举值
        /// </summary>
        /// <typeparam name="TValue">枚举值</typeparam>
        /// <param name="type">枚举类型</param>
        /// <returns>键值对(枚举名称-枚举值)</returns>
        /// <exception cref="InvalidOperationException">type必须是枚举类型</exception>
        public static Dictionary<string, TValue> GetEnumNameValues<TValue>(this Type type)
            where TValue : struct
        {
            //断言值类型有效
            AssertValueTypeValid<TValue>();
            //根据type获取枚举类型基础信息
            var info = GetEnumTypeInfo(type);
            //通过枚举项集合转为目标类型
            return info.Items.ToDictionary(r => r.ToString(), r => r.ToEnumValue<TValue>());
        }

        /// <summary>
        /// 获取枚举名称+枚举描述
        /// </summary>
        /// <param name="type">枚举类型</param>
        /// <returns>键值对(枚举名称-枚举描述)</returns>
        /// <exception cref="InvalidOperationException">type必须是枚举类型</exception>
        public static Dictionary<string, string> ToEnumNameDescs(this Type type)
        {
            //根据type获取枚举类型基础信息
            var info = GetEnumTypeInfo(type);
            //通过枚举项集合转为目标类型
            return info.Items.ToDictionary(r => r.ToString(), r => r.ToEnumDesc());
        }

        /// <summary>
        /// 获取枚举描述+枚举值
        /// </summary>
        /// <param name="type">枚举类型</param>
        /// <returns>键值对(枚举描述-枚举值)</returns>
        /// <exception cref="InvalidOperationException">type必须是枚举类型</exception>
        public static Dictionary<string, int> ToEnumDescValues(this Type type)
        {
            return ToEnumDescValues<int>(type);
        }

        ///<summary>
        /// 获取枚举描述+枚举值
        /// </summary>
        /// <typeparam name="TValue">枚举值</typeparam>
        /// <param name="type">枚举类型</param>
        /// <returns>键值对(枚举描述-枚举值)</returns>
        /// <exception cref="InvalidOperationException">type必须是枚举类型</exception>
        public static Dictionary<string, TValue> ToEnumDescValues<TValue>(this Type type)
            where TValue : struct
        {
            //断言值类型有效
            AssertValueTypeValid<TValue>();
            //根据type获取枚举类型基础信息
            var info = GetEnumTypeInfo(type);
            //通过枚举项集合转为目标类型
            return info.Items.ToDictionary(r => r.ToEnumDesc(), r => r.ToEnumValue<TValue>());
        }

        /// <summary>
        /// 获取枚举描述+枚举名称
        /// </summary>
        /// <param name="type">枚举类型</param>
        /// <returns>键值对(枚举描述-枚举名称)</returns>
        /// <exception cref="InvalidOperationException">type必须是枚举类型</exception>
        public static Dictionary<string, string> ToEnumDescNames(this Type type)
        {
            //根据type获取枚举类型基础信息
            var info = GetEnumTypeInfo(type);
            //通过枚举项集合转为目标类型
            return info.Items.ToDictionary(r => r.ToEnumDesc(), r => r.ToString());
        }
    }

    /// <summary>
    /// 枚举相关扩展方法（私有信息）
    /// </summary>
    public static partial class EnumExtension
    {
        /// <summary>
        /// 枚举类型基础信息
        /// </summary>
        private sealed class EnumTypeInfo
        {
            /// <summary>
            /// 是否是位标志
            /// </summary>
            public bool IsFlags { get; set; }

            /// <summary>
            /// 枚举掩码
            /// </summary>
            public ulong Mask { get; set; }

            /// <summary>
            /// 枚举项集合
            /// </summary>
            public List<Enum> Items { get; set; } = [];
        }

        //存储枚举项对应的描述
        private static readonly ConcurrentDictionary<Enum, string> _descs = new();
        //存储枚举相关信息
        private static readonly ConcurrentDictionary<Type, EnumTypeInfo> _infos = new();

        /// <summary>
        /// 根据type获取枚举类型基础信息
        /// </summary>
        /// <param name="type">枚举类型</param>
        /// <returns>EnumTypeInfo</returns>
        /// <exception cref="InvalidOperationException">type必须是枚举类型</exception>
        private static EnumTypeInfo GetEnumTypeInfo(Type type)
        {
            //不是枚举类型则报错
            if (!type.IsEnum)
            {
                throw new InvalidOperationException("Type must be an enum type.");
            }

            //从缓存中获取枚举相关基础信息，没有则计算后存入缓存并返回
            return _infos.GetOrAdd(type, (key) =>
            {
                var info = new EnumTypeInfo()
                {
                    IsFlags = Attribute.IsDefined(key, typeof(FlagsAttribute)),
                };

                //获取枚举所有值
                var values = Enum.GetValues(key);
                //初始化存储掩码变量
                ulong mask = 0L;
                //遍历所有枚举值，通过位或运算合并所有枚举值
                foreach (Enum value in values)
                {
                    info.Items.Add(value);

                    if (info.IsFlags)
                    {
                        //将枚举值转为long类型
                        var valueLong = Convert.ToUInt64(value);
                        // 过滤掉负数或无效的值，规范的位标志枚举应该都为非负数
                        if (valueLong >= 0)
                        {
                            //合并枚举值至mask
                            mask |= valueLong;
                        }
                    }
                }

                info.Mask = mask;
                //检查枚举类型是否有Flags特性
                return info;
            });
        }

        /// <summary>
        /// 断言值类型有效
        /// </summary>
        /// <typeparam name="TValue">枚举值类型</typeparam>
        /// <exception cref="InvalidOperationException">TValue必须是以下类型：byte, sbyte, short, ushort, int, uint, long, ulong.</exception>
        private static void AssertValueTypeValid<TValue>()
        {
            var type = typeof(TValue);

            if (type != typeof(byte) &&
                type != typeof(sbyte) &&
                type != typeof(short) &&
                type != typeof(ushort) &&
                type != typeof(int) &&
                type != typeof(uint) &&
                type != typeof(long) &&
                type != typeof(ulong))
            {
                throw new InvalidOperationException("TValue must be of type byte, sbyte, short, ushort, int, uint, long, or ulong.");
            }
        }

        /// <summary>
        /// 计算是否为有效的位标志组合项
        /// </summary>
        /// <typeparam name="TValue">枚举值类型</typeparam>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        /// <param name="value">枚举值</param>
        /// <returns>是否为有效的枚举项</returns>
        private static bool IsValidFlagsMask<TValue, TEnum>(TValue value)
            where TValue : struct
            where TEnum : struct, Enum
        {
            //根据type获取枚举类型基础信息
            var info = GetEnumTypeInfo(typeof(TEnum));
            //如果不是位标志枚举则返回false
            if (!info.IsFlags)
            {
                return false;
            }

            var source = Convert.ToUInt64(value);
            //使用待验证值value和枚举掩码取反做与运算
            //结果等于0表示value为有效枚举值
            return (source & ~info.Mask) == 0;
        }


        /// <summary>
        /// 获取描述信息，如果没有描述信息则取字段名称
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        private static string GetFieldDescription(FieldInfo field)
        {
            //获取描述特性
            var attr = field.GetCustomAttribute<DescriptionAttribute>();
            //如果存在描述特性则返回，否则返回字段名称
            return attr?.Description ?? field.Name;
        }

        /// <summary>
        /// 根据枚举描述转换成枚举
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="description"></param>
        /// <returns></returns>
        private static TEnum? ToEnumDesc<TEnum>(string description) where TEnum : struct, Enum
        {
            //变量枚举所有项
            foreach (Enum value in Enum.GetValues(typeof(TEnum)))
            {
                //取枚举项描述与目标描述相比较，相同则返回该枚举项
                if (value.ToEnumDesc() == description)
                {
                    //返回枚举
                    return (TEnum)value;
                }
            }

            //未查到匹配描述则返回空
            return default;
        }

        /// <summary>
        /// 根据枚举值转换成枚举，转换失败则返回空
        /// </summary>
        /// <typeparam name="TValue">枚举值类型</typeparam>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        /// <param name="value">枚举值</param>
        /// <returns></returns>
        private static TEnum? ToEnumByValue<TValue, TEnum>(this TValue value)
            where TValue : struct
            where TEnum : struct, Enum
        {
            //检查整数值是否是有效的枚举值并且是否是有效位标志枚举组合项
            if (!Enum.IsDefined(typeof(TEnum), value) && !IsValidFlagsMask<TValue, TEnum>(value))
            {
                //非法数据则返回空
                return default;
            }

            //有效枚举值则进行转换
            return (TEnum)Enum.ToObject(typeof(TEnum), value);
        }

        /// <summary>
        /// 根据枚举值转换成枚举，转换失败则返回默认枚举
        /// </summary>
        /// <typeparam name="TValue">枚举值类型</typeparam>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        /// <param name="value">枚举值</param>
        /// <param name="defaultValue">默认枚举</param>
        /// <returns>枚举</returns>
        private static TEnum? ToEnumOrDefaultByValue<TValue, TEnum>(this TValue value, TEnum defaultValue)
            where TValue : struct
            where TEnum : struct, Enum
        {
            //调用根据枚举值转换成枚举方法
            var result = value.ToEnumByValue<TValue, TEnum>();
            if (result.HasValue)
            {
                //返回枚举
                return result.Value;
            }

            //转换失败则返回默认枚举
            return defaultValue;
        }

        /// <summary>
        /// 根据枚举值转换成枚举名称，转换失败则返回空
        /// </summary>
        /// <typeparam name="TValue">枚举值类型</typeparam>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        /// <param name="value">枚举值</param>
        /// <returns>枚举名称</returns>
        private static string? ToEnumNameByValue<TValue, TEnum>(this TValue value)
            where TValue : struct
            where TEnum : struct, Enum
        {
            //调用根据枚举值转换成枚举方法
            var result = value.ToEnumByValue<TValue, TEnum>();
            if (result.HasValue)
            {
                //返回枚举名称
                return result.Value.ToString();
            }

            //转换失败则返回空
            return default;
        }

        /// <summary>
        /// 根据枚举值转换成枚举名称，转换失败则返回默认枚举名称
        /// </summary>
        /// <typeparam name="TValue">枚举值类型</typeparam>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        /// <param name="value">枚举值</param>
        /// <param name="defaultValue">默认枚举名称</param>
        /// <returns>枚举名称</returns>
        private static string? ToEnumNameOrDefaultByValue<TValue, TEnum>(this TValue value, string defaultValue)
            where TValue : struct
            where TEnum : struct, Enum
        {
            //调用根据枚举值转换成枚举名称方法
            var result = value.ToEnumNameByValue<TValue, TEnum>();
            if (!string.IsNullOrWhiteSpace(result))
            {
                //返回枚举名称
                return result;
            }

            //转换失败则返回默认枚举名称
            return defaultValue;
        }

        /// <summary>
        /// 根据枚举值转换成枚举描述，转换失败则返回空
        /// </summary>
        /// <typeparam name="TValue">枚举值类型</typeparam>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        /// <param name="value">枚举值</param>
        /// <returns>枚举描述</returns>
        private static string? ToEnumDescByValue<TValue, TEnum>(this TValue value)
            where TValue : struct
            where TEnum : struct, Enum
        {
            //调用根据枚举值转换成枚举方法
            var result = value.ToEnumByValue<TValue, TEnum>();
            if (result.HasValue)
            {
                //返回枚举描述
                return result.Value.ToEnumDesc();
            }

            //转换失败则返回空
            return default;
        }

        /// <summary>
        /// 根据枚举值转换成枚举描述，转换失败则返回默认枚举描述
        /// </summary>
        /// <typeparam name="TValue">枚举值类型</typeparam>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        /// <param name="value">枚举值</param>
        /// <param name="defaultValue">默认枚举描述</param>
        /// <returns>枚举描述</returns>
        private static string? ToEnumDescOrDefaultByValue<TValue, TEnum>(this TValue value, string defaultValue)
            where TValue : struct
            where TEnum : struct, Enum
        {
            //调用根据枚举值转换成枚举描述方法
            var result = value.ToEnumDescByValue<TValue, TEnum>();
            if (!string.IsNullOrWhiteSpace(result))
            {
                //返回枚举描述
                return result;
            }

            //转换失败则返回默认枚举描述
            return defaultValue;
        }
    }


    #region 枚举值转枚举

    /// <summary>
    /// 枚举相关扩展方法（sbyte类型）
    /// </summary>
    public static partial class EnumExtension
    {
        /// <summary>
        /// 根据枚举值转换成枚举，转换失败则返回空
        /// </summary>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        /// <param name="value">枚举值</param>
        /// <returns>枚举</returns>
        public static TEnum? ToEnumByValue<TEnum>(this sbyte value)
            where TEnum : struct, Enum
        {
            //调用根据枚举值转换成枚举方法
            return ToEnumByValue<sbyte, TEnum>(value);
        }

        /// <summary>
        /// 根据枚举值转换成枚举，转换失败则返回默认枚举
        /// </summary>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        /// <param name="value">枚举值</param>
        /// <param name="defaultValue">默认枚举</param>
        /// <returns>枚举</returns>
        public static TEnum? ToEnumOrDefaultByValue<TEnum>(this sbyte value, TEnum defaultValue)
            where TEnum : struct, Enum
        {
            //调用根据枚举值转换成枚举方法
            return ToEnumOrDefaultByValue<sbyte, TEnum>(value, defaultValue);
        }

        /// <summary>
        /// 根据枚举值转换成枚举名称，转换失败则返回空
        /// </summary>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        /// <param name="value">枚举值</param>
        /// <returns>枚举名称</returns>
        public static string? ToEnumNameByValue<TEnum>(this sbyte value)
            where TEnum : struct, Enum
        {
            //调用根据枚举值转换成枚举名称方法
            return ToEnumNameByValue<sbyte, TEnum>(value);
        }

        /// <summary>
        /// 根据枚举值转换成枚举名称，转换失败则返回默认枚举名称
        /// </summary>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        /// <param name="value">枚举值</param>
        /// <param name="defaultValue">默认枚举名称</param>
        /// <returns>枚举名称</returns>
        public static string? ToEnumNameOrDefaultByValue<TEnum>(this sbyte value, string defaultValue)
            where TEnum : struct, Enum
        {
            //调用根据枚举值转换成枚举名称方法
            return ToEnumNameOrDefaultByValue<sbyte, TEnum>(value, defaultValue);
        }

        /// <summary>
        /// 根据枚举值转换成枚举描述，转换失败则返回空
        /// </summary>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        /// <param name="value">枚举值</param>
        /// <returns>枚举描述</returns>
        public static string? ToEnumDescByValue<TEnum>(this sbyte value)
            where TEnum : struct, Enum
        {
            //调用根据枚举值转换成枚举描述方法
            return ToEnumDescByValue<sbyte, TEnum>(value);
        }

        /// <summary>
        /// 根据枚举值转换成枚举描述，转换失败则返回默认枚举描述
        /// </summary>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        /// <param name="value">枚举值</param>
        /// <param name="defaultValue">默认枚举描述</param>
        /// <returns>枚举描述</returns>
        public static string? ToEnumDescOrDefaultByValue<TEnum>(this sbyte value, string defaultValue)
            where TEnum : struct, Enum
        {
            //调用根据枚举值转换成枚举描述方法
            return ToEnumDescOrDefaultByValue<sbyte, TEnum>(value, defaultValue);
        }
    }

    /// <summary>
    /// 枚举相关扩展方法（byte类型）
    /// </summary>
    public static partial class EnumExtension
    {
        /// <summary>
        /// 根据枚举值转换成枚举，转换失败则返回空
        /// </summary>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        /// <param name="value">枚举值</param>
        /// <returns>枚举</returns>
        public static TEnum? ToEnumByValue<TEnum>(this byte value)
            where TEnum : struct, Enum
        {
            //调用根据枚举值转换成枚举方法
            return ToEnumByValue<byte, TEnum>(value);
        }

        /// <summary>
        /// 根据枚举值转换成枚举，转换失败则返回默认枚举
        /// </summary>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        /// <param name="value">枚举值</param>
        /// <param name="defaultValue">默认枚举</param>
        /// <returns>枚举</returns>
        public static TEnum? ToEnumOrDefaultByValue<TEnum>(this byte value, TEnum defaultValue)
            where TEnum : struct, Enum
        {
            //调用根据枚举值转换成枚举方法
            return ToEnumOrDefaultByValue<byte, TEnum>(value, defaultValue);
        }

        /// <summary>
        /// 根据枚举值转换成枚举名称，转换失败则返回空
        /// </summary>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        /// <param name="value">枚举值</param>
        /// <returns>枚举名称</returns>
        public static string? ToEnumNameByValue<TEnum>(this byte value)
            where TEnum : struct, Enum
        {
            //调用根据枚举值转换成枚举名称方法
            return ToEnumNameByValue<byte, TEnum>(value);
        }

        /// <summary>
        /// 根据枚举值转换成枚举名称，转换失败则返回默认枚举名称
        /// </summary>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        /// <param name="value">枚举值</param>
        /// <param name="defaultValue">默认枚举名称</param>
        /// <returns>枚举名称</returns>
        public static string? ToEnumNameOrDefaultByValue<TEnum>(this byte value, string defaultValue)
            where TEnum : struct, Enum
        {
            //调用根据枚举值转换成枚举名称方法
            return ToEnumNameOrDefaultByValue<byte, TEnum>(value, defaultValue);
        }

        /// <summary>
        /// 根据枚举值转换成枚举描述，转换失败则返回空
        /// </summary>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        /// <param name="value">枚举值</param>
        /// <returns>枚举描述</returns>
        public static string? ToEnumDescByValue<TEnum>(this byte value)
            where TEnum : struct, Enum
        {
            //调用根据枚举值转换成枚举描述方法
            return ToEnumDescByValue<byte, TEnum>(value);
        }

        /// <summary>
        /// 根据枚举值转换成枚举描述，转换失败则返回默认枚举描述
        /// </summary>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        /// <param name="value">枚举值</param>
        /// <param name="defaultValue">默认枚举描述</param>
        /// <returns>枚举描述</returns>
        public static string? ToEnumDescOrDefaultByValue<TEnum>(this byte value, string defaultValue)
            where TEnum : struct, Enum
        {
            //调用根据枚举值转换成枚举描述方法
            return ToEnumDescOrDefaultByValue<byte, TEnum>(value, defaultValue);
        }
    }

    /// <summary>
    /// 枚举相关扩展方法（short类型）
    /// </summary>
    public static partial class EnumExtension
    {
        /// <summary>
        /// 根据枚举值转换成枚举，转换失败则返回空
        /// </summary>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        /// <param name="value">枚举值</param>
        /// <returns>枚举</returns>
        public static TEnum? ToEnumByValue<TEnum>(this short value)
            where TEnum : struct, Enum
        {
            //调用根据枚举值转换成枚举方法
            return ToEnumByValue<short, TEnum>(value);
        }

        /// <summary>
        /// 根据枚举值转换成枚举，转换失败则返回默认枚举
        /// </summary>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        /// <param name="value">枚举值</param>
        /// <param name="defaultValue">默认枚举</param>
        /// <returns>枚举</returns>
        public static TEnum? ToEnumOrDefaultByValue<TEnum>(this short value, TEnum defaultValue)
            where TEnum : struct, Enum
        {
            //调用根据枚举值转换成枚举方法
            return ToEnumOrDefaultByValue<short, TEnum>(value, defaultValue);
        }

        /// <summary>
        /// 根据枚举值转换成枚举名称，转换失败则返回空
        /// </summary>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        /// <param name="value">枚举值</param>
        /// <returns>枚举名称</returns>
        public static string? ToEnumNameByValue<TEnum>(this short value)
            where TEnum : struct, Enum
        {
            //调用根据枚举值转换成枚举名称方法
            return ToEnumNameByValue<short, TEnum>(value);
        }

        /// <summary>
        /// 根据枚举值转换成枚举名称，转换失败则返回默认枚举名称
        /// </summary>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        /// <param name="value">枚举值</param>
        /// <param name="defaultValue">默认枚举名称</param>
        /// <returns>枚举名称</returns>
        public static string? ToEnumNameOrDefaultByValue<TEnum>(this short value, string defaultValue)
            where TEnum : struct, Enum
        {
            //调用根据枚举值转换成枚举名称方法
            return ToEnumNameOrDefaultByValue<short, TEnum>(value, defaultValue);
        }

        /// <summary>
        /// 根据枚举值转换成枚举描述，转换失败则返回空
        /// </summary>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        /// <param name="value">枚举值</param>
        /// <returns>枚举描述</returns>
        public static string? ToEnumDescByValue<TEnum>(this short value)
            where TEnum : struct, Enum
        {
            //调用根据枚举值转换成枚举描述方法
            return ToEnumDescByValue<short, TEnum>(value);
        }

        /// <summary>
        /// 根据枚举值转换成枚举描述，转换失败则返回默认枚举描述
        /// </summary>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        /// <param name="value">枚举值</param>
        /// <param name="defaultValue">默认枚举描述</param>
        /// <returns>枚举描述</returns>
        public static string? ToEnumDescOrDefaultByValue<TEnum>(this short value, string defaultValue)
            where TEnum : struct, Enum
        {
            //调用根据枚举值转换成枚举描述方法
            return ToEnumDescOrDefaultByValue<short, TEnum>(value, defaultValue);
        }
    }

    /// <summary>
    /// 枚举相关扩展方法（ushort类型）
    /// </summary>
    public static partial class EnumExtension
    {
        /// <summary>
        /// 根据枚举值转换成枚举，转换失败则返回空
        /// </summary>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        /// <param name="value">枚举值</param>
        /// <returns>枚举</returns>
        public static TEnum? ToEnumByValue<TEnum>(this ushort value)
            where TEnum : struct, Enum
        {
            //调用根据枚举值转换成枚举方法
            return ToEnumByValue<ushort, TEnum>(value);
        }

        /// <summary>
        /// 根据枚举值转换成枚举，转换失败则返回默认枚举
        /// </summary>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        /// <param name="value">枚举值</param>
        /// <param name="defaultValue">默认枚举</param>
        /// <returns>枚举</returns>
        public static TEnum? ToEnumOrDefaultByValue<TEnum>(this ushort value, TEnum defaultValue)
            where TEnum : struct, Enum
        {
            //调用根据枚举值转换成枚举方法
            return ToEnumOrDefaultByValue<ushort, TEnum>(value, defaultValue);
        }

        /// <summary>
        /// 根据枚举值转换成枚举名称，转换失败则返回空
        /// </summary>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        /// <param name="value">枚举值</param>
        /// <returns>枚举名称</returns>
        public static string? ToEnumNameByValue<TEnum>(this ushort value)
            where TEnum : struct, Enum
        {
            //调用根据枚举值转换成枚举名称方法
            return ToEnumNameByValue<ushort, TEnum>(value);
        }

        /// <summary>
        /// 根据枚举值转换成枚举名称，转换失败则返回默认枚举名称
        /// </summary>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        /// <param name="value">枚举值</param>
        /// <param name="defaultValue">默认枚举名称</param>
        /// <returns>枚举名称</returns>
        public static string? ToEnumNameOrDefaultByValue<TEnum>(this ushort value, string defaultValue)
            where TEnum : struct, Enum
        {
            //调用根据枚举值转换成枚举名称方法
            return ToEnumNameOrDefaultByValue<ushort, TEnum>(value, defaultValue);
        }

        /// <summary>
        /// 根据枚举值转换成枚举描述，转换失败则返回空
        /// </summary>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        /// <param name="value">枚举值</param>
        /// <returns>枚举描述</returns>
        public static string? ToEnumDescByValue<TEnum>(this ushort value)
            where TEnum : struct, Enum
        {
            //调用根据枚举值转换成枚举描述方法
            return ToEnumDescByValue<ushort, TEnum>(value);
        }

        /// <summary>
        /// 根据枚举值转换成枚举描述，转换失败则返回默认枚举描述
        /// </summary>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        /// <param name="value">枚举值</param>
        /// <param name="defaultValue">默认枚举描述</param>
        /// <returns>枚举描述</returns>
        public static string? ToEnumDescOrDefaultByValue<TEnum>(this ushort value, string defaultValue)
            where TEnum : struct, Enum
        {
            //调用根据枚举值转换成枚举描述方法
            return ToEnumDescOrDefaultByValue<ushort, TEnum>(value, defaultValue);
        }
    }

    /// <summary>
    /// 枚举相关扩展方法（int类型）
    /// </summary>
    public static partial class EnumExtension
    {
        /// <summary>
        /// 根据枚举值转换成枚举，转换失败则返回空
        /// </summary>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        /// <param name="value">枚举值</param>
        /// <returns>枚举</returns>
        public static TEnum? ToEnumByValue<TEnum>(this int value)
            where TEnum : struct, Enum
        {
            //调用根据枚举值转换成枚举方法
            return ToEnumByValue<int, TEnum>(value);
        }

        /// <summary>
        /// 根据枚举值转换成枚举，转换失败则返回默认枚举
        /// </summary>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        /// <param name="value">枚举值</param>
        /// <param name="defaultValue">默认枚举</param>
        /// <returns>枚举</returns>
        public static TEnum? ToEnumOrDefaultByValue<TEnum>(this int value, TEnum defaultValue)
            where TEnum : struct, Enum
        {
            //调用根据枚举值转换成枚举方法
            return ToEnumOrDefaultByValue<int, TEnum>(value, defaultValue);
        }

        /// <summary>
        /// 根据枚举值转换成枚举名称，转换失败则返回空
        /// </summary>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        /// <param name="value">枚举值</param>
        /// <returns>枚举名称</returns>
        public static string? ToEnumNameByValue<TEnum>(this int value)
            where TEnum : struct, Enum
        {
            //调用根据枚举值转换成枚举名称方法
            return ToEnumNameByValue<int, TEnum>(value);
        }

        /// <summary>
        /// 根据枚举值转换成枚举名称，转换失败则返回默认枚举名称
        /// </summary>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        /// <param name="value">枚举值</param>
        /// <param name="defaultValue">默认枚举名称</param>
        /// <returns>枚举名称</returns>
        public static string? ToEnumNameOrDefaultByValue<TEnum>(this int value, string defaultValue)
            where TEnum : struct, Enum
        {
            //调用根据枚举值转换成枚举名称方法
            return ToEnumNameOrDefaultByValue<int, TEnum>(value, defaultValue);
        }

        /// <summary>
        /// 根据枚举值转换成枚举描述，转换失败则返回空
        /// </summary>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        /// <param name="value">枚举值</param>
        /// <returns>枚举描述</returns>
        public static string? ToEnumDescByValue<TEnum>(this int value)
            where TEnum : struct, Enum
        {
            //调用根据枚举值转换成枚举描述方法
            return ToEnumDescByValue<int, TEnum>(value);
        }

        /// <summary>
        /// 根据枚举值转换成枚举描述，转换失败则返回默认枚举描述
        /// </summary>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        /// <param name="value">枚举值</param>
        /// <param name="defaultValue">默认枚举描述</param>
        /// <returns>枚举描述</returns>
        public static string? ToEnumDescOrDefaultByValue<TEnum>(this int value, string defaultValue)
            where TEnum : struct, Enum
        {
            //调用根据枚举值转换成枚举描述方法
            return ToEnumDescOrDefaultByValue<int, TEnum>(value, defaultValue);
        }
    }

    /// <summary>
    /// 枚举相关扩展方法（uint类型）
    /// </summary>
    public static partial class EnumExtension
    {
        /// <summary>
        /// 根据枚举值转换成枚举，转换失败则返回空
        /// </summary>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        /// <param name="value">枚举值</param>
        /// <returns>枚举</returns>
        public static TEnum? ToEnumByValue<TEnum>(this uint value)
            where TEnum : struct, Enum
        {
            //调用根据枚举值转换成枚举方法
            return ToEnumByValue<uint, TEnum>(value);
        }

        /// <summary>
        /// 根据枚举值转换成枚举，转换失败则返回默认枚举
        /// </summary>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        /// <param name="value">枚举值</param>
        /// <param name="defaultValue">默认枚举</param>
        /// <returns>枚举</returns>
        public static TEnum? ToEnumOrDefaultByValue<TEnum>(this uint value, TEnum defaultValue)
            where TEnum : struct, Enum
        {
            //调用根据枚举值转换成枚举方法
            return ToEnumOrDefaultByValue<uint, TEnum>(value, defaultValue);
        }

        /// <summary>
        /// 根据枚举值转换成枚举名称，转换失败则返回空
        /// </summary>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        /// <param name="value">枚举值</param>
        /// <returns>枚举名称</returns>
        public static string? ToEnumNameByValue<TEnum>(this uint value)
            where TEnum : struct, Enum
        {
            //调用根据枚举值转换成枚举名称方法
            return ToEnumNameByValue<uint, TEnum>(value);
        }

        /// <summary>
        /// 根据枚举值转换成枚举名称，转换失败则返回默认枚举名称
        /// </summary>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        /// <param name="value">枚举值</param>
        /// <param name="defaultValue">默认枚举名称</param>
        /// <returns>枚举名称</returns>
        public static string? ToEnumNameOrDefaultByValue<TEnum>(this uint value, string defaultValue)
            where TEnum : struct, Enum
        {
            //调用根据枚举值转换成枚举名称方法
            return ToEnumNameOrDefaultByValue<uint, TEnum>(value, defaultValue);
        }

        /// <summary>
        /// 根据枚举值转换成枚举描述，转换失败则返回空
        /// </summary>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        /// <param name="value">枚举值</param>
        /// <returns>枚举描述</returns>
        public static string? ToEnumDescByValue<TEnum>(this uint value)
            where TEnum : struct, Enum
        {
            //调用根据枚举值转换成枚举描述方法
            return ToEnumDescByValue<uint, TEnum>(value);
        }

        /// <summary>
        /// 根据枚举值转换成枚举描述，转换失败则返回默认枚举描述
        /// </summary>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        /// <param name="value">枚举值</param>
        /// <param name="defaultValue">默认枚举描述</param>
        /// <returns>枚举描述</returns>
        public static string? ToEnumDescOrDefaultByValue<TEnum>(this uint value, string defaultValue)
            where TEnum : struct, Enum
        {
            //调用根据枚举值转换成枚举描述方法
            return ToEnumDescOrDefaultByValue<uint, TEnum>(value, defaultValue);
        }
    }

    /// <summary>
    /// 枚举相关扩展方法（long类型）
    /// </summary>
    public static partial class EnumExtension
    {
        /// <summary>
        /// 根据枚举值转换成枚举，转换失败则返回空
        /// </summary>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        /// <param name="value">枚举值</param>
        /// <returns>枚举</returns>
        public static TEnum? ToEnumByValue<TEnum>(this long value)
            where TEnum : struct, Enum
        {
            //调用根据枚举值转换成枚举方法
            return ToEnumByValue<long, TEnum>(value);
        }

        /// <summary>
        /// 根据枚举值转换成枚举，转换失败则返回默认枚举
        /// </summary>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        /// <param name="value">枚举值</param>
        /// <param name="defaultValue">默认枚举</param>
        /// <returns>枚举</returns>
        public static TEnum? ToEnumOrDefaultByValue<TEnum>(this long value, TEnum defaultValue)
            where TEnum : struct, Enum
        {
            //调用根据枚举值转换成枚举方法
            return ToEnumOrDefaultByValue<long, TEnum>(value, defaultValue);
        }

        /// <summary>
        /// 根据枚举值转换成枚举名称，转换失败则返回空
        /// </summary>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        /// <param name="value">枚举值</param>
        /// <returns>枚举名称</returns>
        public static string? ToEnumNameByValue<TEnum>(this long value)
            where TEnum : struct, Enum
        {
            //调用根据枚举值转换成枚举名称方法
            return ToEnumNameByValue<long, TEnum>(value);
        }

        /// <summary>
        /// 根据枚举值转换成枚举名称，转换失败则返回默认枚举名称
        /// </summary>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        /// <param name="value">枚举值</param>
        /// <param name="defaultValue">默认枚举名称</param>
        /// <returns>枚举名称</returns>
        public static string? ToEnumNameOrDefaultByValue<TEnum>(this long value, string defaultValue)
            where TEnum : struct, Enum
        {
            //调用根据枚举值转换成枚举名称方法
            return ToEnumNameOrDefaultByValue<long, TEnum>(value, defaultValue);
        }

        /// <summary>
        /// 根据枚举值转换成枚举描述，转换失败则返回空
        /// </summary>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        /// <param name="value">枚举值</param>
        /// <returns>枚举描述</returns>
        public static string? ToEnumDescByValue<TEnum>(this long value)
            where TEnum : struct, Enum
        {
            //调用根据枚举值转换成枚举描述方法
            return ToEnumDescByValue<long, TEnum>(value);
        }

        /// <summary>
        /// 根据枚举值转换成枚举描述，转换失败则返回默认枚举描述
        /// </summary>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        /// <param name="value">枚举值</param>
        /// <param name="defaultValue">默认枚举描述</param>
        /// <returns>枚举描述</returns>
        public static string? ToEnumDescOrDefaultByValue<TEnum>(this long value, string defaultValue)
            where TEnum : struct, Enum
        {
            //调用根据枚举值转换成枚举描述方法
            return ToEnumDescOrDefaultByValue<long, TEnum>(value, defaultValue);
        }
    }

    /// <summary>
    /// 枚举相关扩展方法（ulong类型）
    /// </summary>
    public static partial class EnumExtension
    {
        /// <summary>
        /// 根据枚举值转换成枚举，转换失败则返回空
        /// </summary>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        /// <param name="value">枚举值</param>
        /// <returns>枚举</returns>
        public static TEnum? ToEnumByValue<TEnum>(this ulong value)
            where TEnum : struct, Enum
        {
            //调用根据枚举值转换成枚举方法
            return ToEnumByValue<ulong, TEnum>(value);
        }

        /// <summary>
        /// 根据枚举值转换成枚举，转换失败则返回默认枚举
        /// </summary>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        /// <param name="value">枚举值</param>
        /// <param name="defaultValue">默认枚举</param>
        /// <returns>枚举</returns>
        public static TEnum? ToEnumOrDefaultByValue<TEnum>(this ulong value, TEnum defaultValue)
            where TEnum : struct, Enum
        {
            //调用根据枚举值转换成枚举方法
            return ToEnumOrDefaultByValue<ulong, TEnum>(value, defaultValue);
        }

        /// <summary>
        /// 根据枚举值转换成枚举名称，转换失败则返回空
        /// </summary>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        /// <param name="value">枚举值</param>
        /// <returns>枚举名称</returns>
        public static string? ToEnumNameByValue<TEnum>(this ulong value)
            where TEnum : struct, Enum
        {
            //调用根据枚举值转换成枚举名称方法
            return ToEnumNameByValue<ulong, TEnum>(value);
        }

        /// <summary>
        /// 根据枚举值转换成枚举名称，转换失败则返回默认枚举名称
        /// </summary>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        /// <param name="value">枚举值</param>
        /// <param name="defaultValue">默认枚举名称</param>
        /// <returns>枚举名称</returns>
        public static string? ToEnumNameOrDefaultByValue<TEnum>(this ulong value, string defaultValue)
            where TEnum : struct, Enum
        {
            //调用根据枚举值转换成枚举名称方法
            return ToEnumNameOrDefaultByValue<ulong, TEnum>(value, defaultValue);
        }

        /// <summary>
        /// 根据枚举值转换成枚举描述，转换失败则返回空
        /// </summary>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        /// <param name="value">枚举值</param>
        /// <returns>枚举描述</returns>
        public static string? ToEnumDescByValue<TEnum>(this ulong value)
            where TEnum : struct, Enum
        {
            //调用根据枚举值转换成枚举描述方法
            return ToEnumDescByValue<ulong, TEnum>(value);
        }

        /// <summary>
        /// 根据枚举值转换成枚举描述，转换失败则返回默认枚举描述
        /// </summary>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        /// <param name="value">枚举值</param>
        /// <param name="defaultValue">默认枚举描述</param>
        /// <returns>枚举描述</returns>
        public static string? ToEnumDescOrDefaultByValue<TEnum>(this ulong value, string defaultValue)
            where TEnum : struct, Enum
        {
            //调用根据枚举值转换成枚举描述方法
            return ToEnumDescOrDefaultByValue<ulong, TEnum>(value, defaultValue);
        }
    }
    #endregion
}
