import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { UserForRegister } from '../../core/_models/_dtos/userForRegister';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  registerForm: FormGroup;
  userForRegister: UserForRegister;


  constructor(private formBuilder: FormBuilder) { }

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

    this.userForRegister = new UserForRegister(registerFormValues);
    console.log(this.userForRegister);
  }

}
