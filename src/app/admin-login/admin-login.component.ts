import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { DatafetchService } from '../datafetch.service';
import { Router } from '@angular/router';


@Component({
  selector: 'app-admin-login',
  templateUrl: './admin-login.component.html',
  styleUrls: ['./admin-login.component.css']
})
export class AdminLoginComponent {
  LoginForm!: FormGroup;
  loginErrorMessage:any;

  constructor(private fb: FormBuilder,private dataService: DatafetchService,private router:Router) { }

  ngOnInit() {
    this.LoginFormData();
     
 }


 LoginFormData(){

    this.LoginForm = this.fb.group({
 
    email: ['', [Validators.required, Validators.email]],
    password: ['', [Validators.required, Validators.minLength(6)]],
   
  }); 
}



  onLoginSubmit(){

   this.dataService.LoginData(this.LoginForm.value).subscribe (
    (response)=>{

      if(response && response.message == "Login details are successful" ){
            console.log("Login Success:",response);
            this.LoginForm.reset();
            this.dataService.storeToken(response.token);
            this.router.navigate(['/home']);
      }
      
      else{
        console.error("Invalid login Credentials");
      }

    },
  (error)=>{
    this.loginErrorMessage = true;  
    console.error('Login error:', error);
   }

   );
      
  }

}
