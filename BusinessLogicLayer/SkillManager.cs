using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class SkillManager
    {
        private readonly SkillTrackerDBEntities _db;

        public SkillManager()
        {
            _db = new SkillTrackerDBEntities(); // You may want to use dependency injection to inject SkillTrackerDBEntities
        }

        public void AddSkill(SkillDTO skillDTO)
        {
            // Check if the skill already exists
            var existingSkill = _db.Skills.FirstOrDefault(s => s.Name == skillDTO.Name);

            if (existingSkill != null)
            {
                // Skill already exists, you may handle this case as per your requirements (e.g., return a message)
                return;
            }

            // Create a new Skill entity and map properties from SkillDTO
            var newSkill = new Skill
            {
                Name = skillDTO.Name
            };

            // Add the new skill to the database
            _db.Skills.Add(newSkill);
            _db.SaveChanges();
        }

        public void UpdateSkill(int skillId, SkillDTO skillDTO)
        {
            // Find the skill by its ID
            var existingSkill = _db.Skills.FirstOrDefault(s => s.Id == skillId);

            if (existingSkill != null)
            {
                // Update the skill name
                existingSkill.Name = skillDTO.Name;

                // Save changes to the database
                _db.SaveChanges();
            }
            else
            {
                throw new ArgumentException("Skill not found");
            }
        }
    }
}
