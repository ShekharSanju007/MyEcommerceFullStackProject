import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { DatafetchService } from '../datafetch.service';


@Component({
  selector: 'app-admin-register',
  templateUrl: './admin-register.component.html',
  styleUrls: ['./admin-register.component.css']
})
export class AdminRegisterComponent {
 
  registrationForm!: FormGroup;
    constructor(private fb: FormBuilder,private dataService: DatafetchService) { }


  ngOnInit() {

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
      validators: this.passwordMatchValidator // Custom validator function
    });  
   
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



onSubmit() {

  if (this.registrationForm.valid) {
    this.dataService.postData(this.registrationForm.value).subscribe(
      (response) => {
        console.log('Post success:', response);
      },
      (error) => {
        console.error('Post error:', error);
      }
    );
  
    this.dataService.getData().subscribe(
      (data) => {
        console.log('Get success:', data);
        
      },
      (error) => {
        console.error('Get error:', error);
        
      }
    );


    // // Assuming you want to update data using PUT
    // this.dataService.putData(this.registrationForm.value).subscribe(
    //   (response) => {
    //     console.log('Put success:', response);
    //     // Optionally, handle success response
    //   },
    //   (error) => {
    //     console.error('Put error:', error);
    //     // Optionally, handle error
    //   }
    // );
  

 
    // // Assuming you want to delete data using DELETE
    // this.dataService.deleteData(id).subscribe(
    //   (response) => {
    //     console.log('Delete success:', response);
    //     // Optionally, handle success response
    //   },
    //   (error) => {
    //     console.error('Delete error:', error);
    //     // Optionally, handle error
    //   }
    // );
  

  // You can also fetch data using GET if needed
  
    
  
}


}







}


