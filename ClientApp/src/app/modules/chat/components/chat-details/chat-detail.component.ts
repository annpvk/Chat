import { Component, OnInit } from '@angular/core';
import { ChatsService } from 'src/app/core/api/services';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { ChatDto } from 'src/app/core/api/models';

@Component({
    selector: 'chat-detail-component',
    templateUrl: './chat-detail.component.html',
    styleUrls: ['./chat-detail.component.css']
})
export class ChatDetailComponent implements OnInit {
    chat: ChatDto;

    constructor(
        private route: ActivatedRoute,
        private chatService: ChatsService,
        private location: Location
    ) {
    }

    ngOnInit() {

    }

    getChat(): void{
        const id = +this.route.snapshot.paramMap.get('id');
        //TODO: get chat by id
    }
}