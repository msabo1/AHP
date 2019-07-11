import { Component, OnInit } from '@angular/core';
import { ChoiceService } from 'src/app/services/choice.service';
import { ChoiceRequest } from '../../classes/choice-request';
import { Choice } from '../../classes/choice';

@Component({
  selector: 'app-choices',
  templateUrl: './choices.component.html',
  styleUrls: ['./choices.component.css']
})
export class ChoicesComponent implements OnInit {

  constructor(private choiceService: ChoiceService) { }
  public choiceRequest = new ChoiceRequest(window.localStorage['UserID'], 1);

  public choices: any;

  ngOnInit() {
    $("#chooseChoicesText").hide()

    this.choiceService.getChoices(this.choiceRequest).subscribe(data => {
      this.choices = data;
      $("#noChoicesText").hide()
      $("#chooseChoicesText").show()
      //console.log(this.choices);
    });
  }

  onClick(choice: Choice) {
    localStorage.setItem('choice', JSON.stringify(choice));
  }

  deleteChoice(choice: Choice, i: number) {
    this.choiceService.deleteChoice(choice).subscribe(() => {
      //this.choices = this.choices.filter(c => c.ChoiceID != choice.ChoiceID)
      document.getElementById("choiceContainer" + i).remove();
    });
  }
}
