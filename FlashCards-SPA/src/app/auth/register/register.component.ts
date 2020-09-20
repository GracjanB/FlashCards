import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { UserForRegister } from '../../core/_models/_dtos/userForRegister';
import { AuthService } from '../../core/_services/auth.service';
import {AlertifyService} from '../../core/_services/alertify.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  registerForm: FormGroup;

  constructor(private formBuilder: FormBuilder,
              private authService: AuthService,
              private alertifyService: AlertifyService) { }

  ngOnInit(): void {
    this.createRegisterForm();
  }

  createRegisterForm() {
    this.registerForm = this.formBuilder.group({
      email: ['', Validators.required],
      displayName: ['', Validators.required],
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      city: ['', Validators.required],
      country: ['', Validators.required],
      password: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(16)]],
      confirmPassword: ['', Validators.required]
    }, { validators: [this.passwordMatchValidator] });
  }

  // TODO: Think about move this to core folder into helper/validators or sth
  passwordMatchValidator(form: FormGroup) {
    if (form.get('password').value !== form.get('confirmPassword').value) {
      form.controls.confirmPassword.setErrors({ mismatch: true });
      return;
    }
  }

  register() {
    // TODO: Think about how to map this to UserForRegister without manually delete
    const registerFormValues = this.registerForm.value;
    delete registerFormValues.confirmPassword;

    const userForRegister = new UserForRegister(registerFormValues);
    this.authService.register(userForRegister).subscribe(next => {
      this.alertifyService.showSuccessAlert('Successfully registered');
    }, error => {
      this.alertifyService.showErrorAlert('Failed to register');
    });
  }

}
