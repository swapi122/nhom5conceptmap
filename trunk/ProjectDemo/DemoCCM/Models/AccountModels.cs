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

            [Required]
            [Display(Name = "User name")]
            public string UserName { get; set; }

            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Pass { get; set; }



        }

    }

}