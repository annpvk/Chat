﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChatWebApi.Application;
using ChatWebApi.Application.Chats.Queries;
using ChatWebApi.Application.Messages.Queries;
using ChatWebApi.Application.PersonalMessages.Queries;
using ChatWebApi.Application.Users.Commands;
using ChatWebApi.Application.Users.Queries;
using ChatWebApi.Application.Users.UserDTOs;
using ChatWebApi.Interfaces.Requests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChatWebApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<UsersQueryResult> GetAllUsers()
        {
            return await _mediator.Send(new GetAllUsersQuery());
        }
        [HttpGet]
        [Route("user/{id}")]
        public async Task<GetUserQueryResult> GetUser(int id) 
        {
            return await _mediator.Send(new GetUserQuery { Id = id });
        }

        [HttpGet]
        [Route("{nickname}")]
        public async Task<GetUsersByNicknameQueryResult> GetUsers(string nickname)
        {
            return await _mediator.Send(new GetUsersByNicknameQuery { Nickname = nickname });
        }

        [HttpGet]
        [Route("user/{id}/chats")]
        public async Task<GetUserChatsQueryResult> GetUserChats(int id)
        {
            var chats = await _mediator.Send(new GetUserChatsQuery { Id = id });
            var result = await _mediator.Send(new GetUnreadMessagesQuery { UserId = id, Chats = chats.Chats });
            return new GetUserChatsQueryResult { Chats = result.Chats };
        }

        [HttpGet]
        [Route("user/{id}/profile")]
        public async Task<ProfileInfoDTO> GetUserProfileInfo(int id)
        {
            return await _mediator.Send(new GetUserProfileInfoQuery { Id = id });
        }

        [HttpGet]
        [Route("user/{id}/messages/chat/{term}")]
        public async Task<SearchMessagesQueryResult> SearchMessagesInUserChats(int id, string term)
        {
            return await _mediator.Send(new SearchMessagesQuery { UserId = id, Term = term });
        }

        [HttpGet]
        [Route("user/{id}/message/private/{term}")]
        public async Task<SearchPersonalMessagesByTermQueryResult> SearchPersonalMessages(int id, string term)
        {
            return await _mediator.Send(new SearchPersonalMessagesByTermQuery { Id = id, Term = term });
        }

        [HttpGet]
        [Route("user/{id}/as/chat")]
        public async Task<GetPersonalChatQueryResult> GetPersonalChat(int id)
        {
            return await _mediator.Send(new GetPersonalChatQuery { UserId = id });
        }

        [HttpGet]
        [Route("admin/user/{id}")]
        public async Task<IsUserAdminQueryResult> IsAdmin(int id)
        {
            return await _mediator.Send(new IsUserAdminQuery { UserId = id });
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<CommandCreateResult> CreateUser(CreateUserCommand request)
        {
            return await _mediator.Send(request);
        }

        [HttpPut]
        [Route("user/{id}/password")]
        public async Task<CommandResult> UpdatePassword(UpdatePasswordCommand request)
        {
            return await _mediator.Send(request);
        }

        [HttpPut]
        [Route("user/{id}/picture")]
        public async Task<CommandResult> UpdatePicture(UpdateUserPictureCommand request)
        {
            return await _mediator.Send(request);
        }

        [HttpPut]
        [Route("user/{id}/profile")]
        public async Task<CommandResult> UpdateProfile(UpdateProfileInfoCommand request)
        {
            return await _mediator.Send(request);
        }

        [HttpPut]
        [Route("user/{id}")]
        public async Task<CommandResult> UpdateUser(EditUserProfileCommand request)
        {
            return await _mediator.Send(request);
        }

        [HttpPut]
        [Route("user/{id}/status")]
        public async Task<CommandResult> UpdateActiveDate(int id)
        {
            return await _mediator.Send(new UpdateUserActivityStatusCommand { UserId = id });
        }

    }
}