import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { LayoutModule } from '@angular/cdk/layout';

import { AppComponent } from './app.component';
import { NavbarComponent } from './navbar/navbar.component';
import { LoginComponent } from './auth/login/login.component';
import { RegisterComponent } from './auth/register/register.component';
import { AuthService } from './core/_services/auth.service';
import { AlertifyService } from './core/_services/alertify.service';
import {HttpClientModule} from '@angular/common/http';
import { HomeComponent } from './home/home.component';
import {RouterModule} from '@angular/router';
import {appRoutes} from './routes';
import {BsDropdownModule} from 'ngx-bootstrap/dropdown';
import { DashboardComponent } from './dashboard/dashboard.component';
import { CoursesComponent } from './courses/courses.component';
import { AnalyticsComponent } from './analytics/analytics.component';
import { CourseGenComponent } from './course-gen/course-gen.component';
import { SubCourseCardComponent } from './dashboard/sub-course-card/sub-course-card.component';
import { CourseDetailComponent } from './course-detail/course-detail.component';
import { CourseCardComponent } from './course-card/course-card.component';
import { LessonCardComponent } from './lesson-card/lesson-card.component';
import { LessonDetailComponent } from './lesson-detail/lesson-detail.component';
import {TabsModule} from 'ngx-bootstrap/tabs';

@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    LoginComponent,
    RegisterComponent,
    HomeComponent,
    DashboardComponent,
    CoursesComponent,
    AnalyticsComponent,
    CourseGenComponent,
    SubCourseCardComponent,
    CourseDetailComponent,
    CourseCardComponent,
    LessonCardComponent,
    LessonDetailComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    LayoutModule,
    NgbModule,
    FormsModule,
    HttpClientModule,
    ReactiveFormsModule,
    RouterModule.forRoot(appRoutes),
    BsDropdownModule.forRoot(),
    TabsModule
  ],
  providers: [
    AuthService,
    AlertifyService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
