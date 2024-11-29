using Ideal.Core.Common.Helpers;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.Data;

namespace Ideal.Core.Document
{
    /// <summary>
    /// Excel帮助类
    /// </summary>
    public static partial class ExcelHelper
    {
        /// <summary>
        /// 根据文件路径读取Excel到DataSet
        /// 指定sheetName，sheetNumber则读取相应工作簿Sheet
        /// 如果不指定则读取所有工作簿Sheet
        /// </summary>
        /// <param name="path">Excel文件路径</param>
        /// <param name="isFirstRowAsColumnName">是否把第一行数据作为表格列名</param>
        /// <param name="sheetName">工作簿名称</param>
        /// <param name="sheetNumber">工作簿编号，从 1 开始</param>
        /// <returns>DataSet</returns>
        public static DataSet Read(string path, bool isFirstRowAsColumnName = false, string? sheetName = null, int? sheetNumber = null)
        {
            using var stream = new FileStream(path, FileMode.Open, FileAccess.Read);
            return Read(stream, IsXlsxFile(path), isFirstRowAsColumnName, sheetName, sheetNumber);
        }

        /// <summary>
        /// 根据文件流读取Excel到DataSet
        /// 指定sheetName，sheetNumber则读取相应工作簿Sheet
        /// 如果不指定则读取所有工作簿Sheet
        /// </summary>
        /// <param name="stream">Excel文件流</param>
        /// <param name="fileName">Excel文件名称</param>
        /// <param name="isFirstRowAsColumnName">是否把第一行数据作为表格列名</param>
        /// <param name="sheetName">工作簿名称</param>
        /// <param name="sheetNumber">工作簿编号，从 1 开始</param>
        /// <returns>DataSet</returns>
        public static DataSet Read(Stream stream, string fileName, bool isFirstRowAsColumnName = false, string? sheetName = null, int? sheetNumber = null)
        {
            return Read(stream, IsXlsxFile(fileName), isFirstRowAsColumnName, sheetName, sheetNumber);
        }

        /// <summary>
        /// 根据文件流读取Excel到DataSet
        /// 指定sheetName，sheetNumber则读取相应工作簿Sheet
        /// 如果不指定则读取所有工作簿Sheet
        /// </summary>
        /// <param name="stream">Excel文件流</param>
        /// <param name="isXlsx">Excel文件是否是Xlsx后缀</param>
        /// <param name="isFirstRowAsColumnName">是否把第一行数据作为表格列名</param>
        /// <param name="sheetName">工作簿名称</param>
        /// <param name="sheetNumber">工作簿编号，从 1 开始</param>
        /// <returns>DataSet</returns>
        public static DataSet Read(Stream stream, bool isXlsx, bool isFirstRowAsColumnName = false, string? sheetName = null, int? sheetNumber = null)
        {
            if (sheetName == null && sheetNumber == null)
            {
                //读取所有工作簿Sheet至DataSet
                return FillDataSetWithStreamOfSheets(stream, isXlsx, isFirstRowAsColumnName);
            }

            //读取指定工作簿Sheet至DataSet
            return CreateDataSetWithStreamOfSheet(stream, isXlsx, isFirstRowAsColumnName, sheetName, sheetNumber ?? 1);
        }

        /// <summary>
        /// 根据文件流读取Excel到对象集合
        /// 指定sheetName，sheetNumber则读取相应工作簿Sheet
        /// 如果不指定则默认读取第一个工作簿Sheet
        /// </summary>
        /// <param name="path">Excel文件路径</param>
        /// <param name="isFirstRowAsColumnName">是否把第一行数据作为表格列名</param>
        /// <param name="sheetName">工作簿名称</param>
        /// <param name="sheetNumber">工作簿编号，从 1 开始</param>
        /// <returns>对象集合</returns>
        /// <exception cref="FormatException">表格中数据类型无法转为对象属性类型</exception>
        public static IEnumerable<T> Read<T>(string path, bool isFirstRowAsColumnName = false, string? sheetName = null, int? sheetNumber = null)
        {
            using var stream = new FileStream(path, FileMode.Open, FileAccess.Read);
            return Read<T>(stream, IsXlsxFile(path), isFirstRowAsColumnName, sheetName, sheetNumber);
        }

        /// <summary>
        /// 根据文件流读取Excel到对象集合
        /// 指定sheetName，sheetNumber则读取相应工作簿Sheet
        /// 如果不指定则默认读取第一个工作簿Sheet
        /// </summary>
        /// <param name="stream">Excel文件流</param>
        /// <param name="fileName">Excel文件名称</param>
        /// <param name="isFirstRowAsColumnName">是否把第一行数据作为表格列名</param>
        /// <param name="sheetName">工作簿名称</param>
        /// <param name="sheetNumber">工作簿编号，从 1 开始</param>
        /// <returns>对象集合</returns>
        /// <exception cref="FormatException">表格中数据类型无法转为对象属性类型</exception>
        public static IEnumerable<T> Read<T>(Stream stream, string fileName, bool isFirstRowAsColumnName = false, string? sheetName = null, int? sheetNumber = null)
        {
            return Read<T>(stream, IsXlsxFile(fileName), isFirstRowAsColumnName, sheetName, sheetNumber);
        }

        /// <summary>
        /// 根据文件流读取Excel到对象集合
        /// 指定sheetName，sheetNumber则读取相应工作簿Sheet
        /// 如果不指定则默认读取第一个工作簿Sheet
        /// </summary>
        /// <param name="stream">Excel文件流</param>
        /// <param name="isXlsx">Excel文件是否是Xlsx后缀</param>
        /// <param name="isFirstRowAsColumnName">是否把第一行数据作为表格列名</param>
        /// <param name="sheetName">工作簿名称</param>
        /// <param name="sheetNumber">工作簿编号，从 1 开始</param>
        /// <returns>对象集合</returns>
        /// <exception cref="FormatException">表格中数据类型无法转为对象属性类型</exception>
        public static IEnumerable<T> Read<T>(Stream stream, bool isXlsx, bool isFirstRowAsColumnName = false, string? sheetName = null, int? sheetNumber = null)
        {
            //读取指定工作簿Sheet至DataSet
            var dataSet = CreateDataSetWithStreamOfSheet(stream, isXlsx, isFirstRowAsColumnName, sheetName, sheetNumber ?? 1);
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                return [];
            }

            //DataTable转对象集合
            return TableHelper.ToModels<T>(dataSet.Tables[0]);
        }

        /// <summary>
        /// 把表格数组写入Excel文件流
        /// </summary>
        /// <param name="dataTables">表格数组</param>
        /// <param name="isXlsx">Excel文件是否是Xlsx后缀</param>
        /// <param name="isColumnNameAsData">表格列名是否作为Excel工作簿第一行数据</param>
        /// <returns>MemoryStream</returns>
        public static MemoryStream Write(DataTable[] dataTables, bool isXlsx, bool isColumnNameAsData)
        {
            //表格数组写入Excel对象
            using var workbook = CreateWorkbook(dataTables, isXlsx, isColumnNameAsData);
            var stream = new MemoryStream();
            workbook.Write(stream, true);
            stream.Flush();
            return stream;
        }

        /// <summary>
        /// 把表格数组写入Excel文件
        /// </summary>
        /// <param name="dataTables">表格数组</param>
        /// <param name="path">Excel文件路径</param>
        /// <param name="isColumnNameAsData">表格列名是否作为Excel工作簿第一行数据</param>
        public static void Write(DataTable[] dataTables, string path, bool isColumnNameAsData)
        {
            //检查文件夹是否存在，不存在则创建
            var directoryName = Path.GetDirectoryName(path);
            if (!string.IsNullOrEmpty(directoryName) && !Directory.Exists(directoryName))
            {
                Directory.CreateDirectory(directoryName);
            }

            //检查是否指定文件名，没有则默认以“时间+随机数.xlsx”作为文件名
            var fileName = Path.GetFileName(path);
            if (string.IsNullOrEmpty(fileName))
            {
                directoryName = Path.GetFullPath(path);
                fileName = DateTime.Now.ToString("yyyyMMdd-hhmmss-") + new Random().Next(0000, 9999).ToString("D4") + ".xlsx";
                path = Path.Combine(directoryName, fileName);
            }

            //表格数组写入Excel对象
            using var workbook = CreateWorkbook(dataTables, IsXlsxFile(path), isColumnNameAsData);
            using var fs = new FileStream(path, FileMode.Create, FileAccess.Write);
            workbook.Write(fs, true);
        }

        /// <summary>
        /// 把对象集合写入Excel文件流
        /// </summary>
        /// <param name="models">对象集合</param>
        /// <param name="isXlsx">Excel文件是否是Xlsx后缀</param>
        /// <param name="isColumnNameAsData">表格列名是否作为Excel工作簿第一行数据</param>
        /// <param name="sheetName">工作簿名称</param>
        /// <returns>MemoryStream</returns>
        public static MemoryStream Write<T>(IEnumerable<T> models, bool isXlsx, bool isColumnNameAsData, string? sheetName = null)
        {
            //对象集合转为表格
            var table = TableHelper.ToDataTable<T>(models, sheetName);
            //表格数组写入Excel文件流
            return Write([table], isXlsx, isColumnNameAsData);
        }

        /// <summary>
        /// 把对象集合写入Excel文件
        /// </summary>
        /// <param name="models">对象集合</param>
        /// <param name="path">Excel文件路径</param>
        /// <param name="isColumnNameAsData">表格列名是否作为Excel工作簿第一行数据</param>
        /// <param name="sheetName">工作簿名称</param>
        public static void Write<T>(IEnumerable<T> models, string path, bool isColumnNameAsData, string? sheetName = null)
        {
            //对象集合转为表格
            var table = TableHelper.ToDataTable<T>(models, sheetName);
            //表格数组写入Excel文件
            Write([table], path, isColumnNameAsData);
        }
    }


    /// <summary>
    /// Excel帮助类
    /// </summary>
    public static partial class ExcelHelper
    {
        /// <summary>
        /// 读取所有工作簿Sheet至DataSet
        /// </summary>
        /// <param name="stream">Excel文件流</param>
        /// <param name="isXlsx">Excel文件是否是Xlsx后缀</param>
        /// <param name="isFirstRowAsColumnName">是否把第一行数据作为表格列名</param>
        /// <returns></returns>
        private static DataSet FillDataSetWithStreamOfSheets(Stream stream, bool isXlsx, bool isFirstRowAsColumnName)
        {
            //根据Excel文件后缀创建IWorkbook
            using var workbook = CreateWorkbook(isXlsx, stream);

            //根据Excel文件后缀创建公式求值器
            var evaluator = CreateFormulaEvaluator(isXlsx, workbook);
            var dataSet = new DataSet();
            for (var i = 0; i < workbook.NumberOfSheets; i++)
            {
                //获取工作簿Sheet
                var sheet = workbook.GetSheetAt(i);
                //通过工作簿Sheet创建表格
                var table = CreateDataTableBySheet(sheet, evaluator, isFirstRowAsColumnName);
                dataSet.Tables.Add(table);
            }

            return dataSet;
        }

        /// <summary>
        /// 读取指定工作簿Sheet至DataSet
        /// </summary>
        /// <param name="stream">Excel文件流</param>
        /// <param name="isXlsx">Excel文件是否是Xlsx后缀</param>
        /// <param name="isFirstRowAsColumnName">是否把第一行数据作为表格列名</param>
        /// <param name="sheetName">工作簿名称</param>
        /// <param name="sheetNumber">工作簿编号，从 1 开始</param>
        /// <returns></returns>
        private static DataSet CreateDataSetWithStreamOfSheet(Stream stream, bool isXlsx, bool isFirstRowAsColumnName, string? sheetName = null, int sheetNumber = 1)
        {
            //把工作簿sheet编号转为索引
            var sheetIndex = sheetNumber - 1;
            var dataSet = new DataSet();
            if (string.IsNullOrWhiteSpace(sheetName) && sheetIndex < 0)
            {
                //工作簿sheet索引非法则返回
                return dataSet;
            }

            //根据Excel文件后缀创建IWorkbook
            using var workbook = CreateWorkbook(isXlsx, stream);
            if (string.IsNullOrWhiteSpace(sheetName) && sheetIndex >= workbook.NumberOfSheets)
            {
                //工作簿sheet索引非法则返回
                return dataSet;
            }

            //根据Excel文件后缀创建公式求值器
            var evaluator = CreateFormulaEvaluator(isXlsx, workbook);
            //优先通过工作簿名称获取工作簿sheet
            var sheet = !string.IsNullOrWhiteSpace(sheetName) ? workbook.GetSheet(sheetName) : workbook.GetSheetAt(sheetIndex);
            if (sheet != null)
            {
                //通过工作簿sheet创建表格
                var table = CreateDataTableBySheet(sheet, evaluator, isFirstRowAsColumnName);
                dataSet.Tables.Add(table);
            }

            return dataSet;
        }

        /// <summary>
        /// 根据Excel文件后缀创建公式求值器
        /// </summary>
        /// <param name="isXlsx">Excel文件是否是Xlsx后缀</param>
        /// <param name="workbook">IWorkbook</param>
        /// <returns>公式求值器</returns>
        private static IFormulaEvaluator CreateFormulaEvaluator(bool isXlsx, IWorkbook workbook)
        {
            return isXlsx ? new XSSFFormulaEvaluator(workbook) : new HSSFFormulaEvaluator(workbook);
        }

        /// <summary>
        /// 通过Sheet创建表格
        /// </summary>
        /// <param name="sheet">工作簿Sheet</param>
        /// <param name="evaluator">公式求值器</param>
        /// <param name="isFirstRowAsColumnName">是否把第一行数据作为表格列名</param>
        /// <returns>DataTable</returns>
        private static DataTable CreateDataTableBySheet(ISheet sheet, IFormulaEvaluator evaluator, bool isFirstRowAsColumnName)
        {
            var dataTable = new DataTable(sheet.SheetName);
            //获取Sheet中最大的列数，并以此数为新的表格列数
            var maxColumnNumber = GetMaxColumnNumber(sheet);
            if (isFirstRowAsColumnName)
            {
                //如果第一行数据作为表头，则先获取第一行数据
                var firstRow = sheet.GetRow(sheet.FirstRowNum);
                for (var i = 0; i < maxColumnNumber; i++)
                {
                    //尝试读取第一行每一个单元格数据，有值则作为列名，否则忽略
                    string? columnName = null;
                    var cell = firstRow?.GetCell(i);
                    if (cell != null)
                    {
                        cell.SetCellType(CellType.String);
                        if (cell.StringCellValue != null)
                        {
                            columnName = cell.StringCellValue;
                        }
                    }

                    dataTable.Columns.Add(columnName);
                }
            }
            else
            {
                for (var i = 0; i < maxColumnNumber; i++)
                {
                    dataTable.Columns.Add();
                }
            }

            //循环处理有效行数据
            for (var i = isFirstRowAsColumnName ? sheet.FirstRowNum + 1 : sheet.FirstRowNum; i <= sheet.LastRowNum; i++)
            {
                var row = sheet.GetRow(i);
                var newRow = dataTable.NewRow();
                //通过工作簿sheet行数据填充表格新行数据
                FillDataRowBySheetRow(row, evaluator, newRow);

                //检查每单元格是否都有值
                var isNullRow = true;
                for (var j = 0; j < maxColumnNumber; j++)
                {
                    isNullRow = isNullRow && newRow.IsNull(j);
                }

                if (!isNullRow)
                {
                    dataTable.Rows.Add(newRow);
                }
            }

            return dataTable;
        }

        /// <summary>
        /// 通过工作簿sheet行数据填充表格行数据
        /// </summary>
        /// <param name="row">工作簿sheet行</param>
        /// <param name="evaluator">公式求值器</param>
        /// <param name="dataRow">表格行</param>
        /// <exception cref="NotSupportedException"></exception>
        private static void FillDataRowBySheetRow(IRow row, IFormulaEvaluator evaluator, DataRow dataRow)
        {
            if (row == null)
            {
                return;
            }

            for (var j = 0; j < dataRow.Table.Columns.Count; j++)
            {
                var cell = row.GetCell(j);
                if (cell != null)
                {
                    switch (cell.CellType)
                    {
                        case CellType.Blank:
                            dataRow[j] = DBNull.Value;
                            break;
                        case CellType.Boolean:
                            dataRow[j] = cell.BooleanCellValue;
                            break;
                        case CellType.Numeric:
                            if (DateUtil.IsCellDateFormatted(cell))
                            {
                                dataRow[j] = cell.DateCellValue;
                            }
                            else
                            {
                                dataRow[j] = cell.NumericCellValue;
                            }
                            break;
                        case CellType.String:
                            dataRow[j] = !string.IsNullOrWhiteSpace(cell.StringCellValue) ? cell.StringCellValue : DBNull.Value;
                            break;
                        case CellType.Error:
                            dataRow[j] = cell.ErrorCellValue;
                            break;
                        case CellType.Formula:
                            dataRow[j] = evaluator.EvaluateInCell(cell).ToString();
                            break;
                        default:
                            throw new NotSupportedException("不支持该枚举值");
                    }
                }
            }
        }

        /// <summary>
        /// 获取工作簿Sheet中最大的列数
        /// </summary>
        /// <param name="sheet">工作簿</param>
        /// <returns>最大列数</returns>
        private static int GetMaxColumnNumber(ISheet sheet)
        {
            var maxColumnNumber = 0;
            //在有效的行数据中
            for (var i = sheet.FirstRowNum; i <= sheet.LastRowNum; i++)
            {
                var row = sheet.GetRow(i);
                //找到最大的列编号
                if (row != null && row.LastCellNum > maxColumnNumber)
                {
                    maxColumnNumber = row.LastCellNum;
                }
            }

            return maxColumnNumber;
        }

        /// <summary>
        /// 根据Excel文件后缀创建IWorkbook
        /// </summary>
        /// <param name="isXlsx">Excel文件是否是Xlsx后缀</param>
        /// <param name="stream">Excel文件流</param>
        /// <returns>IWorkbook</returns>
        private static IWorkbook CreateWorkbook(bool isXlsx, Stream? stream = null)
        {
            if (stream == null)
            {
                return isXlsx ? new XSSFWorkbook() : new HSSFWorkbook();
            }

            return isXlsx ? new XSSFWorkbook(stream) : new HSSFWorkbook(stream);
        }

        /// <summary>
        /// 填充IWorkbook
        /// </summary>
        /// <param name="dataTables">表格数组</param>
        /// <param name="isXlsx">Excel文件是否是Xlsx后缀</param>
        /// <param name="isColumnNameAsData">表格列名是否作为Excel工作簿第一行数据</param>
        /// <returns>IWorkbook</returns>
        private static IWorkbook CreateWorkbook(DataTable[] dataTables, bool isXlsx, bool isColumnNameAsData)
        {
            //根据Excel文件后缀创建IWorkbook
            var workbook = CreateWorkbook(isXlsx);
            foreach (var dt in dataTables)
            {
                //根据表格填充Sheet
                FillSheetByDataTable(workbook, dt, isColumnNameAsData);
            }

            return workbook;
        }

        /// <summary>
        /// 根据表格填充工作簿Sheet
        /// </summary>
        /// <param name="workbook">IWorkbook</param>
        /// <param name="dataTable">表格</param>
        /// <param name="isColumnNameAsData">表格列名是否作为Excel工作簿第一行数据</param>
        private static void FillSheetByDataTable(IWorkbook workbook, DataTable dataTable, bool isColumnNameAsData)
        {
            var sheet = string.IsNullOrWhiteSpace(dataTable.TableName) ? workbook.CreateSheet() : workbook.CreateSheet(dataTable.TableName);
            if (isColumnNameAsData)
            {
                //把列名加入数据第一行
                var dataRow = sheet.CreateRow(0);
                foreach (DataColumn column in dataTable.Columns)
                {
                    dataRow.CreateCell(column.Ordinal).SetCellValue(column.ColumnName);
                }
            }

            //循环处理表格的所有行数据
            for (var i = 0; i < dataTable.Rows.Count; i++)
            {
                var dataRow = sheet.CreateRow(i + (isColumnNameAsData ? 1 : 0));
                for (var j = 0; j < dataTable.Columns.Count; j++)
                {
                    dataRow.CreateCell(j).SetCellValue(dataTable.Rows[i][j].ToString());
                }
            }
        }

        /// <summary>
        /// 判断Excel文件后缀
        /// </summary>
        /// <param name="fileName">文件名称</param>
        /// <returns>是否是Xlsx格式</returns>
        /// <exception cref="NotSupportedException"></exception>
        private static bool IsXlsxFile(string fileName)
        {
            var extension = Path.GetExtension(fileName) ?? throw new NotSupportedException("Unsupported file types.");
            var ext = extension.ToLower();
            if (ext is not ".xls" and not ".xlsx")
            {
                throw new NotSupportedException("Unsupported file types.");
            }

            return ext == ".xlsx";
        }
    }
}
