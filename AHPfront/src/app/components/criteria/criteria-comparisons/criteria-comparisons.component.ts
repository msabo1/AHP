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
  toUpdate: CriteriaComparison[];
  criteriaID: string;
  page: number;
  criterionComparisons: CriteriaComparison[];
  constructor(private criteriaComparisonService: CriteriaComparisonService, private route: ActivatedRoute, private router: Router) {}

  ngOnInit() {
    this.toUpdate = [];
    this.criteriaID = this.route.snapshot.paramMap.get('criteriaid');
    this.page = 1;
    this.criteriaComparisonService.GetCriterionComparisons(this.criteriaID, this.page).subscribe(cc => {this.criterionComparisons = cc;});
  }

  DisplayName(comparison: CriteriaComparison):string {
    return comparison.CriteriaID1 == this.criteriaID ? comparison.CriteriaName2 : comparison.CriteriaName1;
  }

  DisplayRatio(comparison: CriteriaComparison): number {
    return comparison.CriteriaID1 == this.criteriaID ? comparison.CriteriaRatio : 1/comparison.CriteriaRatio;
  }


  PreviousPage() {
    if (this.page > 1) {
      this.page--;
      this.criteriaComparisonService.GetCriterionComparisons(this.criteriaID, this.page).subscribe(cc => this.criterionComparisons = cc);
    }
  }

  NextPage() {
    if (this.criterionComparisons.length >= 5) {
      this.criteriaComparisonService.GetCriterionComparisons(this.criteriaID, this.page + 1).subscribe(cc => {this.criterionComparisons = cc; this.page++; });
    }
  }

  AddToUpdate(comparison: CriteriaComparison, ratio: number) {
    if (comparison.CriteriaRatio != ratio && ratio != null) {
      comparison.CriteriaRatio = comparison.CriteriaID1 == this.criteriaID ? ratio : 1 / ratio;
      this.toUpdate[comparison.CriteriaID1 + comparison.CriteriaID2] = comparison;
    }
  }
  Save() {
    let comparisons: CriteriaComparison[] = [];
    for (var key in this.toUpdate) {
      comparisons.push(this.toUpdate[key]);
    }
    this.criteriaComparisonService.Update(comparisons).subscribe(() => this.toUpdate = this.toUpdate.slice(0, 0));
    
  }
}
