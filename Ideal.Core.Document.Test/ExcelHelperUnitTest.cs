using Ideal.Core.Common.Helpers;
using System.ComponentModel;

namespace Ideal.Core.Document.Test
{
    public partial class ExcelHelperUnitTest
    {
        [Fact]
        public void Read_FileName_DataSet()
        {
            //读取所有工作簿
            var dataSet = ExcelHelper.Read("Read.xlsx");
            Assert.Equal(3, dataSet.Tables.Count);
            var table1 = dataSet.Tables[0];
            Assert.Equal("Sheet1", table1.TableName);
            Assert.Equal("A", table1.Rows[0][0]);
            Assert.Equal("B", table1.Rows[0][1]);
            Assert.Equal("1", table1.Rows[0][2]);
            Assert.Equal("C", table1.Rows[1][0]);
            Assert.Equal("D", table1.Rows[1][1]);
            Assert.Equal("2", table1.Rows[1][2]);


            //读取所有工作簿，并且首行数据作为表头
            dataSet = ExcelHelper.Read("Read.xlsx", true);
            Assert.Equal(3, dataSet.Tables.Count);
            table1 = dataSet.Tables[1];
            var columus = table1.Columns;
            Assert.Equal("Sheet2", table1.TableName);
            Assert.Equal("E", columus[0].ColumnName);
            Assert.Equal("F", columus[1].ColumnName);
            Assert.Equal("3", columus[2].ColumnName);
            Assert.Equal("G", table1.Rows[0][0]);
            Assert.Equal("H", table1.Rows[0][1]);
            Assert.Equal("4", table1.Rows[0][2]);

            //根据工作簿名称读取指定工作簿
            dataSet = ExcelHelper.Read("Read.xlsx", true, "Sheet2");
            Assert.Single(dataSet.Tables);
            Assert.Equal("Sheet2", dataSet.Tables[0].TableName);

            //通过sheetName读取不存在的工作簿
            dataSet = ExcelHelper.Read("Read.xlsx", true, "Sheet99");
            Assert.Empty(dataSet.Tables);

            //同时指定sheetName和sheetNumber优先使用sheetName
            dataSet = ExcelHelper.Read("Read.xlsx", true, "Sheet1", 2);
            Assert.Single(dataSet.Tables);
            Assert.Equal("Sheet1", dataSet.Tables[0].TableName);

            //通过sheetNumber读取不存在的工作簿
            dataSet = ExcelHelper.Read("Read.xlsx", true, null, 99);
            Assert.Empty(dataSet.Tables);

            //通过sheetNumber读取指定工作簿
            dataSet = ExcelHelper.Read("Read.xlsx", true, null, 1);
            Assert.Single(dataSet.Tables);
            Assert.Equal("Sheet1", dataSet.Tables[0].TableName);
        }

        public class Student
        {
            public string A { get; set; }
            [Description("B")]
            public string Name { get; set; }
            [Description("1")]
            public DateTime Age { get; set; }
        }

        [Fact]
        public void Read_FileName_T()
        {
            //表格数据格式无法转为对象数据类型，则抛异常
            Assert.Throws<FormatException>(() => ExcelHelper.Read<Student>("Read.xlsx", true, "Sheet1"));

            //表格成功转为对象集合
            var models = ExcelHelper.Read<Student>("Read.xlsx", true, "Sheet3");
            Assert.Single(models);
            var model = models.First();
            Assert.Equal("C", model.A);
            Assert.Equal("D", model.Name);
            Assert.Equal(new DateTime(2024, 11, 29), model.Age);
        }


        [Fact]
        public void Write_Table()
        {
            var table = TableHelper.Create<Student>();
            var row1 = table.NewRow();
            row1[0] = "Id-11";
            row1[1] = "名称-12";
            row1[2] = new DateTime(2024, 11, 28);
            table.Rows.Add(row1);
            var row2 = table.NewRow();
            row2[0] = "Id-21";
            row2[1] = "名称-22";
            row2[2] = new DateTime(2024, 11, 29);
            table.Rows.Add(row2);

            var message = "The column name of the table cannot be mapped to an object property, and the conversion cannot be completed.";

            //把表格写入Excel，并且列名不作为数据行，结果重新读取Excel无法和对象完成转换
            ExcelHelper.Write([table], "Write.xls", false);
            var exception1 = Assert.Throws<NotSupportedException>(() => ExcelHelper.Read<Student>("Write.xls", true, "Sheet0"));
            Assert.Equal(message, exception1.Message);

            //把表格写入Excel，并且列名作为数据行，但是重新读取Excel时第一行没有作为列名，结果还是无法和对象完成转换
            ExcelHelper.Write([table], "Write.xls", true);
            var exception2 = Assert.Throws<NotSupportedException>(() => ExcelHelper.Read<Student>("Write.xls", false, "Sheet0"));
            Assert.Equal(message, exception2.Message);

            //重新读取Excel时第一行作为列名
            var models = ExcelHelper.Read<Student>("Write.xls", true, "Sheet0");
            Assert.Equal(2, models.Count());
            var model = models.First();
            Assert.Equal("Id-11", model.A);
            Assert.Equal("名称-12", model.Name);
            Assert.Equal(new DateTime(2024, 11, 28), model.Age);

            File.Delete("Write.xls");
        }


        [Fact]
        public void Write_T()
        {
            //验证正常情况
            var students = new List<Student>();
            var student1 = new Student
            {
                A = "Id-11",
                Name = "名称-12",
                Age = new DateTime(2024, 11, 28)
            };
            students.Add(student1);
            var student2 = new Student
            {
                A = "Id-21",
                Name = "名称-22",
                Age = new DateTime(2024, 11, 29)
            };
            students.Add(student2);

            var message = "The column name of the table cannot be mapped to an object property, and the conversion cannot be completed.";

            //把对象集合写入Excel，并且列名不作为数据行，结果重新读取Excel无法和对象完成转换
            ExcelHelper.Write<Student>(students, "Write_T.xls", false);
            var exception1 = Assert.Throws<NotSupportedException>(() => ExcelHelper.Read<Student>("Write_T.xls", true, "Sheet0"));
            Assert.Equal(message, exception1.Message);

            //把对象集合写入Excel，并且列名作为数据行，但是重新读取Excel时第一行没有作为列名，结果还是无法和对象完成转换
            ExcelHelper.Write<Student>(students, "Write_T.xls", true);
            var exception2 = Assert.Throws<NotSupportedException>(() => ExcelHelper.Read<Student>("Write_T.xls", false, "Sheet0"));
            Assert.Equal(message, exception2.Message);

            //重新读取Excel时第一行作为列名
            var models = ExcelHelper.Read<Student>("Write_T.xls", true, "Sheet0");
            Assert.Equal(2, models.Count());
            var model = models.First();
            Assert.Equal("Id-11", model.A);
            Assert.Equal("名称-12", model.Name);
            Assert.Equal(new DateTime(2024, 11, 28), model.Age);

            File.Delete("Write_T.xls");
        }
    }


    public partial class TableHelperUnitTest
    {
    }
}