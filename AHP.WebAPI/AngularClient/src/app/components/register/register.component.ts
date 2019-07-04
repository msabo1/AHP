import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  constructor(private userService: UserService) { }

  RegisterForm: FormGroup;

  ngOnInit() {
    this.RegisterForm = new FormGroup({
      Username: new FormControl(null, [Validators.required]),
      Password: new FormControl(null, [Validators.required])
    });
  }

  onSubmit() {
    let value: any = this.RegisterForm.value;
    this.userService.register(value).subscribe(data => {
    console.log(data)});
  }
}
