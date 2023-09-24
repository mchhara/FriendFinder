import { AccountService } from './../_services/account.service';
import { Component, OnInit } from '@angular/core';
import { Post } from '../_models/post';
import { PostService } from '../_services/post.service';
import { take } from 'rxjs';
import { User } from '../_models/user';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.css']
})
export class MainComponent implements OnInit {
  posts: Post[] = [];
  content: string = '';
  user?: User;
  showAddPostForm: boolean = false;

  constructor(
    private postService: PostService,
    private accountService: AccountService) {
      this.accountService.currentUser$.pipe(take(1)).subscribe({
        next: user => {
          if(user) this.user = user;
        }
      })
    }

  ngOnInit(): void {
    this.loadPosts();
  }

  loadPosts() {
    this.postService.getPosts().subscribe((posts) => {
      this.posts = posts;
    });
  }

  addPost() {
    if (this.content) {
      const newPost: Post = {
        userName: this.user?.userName!,
        content: this.content,
        createdAt: new Date()
      };

      this.postService.addPost(newPost).subscribe((addedPost) => {
        this.posts.unshift(addedPost);
        this.content = '';
        this.toggleAddPostForm();
      });
    }
  }

  toggleAddPostForm() {
    this.showAddPostForm  = !this.showAddPostForm ;
  }
}
