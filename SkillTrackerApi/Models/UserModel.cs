﻿using BusinessLogicLayer;
using System;
using System.Collections.Generic;

namespace SkillTrackerApi.Models
{
    public class UserModel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public UserModel()
        {
            this.UserSkills = new HashSet<UserSkillDTO>();
        }

        public int Id { get; set; }
        public string EmailId { get; set; }
        public string Password { get; set; }
        public bool Is_Admin { get; set; }
        public string FullName { get; set; }
        public Nullable<System.DateTime> DateOfBirth { get; set; }
        public Nullable<decimal> ContactNo { get; set; }
        public string Gender { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserSkillDTO> UserSkills { get; set; }

    }
}