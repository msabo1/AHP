import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  LoginForm: FormGroup;

  constructor(private userService: UserService) { }

  ngOnInit() {
    this.LoginForm = new FormGroup({
      Username: new FormControl(null, [Validators.required]),
      Password: new FormControl(null, [Validators.required])
    });
  }

  onSubmit() {
    let value: any = this.LoginForm.value;
    this.userService.login(value).subscribe(data => {
      window.localStorage['UserID'] = data["UserID"];
      //console.log(window.localStorage['UserID']);
    });
  }
}
