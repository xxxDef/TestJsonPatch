using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace TestJsonPatch.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CodeSetsController : ControllerBase
    {
        static readonly List<CodeSet> codesets = new List<CodeSet>
        {
            new CodeSet
            {
                Name = "Set1",
                Id = "1",
                Codes = new Dictionary<string, Code>
                {
                    { "id1", new Code { CodeType = "Type1", CodeName = "Name1" } },
                    { "id2", new Code { CodeType = "Type2", CodeName = "Name2" } }
                }
            },
            new CodeSet
            {
                Name = "Set2",
                Id = "2",
                Codes = new Dictionary<string, Code>
                {
                    { "id3", new Code { CodeType = "Type3", CodeName = "Name3" } },
                    { "id4", new Code { CodeType = "Type4", CodeName = "Name4" } }
                }
            },
            new CodeSet
            {
                Name = "Set3",
                Id = "3",
                Codes = new Dictionary<string, Code>
                {
                    { "id5", new Code { CodeType = "Type5", CodeName = "Name5" } },
                    { "id6", new Code { CodeType = "Type6", CodeName = "Name6" } }
                }
            }
        };

        [HttpGet("{id}")]
        public ActionResult<CodeSet> GetCodeSetById(string id)
        {
            var codeSet = codesets.FirstOrDefault(cs => cs.Id == id);
            if (codeSet == null)
            {
                return NotFound();
            }
            return Ok(codeSet);
        }

        [HttpPost("{id}")]
        public ActionResult UpdateCodeSetById(string id, [FromBody] CodeSet updatedCodeSet)
        {
            var codeSet = codesets.FirstOrDefault(cs => cs.Id == id);
            if (codeSet == null)
            {
                return NotFound();
            }

            codeSet.Name = updatedCodeSet.Name;
            codeSet.Codes = updatedCodeSet.Codes;

            return NoContent();
        }

        [HttpPatch("{id}")]
        [Consumes("application/json-patch+json")]
        public ActionResult<CodeSet> PatchCodeSetById(string id, [FromBody] JsonPatchDocument<CodeSet> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }

            var codeSet = codesets.FirstOrDefault(cs => cs.Id == id);
            if (codeSet == null)
            {
                return NotFound();
            }

            patchDoc.ApplyTo(codeSet);

            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            return codeSet;
        }
    }
}
