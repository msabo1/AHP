import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { CriteriaComparison } from '../../../Models/CriteriaComparison';
import { CriteriaComparisonService } from '../../../services/criteria-comparison.service';


@Component({
  selector: 'app-criteria-comparisons',
  templateUrl: './criteria-comparisons.component.html',
  styleUrls: ['./criteria-comparisons.component.css']
})
export class CriteriaComparisonsComponent implements OnInit {

  criteriaComparisons: CriteriaComparison[];
  constructor(private criteriaComparisonService: CriteriaComparisonService, private route: ActivatedRoute) { }

  ngOnInit() {
    //this.criteriaComparisonService.GetCriterionComparisons(this.route.snapshot.paramMap.get('id'), 1).subscribe(cc => this.criteriaComparisons = cc);
  }

}
