using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using CalcMean.Models;

namespace CalcMean
{
    public static class Common
    {
        public static string Encode(string text)
        {
            try
            {
                if (string.IsNullOrEmpty(text)) return text;

                byte[] mybyte = Encoding.UTF8.GetBytes(text);
                string returntext = Convert.ToBase64String(mybyte);
                return returntext;
            }
            catch (Exception)
            {
                return text;
            }
        }

        public static string Decode(string text)
        {
            try
            {
                byte[] mybyte = Convert.FromBase64String(text);
                string returntext = Encoding.UTF8.GetString(mybyte);
                return returntext;
            }
            catch (Exception)
            {
                return text;
            }

        }
    }

    public static class Extension
    {
        public static DotChotSo ToDeCodeDotChotSo(this DotChotSo from)
        {
            from.Title = Common.Decode(from.Title);
            return from;
        }

        public static CmUser ToCmUser(this CmUserModel from)
        {
            return new CmUser
            {
                Id = from.Id,
                UserName = Common.Encode(from.UserName),
                Name = from.Name,
                PhoneNumber = from.Phone,
                IsShow = from.IsShow,
                CoAnSang = from.CoAnSang,
            };
        }

        public static CmUserModel ToCmUserModel(this CmUser from)
        {
            return new CmUserModel
            {
                UserName = from.UserName,
                Name = from.Name,
                Phone = from.PhoneNumber,
                IsShow = from.IsShow,
                CoAnSang = from.CoAnSang,
                Password = from.PasswordHash,
                ConfirmPassword = from.PasswordHash
            };
        }
    }
}