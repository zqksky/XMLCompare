using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Collections;
using System.IO;
using System.Xml.Linq;

namespace xmlCompare
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            txtPath1.Text = @"D:\xmlTest\xmlFile1\";
            txtPath2.Text = @"D:\xmlTest\xmlFile2\";
            //getPath(1);
        }

        private void btnOpenPath1_Click(object sender, EventArgs e)
        {
            //txtPath1.Text = getFilePath(1);
            string stemp = getPath(1);
            if (stemp == "ERROR")
            {
                MessageBox.Show("请选择包含xml文件的路径");
                txtPath1.Text = "";
            }
            else
            {
                txtPath1.Text = stemp;
            }
        }

        private void btnOpenPath2_Click(object sender, EventArgs e)
        {
            //txtPath2.Text = getFilePath(2);
            string stemp = getPath(2);
            if (stemp == "ERROR")
            {
                MessageBox.Show("请选择包含xml文件的路径");
                txtPath2.Text = "";
            }
            else
            {
                txtPath2.Text = stemp;
            }
        }

        List<string> strListFileName = new List<string>();
        private void btnTest_Click(object sender, EventArgs e)
        {
            if (txtPath1.Text.ToString() == "" || txtPath2.Text.ToString() == "")
            {
                MessageBox.Show("文件路径不能为空！");
            }
            else if (!xmlExist(txtPath1.Text.ToString()))
            {
                MessageBox.Show("文件路径1中没有xml文件！");
            }
            else if (!xmlExist(txtPath2.Text.ToString()))
            {
                MessageBox.Show("文件路径2中没有xml文件！");
            }
            else
            {
                Thread thread = new Thread(() =>
                {
                    bool flagSame = false;

                    string strPath1 = txtPath1.Text;
                    string strPath2 = txtPath2.Text;
                    string strSavePath = strPath1.TrimEnd('\\');
                    strSavePath = strSavePath.Substring(0, strSavePath.LastIndexOf('\\') + 1);

                    Hashtable ht1 = new Hashtable();
                    Hashtable ht2 = new Hashtable();
                    List<string> strPathList1 = new List<string>();
                    List<string> strPathList2 = new List<string>();

                    strPathList1 = getXmlFile(strPath1);
                    strPathList2 = getXmlFile(strPath2);
                    foreach (var str in strPathList1)
                    {
                        if (strPathList2.Contains(str))
                        {
                            ht1 = xmlToHashtable(strPath1 + str);
                            ht2 = xmlToHashtable(strPath2 + str);

                            flagSame = xmlCompare(ht1, ht2, strSavePath + str.Substring(0, str.IndexOf('.')) + ".csv");

                            if (!flagSame)
                            {
                                strListFileName.Add(str.Substring(0, str.IndexOf('.')) + ".csv");
                            }
                        }
                    }
                    if (File.Exists(strSavePath + "result.xlsx"))
                    {
                        //存在
                        //File.Delete(strSavePath + "result.xlsx");
                    }
                    else
                    {
                        //不存在
                    }

                    saveCsvToExcel(strListFileName, strSavePath + "result.xlsx");

                    //MethodCall mc = new MethodCall(saveCsvToExcel);
                    //IAsyncResult result = mc.BeginInvoke(strListFileName, strSavePath + "result.xlsx", null, null);
                    //mc.EndInvoke(result);//用于接收返回值 
                });
                thread.Start();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #region 方法
        private string getFilePath(int fileNum)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "xml文件(*.xml)|;*.xml|所有文件|*.*";
            ofd.ValidateNames = true;
            ofd.CheckPathExists = true;
            ofd.CheckFileExists = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string strFileName = ofd.FileName;
                strFileName = strFileName.Substring(0, strFileName.LastIndexOf('\\') + 1);
                return strFileName;
            }
            else
            {
                if (xmlExist(@"D:\xmlTest\xmlFile" + fileNum))
                {
                    string strFileName = @"D:\xmlTest\xmlFile" + fileNum + "\\";
                    return strFileName;
                }
                else
                {
                    return "ERROR";
                }
            }
        }
        private string getPath(int fileNum)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "请选择XML所在文件夹";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string strFileName = dialog.SelectedPath;
                if (xmlExist(strFileName))
                {
                    strFileName += "\\";
                    //MessageBox.Show(strFileName);
                    return strFileName;
                }
                else
                {
                    return "ERROR";
                }
            }
            else
            {
                if (xmlExist(@"D:\xmlTest\xmlFile" + fileNum))
                {
                    string strFileName = @"D:\xmlTest\xmlFile" + fileNum + "\\";
                    return strFileName;
                }
                else
                {
                    return "ERROR";
                }
            }
        }
        private bool xmlExist(string strPath)
        {
            //判断文件路径是否存在，不存在则创建文件夹 
            if (!Directory.Exists(strPath))
            {
                //System.IO.Directory.CreateDirectory(@"D:\Export");//不存在就创建目录 
                return false;
            }
            else
            {
                foreach (string str in Directory.GetFiles(strPath))
                {
                    string strTemp = str.Substring(str.LastIndexOf('.') + 1);
                    strTemp = strTemp.ToUpper();
                    //strTemp = strTemp.ToLower();
                    //MessageBox.Show(strTemp);
                    if (strTemp == "XML")
                    {
                        return true;
                    }
                }
                return false;
            }
        }
        private List<string> getXmlFile(string path)
        {
            //path = @"D:\Qiankun Zheng\zqk\mySpireTest\";
            List<string> strList = new List<string>();
            DirectoryInfo directory = new DirectoryInfo(path);

            FileInfo[] files = directory.GetFiles("*.xml");

            //输出文件个数 
            //MessageBox.Show(files.Length.ToString()); 

            //遍历文件 
            foreach (FileInfo file in files)
            {
                strList.Add(file.Name);
                //MessageBox.Show(file.Name); 
                //MessageBox.Show(file.Directory.ToString()); 
            }
            return strList;
        }

        private void saveCsvToExcel(List<string> strListFileName, string strSaveFilePath)
        {
            string strCsvPath;
            strCsvPath = txtPath1.Text;
            strCsvPath = strCsvPath.TrimEnd('\\');
            strCsvPath = strCsvPath.Substring(0,strCsvPath.LastIndexOf('\\')+1);

            //ExcelOperator myExcel = null;
            ExcelOperator myExcel = new ExcelOperator();

            myExcel.Create();
            //myExcel.Open(@"D:\Qiankun Zheng\zqk\mySpireTest\file\result.xlsx");
            foreach (var str in strListFileName)
            {
                string strTemp = str.Substring(0, str.IndexOf('.'));

                //sheet表名不能超过31个字符
                if (strTemp.Length > 31)
                {
                    strTemp = strTemp.Substring(0, 30);
                }

                myExcel.ImportCSV(strCsvPath + str, myExcel.AddSheet(strTemp),
                              (Microsoft.Office.Interop.Excel.Range)((myExcel.GetSheet(strTemp)).get_Range("$A$1")),
                              new int[] { 2, 2, 2, 2, 2 }, true);
            }

            myExcel.SaveAs(strSaveFilePath);
            //myExcel.SaveAs(@"D:\Qiankun Zheng\zqk\mySpireTest\file\result.xlsx");
            myExcel.Close();
        }

        private Hashtable xmlToHashtable(string xmlPath)
        {
            Hashtable ht = new Hashtable();

            XElement xe = XElement.Load(xmlPath);
            IEnumerable<XElement> elements = from ele in xe.Elements("RULES").Elements("SAPDATA").Elements("CONFIGURATION").Elements("INST").Elements("CSTICS").Elements("CSTIC")
                                             select ele;
            foreach (var ele in elements)
            {
                string strKey;
                string strValue;

                strKey = ele.Attribute("CHARC").Value;
                strValue = ele.Attribute("VALUE").Value;
                if (ele.LastAttribute.Name == "VALUE_TXT")
                {
                    strValue += ";" + ele.Attribute("VALUE_TXT").Value;
                }
                //MessageBox.Show("strKey=" + strKey + "; strValue=" + strValue);
                ht.Add(strKey, strValue);
            }
            return ht;
        }
        private bool xmlCompare(Hashtable ht1, Hashtable ht2, string SaveFilePath)
        {
            bool flagSame = false;

            List<string> strList1 = new List<string>();
            List<string> strList2 = new List<string>();
            List<string> strList3 = new List<string>();
            //SaveFilePath = @"D:\Qiankun Zheng\zqk\mySpireTest\test.csv";

            foreach (DictionaryEntry de in ht1)
            {
                if (ht2.ContainsKey(de.Key))
                {
                    if (de.Value.ToString() == ht2[de.Key].ToString())
                    {
                        ht2.Remove(de.Key);
                    }
                    else
                    {
                        strList1.Add(de.Key.ToString() + "," + formatStr(de.Value.ToString()) + "," + formatStr(ht2[de.Key].ToString()));
                        //strList1.Add(de.Key.ToString() + "," + csvHandlerStr(formatStr(de.Value.ToString())) + "," + csvHandlerStr(formatStr(ht2[de.Key].ToString())));
                        ht2.Remove(de.Key);
                    }
                }
                else
                {
                    strList2.Add(de.Key.ToString() + "," + formatStr(de.Value.ToString()));
                    //strList2.Add(de.Key.ToString() + "," + csvHandlerStr(formatStr(de.Value.ToString())));

                }
            }
            foreach (DictionaryEntry de in ht2)
            {
                strList3.Add(de.Key.ToString() + "," + formatStr(de.Value.ToString()));
            }
            if ((strList1.Count + strList2.Count + strList3.Count) > 0)
            {
                string str = SaveFilePath.Substring(SaveFilePath.LastIndexOf('\\') + 1);

                //若CSV文件已经打开，先关闭文件
                //closeExcel(str);

                using (FileStream fs = new FileStream(SaveFilePath, FileMode.Create))
                {
                    StreamWriter sw = new StreamWriter(fs, UTF8Encoding.UTF8);
                    //开始写入
                    sw.WriteLine("CHARC,VALUE,VALUE_TEXT,VALUE,VALUE_TEXT");
                    if (strList1.Count > 0)
                    {
                        sw.WriteLine("The data in A and B is not the same");
                        foreach (var s in strList1)
                        {
                            sw.WriteLine(s);
                        }
                    }
                    if (strList2.Count > 0)
                    {
                        sw.WriteLine("There is no file1 in file2");
                        foreach (var s in strList2)
                        {
                            sw.WriteLine(s);
                        }
                    }
                    if (strList3.Count > 0)
                    {
                        sw.WriteLine("There is no file2 in file1");
                        foreach (var s in strList3)
                        {
                            sw.WriteLine(s);
                        }
                    }
                    //清空缓冲区
                    sw.Flush();
                    //关闭流
                    sw.Close();
                    fs.Close();
                    //System.Diagnostics.Process.Start(SaveFilePath); //打开此文件
                }
                flagSame = false;
            }
            else
            {
                string str = SaveFilePath.Substring(SaveFilePath.LastIndexOf('\\') + 1);
                str = str.Substring(0, str.LastIndexOf('.'));
                //MessageBox.Show(str + " 结果相同");
                flagSame = true;
            }
            return flagSame;
        }

        private string formatStr(string str)
        {
            string strValue = "";

            string[] strArrayTemp = new string[2];
            strArrayTemp = str.Split(';');
            if (strArrayTemp.Length == 1)
            {
                strValue = strArrayTemp[0] + ",";
            }
            else
            {
                for (int i = 0; i < strArrayTemp.Length; i++)
                {
                    strValue += strArrayTemp[i] + ",";
                }
                strValue = strValue.Trim(',');
            }
            return strValue;
        }
        #endregion
    }
}
