import { Component, OnInit } from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {UserDetailed} from '../core/_models/_dtos/fromServer/userDetailed';
import {ActivatedRoute, Router} from '@angular/router';
import {UserService} from '../core/_services/user.service';
import {UserAdapter} from '../core/_adapters/userAdapter';
import {AlertifyService} from '../core/_services/alertify.service';

@Component({
  selector: 'app-account-edit',
  templateUrl: './account-edit.component.html',
  styleUrls: ['./account-edit.component.css']
})
export class AccountEditComponent implements OnInit {
  user: UserDetailed;
  profileTabActive: boolean;
  learningTabActive: boolean;
  passwordTabActive: boolean;
  accountForm: FormGroup;
  learningForm: FormGroup;
  passwordForm: FormGroup;
  isBusy = false;

  constructor(private formBuilder: FormBuilder,
              private route: ActivatedRoute,
              private userService: UserService,
              private userAdapter: UserAdapter,
              private alertifyService: AlertifyService,
              private router: Router) {
    this.profileTabActive = true;
    this.learningTabActive = false;
    this.passwordTabActive = false;
  }

  ngOnInit(): void {
    this.route.data.subscribe(data => {
      this.user = data.userDetailed;
    });
    this.createAccountForm();
    this.createLearningForm();
    this.createPasswordForm();
    this.fillAccountForm(this.user);
    this.fillLearningForm(this.user);
  }

  private createAccountForm(): void {
    this.accountForm = this.formBuilder.group({
      firstName: ['', Validators.maxLength(64)],
      lastName: ['', Validators.maxLength(64)],
      displayName: ['', [Validators.required, Validators.maxLength(64)]],
      city: ['', Validators.maxLength(64)],
      country: ['', Validators.maxLength(64)]
    });
  }

  private createLearningForm(): void {
    this.learningForm = this.formBuilder.group({
      flashcardsInLearningSession: ['', [Validators.required, Validators.min(5), Validators.max(20)]],
      flashcardsInRevisionSession: ['', [Validators.required, Validators.min(5), Validators.max(50)]]
    });
  }

  private createPasswordForm(): void {
    this.passwordForm = this.formBuilder.group({
      oldPassword: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(64)]],
      newPassword: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(64)]],
      newPasswordConfirm: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(64)]]
    }, { validators: [this.passwordMatchValidator] });
  }

  private fillAccountForm(user: UserDetailed): void {
    this.accountForm.get('firstName').setValue(user.firstName);
    this.accountForm.get('lastName').setValue(user.lastName);
    this.accountForm.get('displayName').setValue(user.displayName);
    this.accountForm.get('city').setValue(user.city);
    this.accountForm.get('country').setValue(user.country);
  }

  private fillLearningForm(user: UserDetailed): void {
    this.learningForm.get('flashcardsInLearningSession').setValue(user.numberOfWordsInLearningSession);
    this.learningForm.get('flashcardsInRevisionSession').setValue(user.numberOfWordsInReviewSession);
  }

  public submitAccountForm(): void {
    this.isBusy = true;
    const userForUpdate = this.userAdapter.adaptUserForUpdate(this.accountForm.value);
    userForUpdate.numberOfWordsInLearningSession = this.learningForm.get('flashcardsInLearningSession').value;
    userForUpdate.numberOfWordsInReviewSession = this.learningForm.get('flashcardsInRevisionSession').value;
    this.userService.updateUser(this.user.id, userForUpdate).subscribe((userDetailed) => {
      this.user = userDetailed;
      this.alertifyService.showSuccessAlert('Pomyślnie zapisano zmiany');
    }, error => {
      this.alertifyService.showErrorAlert('Wystąpił błąd, spróbuj ponownie');
    }, () => {
      this.isBusy = false;
    });
  }

  public submitPasswordForm(): void {
    this.isBusy = true;
    const userForPasswordUpdate = this.userAdapter.adaptUserForPasswordChange(this.passwordForm.value);
    this.userService.changePassword(this.user.id, userForPasswordUpdate).subscribe(() => {
      this.alertifyService.showSuccessAlert('Hasło zostało zmienione, możesz się teraz ponownie zalogować.');
      this.passwordForm.reset();
    }, error => {
      this.alertifyService.showErrorAlert('Wystąpił błąd, spróbuj ponownie');
    }, () => {
      this.isBusy = false;
    });
  }

  private passwordMatchValidator(form: FormGroup) {
    if (form.get('newPassword').value !== form.get('newPasswordConfirm').value) {
      form.controls.newPasswordConfirm.setErrors({ mismatch: true });
      return;
    }
  }

  navigateToAccountProfile(): void {
    this.router.navigate(['/account/profile/' + this.user.id]);
  }

  changePhoto(): void {
    this.alertifyService.showMessageAlert('Will be soon');
  }

  menuToggle(option: number): void {
    switch (option) {
      case 0:
        this.learningTabActive = false;
        this.passwordTabActive = false;
        this.profileTabActive = true;
        break;
      case 1:
        this.passwordTabActive = false;
        this.profileTabActive = false;
        this.learningTabActive = true;
        break;
      case 2:
        this.profileTabActive = false;
        this.learningTabActive = false;
        this.passwordTabActive = true;
        break;
    }
  }
}
