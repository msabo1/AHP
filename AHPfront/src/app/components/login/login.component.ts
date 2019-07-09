import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import {UserService } from '../../services/user.service'
import { User } from '../../Models/User';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  LoginForm: FormGroup;
  

  constructor(private userService: UserService, private router: Router) { }

  ngOnInit() {
    this.LoginForm = new FormGroup({
      Username: new FormControl(null, [Validators.required]),
      Password: new FormControl(null, [Validators.required])
    });
  }

  onSubmit() {
    let user: User = new User();
    user.Username = this.LoginForm.value['Username'];
    user.Password = this.LoginForm.value['Password'];
    console.log(user);
    this.userService.Login(user).subscribe(user => {
      if (user.UserID != null) {
        localStorage['UserID'] = user.UserID;
        this.router.navigate(['choices'])
      }
    });
  }

}
