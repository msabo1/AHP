import { Component, OnInit } from '@angular/core';
import { Alternative } from '../../../Models/Alternative';
import { AlternativeService } from '../../../services/alternative.service';
import { Router, ActivatedRoute } from '@angular/router';
import { ChoiceService } from '../../../services/choice.service';

@Component({
  selector: 'app-choice',
  templateUrl: './choice.component.html',
  styleUrls: ['./choice.component.css']
})
export class ChoiceComponent implements OnInit {
  page: number;
  choiceID: string;
  alternatives: Alternative[];
  constructor(private alternativeService: AlternativeService, private choiceService: ChoiceService, private router: Router, private route: ActivatedRoute) { }

  ngOnInit() {
    this.page = 1;
    this.choiceID = this.route.snapshot.paramMap.get('id');
    this.alternativeService.GetChoiceAlternatives(this.choiceID, this.page).subscribe(alternatives => this.alternatives = alternatives);
  }

  Calculate() {
    this.choiceService.Calculate(this.choiceID).subscribe(messages => {
      if (messages.length > 0) {
        let msg: string = '';
        messages.forEach(message => msg += message + '\n');
        msg += "You should reevaluate your comparisons";
        alert(msg);
      }
      });
    this.alternativeService.GetChoiceAlternatives(this.choiceID, this.page).subscribe(alternatives => this.alternatives = alternatives);
  }

  PreviousPage() {
    if (this.page > 1) {
      this.page--;
      this.alternativeService.GetChoiceAlternatives(this.choiceID, this.page).subscribe(alternatives => this.alternatives = alternatives);
    }
  }

  NextPage() {
    if (this.alternatives.length >= 5) {
      this.alternativeService.GetChoiceAlternatives(this.choiceID, this.page + 1).subscribe(alternatives => {
        if (alternatives.length > 0) {
          this.alternatives = alternatives;
          this.page++;
        }
      });
    }
  }
}
