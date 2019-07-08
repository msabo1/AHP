import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { AlternativeService } from '../../../services/alternative.service';
import { Alternative } from '../../../Models/Alternative';


@Component({
  selector: 'app-add-alternative',
  templateUrl: './add-alternative.component.html',
  styleUrls: ['./add-alternative.component.css']
})
export class AddAlternativeComponent implements OnInit {

  LoginForm: FormGroup;


  constructor(private alternativeService: AlternativeService, private router: Router, private route: ActivatedRoute) { }

  ngOnInit() {
    this.LoginForm = new FormGroup({
      Name: new FormControl(null, [Validators.required]),
    });
  }

  onSubmit() {
    let alternative: Alternative = new Alternative();
    alternative.AlternativeName = this.LoginForm.value['Name'];
    alternative.ChoiceID = this.route.snapshot.paramMap.get('id');
    this.alternativeService.Add(alternative).subscribe(alternative => {
      if (alternative.AlternativeID != null) {
        this.router.navigate(['alternatives/' + this.route.snapshot.paramMap.get('id')])
      }
    });
  }

}
