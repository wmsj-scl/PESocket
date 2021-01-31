﻿using PENet;
using Protocol;
using Protocol.C2S;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DBHelper
{
    public static class TxtHelp
    {

        static TxtHelp()
        {
            PETool.logCB += (msg, lv) =>
            {
                WriteLog(FileType.Log, GetLogFileName(), msg);// GetLogFileName(

                if (lv == (int)LogLevel.None)
                {
                    Console.WriteLine(msg);
                }
                else if (lv == (int)LogLevel.Warn)
                {
                    //Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("//--------------------Warn--------------------//");
                    Console.WriteLine(msg);
                    //Console.ForegroundColor = ConsoleColor.Gray;
                }
                else if (lv == (int)LogLevel.Error)
                {
                    //Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("//--------------------Error--------------------//");
                    Console.WriteLine(msg);
                    //Console.ForegroundColor = ConsoleColor.Gray;
                }
                else if (lv == (int)LogLevel.Info)
                {
                    //Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("//--------------------Info--------------------//");
                    Console.WriteLine(msg);
                    //Console.ForegroundColor = ConsoleColor.Gray;
                }
                else
                {
                    //Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("//--------------------Error--------------------//");
                    Console.WriteLine(msg + " >> Unknow Log Type\n");
                    //Console.ForegroundColor = ConsoleColor.Gray;
                }
            };
        }

        private static string GetLogFileName()
        {
            return DateTime.Now.AddDays(1).ToString("yyyy年MM月dd日");
        }

        public const string fileSuffix = ".txt";

        private static string GetDirectoryPatch(FileType fileType)
        {
            var path = Directory.GetCurrentDirectory() + "\\Data\\" + fileType.ToString();
            if (!Directory.Exists(path))
            {
                var basePath = Directory.GetCurrentDirectory() + "\\Data";
                if (!Directory.Exists(basePath))
                {
                    Directory.CreateDirectory(basePath);
                }
                Directory.CreateDirectory(path);
            }

            return Directory.GetCurrentDirectory() + "\\Data\\" + fileType.ToString();
        }

        public static string GetPath(FileType fileType, string name)
        {
            Console.WriteLine(name);
            var path = GetDirectoryPatch(fileType);
            Console.WriteLine(path);
            return path + "\\" + name + fileSuffix;
        }

        /// <summary>
        /// 覆盖写入
        /// </summary>
        /// <param name="fileType"></param>
        /// <param name="name"></param>
        /// <param name="txt"></param>
        public static void Write(FileType fileType, string name, byte[] bytes)
        {
            FileStream fs = new FileStream(GetPath(fileType, name), FileMode.OpenOrCreate);

            //开始写入
            fs.Write(bytes, 0, bytes.Length);
            //清空缓冲区、关闭流
            fs.Flush();
            fs.Close();
            fs.Dispose();
        }

        public static void WriteLog(FileType fileType, string name,string log)
        {
            using (FileStream fs = new FileStream(GetPath(fileType, name), FileMode.Append,FileAccess.Write))
{
                StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);
                sw.WriteLine(log);
                sw.Close();
            }
        }

        /// <summary>
        /// 读取数据
        /// </summary>
        /// <param name="fileType"></param>
        /// <param name="name"></param>
        /// <param name="error"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static byte[] Read(FileType fileType, string name, out ErrorCode error, int size = 1024 * 1024)
        {
            error = ErrorCode.Succeed;
            var filePath = GetPath(fileType, name);
            byte[] res = new Byte[size];



            if (File.Exists(filePath))
            {
                try
                {
                    FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                    fs.Read(res, 0, res.Length);
                    fs.Close();
                    fs.Dispose();
                }
                catch (IOException e)
                {
                    PETool.LogMsg(string.Format("读取文件失败：file:{0} err:{1}", filePath, e.Message));
                    error = ErrorCode.FailedReadFile;
                }
            }
            else
            {
                PETool.LogMsg(string.Format("读取文件失败：file:{0} err:{1}", filePath, "文件不存在!"));
                error = ErrorCode.FailedFileNotExists;
            }

            return res;
        }

        public static string[] GetFileList(FileType fileType)
        {
            return Directory.GetFiles(GetDirectoryPatch(FileType.AccountSingle), ".", SearchOption.AllDirectories);
        }

        public static byte[] ReadByPath(string filePath, out ErrorCode error, int size = 1024 * 1024)
        {
            error = ErrorCode.Succeed;
            byte[] res = new Byte[size];
            if (File.Exists(filePath))
            {
                try
                {
                    FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                    fs.Read(res, 0, res.Length);
                    fs.Close();
                    fs.Dispose();
                }
                catch (IOException e)
                {
                    PETool.LogMsg(string.Format("读取文件失败：file:{0} err:{1}", filePath, e.Message));
                    error = ErrorCode.FailedReadFile;
                }
            }
            else
            {
                error = ErrorCode.FailedFileNotExists;
            }

            return res;
        }

        public static ErrorCode CheckAccountPower(string account, params AccountPower[] accountPower)
        {
            ErrorCode errorCode;
            var redByte = TxtHelp.Read(FileType.AccountSingle, account, out errorCode);
            if (errorCode == ErrorCode.Succeed)
            {
                var readC2SRegisterAccount = PETool.DeSerialize<C2SRegisterAccount>(redByte);
                List<AccountPower> powers = new List<AccountPower>();
                powers.AddRange(accountPower);
                if (powers.Contains(readC2SRegisterAccount.comData.accountPower))
                    errorCode = ErrorCode.Succeed;
                else
                    errorCode = ErrorCode.AccountNoRight;
            }
            return errorCode;
        }
    }
}
