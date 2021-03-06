﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChatWebApi.Application;
using ChatWebApi.Application.Chats.ChatDTOs;
using ChatWebApi.Application.Chats.Commands;
using ChatWebApi.Application.Chats.Queries;
using ChatWebApi.Application.UserChats.Commands;
using ChatWebApi.Infrastructure.Entities;
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
    public class ChatsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ChatsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("chat/search/{name}")]
        public async Task<FindChatsByNameResult> Get(string name)
        {
            return await _mediator.Send(new FindChatsByNameQuery { Name = name });
        }

        [HttpGet]
        [Route("chat/{chatId}/user/{userId}")]
        public async Task<bool> IsUserInChat(int chatId, int userId)
        {
            return await _mediator.Send(new IsUserInChatQuery { ChatId = chatId, UserId = userId });
        }

        [HttpGet()]
        public async Task<FindChatsByNameResult> Get()
        {
            return await _mediator.Send(new GetAllChatsQuery());
        }

        [HttpGet("chat/{id}")]
        public async Task<GetChatByIdQueryResult> Get(int id)
        {
            return await _mediator.Send(new GetChatByIdQuery { Id = id });
        }

        [HttpGet]
        [Route("chat/{id}/user/count")]
        public async Task<GetCountOfUsersInChatQueryResult> GetCountOfUsers(int id)
        {
            return await _mediator.Send(new GetCountOfUsersInChatQuery { ChatId = id });
        }

        [HttpPost]
        public async Task<CommandChatResult> CreateChat(CreateChatCommand request)
        {
            return await _mediator.Send(request);
        }

        [HttpPost]
        [Route("chat/{chatId}/user/{userId}")]
        public async Task<ChatDTO> AddUserToChat(int chatId, int userId)
        {
            return await _mediator.Send(new AddUserToChatCommand { ChatId = chatId, UserId = userId });
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("admin/user/{userId}")]
        public async Task<CommandResult> AddUserToAdminChat(int userId)
        {
            return await _mediator.Send(new AddUserToAdminChatCommand { UserId = userId });
        }

        [HttpPut]
        public async Task<CommandResult> ChangeChatPrivacy(ChangeChatCommand request)
        {
            return await _mediator.Send(request);
        }

        [HttpPut]
        [Route("{id}/picture")]
        public async Task<CommandResult> ChangeChatPicture(AddChatPictureCommand request, int id)
        {
            request.Id = id;
            return await _mediator.Send(request);
        }
    }
}