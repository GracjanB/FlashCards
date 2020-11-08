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
import {CourseService} from './core/_services/course.service';
import {CourseAdapter} from './core/_adapters/courseAdapter';
import {CoursesResolver} from './core/_resolvers/courses.resolver';
import {CourseDetailResolver} from './core/_resolvers/course-detail.resolver';
import {LessonAdapter} from './core/_adapters/lessonAdapter';
import {FlashcardAdapter} from './core/_adapters/flashcardAdapter';
import {LessonService} from './core/_services/lesson.service';
import {MapExtensions} from './core/_extensions/mapExtensions';
import {LessonDetailResolver} from './core/_resolvers/lesson-detail.resolver';
import { CourseFormComponent } from './course-gen/course-form/course-form.component';
import { LessonFormComponent } from './course-gen/lesson-form/lesson-form.component';
import { LessonListComponent } from './course-gen/lesson-list/lesson-list.component';
import { FlashcardFormComponent } from './course-gen/flashcard-form/flashcard-form.component';
import {CollapseModule} from 'ngx-bootstrap/collapse';
import {FlashcardService} from './core/_services/flashcard.service';

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
    LessonDetailComponent,
    CourseFormComponent,
    LessonFormComponent,
    LessonListComponent,
    FlashcardFormComponent
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
        TabsModule,
        CollapseModule
    ],
  providers: [
    AuthService,
    AlertifyService,
    CourseService,
    CourseAdapter,
    CoursesResolver,
    CourseDetailResolver,
    LessonAdapter,
    FlashcardAdapter,
    LessonService,
    MapExtensions,
    LessonDetailResolver,
    FlashcardService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
