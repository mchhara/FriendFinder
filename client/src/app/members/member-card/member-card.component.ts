import {
  Component,
  Input,
  OnInit,
  ViewEncapsulation,
  Output,
  EventEmitter,
} from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Member } from 'src/app/_models/member';
import { MembersService } from 'src/app/_services/members.service';
import { PresenceService } from 'src/app/_services/presence.service';

@Component({
  selector: 'app-member-card',
  templateUrl: './member-card.component.html',
  styleUrls: ['./member-card.component.css'],
})
export class MemberCardComponent implements OnInit {
  @Input() member: Member | undefined;
  @Input() context: 'invitations' | 'members' | 'friends' = 'members'; // Nowy input do określenia kontekstu
  @Input() invitationType: 'invided' | 'invidedBy' | 'friends' = 'invided'; // Typ zaproszenia
  @Output() invitationHandled = new EventEmitter<void>(); // Event do odświeżenia listy

  isFriend = false;

  constructor(
    private memberService: MembersService,
    private toastr: ToastrService,
    public presenceService: PresenceService
  ) {}

  ngOnInit(): void {
    if (this.context === 'members' && this.member) {
      this.checkIfFriend();
    }
  }

  checkIfFriend() {
    if (this.member) {
      this.memberService.checkIfFriend(this.member.userName).subscribe({
        next: (isFriend) => {
          this.isFriend = isFriend;
        },
        error: (error) => {
          console.error('Error checking if friend:', error);
        },
      });
    }
  }

  invideMember(member: Member) {
    this.memberService.invideMember(member.userName).subscribe({
      next: () => {
        this.toastr.success('You have invided ' + member.knownAs);
        this.invitationHandled.emit(); // Emituj event po wysłaniu zaproszenia
      },
    });
  }

  acceptInvitation(member: Member) {
    this.memberService.acceptInvitation(member.userName).subscribe({
      next: () => {
        this.toastr.success('You accepted invitation from ' + member.knownAs);
        this.invitationHandled.emit(); // Emituj event po akceptacji
      },
      error: (error) => {
        this.toastr.error('Failed to accept invitation');
        console.error(error);
      },
    });
  }

  rejectInvitation(member: Member) {
    this.memberService.rejectInvitation(member.userName).subscribe({
      next: () => {
        this.toastr.success('You rejected invitation from ' + member.knownAs);
        this.invitationHandled.emit(); // Emituj event po odrzuceniu
      },
      error: (error) => {
        this.toastr.error('Failed to reject invitation');
        console.error(error);
      },
    });
  }
}
