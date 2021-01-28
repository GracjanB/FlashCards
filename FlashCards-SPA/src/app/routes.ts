import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './auth/login/login.component';
import { RegisterComponent } from './auth/register/register.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { CoursesComponent } from './courses/courses.component';
import { CourseGenComponent } from './course-gen/course-gen.component';
import { AnalyticsComponent } from './analytics/analytics.component';
import { CourseDetailComponent } from './course-detail/course-detail.component';
import { LessonDetailComponent } from './lesson-detail/lesson-detail.component';
import { CoursesResolver } from './core/_resolvers/courses.resolver';
import { CourseDetailResolver } from './core/_resolvers/course-detail.resolver';
import { LessonDetailResolver } from './core/_resolvers/lesson-detail.resolver';
import { AccountProfileComponent } from './account-profile/account-profile.component';
import { AccountEditComponent } from './account-edit/account-edit.component';
import { DashboardResolver } from './core/_resolvers/dashboard.resolver';
import { LearnComponent } from './learn/learn.component';
import {AccountProfileResolver} from './core/_resolvers/account-profile.resolver';
import {AccountEditResolver} from './core/_resolvers/account-edit.resolver';

export const appRoutes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'dashboard', component: DashboardComponent, resolve: { subscribedCourses: DashboardResolver}},
  { path: 'courses', component: CoursesComponent, resolve: { courses: CoursesResolver }},
  { path: 'courses/:id', component: CourseDetailComponent, resolve: { course: CourseDetailResolver }},
  { path: 'courses/:courseId/lessons/:id', component: LessonDetailComponent, resolve: { lesson: LessonDetailResolver }},
  { path: 'course-generator', component: CourseGenComponent },
  { path: 'account/profile/:id', component: AccountProfileComponent, resolve: { userDetailedWithCourses: AccountProfileResolver }},
  { path: 'account/edit/:id', component: AccountEditComponent, resolve: { userDetailed: AccountEditResolver }},
  { path: 'analytics', component: AnalyticsComponent },
  { path: 'learn', component: LearnComponent },
  { path: '**', redirectTo: '', pathMatch: 'full'}
];



