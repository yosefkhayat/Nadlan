﻿using Application.Profiles;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ProfilesController : BaseApiController
    {
        [HttpGet("{username}")]
        public async Task<IActionResult> GetProfile(string username)
        {
            return HandleResult(await Mediator.Send(new Details.Query { Username = username }));
        }
        [HttpPut]
        public async Task<IActionResult> Edit(Edit.Command command)
        {
            return HandleResult(await Mediator.Send(command));
        }

        [HttpGet("{username}/Listings")]
        public async Task<IActionResult> GetUserListings(string username,
           string predicate)
        {
            return HandleResult(await Mediator.Send(new ListListings.Query
            { Username = username, Predicate = predicate }));
        }

    }
}
