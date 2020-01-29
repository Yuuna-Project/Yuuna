
namespace Yuuna.Interaction.AspNetCore.Mvc.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Yuuna.Interaction.AspNetCore.Mvc.Models;

    [ApiController]
    [Route("/")]
    public class ActorController : ControllerBase
    {
        public ActorController(Actor actor)
        {
            this._actor = actor;
        }

        private readonly Actor _actor;

        [HttpGet, HttpPost]
        public Output Post([FromBody]Input text)
        {
            return new Output(this._actor.Accept(text.Text, out var r), r.Message, r.Mood, text.Text);
        }
    }
}
