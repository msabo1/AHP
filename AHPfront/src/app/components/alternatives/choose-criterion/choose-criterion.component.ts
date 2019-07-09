import { Component, OnInit } from '@angular/core';
import { CriterionService } from '../../../services/criterion.service';
import { ActivatedRoute } from '@angular/router';
import { Criterion } from '../../../Models/Criterion';

@Component({
  selector: 'app-choose-criterion',
  templateUrl: './choose-criterion.component.html',
  styleUrls: ['./choose-criterion.component.css']
})
export class ChooseCriterionComponent implements OnInit {
  page: number;
  criteria: Criterion[];
  constructor(private criterionService: CriterionService, private route: ActivatedRoute) { }

  ngOnInit() {
    this.page = 1;
    this.criterionService.GetChoiceCriteria(this.route.snapshot.paramMap.get('id'), this.page).subscribe(criteria => this.criteria = criteria);
  }
  PreviousPage() {
    if (this.page > 1) {
      this.page--;
      this.criterionService.GetChoiceCriteria(this.route.snapshot.paramMap.get('id'), this.page).subscribe(criteria => this.criteria = criteria);
    }
  }

  NextPage() {
    if (this.criteria.length >= 5) {
      this.criterionService.GetChoiceCriteria(this.route.snapshot.paramMap.get('id'), this.page + 1).subscribe(criteria => { this.criteria = criteria; this.page++; });
    }
  }
}
