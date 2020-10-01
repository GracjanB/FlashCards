import { Component, OnInit } from '@angular/core';
import {User} from '../core/_models/user';
import {SubscribedCourse} from '../core/_models/subscribedCourse';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {
  public currentUser: User;
  public exampleCourse: SubscribedCourse;

  constructor() {
    this.currentUser = JSON.parse(localStorage.getItem('user')) as User;
    this.exampleCourse = new SubscribedCourse(
      1, 'Angielski phrasal verbs 2', 250, 500, 50, '');
  }

  ngOnInit(): void {
  }

}
