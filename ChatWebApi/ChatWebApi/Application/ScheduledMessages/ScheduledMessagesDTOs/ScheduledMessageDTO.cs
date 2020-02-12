﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChatWebApi.Application.Chats.ChatDTOs;
using ChatWebApi.Application.Users.Queries.UserDTOs;

namespace ChatWebApi.Application.ScheduledMessages.ScheduledMessagesDTOs
{
	public class ScheduledMessageDTO
	{
		public int Id { get; set; }
		public DateTime Date { get; set; }
		public DateTime Delivery { get; set; }
		public string Text { get; set; }

		public int ChatId { get; set; }
		public ChatDTO Chat { get; set; }

		public int SenderId { get; set; }
		public UserDTO Sender { get; set; }
	}
}
