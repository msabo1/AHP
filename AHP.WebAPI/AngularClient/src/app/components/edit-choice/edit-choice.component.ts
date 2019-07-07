import { Component, OnInit } from '@angular/core';
import * as $ from 'jquery';
import { UserService } from 'src/app/services/user.service';
import { ChoiceRequest } from '../../classes/choice-request';

@Component({
  selector: 'app-edit-choice',
  templateUrl: './edit-choice.component.html',
  styleUrls: ['./edit-choice.component.css']
})
export class EditChoiceComponent implements OnInit {

  constructor(private userService: UserService) {}
  choice = window.localStorage['choice'];
  choiceName = JSON.parse(this.choice)['ChoiceName'];
  choiceID = JSON.parse(this.choice)['ChoiceID'];

  public criteriaRequest = new ChoiceRequest(this.choiceID, 1);

  criteria: any;
  alternatives: any;
  criteriaComparisons: any;

  ngOnInit() {
    var add = "<h1>" + this.choiceName + "</h1>";
    $("#ShowChoiceName").text("");
    $("#ShowChoiceName").append(add);
    
    this.userService.getCriteria(this.criteriaRequest).subscribe(data => {
      this.criteria = data;

      window.localStorage['criteria'] = JSON.stringify(this.criteria);
      console.log(this.criteria);

      let requestList: ChoiceRequest[] = [];
      //let criteriaComparisonList: any[] = [];
      
      for (let i = 0; i < Object.keys(this.criteria).length; i++) {
        let criteriaComparisonRequest = new ChoiceRequest(JSON.parse(window.localStorage['criteria'])[i]['CriteriaID'], 1);
        requestList.push(criteriaComparisonRequest);
        this.userService.getCriteriaComparison(criteriaComparisonRequest).subscribe(data => {
          //criteriaComparisonList[i]=data;
          //console.log(criteriaComparisonList[i]);
          this.criteriaComparisons = data;
        });
      }

        this.userService.getAlternatives(this.criteriaRequest).subscribe(data => {
          this.alternatives = data;
        });
    });
  }

  findCriterionName(id: String) {
    for (let criterion of this.criteria) {
      if (criterion['CriteriaID'] == id) return criterion['CriteriaName'];
    }
  }

  setSliderValue(value: number) {
    if (value < 1) return (-1 / value);
    else return value;
  }

  toggleCriteriaComparisons(i: number) {
    if ($("#sliderContainer" + i).is(":visible")) $("#sliderContainer" + i).hide();
    else $("#sliderContainer" + i).show();
  }
}
