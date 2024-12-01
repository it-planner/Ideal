# 文档组件
用于导入导出Excel表格，导出PDF文件等
## Excel操作
### 读取
#### 读取Excel至DataSet
根据路径读取
~~~
//读取所有工作簿
var dataSet = ExcelHelper.Read("C:\\Read.xlsx");

//读取所有工作簿，并且首行数据作为表头
var dataSet = ExcelHelper.Read("C:\\Read.xlsx", true);

//根据工作簿名称sheetName读取指定工作簿
var dataSet = ExcelHelper.Read("C:\\Read.xlsx", true, "Sheet2");

//通过工作簿编号sheetNumber读取指定工作簿
var dataSet = ExcelHelper.Read("C:\\Read.xlsx", true, null, 1);
~~~
根据文件流读取
~~~
//读取所有工作簿
var dataSet = ExcelHelper.Read(stream, fileName);
~~~
#### 读取Excel至对象集合
对象既可以是类，也可以是结构体，并且只支持属性并不支持字段。
如果属性指定Description特性，则特性值用于和Excel第一行单元格数据关联，否则使用属性名称和Excel第一行单元格数据关联。
~~~
public class Student
{
    public string A { get; set; }
    [Description("B")]
    public string Name { get; set; }
    [Description("1")]
    public DateTime Age { get; set; }
}
var students = ExcelHelper.Read<Student>("Read.xlsx", true, "Sheet3");
~~~
### 写入表格数组至Excel
写入指定路径Excel
~~~
ExcelHelper.Write([table], "C:\\Write.xls", false)
~~~
写入Excel文件流
~~~
var stream = ExcelHelper.Write([table], true, true)
~~~
### 写入对象集合至Excel
写入指定路径Excel
~~~
ExcelHelper.Write<Student>(students, "C:\\Write_T.xls", true, "学生表");
~~~
写入Excel文件流
~~~
var stream = ExcelHelper.Write<Student>(students, true, true, "学生表");
~~~
