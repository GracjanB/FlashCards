import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './auth/login/login.component';
import { RegisterComponent } from './auth/register/register.component';
import {DashboardComponent} from './dashboard/dashboard.component';
import {CoursesComponent} from './courses/courses.component';
import {CourseGenComponent} from './course-gen/course-gen.component';
import {AnalyticsComponent} from './analytics/analytics.component';

export const appRoutes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'dashboard', component: DashboardComponent },
  { path: 'courses', component: CoursesComponent },
  { path: 'newCourseTool', component: CourseGenComponent },
  { path: 'analytics', component: AnalyticsComponent },
  { path: '**', redirectTo: '', pathMatch: 'full'}
];
