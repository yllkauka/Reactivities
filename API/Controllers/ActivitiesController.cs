using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Application.Activities;

namespace API.Controllers
{
    public class ActivitiesController : BaseApiController
    {
     

        [HttpGet]
        public async Task<ActionResult<List<Activity>>> GetActivities()
        {
            return await Mediator.Send(new List.Query());
        }

        [HttpGet("{id}")]  // activities/id
        public async Task<ActionResult<Activity>> GetActivity(Guid id)
        {
            return await Mediator.Send(new Details.Query{Id = id});  //nese activity nuk ekziston na kthen null prej Mediator(null nuk eshte error, po kthen no content)
        }

        [HttpPost]
        public async Task<IActionResult> CreateActivity(Activity activity) //kthen akces ne Http response types si: ok, return bad request, return not found...
        {
            return Ok (await Mediator.Send(new Create.Command {Activity = activity}));
        }

         [HttpPut("{id}")]
        public async Task<IActionResult> EditActivity(Guid id, Activity activity)
        {
            activity.Id = id;
            return Ok(await Mediator.Send(new Edit.Command{Activity = activity}));  //me update veq ni single field "Id" ne kete rast
        } 

         [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActivity(Guid id)
        {
            return Ok(await Mediator.Send(new Delete.Command{Id = id}));
        } 
    }
}