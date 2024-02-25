using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class UserSkillDTO
    {
            public int Id { get; set; }
            public Nullable<int> UserID { get; set; }
            public Nullable<int> SkillID { get; set; }
            public string Proficiency { get; set; }

        public virtual SkillDTO Skill { get; set; }
        public virtual User User { get; set; }
    }
}
