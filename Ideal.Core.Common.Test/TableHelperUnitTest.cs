using Ideal.Core.Common.Helpers;
using System.ComponentModel;
using System.Data;
using Xunit;

namespace Ideal.Core.Common.Test
{
    public struct Student<T>
    {
        [Description("��ʶ")]
        public string Id { get; set; }
        [Description("����")]
        public string Name { get; set; }
        public T Age { get; set; }
    }

    public partial class TableHelperUnitTest
    {
        [Fact]
        public void Create_ColumnNames()
        {
            //���������ɹ�
            var columnNames = new string[] { "A", "B" };
            var table = TableHelper.Create(columnNames);
            Assert.Equal("", table.TableName);
            Assert.Equal(2, table.Columns.Count);
            Assert.Equal(columnNames[0], table.Columns[0].ColumnName);
            Assert.Equal(columnNames[1], table.Columns[1].ColumnName);
            Assert.Equal(typeof(string), table.Columns[0].DataType);
            Assert.Equal(typeof(string), table.Columns[1].DataType);

            //��֤����
            table = TableHelper.Create(columnNames, "test");
            Assert.Equal("test", table.TableName);

            //��֤���������ظ�
            columnNames = new string[] { "A", "A" };
            Assert.Throws<DuplicateNameException>(() => TableHelper.Create(columnNames));
        }

        [Fact]
        public void Create_Columns()
        {
            //���������ɹ�
            var columns = new Dictionary<string, Type>
            {
                { "A", typeof(string) },
                { "B", typeof(int) }
            };

            var table = TableHelper.Create(columns);
            Assert.Equal("", table.TableName);
            Assert.Equal(2, table.Columns.Count);
            Assert.Equal(columns.Keys.ElementAt(0), table.Columns[0].ColumnName);
            Assert.Equal(columns.Keys.ElementAt(1), table.Columns[1].ColumnName);
            Assert.Equal(columns.Values.ElementAt(0), table.Columns[0].DataType);
            Assert.Equal(columns.Values.ElementAt(1), table.Columns[1].DataType);

            //��֤����
            table = TableHelper.Create(columns, "test");
            Assert.Equal("test", table.TableName);
        }


        [Fact]
        public void Create_T()
        {
            //��֤ö��
            var expectedMessage = "T must be a struct or class and cannot be a collection type.";
            var exception1 = Assert.Throws<InvalidOperationException>(() => TableHelper.Create<StatusEnum>());
            Assert.Equal(expectedMessage, exception1.Message);

            //��֤��������
            var exception2 = Assert.Throws<InvalidOperationException>(() => TableHelper.Create<int>());
            Assert.Equal(expectedMessage, exception2.Message);

            //��֤�ַ�������
            var exception3 = Assert.Throws<InvalidOperationException>(() => TableHelper.Create<string>());
            Assert.Equal(expectedMessage, exception3.Message);

            //��֤����
            var exception4 = Assert.Throws<InvalidOperationException>(() => TableHelper.Create<Dictionary<string, Type>>());
            Assert.Equal(expectedMessage, exception4.Message);

            //��֤�������
            var table = TableHelper.Create<Student<double>>();
            Assert.Equal("", table.TableName);
            Assert.Equal(3, table.Columns.Count);
            Assert.Equal("��ʶ", table.Columns[0].ColumnName);
            Assert.Equal("����", table.Columns[1].ColumnName);
            Assert.Equal("Age", table.Columns[2].ColumnName);
            Assert.Equal(typeof(string), table.Columns[0].DataType);
            Assert.Equal(typeof(string), table.Columns[1].DataType);
            Assert.Equal(typeof(double), table.Columns[2].DataType);
        }
    }


    public partial class TableHelperUnitTest
    {
        [Fact]
        public void ToModels()
        {
            //��֤�������
            var table = TableHelper.Create<Student<double>>();
            var row1 = table.NewRow();
            row1[0] = "Id-11";
            row1[1] = "����-12";
            row1[2] = 33.13;
            table.Rows.Add(row1);
            var row2 = table.NewRow();
            row2[0] = "Id-21";
            row2[1] = "����-22";
            row2[2] = 33.23;
            table.Rows.Add(row2);

            var students = TableHelper.ToModels<Student<double>>(table);
            Assert.Equal(2, students.Count());
            Assert.Equal("Id-11", students.ElementAt(0).Id);
            Assert.Equal("����-12", students.ElementAt(0).Name);
            Assert.Equal(33.13, students.ElementAt(0).Age);
            Assert.Equal("Id-21", students.ElementAt(1).Id);
            Assert.Equal("����-22", students.ElementAt(1).Name);
            Assert.Equal(33.23, students.ElementAt(1).Age);
        }

        [Fact]
        public void ToDataTable()
        {
            //��֤�������
            var students = new List<Student<double>>();
            var student1 = new Student<double>
            {
                Id = "Id-11",
                Name = "����-12",
                Age = 33.13
            };
            students.Add(student1);
            var student2 = new Student<double>
            {
                Id = "Id-21",
                Name = "����-22",
                Age = 33.23
            };
            students.Add(student2);

            var table = TableHelper.ToDataTable<Student<double>>(students, "ѧ����");
            Assert.Equal("ѧ����", table.TableName);
            Assert.Equal(2, table.Rows.Count);
            Assert.Equal("Id-11", table.Rows[0][0]);
            Assert.Equal("����-12", table.Rows[0][1]);
            Assert.Equal("33.13", table.Rows[0][2].ToString());
            Assert.Equal("Id-21", table.Rows[1][0]);
            Assert.Equal("����-22", table.Rows[1][1]);
            Assert.Equal("33.23", table.Rows[1][2].ToString());
        }

        [Fact]
        public void ToDataTableWithColumnArray()
        {
            //��֤�������
            var columns = new string[] { "A", "B" };
            var table = TableHelper.ToDataTableWithColumnArray<string>(columns, "ѧ����");
            Assert.Equal("ѧ����", table.TableName);
            Assert.Equal("Column1", table.Columns[0].ColumnName);
            Assert.Equal(2, table.Rows.Count);
            Assert.Equal("A", table.Rows[0][0]);
            Assert.Equal("B", table.Rows[1][0]);

            table = TableHelper.ToDataTableWithColumnArray<string>(columns, "ѧ����", "��");
            Assert.Equal("��", table.Columns[0].ColumnName);
        }

        [Fact]
        public void ToDataTableWithRowArray()
        {
            //��֤�������
            var rows = new string[] { "A", "B" };
            var table = TableHelper.ToDataTableWithRowArray<string>(rows);
            Assert.Equal("", table.TableName);
            Assert.Equal("Column1", table.Columns[0].ColumnName);
            Assert.Equal("Column2", table.Columns[1].ColumnName);
            Assert.Equal(1, table.Rows.Count);
            Assert.Equal("A", table.Rows[0][0]);
            Assert.Equal("B", table.Rows[0][1]);

            table = TableHelper.ToDataTableWithRowArray<string>(rows, "ѧ����");
            Assert.Equal("ѧ����", table.TableName);
        }

        [Fact]
        public void Transpose_ColumnNameAsData()
        {
            var originalTable = new DataTable("����");
            originalTable.Columns.Add("A", typeof(string));
            originalTable.Columns.Add("B", typeof(int));
            originalTable.Columns.Add("C", typeof(int));

            originalTable.Rows.Add("D", 1, 2);

            //������Ϊ���ݵ����
            var table = TableHelper.Transpose(originalTable, true);

            Assert.Equal(originalTable.TableName, table.TableName);
            Assert.Equal("Column1", table.Columns[0].ColumnName);
            Assert.Equal("Column2", table.Columns[1].ColumnName);
            Assert.Equal(3, table.Rows.Count);
            Assert.Equal("A", table.Rows[0][0]);
            Assert.Equal("D", table.Rows[0][1]);
            Assert.Equal("B", table.Rows[1][0]);
            Assert.Equal("1", table.Rows[1][1].ToString());
            Assert.Equal("C", table.Rows[2][0]);
            Assert.Equal("2", table.Rows[2][1].ToString());
        }

        [Fact]
        public void Transpose_ColumnNameNotAsData()
        {
            var originalTable = new DataTable("����");
            originalTable.Columns.Add("A", typeof(string));
            originalTable.Columns.Add("B", typeof(int));
            originalTable.Columns.Add("C", typeof(int));

            originalTable.Rows.Add("D", 1, 2);
            originalTable.Rows.Add("E", 3, 4);

            //��������Ϊ���ݵ����
            var table = TableHelper.Transpose(originalTable, false);

            Assert.Equal(originalTable.TableName, table.TableName);
            Assert.Equal("Column1", table.Columns[0].ColumnName);
            Assert.Equal("Column2", table.Columns[1].ColumnName);
            Assert.Equal(3, table.Rows.Count);
            Assert.Equal("D", table.Rows[0][0]);
            Assert.Equal("E", table.Rows[0][1]);
            Assert.Equal("1", table.Rows[1][0].ToString());
            Assert.Equal("3", table.Rows[1][1].ToString());
            Assert.Equal("2", table.Rows[2][0].ToString());
            Assert.Equal("4", table.Rows[2][1].ToString());
        }
    }
}







