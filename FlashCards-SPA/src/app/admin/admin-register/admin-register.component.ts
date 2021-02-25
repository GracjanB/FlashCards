import { Component, OnInit } from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {AlertifyService} from '../../core/_services/alertify.service';
import {UserForRegister} from '../../core/_models/_dtos/userForRegister';
import {AuthService} from '../../core/_services/auth.service';

@Component({
  selector: 'app-admin-register',
  templateUrl: './admin-register.component.html',
  styleUrls: ['./admin-register.component.css']
})
export class AdminRegisterComponent implements OnInit {
  administratorForm: FormGroup;

  constructor(private formBuilder: FormBuilder,
              private authService: AuthService,
              private alertifyService: AlertifyService) { }

  ngOnInit(): void {
    this.createAdministratorForm();
  }

  private createAdministratorForm(): void {
    this.administratorForm = this.formBuilder.group({
      email: ['', Validators.required],
      displayName: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(32)]],
      password: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(16)]],
      confirmPassword: ['', Validators.required]
    }, { validators: [this.passwordMatchValidator] });
  }

  passwordMatchValidator(form: FormGroup) {
    if (form.get('password').value !== form.get('confirmPassword').value) {
      form.controls.confirmPassword.setErrors({ mismatch: true });
      return;
    }
  }

  registerAdministrator(): void {
    const adminFormValues = this.administratorForm.value;
    delete adminFormValues.confirmPassword;
    const userForRegister = new UserForRegister(adminFormValues);

    this.authService.registerAdministrator(userForRegister).subscribe(next => {
      this.alertifyService.showSuccessAlert('Administrator został zarejestrowany pomyślnie');
    }, error => {
      this.alertifyService.showErrorAlert('Wystąpił błąd.');
    });
  }
}
