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
  alternatives: Alternative[];
  constructor(private alternativeService: AlternativeService, private choiceService: ChoiceService, private router: Router, private route: ActivatedRoute) { }

  ngOnInit() {
    this.page = 1;
    this.alternativeService.GetChoiceAlternatives(this.route.snapshot.paramMap.get('id'), this.page).subscribe(alternatives => this.alternatives = alternatives);
  }

  Calculate() {
    this.choiceService.Calculate(this.route.snapshot.paramMap.get('id')).subscribe(messages => {
      let msg: string = '';
      messages.forEach(message => msg += message + '\n');
      msg += "You should reevaluate your comparisons";
      alert(msg);
      });
    this.alternativeService.GetChoiceAlternatives(this.route.snapshot.paramMap.get('id'), this.page).subscribe(alternatives => this.alternatives = alternatives);
  }

  PreviousPage() {
    if (this.page > 1) {
      this.page--;
      this.alternativeService.GetChoiceAlternatives(this.route.snapshot.paramMap.get('id'), this.page).subscribe(alternatives => this.alternatives = alternatives);
    }
  }

  NextPage() {
    if (this.alternatives.length >= 5) {
      this.alternativeService.GetChoiceAlternatives(this.route.snapshot.paramMap.get('id'), this.page + 1).subscribe(alternatives => { this.alternatives = alternatives; this.page++; });
    }
  }
}
