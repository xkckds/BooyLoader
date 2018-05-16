using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.IO.Ports;
using System.Threading;     

namespace BootLoader
{
    public partial class BootLoader : Form
    {
        public Command comd;
        public byte[] receivebytes;
        public Int16 interflag = 1;            // 是否交互 正常需要校验
        //public byte[] codebyte;
        //public byte[] receivedata;
        public SerialPort ComClass = new SerialPort();
        //delegate void ReadDataEventHandler(string text);//委托，此为重点
        public BootLoader()
        {
            InitializeComponent();
            SearchCom();
            comd = new Command();
        }

        // 寻找有用的COM口
        public void SearchCom()
        {
            string[] ArryPort = SerialPort.GetPortNames();// 获取所有有效COM口
            if (ArryPort == null)
            {
                MessageBox.Show("没有找到串口！");
                return;
            }
            else
            {
                ComSelect.Items.Clear();
                for (int i = 0; i < ArryPort.Length; i++)
                {
                    ComSelect.Items.Add(ArryPort[i]);
                }
            }
        }   

        private void FileSelect_Click(object sender, EventArgs e)
        {
            // 选择文件
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Multiselect = false;
            fileDialog.Title = "请选择文件";
            fileDialog.Filter = "所有文件(*.*)|*.*";                //  文件过滤
            if (fileDialog.ShowDialog() == DialogResult.OK)         //  确认选择
            {
                string filepath = fileDialog.FileName;                  //  第一个在对话框中显示的文件或最后一个选取的文件
                FilePath.Text = filepath;                               //  显示路径
                UpGrade.Enabled = true;                                 //  使能升级按钮
                //MessageBox.Show("已选择文件:" + file, "选择文件提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
/*
            // 选择文件夹
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "请选择文件路径";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string foldPath = dialog.SelectedPath;
                FilePath.Text = foldPath;                           //  显示文件夹路径
            }
*/

        }

        // 下发软件代码
        public int SendCodeASCII(byte[] buffer,int offset,int count )
        {
            int status = 0;
            int i = 0;
            byte revchecksum = 0;
            if (interflag == 1)     // 交互需要校验
            {
                for (i = 2; i <= 12; i++)
                {
                    revchecksum += buffer[i];
                }
                revchecksum = (byte)~revchecksum;
                buffer[1] = revchecksum;
            }
            else // 升级不需要校验
            {

            }

            try
            {
               // for(int i=0;i<count;i++)
               //{
               //    ComClass.Write(buffer,i,1);
               //     //checkSum = Convert.ToUInt16(checkSum + bufByte);

               //}
                ComClass.Write(buffer, offset, count);
            }
            catch
            {
                //StatusBar.AppendText(DateTime.Now.ToString("HH:mm:ss  ") + "发送指令失败\r\n");
                MessageBox.Show("发送指令失败");
                status = 1;
            }
            Thread.Sleep(100);
            return status;
        }

        public byte[] ReadCodeASCII()
        {
            //直接读取出字符串
            /*
                        string text = System.IO.File.ReadAllText(@FilePath.Text);
                        Console.WriteLine(text);

                        //按行读取为字符串数组
                        string[] lines = System.IO.File.ReadAllLines(@FilePath.Text);
                        foreach (string line in lines)
                        {
                            Console.WriteLine(line);
                        }

                        //从头到尾以流的方式读出文本文件
                        //该方法会一行一行读出文本
                        FileStream file = new FileStream(FilePath.Text, FileMode.Open, FileAccess.Read);
                        BinaryReader binary = new BinaryReader(file);
                        int length = (int)file.Length;
                        while(length>0)
                        {
                            byte tempByte = binary.ReadByte();
                            string tempStr = Convert.ToString(tempByte, 16);
                           // if (tempStr.Length == 1) tempStr = "0" + temStr;
                             //   sw.Write(tempStr);
                            length--;
                        }
                        file.Close();
                        binary.Close();*/
            using (System.IO.StreamReader code = new System.IO.StreamReader(@FilePath.Text))
            {
                //int slength = 0;
                int sacount = 0;            // 字节个数
                int bytecount = 0;          // 字节缓存计数
                string codestring;          // 读取所有的程序字节
                string[] strarray;          // 分割成字节
                codestring = code.ReadToEnd(); // 读取所有字节
                codestring = codestring.Substring(1, codestring.Length-2); // 去掉首尾
                //int num = codestring.Split(' ').Length - 1;        // 计算由空格分割之后的元素个数 strline里面含有\r\n
                codestring = codestring.Replace("\r", string.Empty).Replace("\n", string.Empty); // 去掉 \r\n 头和尾部还有一个元素
                strarray = codestring.Split(' ');                  // 将字符串分割
                sacount = strarray.Length-1;                       // 分割之后的长度,也就是字节数, 去掉尾
                byte[] codebyte = new byte[sacount];                      // codebyte 总长度
                while (bytecount < sacount)                        // codebyte 总长度从0开始
                {
                    codebyte[bytecount] = Convert.ToByte(strarray[bytecount],16);
                    bytecount++;                                    
                }
                code.Close();
                return codebyte;
                /*
                while ((strline = code.ReadLine()) != null)
                {
                    //int num = strline.Split(' ').Length - 1;
                    slength = strline.Length;            // 字符串长度
                    if (slength >= 2)
                    {                      
                        //byte[] array = System.Text.Encoding.ASCII.GetBytes(strline);
                        strarray = strline.Split(' ');      // 将字符串分割
                        //sacount = strarray.Length;        // 分割之后的长度,也就是字节数
                        sacount = 0;
                        while (sacount < (strarray.Length-1))
                        {
                            codebyte[bytecount] = Convert.ToByte(strarray[sacount], 16);
                            bytecount++;
                            sacount++;                      //  strarray寻址加1                                                        
                        }
                    }

                    //readdate = code.Read();
                    //code.Read(buffer,0,2);
                    //Console.WriteLine(strline);
                }
                 */
            }
        }

        private int SendCodeBytes()
        {
            int status = 0;
            //int codebyteslength = codebyte.Length;
            byte[] shakehand = {0x5A,0xA5};
            //byte[] buffer;  // 缓存数据

            SendCodeASCII(shakehand, 0,shakehand.Length);

            return status;
        }

        public int ReadDatafromCom(byte[] revbyte)
        {
            //bool readflag = true;
            int sleepcount = 0;
            int status = 0;
            while (true)
            {
                if (ComClass.BytesToRead >= revbyte.Length)
                {
                    receivebytes = new byte[ComClass.BytesToRead];
                    ComClass.Read(receivebytes, 0, ComClass.BytesToRead);
                    //readflag = false;
                    status = 0;
                    return status;
                }
                else
                {
                    Thread.Sleep(100);       // 100ms查询一次
                    sleepcount++;
                    if (sleepcount > 100)   // 超时等待
                    {
                        status = 1;
                        return status;
                    }
                }
            }
        }

        private ushort CheckSum(byte[] buf, ushort checkSum)
        {
            //uint crc = 0xFFFFFFFF;
            //ushort checkSum = 0;
            //uint crc_function = 0xEDB88320;
            uint checksum = Convert.ToUInt32(checkSum);
            foreach (byte bufByte in buf)
            {
                checksum = checksum + bufByte;
                checksum &= 0x0000FFFF;
            }
            checkSum = Convert.ToUInt16(checksum);
            return (checkSum);
        }

        private void UpGrade_Click(object sender, EventArgs e)
        {
            int status = 0;
            int transmitedbytes = 0;    // 已发送字节
            ushort readchecksum = 0;
            ushort checksum = 0;
            int sectionsize = 0;          // 段长度
            int sectionbuffer = 0x400 * 2;  // 下位机缓存长度单位16位字，需要乘以2
            //int codebyteslength = 0;
            byte[] codebyte;
            //byte[] receivebytes;
            byte[] buffer;                          // 缓存数据
            codebyte = ReadCodeASCII();             // 读取代码数据
            if ((codebyte[0] != 0xAA) || (codebyte[1] != 0x08))        // 判断代码是否正确
            {
                StatusBar.AppendText(DateTime.Now.ToString("HH:mm:ss  ") + "读取文件失败\r\n");
                return;
            }
            else                                    // 代码正确往下执行
            {
                // 先握手
                Int16 mainflag = 0;             // 是否主界面
                //buffer = new byte[6] { 0xAA, 0x01, 0xCC, 0x33, 0xC3, 0x3C };            // 应用程序需要握手才能继续执行
                //buffer = comd.DSPShakehand;            // 应用程序需要握手才能继续执行
                status = SendCodeASCII(comd.DSPShakehand, 0, comd.DSPShakehand.Length);
                if (status != 0)
                {
                    StatusBar.AppendText(DateTime.Now.ToString("HH:mm:ss  ") + "写入缓存失败\r\n");
                    return;
                }
                receivebytes = new byte[13];
                StatusBar.AppendText(DateTime.Now.ToString("HH:mm:ss  ") + "等待握手...\r\n");
                status = ReadDatafromCom(receivebytes);
                comd.CMDRev = receivebytes;
                if (status == 1)
                {
                    StatusBar.AppendText(DateTime.Now.ToString("HH:mm:ss  ") + "下位机无回应\r\n");
                    StatusBar.AppendText(DateTime.Now.ToString("HH:mm:ss  ") + "握手失败\r\n");
                    return;
                }
                else
                {
                    if ((comd.CMDRev[0] == 0xBB) && (comd.CMDRev[2] == 0x01)
                            && (comd.CMDRev[3] == 0x00) && (comd.CMDRev[5] == 0x59)
                            && (comd.CMDRev[6] == 0x58) && (comd.CMDRev[7] == 0x57)
                            && (comd.CMDRev[8] == 0x56) && (comd.CMDRev[9] == 0xCC)
                            && (comd.CMDRev[10] == 0x33) && (comd.CMDRev[11] == 0xC3)
                            && (comd.CMDRev[12] == 0x3C))
                    {
                        if (comd.CMDRev[4] == 0x55)        // 主程序返回握手
                        {
                            if ((comd.CMDRev[5] == 0x59) && (comd.CMDRev[6] == 0x58)
                                && (comd.CMDRev[7] == 0x57) && (comd.CMDRev[8] == 0x56))    // 应用程序握手成功
                            {
                                mainflag = 1;               // 处于应用程序里面
                                StatusBar.AppendText(DateTime.Now.ToString("HH:mm:ss  ") + "应用程序响应,握手成功\r\n");
                            }
                            else
                            {
                                StatusBar.AppendText(DateTime.Now.ToString("HH:mm:ss  ") + "应用程序响应,握手失败\r\n");
                                return;
                            }
                        }
                        else if (comd.CMDRev[4] == 0xAA)   // 升级程序返回
                        {
                            if ((comd.CMDRev[5] == 0x59) && (comd.CMDRev[6] == 0x58)
                                && (comd.CMDRev[7] == 0x57) && (comd.CMDRev[8] == 0x56))    // 升级程序握手成功
                            {
                                StatusBar.AppendText(DateTime.Now.ToString("HH:mm:ss  ") + "升级程序响应,握手成功\r\n");
                                StatusBar.AppendText(DateTime.Now.ToString("HH:mm:ss  ") + "准备升级\r\n");
                            }
                            else
                            {
                                StatusBar.AppendText(DateTime.Now.ToString("HH:mm:ss  ") + "升级程序响应,握手失败\r\n");
                                return;
                            }
                        }
                    }
                    else
                    {
                        StatusBar.AppendText(DateTime.Now.ToString("HH:mm:ss  ") + "下位机响应握手返回错误\r\n");
                        StatusBar.AppendText(DateTime.Now.ToString("HH:mm:ss  ") + "握手失败\r\n");
                        return;
                    }
                }

                // 从应用程序跳转到升级程序 需要发送升级指令
                if (mainflag == 1)  
                {
                    //buffer = new byte[6] { 0xAA, 0x02, 0xCC, 0x33, 0xC3, 0x3C };            // 发送升级指令
                    status = SendCodeASCII(comd.DSPUpgrade, 0, comd.DSPUpgrade.Length);       // 发送升级指令
                    if (status != 0)
                    {
                        StatusBar.AppendText(DateTime.Now.ToString("HH:mm:ss  ") + "写入缓存失败\r\n");
                        return;
                    }
                    receivebytes = new byte[13];
                    StatusBar.AppendText(DateTime.Now.ToString("HH:mm:ss  ") + "等待握手...\r\n");
                    status = ReadDatafromCom(receivebytes);
                    comd.CMDRev = receivebytes;
                    if (status == 1)
                    {
                        StatusBar.AppendText(DateTime.Now.ToString("HH:mm:ss  ") + "应用程序响应升级指令失败\r\n");
                        StatusBar.AppendText(DateTime.Now.ToString("HH:mm:ss  ") + "握手失败\r\n");
                        return;
                    }
                    else                                                            // 等待等级指令响应
                    {
                        if ((comd.CMDRev[0] == 0xBB) && (comd.CMDRev[2] == 0x01)
                            && (comd.CMDRev[3] == 0x00) && (comd.CMDRev[5] == 0x59)
                            && (comd.CMDRev[6] == 0x58) && (comd.CMDRev[7] == 0x57)
                            && (comd.CMDRev[8] == 0x56) && (comd.CMDRev[9] == 0xCC)
                            && (comd.CMDRev[10] == 0x33) && (comd.CMDRev[11] == 0xC3)
                            && (comd.CMDRev[12] == 0x3C))
                        {
                            switch (comd.CMDRev[4])     // 判断哪个程序响应
                            {
                                case 0x01:              // 应用程序响应
                                    StatusBar.AppendText(DateTime.Now.ToString("HH:mm:ss  ") + "应用程序升级跳转成功\r\n");
                                    Thread.Sleep(1000);       // 等待1s时间
                                    //StatusBar.AppendText(DateTime.Now.ToString("HH:mm:ss  ") + "握手失败\r\n");
                                    break;
                                case 0x02:  // 应用程序反馈I2C异常
                                    StatusBar.AppendText(DateTime.Now.ToString("HH:mm:ss  ") + "应用程序反馈I2C异常\r\n");
                                    StatusBar.AppendText(DateTime.Now.ToString("HH:mm:ss  ") + "升级失败\r\n");
                                    return;
                            }
                        }
                        else
                        {
                            StatusBar.AppendText(DateTime.Now.ToString("HH:mm:ss  ") + "应用程序响应升级返回错误\r\n");
                            StatusBar.AppendText(DateTime.Now.ToString("HH:mm:ss  ") + "升级失败\r\n");
                            return;
                        }
                    }

                    // 跳转完成之后进入升级程序,升级程序需要交互一次
                    //buffer = new byte[6] { 0xAA, 0x02, 0xCC, 0x33, 0xC3, 0x3C };            // 发送升级指令,应用程序与升级代码返回不同的值
                    status = SendCodeASCII(comd.DSPUpgrade, 0, comd.DSPUpgrade.Length);
                    if (status != 0)
                    {
                        StatusBar.AppendText(DateTime.Now.ToString("HH:mm:ss  ") + "写入缓存失败\r\n");
                        return;
                    }
                    receivebytes = new byte[13];
                    StatusBar.AppendText(DateTime.Now.ToString("HH:mm:ss  ") + "等待握手...\r\n");
                    status = ReadDatafromCom(receivebytes);
                    comd.CMDRev = receivebytes;
                    if (status == 1)
                    {
                        StatusBar.AppendText(DateTime.Now.ToString("HH:mm:ss  ") + "应用程序跳转后无回应\r\n");
                        StatusBar.AppendText(DateTime.Now.ToString("HH:mm:ss  ") + "握手失败\r\n");
                        return;
                    }
                    else
                    {
                        if ((comd.CMDRev[0] == 0xBB) && (comd.CMDRev[2] == 0x01)
                            && (comd.CMDRev[3] == 0x00) && (comd.CMDRev[5] == 0x59)
                            && (comd.CMDRev[6] == 0x58) && (comd.CMDRev[7] == 0x57)
                            && (comd.CMDRev[8] == 0x56) && (comd.CMDRev[9] == 0xCC)
                            && (comd.CMDRev[10] == 0x33) && (comd.CMDRev[11] == 0xC3)
                            && (comd.CMDRev[12] == 0x3C))
                        {
                            switch (comd.CMDRev[4])     // 判断哪个程序响应
                            {
                                case 0x01:  // 如果主程序继续响应则表明跳转失败
                                    StatusBar.AppendText(DateTime.Now.ToString("HH:mm:ss  ") + "应用程序跳转失败\r\n");
                                    StatusBar.AppendText(DateTime.Now.ToString("HH:mm:ss  ") + "升级失败\r\n");
                                    return;
                                case 0x02:  // 应用程序反馈I2C异常
                                    StatusBar.AppendText(DateTime.Now.ToString("HH:mm:ss  ") + "应用程序跳转失败\r\n");
                                    StatusBar.AppendText(DateTime.Now.ToString("HH:mm:ss  ") + "升级失败\r\n");
                                    return;
                                case 0xAA:              // 升级程序响应
                                    StatusBar.AppendText(DateTime.Now.ToString("HH:mm:ss  ") + "应用程序跳转成功\r\n");
                                    StatusBar.AppendText(DateTime.Now.ToString("HH:mm:ss  ") + "升级程序握手成功\r\n");
                                    StatusBar.AppendText(DateTime.Now.ToString("HH:mm:ss  ") + "准备升级\r\n");
                                    break;
                                case 0x04:              // 升级程序反馈I2C异常
                                    StatusBar.AppendText(DateTime.Now.ToString("HH:mm:ss  ") + "升级程序反馈I2C异常\r\n");
                                    StatusBar.AppendText(DateTime.Now.ToString("HH:mm:ss  ") + "升级失败\r\n");
                                    return;
                            }
                        }
                        else
                        {
                            StatusBar.AppendText(DateTime.Now.ToString("HH:mm:ss  ") + "应用程序跳转后返回错误\r\n");
                            StatusBar.AppendText(DateTime.Now.ToString("HH:mm:ss  ") + "握手失败\r\n");
                            return;
                        }
                    }
                } // end of if (mainflag == 1)  

                // 开始标志位
                interflag = 0;                                  // 交互结束 代码升级 不需要校验
                buffer = new byte[2] { 0xA5, 0x5A };            // 0xA5是低8位,0x5A是高八位
                status = SendCodeASCII(buffer, 0, buffer.Length);
                if (status != 0)
                {
                    StatusBar.AppendText(DateTime.Now.ToString("HH:mm:ss  ") + "写入缓存失败\r\n");
                    return;
                }
                checksum = CheckSum(buffer, checksum);
                receivebytes = new byte[2];
                StatusBar.AppendText(DateTime.Now.ToString("HH:mm:ss  ") + "等待开始\r\n");
                status = ReadDatafromCom(receivebytes);
                if (status == 1)
                {
                    StatusBar.AppendText(DateTime.Now.ToString("HH:mm:ss  ") + "升级程序无回应\r\n");
                    StatusBar.AppendText(DateTime.Now.ToString("HH:mm:ss  ") + "握手失败\r\n");
                    return;
                }
                else
                {
                    if ((receivebytes[0] != buffer[0]) || (receivebytes[1] != buffer[1]))// 通讯异常 下位机先发低八位再发高八位 与上位机一样
                    {
                        //checksum = 0;                   // 出现异常
                        StatusBar.AppendText(DateTime.Now.ToString("HH:mm:ss  ") + "升级程序通讯异常\r\n");
                        StatusBar.AppendText(DateTime.Now.ToString("HH:mm:ss  ") + "升级失败\r\n");
                        return;
                    }
                    else
                    {
                        StatusBar.AppendText(DateTime.Now.ToString("HH:mm:ss  ") + "开始升级\r\n");
                    }
                }
                /*
                                while (readflag)
                                {
                                    if (ComClass.BytesToRead >= 2)
                                    {
                                        receivebytes = new byte[ComClass.BytesToRead];
                                        ComClass.Read(receivebytes, 0, ComClass.BytesToRead);
                                        if ((receivebytes[0] == 0xA5) && (receivebytes[1] == 0x5A))// 通讯成功
                                            readflag = false;
                                        else
                                        {
                                            StatusBar.AppendText(DateTime.Now.ToString("HH:mm:ss  ") + "通讯异常\r\n");
                                            return;
                                        }
                                    }
                                    else
                                    {
                                        Thread.Sleep(10);       // 10ms查询一次
                                        sleepcount++;
                                        if (sleepcount > 100)   // 超时等待
                                        {
                                            StatusBar.AppendText(DateTime.Now.ToString("HH:mm:ss  ") + "下位机无回应\r\n");
                                            return;
                                        }
                                    }
                                }
                    */
                // 通讯正常下
                // 关键字发送
                buffer = new byte[2];
                Array.Copy(codebyte, transmitedbytes, buffer, 0, buffer.Length);
                status = SendCodeASCII(buffer, 0, buffer.Length);
                if (status != 0)
                {
                    StatusBar.AppendText(DateTime.Now.ToString("HH:mm:ss  ") + "写入缓存失败\r\n");
                    return;
                }
                checksum = CheckSum(buffer, checksum);
                transmitedbytes = transmitedbytes + buffer.Length;
                // 二次校验
                status = ReadDatafromCom(receivebytes);
                if (status == 1)
                {
                    StatusBar.AppendText(DateTime.Now.ToString("HH:mm:ss  ") + "升级程序无回应\r\n");
                    StatusBar.AppendText(DateTime.Now.ToString("HH:mm:ss  ") + "升级失败\r\n");
                    return;
                }
                else
                {
                    if ((receivebytes[0] != buffer[0]) || (receivebytes[1] != buffer[1]))// 二次校验失败 下位机先发低八位再发高八位 与上位机一样
                    {
                        //checksum = 0;                   // 出现异常
                        StatusBar.AppendText(DateTime.Now.ToString("HH:mm:ss  ") + "固件代码异常\r\n");
                        StatusBar.AppendText(DateTime.Now.ToString("HH:mm:ss  ") + "升级失败\r\n");
                        return;
                    }
                    else
                        StatusBar.AppendText(DateTime.Now.ToString("HH:mm:ss  ") + "正在升级...\r\n");
                }
                // 预留字节发送 16字节
                buffer = new byte[16];
                Array.Copy(codebyte, transmitedbytes, buffer, 0, buffer.Length);
                status = SendCodeASCII(buffer, 0, buffer.Length);
                if (status != 0)
                {
                    StatusBar.AppendText(DateTime.Now.ToString("HH:mm:ss  ") + "写入缓存失败\r\n");
                    StatusBar.AppendText(DateTime.Now.ToString("HH:mm:ss  ") + "升级失败\r\n");
                    return;
                }
                checksum = CheckSum(buffer, checksum);
                transmitedbytes = transmitedbytes + buffer.Length;
                // 函数入口地址发送 22位 4字节
                buffer = new byte[4];
                Array.Copy(codebyte, transmitedbytes, buffer, 0, buffer.Length);
                status = SendCodeASCII(buffer, 0, buffer.Length);
                if (status != 0)
                {
                    StatusBar.AppendText(DateTime.Now.ToString("HH:mm:ss  ") + "写入缓存失败\r\n");
                    StatusBar.AppendText(DateTime.Now.ToString("HH:mm:ss  ") + "升级失败\r\n");
                    return;
                }
                checksum = CheckSum(buffer, checksum);
                transmitedbytes = transmitedbytes + buffer.Length;
                // 校验checksum
                status = ReadDatafromCom(receivebytes);
                readchecksum = BitConverter.ToUInt16(receivebytes, 0);
                if (checksum != readchecksum)
                {
                    StatusBar.AppendText(DateTime.Now.ToString("HH:mm:ss  ") + "代码传输异常\r\n");
                    StatusBar.AppendText(DateTime.Now.ToString("HH:mm:ss  ") + "升级失败\r\n");
                    return;
                }
                else
                {
                    checksum = 0;
                    StatusBar.AppendText(DateTime.Now.ToString("HH:mm:ss  ") + "代码段传输正常\r\n");
                }

                while (transmitedbytes < codebyte.Length)   // 存在未发送的字节
                {
                    // 段字节数
                    buffer = new byte[2];
                    Array.Copy(codebyte, transmitedbytes, buffer, 0, buffer.Length);
                    status = SendCodeASCII(buffer, 0, buffer.Length);
                    if (status != 0)
                    {
                        StatusBar.AppendText(DateTime.Now.ToString("HH:mm:ss  ") + "写入缓存失败\r\n");
                        StatusBar.AppendText(DateTime.Now.ToString("HH:mm:ss  ") + "升级失败\r\n");
                        return;
                    }
                    checksum = CheckSum(buffer, checksum);
                    transmitedbytes = transmitedbytes + buffer.Length;
                    sectionsize = 2 * BitConverter.ToInt16(buffer, 0);      // 记录长度 以16位字节来计算的，所以需要乘以2 sectionsize表示8位byte个数
                    if (sectionsize == 0)
                    {
                        break;
                    }
                    // 段起始地址
                    buffer = new byte[4];
                    Array.Copy(codebyte, transmitedbytes, buffer, 0, buffer.Length);
                    status = SendCodeASCII(buffer, 0, buffer.Length);
                    if (status != 0)
                    {
                        StatusBar.AppendText(DateTime.Now.ToString("HH:mm:ss  ") + "写入缓存失败\r\n");
                        StatusBar.AppendText(DateTime.Now.ToString("HH:mm:ss  ") + "升级失败\r\n");
                        return;
                    }
                    checksum = CheckSum(buffer, checksum);
                    transmitedbytes = transmitedbytes + buffer.Length;
                    if (sectionsize > sectionbuffer)
                    {
                        // 发送整块
                        for (short j = 0; j < (sectionsize / sectionbuffer); j++) // 分批发放代码段
                        {
                            buffer = new byte[sectionbuffer];             // 发送整段缓存长度
                            Array.Copy(codebyte, transmitedbytes, buffer, 0, buffer.Length);
                            status = SendCodeASCII(buffer, 0, buffer.Length);
                            if (status != 0)
                            {
                                StatusBar.AppendText(DateTime.Now.ToString("HH:mm:ss  ") + "写入缓存失败\r\n");
                                StatusBar.AppendText(DateTime.Now.ToString("HH:mm:ss  ") + "升级失败\r\n");
                                return;
                            }
                            checksum = CheckSum(buffer, checksum);
                            transmitedbytes = transmitedbytes + buffer.Length;
                            // 校验checksum
                            status = ReadDatafromCom(receivebytes);
                            readchecksum = BitConverter.ToUInt16(receivebytes, 0);  // 0索引开始 BitConverter是从receivebytes[0]是低八位,receivebytes[1]是高八位,
                            if (checksum != readchecksum)
                            {
                                StatusBar.AppendText(DateTime.Now.ToString("HH:mm:ss  ") + "代码传输异常\r\n");
                                StatusBar.AppendText(DateTime.Now.ToString("HH:mm:ss  ") + "升级失败\r\n");
                                return;
                            }
                            else
                            {
                                checksum = 0;
                                StatusBar.AppendText(DateTime.Now.ToString("HH:mm:ss  ") + "代码段传输正常\r\n");
                            }
                        }// for (short j = 0; j < (sectionsize / sectionbuffer); j++)

                        //发送剩余的代码
                        short restcodesize = Convert.ToInt16(sectionsize % sectionbuffer);
                        buffer = new byte[restcodesize];             // 发送剩余代码长度
                        Array.Copy(codebyte, transmitedbytes, buffer, 0, buffer.Length);
                        status = SendCodeASCII(buffer, 0, buffer.Length);
                        if (status != 0)
                        {
                            StatusBar.AppendText(DateTime.Now.ToString("HH:mm:ss  ") + "写入缓存失败\r\n");
                            StatusBar.AppendText(DateTime.Now.ToString("HH:mm:ss  ") + "升级失败\r\n");
                            return;
                        }
                        checksum = CheckSum(buffer, checksum);
                        transmitedbytes = transmitedbytes + buffer.Length;
                        // 校验checksum
                        status = ReadDatafromCom(receivebytes);
                        readchecksum = BitConverter.ToUInt16(receivebytes, 0);
                        if (checksum != readchecksum)
                        {
                            StatusBar.AppendText(DateTime.Now.ToString("HH:mm:ss  ") + "代码传输异常\r\n");
                            StatusBar.AppendText(DateTime.Now.ToString("HH:mm:ss  ") + "升级失败\r\n");
                            return;
                        }
                        else
                        {
                            checksum = 0;
                            StatusBar.AppendText(DateTime.Now.ToString("HH:mm:ss  ") + "代码段传输正常\r\n");
                        }
                    }// if (sectionsize > sectionbuffer)
                    else
                    {
                        buffer = new byte[sectionsize];             // 发送全部代码长度
                        Array.Copy(codebyte, transmitedbytes, buffer, 0, buffer.Length);
                        status = SendCodeASCII(buffer, 0, buffer.Length);
                        if (status != 0)
                        {
                            StatusBar.AppendText(DateTime.Now.ToString("HH:mm:ss  ") + "写入缓存失败\r\n");
                            StatusBar.AppendText(DateTime.Now.ToString("HH:mm:ss  ") + "升级失败\r\n");
                            return;
                        }
                        checksum = CheckSum(buffer, checksum);
                        transmitedbytes = transmitedbytes + buffer.Length;
                        // 校验checksum
                        status = ReadDatafromCom(receivebytes);
                        readchecksum = BitConverter.ToUInt16(receivebytes, 0);
                        if (checksum != readchecksum)
                        {
                            StatusBar.AppendText(DateTime.Now.ToString("HH:mm:ss  ") + "代码传输异常\r\n");
                            StatusBar.AppendText(DateTime.Now.ToString("HH:mm:ss  ") + "升级失败\r\n");
                            return;
                        }
                        else
                        {
                            checksum = 0;
                            StatusBar.AppendText(DateTime.Now.ToString("HH:mm:ss  ") + "代码段传输正常\r\n");
                        }
                    }// if (sectionsize > sectionbuffer) else

                } // while (transmitedbytes < codebyte.Length)   // 存在未发送的字节

                checksum = CheckSum(buffer, checksum);
                buffer = new byte[2] { 0xA5, 0x5A };             // 结束标志
                status = SendCodeASCII(buffer, 0, buffer.Length);
                if (status != 0)
                {
                    StatusBar.AppendText(DateTime.Now.ToString("HH:mm:ss  ") + "写入缓存失败\r\n");
                    StatusBar.AppendText(DateTime.Now.ToString("HH:mm:ss  ") + "升级失败\r\n");
                    return;
                }
                checksum = CheckSum(buffer, checksum);
                // 校验checksum
                status = ReadDatafromCom(receivebytes);
                readchecksum = BitConverter.ToUInt16(receivebytes, 0);
                if (checksum != readchecksum)
                {
                    StatusBar.AppendText(DateTime.Now.ToString("HH:mm:ss  ") + "代码传输异常\r\n");
                    StatusBar.AppendText(DateTime.Now.ToString("HH:mm:ss  ") + "升级失败\r\n");
                    return;
                }
                else
                {
                    checksum = 0;
                    StatusBar.AppendText(DateTime.Now.ToString("HH:mm:ss  ") + "升级完成\r\n");
                }
                interflag = 1;            // 代码升级结束 其它正常交互需要校验
            } // 校验代码正确之后,代码往下执行
        } // 函数


        // 选择并打开串口
        private void ComSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            //            search_com();       // 查询COM口并显示
            //连接并打开串口；
            string comname = " ";
            if (ComSelect.Items != null && ComSelect.Items.Count > 0 && ComSelect.SelectedItem != null)
            {
                comname = ComSelect.SelectedItem.ToString();
            }

            try
            {
                if (ComClass.IsOpen)
                {
                    //StatusBar.AppendText(DateTime.Now.ToString("HH:mm:ss  ")  + comname + "打开失败"+ "串口被占用或无效\r\n");
                    //MessageBox.Show("打开串口" + comname + "失败\r\n");
                    //ComClass.PortName = "fail";
                    //return;
                    ComClass.Close();
                }

                if (!ComClass.IsOpen) // 串口未打开
                {
                    ComClass.PortName = comname;
                    ComClass.BaudRate = 115200;
                    ComClass.DataBits = 8;
                    ComClass.Parity = Parity.None;
                    ComClass.StopBits = StopBits.One;
                    //ComClass.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(ReadDatafromCom);
                    ComClass.Open();
                    if (ComClass.IsOpen)
                    {
                        BaudRate.Enabled = true;    // 使能波特率按钮
                        BaudRate.SelectedIndex = 6; // 115200
                        DataBit.Enabled = true;     // 使能数据位按钮
                        DataBit.SelectedIndex = 1;  // 输出8位
                        StopBit.Enabled = true;     // 使能停止位按钮
                        StopBit.SelectedIndex = 0;  // 1位停止位
                        ParityBit.Enabled = true;   // 使能奇偶校验按钮
                        ParityBit.SelectedIndex = 0;// 无奇偶校验
                        FileSelect.Enabled = true;  // 使能固件升级按钮
                        //shakehand.Enabled = true; // 使能握手按钮
                        CloseCom.Enabled = true;    // 使能串口清除按钮

                        StatusBar.AppendText(DateTime.Now.ToString("HH:mm:ss  ") + "串口" + comname + "连接成功\r\n");
                    }
                }
            }
            catch
            {
                StatusBar.AppendText(DateTime.Now.ToString("HH:mm:ss  ") + comname + "打开失败" + "串口被占用或无效\r\n");
                MessageBox.Show("打开串口" + comname + "失败\r\n");
                return;
            }

        }
        // 串口数据位设置
        private void DataBit_SelectedIndexChanged(object sender, EventArgs e)
        {
            string statustring = "";
            string statustring1 = "数据位 ";
            int inputvalue = 0;
            int indexid = 0;
            if (DataBit.Items != null && DataBit.Items.Count > 0 && DataBit.SelectedItem != null)
            {
                statustring = DataBit.Text;
            }
            if (DataBit.Items != null && DataBit.Items.Count > 0 && DataBit.SelectedItem != null)
            {
                indexid = DataBit.SelectedIndex;
                inputvalue = Convert.ToInt32(statustring);
            }
            if (indexid >= 0)
            {
                switch (indexid)
                {
                    case 0: // 7位
                        ComClass.DataBits = inputvalue;
                        StatusBar.AppendText(DateTime.Now.ToString("HH:mm:ss  ") + statustring1 + statustring + " 切换成功\r\n");
                        break;
                    case 1: // 8位
                        ComClass.DataBits = inputvalue;
                        StatusBar.AppendText(DateTime.Now.ToString("HH:mm:ss  ") + statustring1 + statustring + " 切换成功\r\n");
                        break;
                    //case 2:
                    //    ComClass.DataBits = inputvalue;
                    //    StatusBar.AppendText(DateTime.Now.ToString("HH:mm:ss  ") + statustring1 + statustring + " 切换成功\r\n");
                    //    break;
                    //case 3:
                    //    ComClass.DataBits = inputvalue;
                    //    StatusBar.AppendText(DateTime.Now.ToString("HH:mm:ss  ") + statustring1 + statustring + " 切换成功\r\n");
                    //    break; 
                    default:
                        break;
                }
            }
        }

        // 串口波特率设置
        private void BaudRate_SelectedIndexChanged(object sender, EventArgs e)
        {
            string statustring = "";
            string statustring1 = "波特率 ";
            int inputvalue = 0;
            int indexid = 0;
            if (BaudRate.Items != null && BaudRate.Items.Count > 0 && BaudRate.SelectedItem != null)
            {
                statustring = BaudRate.Text;
            }
            if (BaudRate.Items != null && BaudRate.Items.Count > 0 && BaudRate.SelectedItem != null)
            {
                indexid = BaudRate.SelectedIndex;
                inputvalue = Convert.ToInt32(statustring);
            }
            if (indexid >= 0)
            {
                switch (indexid)
                {
                    case 0:
                        ComClass.BaudRate = inputvalue;
                        StatusBar.AppendText(DateTime.Now.ToString("HH:mm:ss  ") + statustring1 + statustring + " 切换成功\r\n");
                        break;
                    case 1:
                        ComClass.BaudRate = inputvalue;
                        StatusBar.AppendText(DateTime.Now.ToString("HH:mm:ss  ") + statustring1 + statustring + " 切换成功\r\n");
                        break;
                    case 2:
                        ComClass.BaudRate = inputvalue;
                        StatusBar.AppendText(DateTime.Now.ToString("HH:mm:ss  ") + statustring1 + statustring + " 切换成功\r\n");
                        break;
                    case 3:
                        ComClass.BaudRate = inputvalue;
                        StatusBar.AppendText(DateTime.Now.ToString("HH:mm:ss  ") + statustring1 + statustring + " 切换成功\r\n");
                        break;
                    case 4:
                        ComClass.BaudRate = inputvalue;
                        StatusBar.AppendText(DateTime.Now.ToString("HH:mm:ss  ") + statustring1 + statustring + " 切换成功\r\n");
                        break;
                    case 5:
                        ComClass.BaudRate = inputvalue;
                        StatusBar.AppendText(DateTime.Now.ToString("HH:mm:ss  ") + statustring1 + statustring + " 切换成功\r\n");
                        break;
                    case 6:
                        ComClass.BaudRate = inputvalue;
                        StatusBar.AppendText(DateTime.Now.ToString("HH:mm:ss  ") + statustring1 + statustring + " 切换成功\r\n");
                        break;
                    case 7:
                        ComClass.BaudRate = inputvalue;
                        StatusBar.AppendText(DateTime.Now.ToString("HH:mm:ss  ") + statustring1 + statustring + " 切换成功\r\n");
                        break;
                    default:
                        break;
                }
            }
        }

        // 串口停止位设置
        private void StopBit_SelectedIndexChanged(object sender, EventArgs e)
        {
            string statustring = "";
            string statustring1 = "停止位 ";
            int inputvalue = 0;
            int indexid = 0;
            if (StopBit.Items != null && StopBit.Items.Count > 0 && StopBit.SelectedItem != null)
            {
                statustring = StopBit.Text;
            }
            if (StopBit.Items != null && StopBit.Items.Count > 0 && StopBit.SelectedItem != null)
            {
                indexid = StopBit.SelectedIndex;
                inputvalue = Convert.ToInt32(statustring);
            }
            if (indexid >= 0)
            {
                switch (indexid)
                {
                    case 0:
                        ComClass.StopBits = StopBits.One;
                        StatusBar.AppendText(DateTime.Now.ToString("HH:mm:ss  ") + statustring1 + statustring + " 切换成功\r\n");
                        break;
                    case 1:
                        ComClass.StopBits = StopBits.Two;
                        StatusBar.AppendText(DateTime.Now.ToString("HH:mm:ss  ") + statustring1 + statustring + " 切换成功\r\n");
                        break;
                    default:
                        break;
                }
            }

        }

        // 串口奇偶校验位设置
        private void ParityBit_SelectedIndexChanged(object sender, EventArgs e)
        {
            string statustring = "";
            string statustring1 = "奇偶校验 ";
            int indexid = 0;
            if (ParityBit.Items != null && ParityBit.Items.Count > 0 && ParityBit.SelectedItem != null)
            {
                statustring = ParityBit.Text;
            }
            if (ParityBit.Items != null && ParityBit.Items.Count > 0 && ParityBit.SelectedItem != null)
            {
                indexid = ParityBit.SelectedIndex;
            }
            if (indexid >= 0)
            {
                switch (indexid)
                {
                    case 0:
                        ComClass.Parity = Parity.None;
                        StatusBar.AppendText(DateTime.Now.ToString("HH:mm:ss  ") + statustring1 + statustring + " 切换成功\r\n");
                        break;
                    case 1:
                        ComClass.Parity = Parity.Odd;
                        StatusBar.AppendText(DateTime.Now.ToString("HH:mm:ss  ") + statustring1 + statustring + " 切换成功\r\n");
                        break;
                    case 2:
                        ComClass.Parity = Parity.Even;
                        StatusBar.AppendText(DateTime.Now.ToString("HH:mm:ss  ") + statustring1 + statustring + " 切换成功\r\n");
                        break;
                    default:
                        break;
                }
            }

        }

        // 关闭串口
        public void Close_Com()
        {
            //if (comCtx.PortName == "fail")
            //{ }
            //else
            ComClass.Close();
        }    

        // 关闭串口
        private void CloseCom_Click(object sender, EventArgs e)
        {
            // 关闭串口
            if (CloseCom.Text == "关闭串口")
            {
                string statustring = "串口并清除其缓存";
                if (ComClass.IsOpen)
                {
                    ComClass.DiscardInBuffer();//清空输入缓存
                    ComClass.DiscardOutBuffer();//清空输出缓存
                    Close_Com();
                    FileSelect.Enabled = false; // 关闭固件选择按钮
                    UpGrade.Enabled = false;    // 关闭固件升级按钮
                    CloseCom.Text = "打开串口";

                    StatusBar.AppendText(DateTime.Now.ToString("HH:mm:ss  ") + "关闭" + ComClass.PortName + statustring + "成功\r\n");
                }
                else
                {
                    CloseCom.Text = "打开串口";
                    StatusBar.AppendText(DateTime.Now.ToString("HH:mm:ss  ") + ComClass.PortName + "已关闭无法清除缓存\r\n");
                }
            }
            else if (CloseCom.Text == "打开串口")
            {
                if (ComClass.IsOpen)
                {
                    CloseCom.Text = "关闭串口";
                    StatusBar.AppendText(DateTime.Now.ToString("HH:mm:ss  ") + ComClass.PortName + "已打开\r\n");
                }
                else
                {
                    ComClass.Open();              //打开串口
                    ComClass.DiscardInBuffer();   //清空输入缓存
                    ComClass.DiscardOutBuffer();  //清空输出缓存
                    FileSelect.Enabled = true;    // 打开固件选择按钮
                    if (FilePath.TextLength != 0)
                        UpGrade.Enabled = true;   // 打开固件升级按钮
                    CloseCom.Text = "关闭串口";

                    StatusBar.AppendText(DateTime.Now.ToString("HH:mm:ss  ") + ComClass.PortName + "打开成功\r\n");
                }
            }
        }

    }
}
