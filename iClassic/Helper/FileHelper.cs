﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace iClassic.Helper
{
    public class FileHelper
    {
        public const string ROOT = "/Uploads";
        #region Files
        public static string GetExtension(string fileName, out string baseName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                baseName = null;
                return null;
            }
            int k = fileName.LastIndexOf('.');
            if (k > 0)
            {
                baseName = fileName.Substring(0, k);
                return fileName.Substring(k, fileName.Length - k);
            }
            else
            {
                baseName = fileName;
                return null;
            }
        }
        public static string CreateFileName(string fileName, int? id = null)
        {
            string basename = null;
            string extension = GetExtension(fileName, out basename);
            fileName = (id == null ? null : id.Value + "-") + Helpers.CreateURLParam(basename) + extension;
            return fileName;
        }

        /// <summary>
        /// The method will create folder, fileFullName automaticlly with format /Root/folder/fileName
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="folder"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string CreateFullName(string fileName, string folder, int? id = null)
        {
            string dir = CreateDirectory(folder);
            fileName = CreateFileName(fileName, id);
            fileName = string.Format("{0}/{1}", dir, fileName).Replace("//", "/");
            return fileName;
        }

        /// <summary>
        /// The method will create folder, fileFullName automaticlly with format /Root/folder/year/month/fileName
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="folder"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string CreateFullName(string fileName, string folder, DateTime date, int? id = null)
        {
            string dir = CreateDirectory(folder, date);
            fileName = CreateFileName(fileName, id);
            fileName = string.Format("{0}/{1}", dir, fileName).Replace("//", "/");
            return fileName;
        }
        /// <summary>
        /// Kiểm tra file có tồn tại hay không. Nếu tên file = null or empty thì return false
        /// </summary>
        public static bool FileExist(string fullName)
        {
            if (string.IsNullOrEmpty(fullName)) return false;
            return System.IO.File.Exists(HttpContext.Current.Server.MapPath(fullName));
        }

        public static List<string> ReadLineToList(string fullName)
        {
            var s = ReadString(fullName);
            if (s == null) return null;
            return s.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToList();
        }

        public static byte[] Read(string fullName)
        {
            try
            {
                fullName = HttpContext.Current.Server.MapPath(fullName);
                using (System.IO.FileStream filestream = new System.IO.FileStream(fullName, FileMode.Open))
                {
                    var buffer = new byte[filestream.Length];
                    filestream.Read(buffer, 0, buffer.Length);
                    return buffer;
                }
            }
            catch { return null; }
        }

        public static string ReadString(string fullName)
        {
            try
            {
                fullName = HttpContext.Current.Server.MapPath(fullName);
                using (System.IO.StreamReader filestream = new System.IO.StreamReader(fullName))
                {
                    return filestream.ReadToEnd();
                }
            }
            catch { return null; }
        }

        /// <summary>
        /// Tạo file với full name = /Root/folder/filename
        /// </summary>
        /// <param name="file"></param>
        /// <param name="folder"></param>
        /// <returns></returns>
        public static string CreateFile(HttpPostedFileBase file, string folder)
        {
            var fullName = CreateFullName(file.FileName, folder);
            file.SaveAs(HttpContext.Current.Server.MapPath(fullName));
            return fullName;
        }
        /// <summary>
        /// Tạo file với full name = /Root/folder/year/month/filename
        /// </summary>
        /// <param name="file"></param>
        /// <param name="folder"></param>
        /// <returns></returns>
        public static string CreateFile(HttpPostedFileBase file, string folder, DateTime date)
        {
            var fullName = CreateFullName(file.FileName, folder, date);
            file.SaveAs(HttpContext.Current.Server.MapPath(fullName));
            return fullName;
        }
        /// <summary>
        /// Tạo file với full name = /Root/year/month/filename
        /// </summary>
        /// <param name="file"></param>
        /// <param name="folder"></param>
        /// <returns></returns>
        public static string CreateFile(HttpPostedFileBase file, DateTime? date = null)
        {
            date = date == null ? DateTime.Now : date;
            var fullName = CreateFullName(file.FileName, null, date.Value);
            file.SaveAs(HttpContext.Current.Server.MapPath(fullName));
            return fullName;
        }
        /// <summary>
        /// Tạo file với full name = /Root/folder/year/month/id-filename
        /// </summary>
        /// <param name="file"></param>
        /// <param name="folder"></param>
        /// <returns></returns>
        public static string CreateFile(HttpPostedFileBase file, string folder, int id, DateTime? date = null)
        {
            date = date == null ? DateTime.Now : date;
            var fullName = CreateFullName(file.FileName, folder, date.Value, id);
            file.SaveAs(HttpContext.Current.Server.MapPath(fullName));
            return fullName;
        }
        /// <summary>
        /// Tạo file với full name = /Root/folder/year/month/milisecond-filename
        /// </summary>
        /// <param name="file"></param>
        /// <param name="folder"></param>
        /// <returns></returns>
        public static string CreateFile(HttpPostedFileBase file, string folder, bool overrideExists, DateTime? date = null)
        {
            date = date == null ? DateTime.Now : date;
            if (overrideExists)
            {
                return CreateFile(file, folder, date.Value);
            }

            string fileName = file.FileName;
            var fullName = CreateFullName(fileName, folder, date.Value);
            while (FileExist(fullName))
            {
                fileName = DateTime.Now.Millisecond + "-" + fileName;
                fullName = CreateFullName(fileName, folder, date.Value);
            }

            file.SaveAs(HttpContext.Current.Server.MapPath(fullName));
            return fullName;
        }

        /// <summary>
        /// Tạo file đồng thời tạo thư mục con dạng folder/Năm/Tháng/id-FileName lấy từ biến date. fileName sẽ được xử lý về dạng chuẩn. Return fullName được chuẩn hóa
        /// </summary>
        public static string CreateFile(byte[] data, string fileName, string folder, int id, DateTime? date = null)
        {
            date = date == null ? DateTime.Now : date;
            var fullName = CreateFullName(fileName, folder, date.Value, id);
            CreateFile(data, fullName);
            return fullName;
        }
        /// <summary>
        /// Tạo file, giữ nguyên fullName. Nếu trong fullName có folder chưa tồn tại trên hệ thống thì sẽ báo lỗi
        /// </summary>
        public static void CreateFile(byte[] data, string fullName, bool overrideExists = false)
        {
            if (overrideExists)
                DeleteFile(fullName);
            using (System.IO.FileStream filestream = new System.IO.FileStream(HttpContext.Current.Server.MapPath(fullName), System.IO.FileMode.Create))
            {
                filestream.Write(data, 0, data.Length);
            }
        }
        /// <summary>
        /// Không xảy ra exception nếu file ko tồn tại hoặc tên file = null or empty
        /// </summary>
        public static void DeleteFile(string fullName)
        {
            if (FileExist(fullName))
                System.IO.File.Delete(HttpContext.Current.Server.MapPath(fullName));
        }
        #endregion

        #region Directories
        public static bool DirectoryExist(string dir, bool appendRoot = true)
        {
            if (appendRoot)
            {
                dir = string.Format("{0}/{1}", ROOT, dir).Replace("//", "/");
                return System.IO.Directory.Exists(dir);
            }
            return System.IO.Directory.Exists(dir);
        }
        /// <summary>
        /// Create dir with format /Root/dir/
        /// </summary>
        /// <param name="dir"></param>
        /// <param name="appendRoot"></param>
        /// <returns></returns>
        public static string CreateDirectory(string dir, bool appendRoot = true)
        {
            if (appendRoot)
            {
                dir = string.Format("{0}/{1}/", ROOT, dir).Replace("//", "/");
                if (!DirectoryExist(dir))
                {
                    System.IO.Directory.CreateDirectory(HttpContext.Current.Server.MapPath(dir));
                }
                return dir;
            }
            if (!DirectoryExist(dir, appendRoot))
                System.IO.Directory.CreateDirectory(HttpContext.Current.Server.MapPath(dir));
            return dir;
        }

        /// <summary>
        /// Create directory with format /Root/dir/year/month/. If dir exists, it will not do anything
        /// </summary>
        /// <param name="dir"></param>
        /// <param name="date"></param>
        public static string CreateDirectory(string dir, DateTime date)
        {
            dir = CreateDirectory(dir);
            dir += date.Year;
            dir = CreateDirectory(dir, false);
            dir += "/" + date.Month;
            dir = CreateDirectory(dir, false);
            return dir;
        }
        #endregion
    }
}