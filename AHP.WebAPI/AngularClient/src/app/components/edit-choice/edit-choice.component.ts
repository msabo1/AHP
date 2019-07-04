import { Component, OnInit } from '@angular/core';
import * as $ from 'jquery';
import { Choice } from '../../classes/choice';

@Component({
  selector: 'app-edit-choice',
  templateUrl: './edit-choice.component.html',
  styleUrls: ['./edit-choice.component.css']
})
export class EditChoiceComponent implements OnInit {

  constructor() {}
  choiceS = JSON.parse(localStorage.getItem('choice'));

  ngOnInit() {
    console.log(this.choiceS);
    var add = "<h1>" + this.choiceS['ChoiceName'] + "</h1>";
    $("#ShowChoiceName").text("");
    $("#ShowChoiceName").append(add);
    //
    //for (let i = 0, i < this.choiceS['Criteria'].length; i++) {
     // var addCriterion = "<tr><th scope = 'row'>" + i + "</th><td>" + this.choiceS['Criteria'] + "</td></tr>";
    //}
  }
}
