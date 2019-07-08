import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { CriteriaComparison } from '../../../Models/CriteriaComparison';
import { CriteriaComparisonService } from '../../../services/criteria-comparison.service';
import { FormGroup, FormControl, Validators } from '@angular/forms';


@Component({
  selector: 'app-criteria-comparisons',
  templateUrl: './criteria-comparisons.component.html',
  styleUrls: ['./criteria-comparisons.component.css']
})
export class CriteriaComparisonsComponent implements OnInit {
  EditForm: FormGroup;
  page: number;
  criterionComparisons: CriteriaComparison[];
  constructor(private criteriaComparisonService: CriteriaComparisonService, private route: ActivatedRoute) {}

  ngOnInit() {
    this.page = 1;
    this.criteriaComparisonService.GetCriterionComparisons(this.route.snapshot.paramMap.get('criteriaid'), this.page).subscribe(cc => this.criterionComparisons = cc);
    this.EditForm = new FormGroup({
      ratio: new FormControl(null, [Validators.required])
    });
  }

}
