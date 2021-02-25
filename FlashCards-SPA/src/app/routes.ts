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
import { AccountProfileResolver } from './core/_resolvers/account-profile.resolver';
import { AccountEditResolver } from './core/_resolvers/account-edit.resolver';
import { LearnCourseResolver } from './core/_resolvers/learn-course.resolver';
import { LearnLessonResolver } from './core/_resolvers/learn-lesson.resolver';
import { RepetitionCourseResolver } from './core/_resolvers/repetition-course.resolver';
import { RepetitionLessonResolver } from './core/_resolvers/repetition-lesson.resolver';
import { AdminPanelComponent } from './admin/admin-panel/admin-panel.component';
import { AdminPanelResolver } from './core/_resolvers/admin-panel.resolver';
import { CourseCheckPanelComponent } from './admin/course-check-panel/course-check-panel.component';
import { AdminCourseCheckResolver } from './core/_resolvers/admin-course-check.resolver';
import { AdminAuthGuard } from './core/_guards/adminAuth.guard';
import { AuthGuard } from './core/_guards/auth.guard';
import {LearnCanDeactivateGuard} from './core/_guards/learnCanDeactivate.guard';

export const appRoutes: Routes = [
  { path: '', component: HomeComponent },
  {
    path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [AdminAuthGuard],
    children: [
      { path: 'admin/panel', component: AdminPanelComponent, resolve: { coursesForCheck: AdminPanelResolver} },
      { path: 'admin/courseCheck/:id', component: CourseCheckPanelComponent, resolve: { courseForCheck: AdminCourseCheckResolver} }
    ]
  },
  {
    path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [AuthGuard],
    children: [
      { path: 'dashboard', component: DashboardComponent, resolve: { subscribedCourses: DashboardResolver}},
      { path: 'courses/:id', component: CourseDetailComponent, resolve: { course: CourseDetailResolver }},
      { path: 'courses/:courseId/lessons/:id', component: LessonDetailComponent, resolve: { lesson: LessonDetailResolver }},
      { path: 'course-generator', component: CourseGenComponent },
      { path: 'account/profile/:id', component: AccountProfileComponent, resolve: { userDetailedWithCourses: AccountProfileResolver }},
      { path: 'account/edit/:id', component: AccountEditComponent, resolve: { userDetailed: AccountEditResolver }},
      { path: 'analytics', component: AnalyticsComponent },
      { path: 'learn/course/:subCourseId', component: LearnComponent, resolve: { learnConfiguration: LearnCourseResolver },
        canDeactivate: [LearnCanDeactivateGuard]},
      { path: 'learn/course/:subCourseId/lesson/:subLessonId', component: LearnComponent,
        resolve: { learnConfiguration: LearnLessonResolver }, canDeactivate: [LearnCanDeactivateGuard]},
      { path: 'repetition/course/:subCourseId', component: LearnComponent, resolve: { learnConfiguration: RepetitionCourseResolver },
        canDeactivate: [LearnCanDeactivateGuard] },
      { path: 'repetition/course/:subCourseId/lesson/:subLessonId', component: LearnComponent,
        resolve: { learnConfiguration: RepetitionLessonResolver }, canDeactivate: [LearnCanDeactivateGuard] },
    ]
  },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'courses', component: CoursesComponent, resolve: { courses: CoursesResolver }},
  { path: '**', redirectTo: '', pathMatch: 'full'}
];



