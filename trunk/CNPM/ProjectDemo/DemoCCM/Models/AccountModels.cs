using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Security;
using DemoCCM.Models;

namespace DemoCCM.Models
{
    [MetadataTypeAttribute(typeof(User.UserMetaData))]

    public partial class User
    {
        internal sealed class UserMetaData //sealed ko cho ke thua
        {
            public UserMetaData() { }

            [Required(ErrorMessage = "(*)")]
            [Display(Name = "User name")]
            public string UserName { get; set; }

            /*[Required(ErrorMessage = "(*)")]
            [RegularExpression(@"^([0-9a-zA-Z]([-\.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$", ErrorMessage = "{0} không đúng định dạng")]
            public string Email;

            [Required(ErrorMessage = "(*)")]
            [Display(Name = "Điện Thoại")]
            [RegularExpression(@"[0-9.]+", ErrorMessage = "{0} phải nhập số")]
            public string PhoneNumber { get; set; }
             * */

            [Required(ErrorMessage = "(*)")]
            [DataType(DataType.Password)]
            [StringLength(100, MinimumLength = 6, ErrorMessage = "{0} dài ít nhất {2} kí tự ")]
            // {0} : ten field, {1} = 100; {2}=6
            [Display(Name = "Password")]
            public string Pass { get; set; }
        }

    }




}