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
import { AccountProfileComponent } from './account-profile/account-profile.component';
import { AccountEditComponent } from './account-edit/account-edit.component';
import {PaginationModule} from 'ngx-bootstrap/pagination';
import {SubscriptionsService} from './core/_services/subscriptions.service';
import {SubscriptionsAdapter} from './core/_adapters/subscriptionsAdapter';
import {DashboardResolver} from './core/_resolvers/dashboard.resolver';
import { LearnComponent } from './learn/learn.component';
import { LearnPhrasePresentationComponent } from './learn/learn-phrase-presentation/learn-phrase-presentation.component';
import { LearnPhraseSelectionComponent } from './learn/learn-phrase-selection/learn-phrase-selection.component';
import { LearnPhraseBlocksComponent } from './learn/learn-phrase-blocks/learn-phrase-blocks.component';
import { LearnPhraseInputComponent } from './learn/learn-phrase-input/learn-phrase-input.component';
import {LearningSessionServiceService} from './core/_services/learningSession.service';
import {UserAdapter} from './core/_adapters/userAdapter';
import {UserService} from './core/_services/user.service';
import {AccountProfileResolver} from './core/_resolvers/account-profile.resolver';
import {AccountEditResolver} from './core/_resolvers/account-edit.resolver';
import {CourseManagerService} from './core/_services/courseManager.service';
import { LearnStartComponent } from './learn/learn-start/learn-start.component';
import { LearnSummaryComponent } from './learn/learn-summary/learn-summary.component';
import {LearnCourseResolver} from './core/_resolvers/learn-course.resolver';
import {LearnLessonResolver} from './core/_resolvers/learn-lesson.resolver';
import {RepetitionCourseResolver} from './core/_resolvers/repetition-course.resolver';
import {RepetitionLessonResolver} from './core/_resolvers/repetition-lesson.resolver';
import {LearnAdapter} from './core/_adapters/learnAdapter';
import { AdminPanelComponent } from './admin/admin-panel/admin-panel.component';
import {AdministrationService} from './core/_services/administration.service';
import {AdminPanelResolver} from './core/_resolvers/admin-panel.resolver';
import { CourseForCheckCardComponent } from './course-for-check-card/course-for-check-card.component';
import { CourseCheckPanelComponent } from './admin/course-check-panel/course-check-panel.component';
import { LessonForCheckCardComponent } from './lesson-for-check-card/lesson-for-check-card.component';
import { FlashcardForCheckComponent } from './flashcard-for-check/flashcard-for-check.component';
import { AdminRegisterComponent } from './admin/admin-register/admin-register.component';
import {HardWordsLearnResolver} from './core/_resolvers/hardWordsLearn.resolver';

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
    FlashcardFormComponent,
    AccountProfileComponent,
    AccountEditComponent,
    LearnComponent,
    LearnPhrasePresentationComponent,
    LearnPhraseSelectionComponent,
    LearnPhraseBlocksComponent,
    LearnPhraseInputComponent,
    LearnStartComponent,
    LearnSummaryComponent,
    AdminPanelComponent,
    CourseForCheckCardComponent,
    CourseCheckPanelComponent,
    LessonForCheckCardComponent,
    FlashcardForCheckComponent,
    AdminRegisterComponent
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
        CollapseModule,
        PaginationModule
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
    FlashcardService,
    SubscriptionsService,
    SubscriptionsAdapter,
    DashboardResolver,
    LearningSessionServiceService,
    UserAdapter,
    UserService,
    AccountProfileResolver,
    AccountEditResolver,
    CourseManagerService,
    LearnCourseResolver,
    LearnLessonResolver,
    RepetitionCourseResolver,
    RepetitionLessonResolver,
    LearnAdapter,
    AdministrationService,
    AdminPanelResolver,
    HardWordsLearnResolver
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
