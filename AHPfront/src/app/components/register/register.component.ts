import { Component, OnInit } from '@angular/core';
import { UserService } from '../../services/user.service';
import { Router } from '@angular/router';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { User } from '../../Models/User';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

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
    this.userService.Register(user).subscribe(user => {
      if (user.UserID != null) {
        this.router.navigate([''])
      }
    });
  }

}
