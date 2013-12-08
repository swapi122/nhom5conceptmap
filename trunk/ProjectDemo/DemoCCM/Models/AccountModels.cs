using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Security;
using   DemoCCM.Models;

namespace DemoCCM.Models
{
   [MetadataTypeAttribute(typeof(User.UserMetaData))]

    public partial class User
    {
        internal sealed class UserMetaData //sealed ko cho ke thua
        {
            public UserMetaData() { }

            [Required(ErrorMessage="{0} không được để trống!")]
            [Display(Name = "User name")]

            public string UserName { get; set; }

            [Required(ErrorMessage="{0} không được để trống!")]
            [DataType(DataType.Password)]
            [StringLength(100, MinimumLength = 6, ErrorMessage = "{0} dài ít nhất {2} kí tự ")]
            [Display(Name = "Password")]
            public string Pass { get; set; }
        }

    }

}