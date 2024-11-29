using Ideal.Core.Common.Helpers;
using System.ComponentModel;

namespace Ideal.Core.Document.Test
{
    public partial class ExcelHelperUnitTest
    {
        [Fact]
        public void Read_FileName_DataSet()
        {
            //��ȡ���й�����
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


            //��ȡ���й���������������������Ϊ��ͷ
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

            //���ݹ��������ƶ�ȡָ��������
            dataSet = ExcelHelper.Read("Read.xlsx", true, "Sheet2");
            Assert.Single(dataSet.Tables);
            Assert.Equal("Sheet2", dataSet.Tables[0].TableName);

            //ͨ��sheetName��ȡ�����ڵĹ�����
            dataSet = ExcelHelper.Read("Read.xlsx", true, "Sheet99");
            Assert.Empty(dataSet.Tables);

            //ͬʱָ��sheetName��sheetNumber����ʹ��sheetName
            dataSet = ExcelHelper.Read("Read.xlsx", true, "Sheet1", 2);
            Assert.Single(dataSet.Tables);
            Assert.Equal("Sheet1", dataSet.Tables[0].TableName);

            //ͨ��sheetNumber��ȡ�����ڵĹ�����
            dataSet = ExcelHelper.Read("Read.xlsx", true, null, 99);
            Assert.Empty(dataSet.Tables);

            //ͨ��sheetNumber��ȡָ��������
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
            //������ݸ�ʽ�޷�תΪ�����������ͣ������쳣
            Assert.Throws<FormatException>(() => ExcelHelper.Read<Student>("Read.xlsx", true, "Sheet1"));

            //���ɹ�תΪ���󼯺�
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
            row1[1] = "����-12";
            row1[2] = new DateTime(2024, 11, 28);
            table.Rows.Add(row1);
            var row2 = table.NewRow();
            row2[0] = "Id-21";
            row2[1] = "����-22";
            row2[2] = new DateTime(2024, 11, 29);
            table.Rows.Add(row2);

            var message = "The column name of the table cannot be mapped to an object property, and the conversion cannot be completed.";

            //�ѱ��д��Excel��������������Ϊ�����У�������¶�ȡExcel�޷��Ͷ������ת��
            ExcelHelper.Write([table], "Write.xls", false);
            var exception1 = Assert.Throws<NotSupportedException>(() => ExcelHelper.Read<Student>("Write.xls", true, "Sheet0"));
            Assert.Equal(message, exception1.Message);

            //�ѱ��д��Excel������������Ϊ�����У��������¶�ȡExcelʱ��һ��û����Ϊ��������������޷��Ͷ������ת��
            ExcelHelper.Write([table], "Write.xls", true);
            var exception2 = Assert.Throws<NotSupportedException>(() => ExcelHelper.Read<Student>("Write.xls", false, "Sheet0"));
            Assert.Equal(message, exception2.Message);

            //���¶�ȡExcelʱ��һ����Ϊ����
            var models = ExcelHelper.Read<Student>("Write.xls", true, "Sheet0");
            Assert.Equal(2, models.Count());
            var model = models.First();
            Assert.Equal("Id-11", model.A);
            Assert.Equal("����-12", model.Name);
            Assert.Equal(new DateTime(2024, 11, 28), model.Age);

            File.Delete("Write.xls");
        }


        [Fact]
        public void Write_T()
        {
            //��֤�������
            var students = new List<Student>();
            var student1 = new Student
            {
                A = "Id-11",
                Name = "����-12",
                Age = new DateTime(2024, 11, 28)
            };
            students.Add(student1);
            var student2 = new Student
            {
                A = "Id-21",
                Name = "����-22",
                Age = new DateTime(2024, 11, 29)
            };
            students.Add(student2);

            var message = "The column name of the table cannot be mapped to an object property, and the conversion cannot be completed.";

            //�Ѷ��󼯺�д��Excel��������������Ϊ�����У�������¶�ȡExcel�޷��Ͷ������ת��
            ExcelHelper.Write<Student>(students, "Write_T.xls", false);
            var exception1 = Assert.Throws<NotSupportedException>(() => ExcelHelper.Read<Student>("Write_T.xls", true, "Sheet0"));
            Assert.Equal(message, exception1.Message);

            //�Ѷ��󼯺�д��Excel������������Ϊ�����У��������¶�ȡExcelʱ��һ��û����Ϊ��������������޷��Ͷ������ת��
            ExcelHelper.Write<Student>(students, "Write_T.xls", true);
            var exception2 = Assert.Throws<NotSupportedException>(() => ExcelHelper.Read<Student>("Write_T.xls", false, "Sheet0"));
            Assert.Equal(message, exception2.Message);

            //���¶�ȡExcelʱ��һ����Ϊ����
            var models = ExcelHelper.Read<Student>("Write_T.xls", true, "Sheet0");
            Assert.Equal(2, models.Count());
            var model = models.First();
            Assert.Equal("Id-11", model.A);
            Assert.Equal("����-12", model.Name);
            Assert.Equal(new DateTime(2024, 11, 28), model.Age);

            File.Delete("Write_T.xls");
        }
    }


    public partial class TableHelperUnitTest
    {
    }
}