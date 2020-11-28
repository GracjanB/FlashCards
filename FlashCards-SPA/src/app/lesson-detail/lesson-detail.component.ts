import { Component, OnInit } from '@angular/core';
import {ActivatedRoute, Router} from '@angular/router';
import {Lesson} from '../core/_models/_dtos/fromServer/lesson';

@Component({
  selector: 'app-lesson-detail',
  templateUrl: './lesson-detail.component.html',
  styleUrls: ['./lesson-detail.component.css']
})
export class LessonDetailComponent implements OnInit {
  lessonDetailed: Lesson;
  isSubscribed: boolean;

  constructor(private route: ActivatedRoute,
              private router: Router) { }

  ngOnInit(): void {
    this.route.data.subscribe(data => {
      this.lessonDetailed = data.lesson;
      this.prepareComponent();
    });
  }

  private prepareComponent(): void {
    this.isSubscribed = this.lessonDetailed.isSubscribed;
  }
}
