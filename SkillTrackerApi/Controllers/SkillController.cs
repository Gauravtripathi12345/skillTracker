using BusinessLogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SkillTrackerApi.Controllers
{
    public class SkillController : ApiController
    {
        private readonly SkillManager _skillManager;

        public SkillController()
        {
            _skillManager = new SkillManager(); // You may want to use dependency injection to inject SkillManager
        }

        [HttpPost]
        public IHttpActionResult AddSkill(SkillDTO skillDTO)
        {
            try
            {
                _skillManager.AddSkill(skillDTO);
                return Ok("Skill added successfully");
            }
            catch (Exception ex)
            {
                // Log the exception
                return InternalServerError(ex);
            }
        }

        [HttpPut]
        [Route("api/skill/{skillId}")]
        public IHttpActionResult UpdateSkill(int skillId, SkillDTO skillDTO)
        {
            try
            {
                _skillManager.UpdateSkill(skillId, skillDTO);
                return Ok("Skill updated successfully");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                // Log the exception
                return InternalServerError(ex);
            }
        }
    }
}
