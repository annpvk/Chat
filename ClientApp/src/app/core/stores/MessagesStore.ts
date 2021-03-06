import { observable, computed, action, toJS } from "mobx";
import { Injectable, Output } from '@angular/core';
import { MessageDto } from '../models/MessageDto';
import { EventEmitter } from 'protractor';
import { Subject, Observable } from 'rxjs';

@Injectable()
export class MessagesStore {
    @observable messages: MessageDto[] = [];
    @observable private first: number = 0;
    @observable private last: number = 20;
    @observable private loading: boolean;
    @observable private noMoreMessages: boolean;
    private subject = new Subject<any>();

    @computed get allMessages(): MessageDto[] {
        return toJS(this.messages);
    }

    @computed get NoMoreMessages(): boolean {
        return this.noMoreMessages;
    }

    @computed get First(): number {
        return this.first;
    }

    @computed get Last(): number {
        return this.last;
    }

    @computed get isLoading(): boolean {
        return this.loading;
    }

    @action startLoading() {
        this.loading = true;
    }

    @action stopLoading() {
        this.loading = false;
    }

    messagesUpdated(): Observable<any> {
        return this.subject.asObservable();
    }

    @action addMessage(value: MessageDto) {
        this.messages = [...this.messages, value];
        this.first++;
    }

    emitMessageAdded(){
        this.subject.next();
    }

    @action setMessages(messages: MessageDto[]) {
        if (!messages.length) {
            this.noMoreMessages = true;
            this.loading = false;
            console.log("No More Messages");
            return;
        }
        this.messages = [...messages, ...this.messages];
        this.first += messages.length;
        console.log(this.first);
        this.loading = false;
    }

    @action clearMessages() {
        this.messages = [];
        this.first = 0;
        this.noMoreMessages = false;
    }
}