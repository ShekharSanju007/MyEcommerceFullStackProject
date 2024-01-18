import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { DatafetchService } from '../datafetch.service';
import { Router } from '@angular/router';



@Component({
  selector: 'app-admin-register',
  templateUrl: './admin-register.component.html',
  styleUrls: ['./admin-register.component.css']
})
export class AdminRegisterComponent {
 
  registrationForm!: FormGroup;
  registererrorMessage!:any;
    constructor(private fb: FormBuilder,private dataService: DatafetchService,private router:Router) { }


  ngOnInit() {
   this.registrationFormData();
    
}

passwordMatchValidator(formGroup: FormGroup) {
  const password = formGroup.get('password')?.value;
  const confirmPassword = formGroup.get('confirmPassword')?.value;

  if (password !== confirmPassword) {
    formGroup.get('confirmPassword')?.setErrors({ passwordMismatch: true });
  } else {
    formGroup.get('confirmPassword')?.setErrors(null);
  }
}


isFieldInvalid(fieldName: string): boolean {
  const control = this.registrationForm.get(fieldName);
     return control!.touched && control!.invalid;
}



registrationFormData(){

  this.registrationForm = this.fb.group({
    firstName: ['', Validators.required],
    lastName: ['', Validators.required],
    email: ['', [Validators.required, Validators.email]],
    password: ['', [Validators.required, Validators.minLength(6)]],
    confirmPassword: ['', Validators.required],
    phoneNumber: ['', Validators.required],
    street: ['', Validators.required],
    city: ['', Validators.required],
    state: ['', Validators.required],
    country: ['', Validators.required],
    pinCode: ['', Validators.required],
  }, {
    validators: this.passwordMatchValidator.bind(this) // Custom validator function
  });  
 
}

 onSubmit() {

 
    this.dataService.RegisterData(this.registrationForm?.value).subscribe(
      (response) => {
        console.log('Post success registered:', response);
        this.registrationForm.reset();
        this.router.navigate(['/login']);

      },
      
      (error) => {
        if(error.error.message=='Email already exists')
        {
          this.registererrorMessage = true;
        }
        console.error('Post error:', error);
      }
    );

}


}


