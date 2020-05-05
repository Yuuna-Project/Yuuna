// Author: Yuuna-Project@Orlys
// Github: github.com/Orlys
// Contact: orlys@yuuna-project.com

namespace Yuuna.Interaction.AspNetCore.Mvc.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using Yuuna.Interaction.AspNetCore.Mvc.Models;

    [ApiController]
    [Route("/")]
    public class ActorController : ControllerBase
    {
        private readonly Actor _actor;

        public ActorController(Actor actor)
        {
            this._actor = actor;
        }

        [HttpGet, HttpPost]
        public Output Post([FromBody]Input text)
        {
            return new Output(this._actor.Accept(text.Text, out var r), r.Message, r.Mood, text.Text);
        }
    }
}