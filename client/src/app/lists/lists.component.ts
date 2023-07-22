import { Component, OnInit } from '@angular/core';
import { Member } from '../_models/member';
import { MembersService } from '../_services/members.service';

@Component({
  selector: 'app-lists',
  templateUrl: './lists.component.html',
  styleUrls: ['./lists.component.css']
})
export class ListsComponent implements OnInit {
  members: Member[] | undefined;
  predicate = 'invided'

  constructor(private memberService: MembersService) {}

  ngOnInit(): void {
    this.loadInvitations();
  }

  loadInvitations() {
    this.memberService.getInvitations(this.predicate).subscribe({
      next: response => {
        this.members = response
      }
    })
  }

}
