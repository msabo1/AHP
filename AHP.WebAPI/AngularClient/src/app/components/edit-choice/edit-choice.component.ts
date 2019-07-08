import { Component, OnInit } from '@angular/core';
import * as $ from 'jquery';
import { UserService } from 'src/app/services/user.service';
import { CriteriaService } from 'src/app/services/criteria.service';
import { AlternativeService } from 'src/app/services/alternative.service';
import { ChoiceRequest } from '../../classes/choice-request';
import { AlternativeRequest } from '../../classes/alternative-request';
import { CriteriaRequest } from '../../classes/criteria-request';

@Component({
  selector: 'app-edit-choice',
  templateUrl: './edit-choice.component.html',
  styleUrls: ['./edit-choice.component.css']
})
export class EditChoiceComponent implements OnInit {

  constructor(private userService: UserService, private criteriaService: CriteriaService, private alternativeService: AlternativeService) {}
  choice = window.localStorage['choice'];
  choiceName = JSON.parse(this.choice)['ChoiceName'];
  choiceID = JSON.parse(this.choice)['ChoiceID'];

  public criteriaRequest = new CriteriaRequest(this.choiceID, 1);
  public choiceRequest = new ChoiceRequest(this.choiceID, 1);

  criteria: any;
  alternatives: any;
  criteriaComparisons: any[] = [];
  alternativeComparisons: any[] = [];

  ngOnInit() {
    var add = "<h1>" + this.choiceName + "</h1>";
    $("#ShowChoiceName").text("");
    $("#ShowChoiceName").append(add);
    
    this.criteriaService.getCriteria(this.criteriaRequest).subscribe(data => {
      this.criteria = data;

      window.localStorage['criteria'] = JSON.stringify(this.criteria);

      this.alternativeService.getAlternatives(this.choiceRequest).subscribe(data => {
        this.alternatives = data;
      });
    });
  }

  findCriterionName(id: String) {
    for (let criterion of this.criteria) {
      if (criterion['CriteriaID'] == id) return criterion['CriteriaName'];
    }
  }

  findAlternativeName(id: String) {
    for (let alternative of this.alternatives) {
      if (alternative['AlternativeID'] == id) return alternative['AlternativeName'];
    }
  }

  setSliderValue(value: number) {
    if (value < 1) return (-1 / value);
    else return value;
  }

  toggleCriteriaComparisons(i: number) {
     if ($("#sliderContainer" + i).is(":visible")) $("#sliderContainer" + i).hide();
     else $("#sliderContainer" + i).show();

    let criteriaComparisonRequest = new CriteriaRequest(JSON.parse(window.localStorage['criteria'])[i - 1]['CriteriaID'], 1);
    this.criteriaService.getCriteriaComparison(criteriaComparisonRequest).subscribe(data => {
      this.criteriaComparisons[i - 1] = data;
    });
  }

  toggleAlternativeComparisons(i: number) {
    if ($("#alternativeSliderContainer" + i).is(":visible")) $("#alternativeSliderContainer" + i).hide();
    else $("#alternativeSliderContainer" + i).show();

    let alternativeComparisonRequest = new AlternativeRequest(JSON.parse(window.localStorage['criteria'])[i - 1]['CriteriaID'], 1);
    this.alternativeService.getAlternativeComparison(alternativeComparisonRequest).subscribe(data => {
      this.alternativeComparisons[i - 1] = data;
    })
  }
}
