using ADOExample.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ADOExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollegeController : ControllerBase
    {
        private readonly ICollegeRep _collegeRep;

        public CollegeController(ICollegeRep collegeRep)
        {
            _collegeRep = collegeRep;
        }

        [HttpGet]

        public ActionResult GetColleges()
        {
            return Ok(_collegeRep.GetColleges());
        }

        [HttpGet("{id}")]

        public ActionResult GetCollegeById(int id)
        {
            return Ok(_collegeRep.GetCollegeById(id));
        }

        [HttpPost]

        public ActionResult AddCollege([FromBody] College college)
        {
            college.Id = _collegeRep.GetColleges().OrderByDescending(s => s.Id).FirstOrDefault().Id + 1;
            _collegeRep.AddCollege(college);
            return Ok();
        }
    }
}
