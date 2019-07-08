import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/services/user.service';
import { ChoiceRequest } from '../../classes/choice-request';
import { Choice } from '../../classes/choice';

@Component({
  selector: 'app-choices',
  templateUrl: './choices.component.html',
  styleUrls: ['./choices.component.css']
})
export class ChoicesComponent implements OnInit {

  constructor(private userService: UserService) { }
  public choiceRequest = new ChoiceRequest(window.localStorage['UserID'], 1);

  choices: any;

  ngOnInit() {
    $("#chooseChoicesText").hide()

    this.userService.getChoices(this.choiceRequest).subscribe(data => {
      this.choices = data;
      $("#noChoicesText").hide()
      $("#chooseChoicesText").show()
    });
  }

  onClick(choice: Choice) {
    localStorage.setItem('choice', JSON.stringify(choice));
  }
}
