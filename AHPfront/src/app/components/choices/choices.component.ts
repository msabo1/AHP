import { Component, OnInit } from '@angular/core';
import { ChoiceService } from '../../services/choice.service';
import { Router, ActivatedRoute } from '@angular/router';
import { Choice } from '../../Models/Choice';
import { FormGroup, FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'app-choices',
  templateUrl: './choices.component.html',
  styleUrls: ['./choices.component.css']
})
export class ChoicesComponent implements OnInit {
  EditForm: FormGroup;
  AddForm: FormGroup;
  choices: Choice[];
  page: number;
  constructor(private choiceService: ChoiceService, private router: Router, private route: ActivatedRoute) { }

  ngOnInit() {
    this.page = 1;
    this.choiceService.GetUserChoices(localStorage['UserID'], this.page).subscribe(choices => this.choices = choices);
    this.EditForm = new FormGroup({
      newName: new FormControl(null, [Validators.required])
    });
    this.AddForm = new FormGroup({
      Name: new FormControl(null, [Validators.required]),
    });
  }

  Add() {
    let choice: Choice = new Choice();
    choice.ChoiceName = this.AddForm.value['Name'];
    choice.UserID = localStorage['UserID'];
    console.log(choice);
    this.choiceService.Add(choice).subscribe(choice => {
      if (choice.ChoiceID != null) {
        this.choices.unshift(choice);
        this.choices = this.choices.slice(0, 5);
      }
    });
  }

  Delete(choice: Choice) {
    this.choiceService.Delete(choice).subscribe(() => { this.choices = this.choices.filter(c => c.ChoiceID != choice.ChoiceID) });
  }

  Edit(choice: Choice) {
    if (choice.ChoiceName != this.EditForm.value['newName'] && this.EditForm.value['newName'] != null) {
      choice.ChoiceName = this.EditForm.value['newName'];
      this.choiceService.Update(choice).subscribe();
    }
  }

  MakeEditVisible(id:string) {
    let elem: HTMLElement = document.getElementById(id);
    elem.style.display = 'initial';
  }
  MakeEditInvisible(id: string) {
    let elem: HTMLElement = document.getElementById(id);
    elem.style.display = 'none';
  }

  PreviousPage() {
    if (this.page > 1) {
      this.page--;
      this.choiceService.GetUserChoices(localStorage['UserID'], this.page).subscribe(choices => this.choices = choices);
    }
  }

  NextPage() {
    if (this.choices.length >= 5) {
      this.choiceService.GetUserChoices(localStorage['UserID'], this.page + 1).subscribe(choices => { this.choices = choices; this.page++; });
    }
  }
}
