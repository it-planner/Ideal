using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Reflection;

namespace Ideal.Core.Common.Helpers
{
    /// <summary>
    /// 表格帮助类 - 创建表格
    /// </summary>
    public static partial class TableHelper
    {
        /// <summary>
        /// 根据列名数组创建表格
        /// </summary>
        /// <param name="columnNames">列名数组</param>
        /// <param name="tableName">表名</param>
        /// <returns>DataTable</returns>
        /// <exception cref="DuplicateNameException">列名重复异常</exception>
        public static DataTable Create(string[] columnNames, string? tableName = null)
        {
            var table = new DataTable(tableName);
            foreach (var columnName in columnNames)
            {
                //添加列名，数据类型默认为string类型
                table.Columns.Add(columnName);
            }

            return table;
        }

        /// <summary>
        /// 根据列名-类型键值对创建表格
        /// </summary>
        /// <param name="columns">列名-类型键值对</param>
        /// <param name="tableName">表名</param>
        /// <returns>DataTable</returns>
        public static DataTable Create(Dictionary<string, Type> columns, string? tableName = null)
        {
            var table = new DataTable(tableName);
            foreach (var column in columns)
            {
                //添加指定的列名和数据类型
                table.Columns.Add(column.Key, column.Value);
            }

            return table;
        }

        /// <summary>
        /// 根据类创建表格
        /// 如果设置DescriptionAttribute，则将特性值作为表格的列名称
        /// 否则将属性名作为表格的列名称
        /// </summary>
        /// <typeparam name="T">类</typeparam>
        /// <param name="tableName">表名</param>
        /// <returns>DataTable</returns>
        /// <exception cref="InvalidOperationException">T必须是结构体或类，并且不能是集合类型</exception>
        public static DataTable Create<T>(string? tableName = null)
        {
            //T必须是结构体或类，并且不能是集合类型
            AssertTypeValid<T>();

            //获取类的所有公共属性
            var properties = typeof(T).GetProperties();

            var columns = new Dictionary<string, Type>();
            foreach (var property in properties)
            {
                //根据属性获取列名
                var columnName = GetColumnName(property);
                //组织列名-类型键值对
                columns.Add(columnName, property.PropertyType);
            }

            return Create(columns, tableName);
        }
    }

    /// <summary>
    /// 表格帮助类 - 转换
    /// </summary>
    public static partial class TableHelper
    {
        /// <summary>
        /// 把表格转换为对象集合
        /// 如果设置DescriptionAttribute，则将特性值作为表格的列名称
        /// 否则将属性名作为表格的列名称
        /// </summary>
        /// <typeparam name="T">类</typeparam>
        /// <param name="dataTable">表格</param>
        /// <returns>对象集合</returns>
        /// <exception cref="FormatException">表格中数据类型无法转为对象属性类型</exception>
        /// <exception cref="NotSupportedException">表格列名无法映射至对象属性，无法完成转换</exception>
        public static IEnumerable<T> ToModels<T>(DataTable dataTable)
        {
            //T必须是结构体或类，并且不能是集合类型
            AssertTypeValid<T>();

            if (0 == dataTable.Rows.Count)
            {
                return [];
            }

            //获取T所有可写入属性
            var properties = typeof(T).GetProperties().Where(u => u.CanWrite);

            //校验表格是否能转换为对象
            var isCanParse = IsCanMapDataTableToModel(dataTable, properties);
            if (!isCanParse)
            {
                throw new NotSupportedException("The column name of the table cannot be mapped to an object property, and the conversion cannot be completed.");
            }

            var models = new List<T>();
            foreach (DataRow dr in dataTable.Rows)
            {
                //通过反射实例化T
                var model = Activator.CreateInstance<T>();

                //把行数据映射到对象上
                if (typeof(T).IsClass)
                {
                    //处理T为类的情况
                    MapRowToModel<T>(dr, model, properties);
                }
                else
                {
                    //处理T为结构体的情况
                    object boxed = model!;
                    MapRowToModel<object>(dr, boxed, properties);
                    model = (T)boxed;
                }

                models.Add(model);
            }

            return models;
        }

        /// <summary>
        /// 把对象集合转为表格
        /// 如果设置DescriptionAttribute，则将特性值作为表格的列名称
        /// 否则将属性名作为表格的列名称
        /// </summary>
        /// <typeparam name="T">类</typeparam>
        /// <param name="tableName">表名</param>
        /// <param name="models">对象集合</param>
        /// <returns>DataTable</returns>
        public static DataTable ToDataTable<T>(IEnumerable<T> models, string? tableName = null)
        {
            //创建表格
            var dataTable = Create<T>(tableName);
            if (models == null || !models.Any())
            {
                return dataTable;
            }

            //获取所有属性
            var properties = typeof(T).GetProperties().Where(u => u.CanRead);
            foreach (var model in models)
            {
                //创建行
                var dataRow = dataTable.NewRow();
                foreach (var property in properties)
                {
                    //根据属性获取列名
                    var columnName = GetColumnName(property);
                    //填充行数据
                    dataRow[columnName] = property.GetValue(model);
                }

                dataTable.Rows.Add(dataRow);
            }

            return dataTable;
        }

        /// <summary>
        /// 把一维数组作为一列转换为表格
        /// </summary>
        /// <typeparam name="TColumn">列类型</typeparam>
        /// <param name="array">一维数组</param>
        /// <param name="tableName">表名</param>
        /// <param name="columnName">列名</param>
        /// <returns>DataTable</returns>
        public static DataTable ToDataTableWithColumnArray<TColumn>(TColumn[] array, string? tableName = null, string? columnName = null)
        {
            var dataTable = new DataTable(tableName);
            //创建列
            dataTable.Columns.Add(columnName, typeof(TColumn));

            //添加行数据
            foreach (var item in array)
            {
                var dataRow = dataTable.NewRow();
                dataRow[0] = item;
                dataTable.Rows.Add(dataRow);
            }

            return dataTable;
        }

        /// <summary>
        /// 把一维数组作为一行转换为表格
        /// </summary>
        /// <typeparam name="TRow">行类型</typeparam>
        /// <param name="array">一维数组</param>
        /// <param name="tableName">表名</param>
        /// <returns>返回DataTable</returns>
        public static DataTable ToDataTableWithRowArray<TRow>(TRow[] array, string? tableName = null)
        {
            var dataTable = new DataTable(tableName);
            //创建列
            for (var i = 0; i < array.Length; i++)
            {
                dataTable.Columns.Add(null, typeof(TRow));
            }

            //添加行数据
            var dataRow = dataTable.NewRow();
            for (var i = 0; i < array.Length; i++)
            {
                dataRow[i] = array[i];
            }

            dataTable.Rows.Add(dataRow);
            return dataTable;
        }

        /// <summary>
        /// 行列转置
        /// </summary>
        /// <param name="dataTable">原表格</param>
        /// <param name="isColumnNameAsData">列名是否作为数据</param>
        /// <returns>DataTable</returns>
        public static DataTable Transpose(DataTable dataTable, bool isColumnNameAsData = true)
        {
            var transposed = new DataTable(dataTable.TableName);

            //如果列名作为数据，则需要多加一列
            if (isColumnNameAsData)
            {
                transposed.Columns.Add();
            }

            //转置后，行数即为新的列数
            for (var i = 0; i < dataTable.Rows.Count; i++)
            {
                transposed.Columns.Add();
            }

            //以列为单位，一次处理一列数据
            for (var column = 0; column < dataTable.Columns.Count; column++)
            {
                //创建新行
                var newRow = transposed.NewRow();
                //如果列名作为数据，则先把列名加入第一列
                if (isColumnNameAsData)
                {
                    newRow[0] = dataTable.Columns[column].ColumnName;
                }

                //把一列数据转为一行数据
                for (var row = 0; row < dataTable.Rows.Count; row++)
                {
                    //如果列名作为数据，则行数据从第二列开始填充
                    var rowIndex = isColumnNameAsData ? row + 1 : row;
                    newRow[rowIndex] = dataTable.Rows[row][column];
                }

                transposed.Rows.Add(newRow);
            }

            return transposed;
        }
    }

    public static partial class TableHelper
    {
        /// <summary>
        /// 断言类型有效性
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <exception cref="InvalidOperationException">T必须是结构体或类，并且不能是集合类型</exception>
        private static void AssertTypeValid<T>()
        {
            var type = typeof(T);
            if (type.IsValueType && !type.IsEnum && !type.IsPrimitive)
            {
                //是值类型，但是不是枚举或基础类型
                return;
            }
            else if (typeof(T).IsClass && !typeof(IEnumerable).IsAssignableFrom(typeof(T)))
            {
                //是类类型，但是不是集合类型，同时也不是委托、接口类型
                return;
            }

            throw new InvalidOperationException("T must be a struct or class and cannot be a collection type.");
        }

        /// <summary>
        /// 根据属性获取列名称
        /// </summary>
        /// <param name="property">对象属性</param>
        /// <returns>列名</returns>
        private static string GetColumnName(PropertyInfo property)
        {
            //获取描述特性
            var attribute = property.GetCustomAttribute<DescriptionAttribute>();
            //如果存在描述特性则返回描述，否则返回属性名称
            return attribute?.Description ?? property.Name;
        }

        /// <summary>
        /// 校验表格是否能转换为对象
        /// </summary>
        /// <param name="dataTable">表格</param>
        /// <param name="properties">对象属性</param>
        /// <returns>是否能转换</returns>
        private static bool IsCanMapDataTableToModel(DataTable dataTable, IEnumerable<PropertyInfo> properties)
        {
            var isCanParse = false;
            foreach (var property in properties)
            {
                //根据属性获取列名
                var columnName = GetColumnName(property);
                if (!dataTable.Columns.Contains(columnName))
                {
                    continue;
                }

                isCanParse = true;
            }

            return isCanParse;
        }

        /// <summary>
        /// 把行数据映射到对象上
        /// </summary>
        /// <typeparam name="T">类</typeparam>
        /// <param name="dataRow">行</param>
        /// <param name="model">对象</param>
        /// <param name="properties">类的属性集合</param>
        private static void MapRowToModel<T>(DataRow dataRow, T model, IEnumerable<PropertyInfo> properties)
        {
            foreach (var property in properties)
            {
                //根据属性获取列名
                var columnName = GetColumnName(property);
                if (!dataRow.Table.Columns.Contains(columnName))
                {
                    continue;
                }

                //获取单元格值
                var value = dataRow[columnName];
                if (value != DBNull.Value)
                {
                    //给对象属性赋值
                    property.SetValue(model, Convert.ChangeType(value, property.PropertyType));
                }
            }
        }
    }
}
