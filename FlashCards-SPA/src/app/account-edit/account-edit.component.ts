import { Component, OnInit } from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';

@Component({
  selector: 'app-account-edit',
  templateUrl: './account-edit.component.html',
  styleUrls: ['./account-edit.component.css']
})
export class AccountEditComponent implements OnInit {
  profileTabActive: boolean;
  learningTabActive: boolean;
  passwordTabActive: boolean;
  accountForm: FormGroup;
  learningForm: FormGroup;
  passwordForm: FormGroup;

  constructor(private formBuilder: FormBuilder) {
    this.profileTabActive = true;
    this.learningTabActive = false;
    this.passwordTabActive = false;
  }

  ngOnInit(): void {
    this.createAccountForm();
    this.createLearningForm();
    this.createPasswordForm();
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
      flashcardsInLearningSession: ['', [Validators.required, Validators.min(5), Validators.max(30)]],
      flashcardsInRevisionSession: ['', [Validators.required, Validators.min(5), Validators.max(50)]]
    });
  }

  private createPasswordForm(): void {
    this.passwordForm = this.formBuilder.group({
      oldPassword: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(64)]],
      newPassword: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(64)]],
      newPasswordConfirm: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(64)]]
    });
  }

  private fillAccountForm(account: any): void {
    this.accountForm.get('firstName').setValue(account.firstName);
    this.accountForm.get('lastName').setValue(account.lastName);
    this.accountForm.get('displayName').setValue(account.displayName);
    this.accountForm.get('city').setValue(account.city);
    this.accountForm.get('country').setValue(account.country);
  }

  private fillLearningForm(learningConfig: any): void {
    this.learningForm.get('flashcardsInLearningSession').setValue(learningConfig.flashcardsInLearningSession);
    this.learningForm.get('flashcardsInRevisionSession').setValue(learningConfig.flashcardsInRevisionSession);
  }

  public submitAccountForm(): void {
    console.log('Account form values: ');
    console.log(this.accountForm.value);
  }

  public submitLearningForm(): void {
    console.log('Learning form values: ');
    console.log(this.learningForm.value);
  }

  public submitPasswordForm(): void {
    console.log('Password form values: ');
    console.log(this.passwordForm.value);
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
