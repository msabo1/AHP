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
  CriterionName: string;
  toUpdate: CriteriaComparison[];
  criteriaID: string;
  choiceID: string;
  page: number;
  criterionComparisons: CriteriaComparison[];
  constructor(private criteriaComparisonService: CriteriaComparisonService, private route: ActivatedRoute, private router: Router) {}

  ngOnInit() {
    this.toUpdate = [];
    this.criteriaID = this.route.snapshot.paramMap.get('criteriaid');
    this.choiceID = this.route.snapshot.paramMap.get('id');
    this.page = 1;
    this.criteriaComparisonService.GetCriterionComparisons(this.criteriaID, this.page).subscribe(cc => { this.criterionComparisons = cc; });
    this.CriterionName = localStorage['Criterion'];
  }

  DisplayName(comparison: CriteriaComparison):string {
    return comparison.CriteriaID1 == this.criteriaID ? comparison.CriteriaName2 : comparison.CriteriaName1;
  }

  DisplayThisName(comparison: CriteriaComparison): string {
    return comparison.CriteriaID1 == this.criteriaID ? comparison.CriteriaName1 : comparison.CriteriaName2;
  }

  DisplayRatio(comparison: CriteriaComparison): number {
    let value: number = comparison.CriteriaID1 == this.criteriaID ? comparison.CriteriaRatio : 1 / comparison.CriteriaRatio;
    return value < 1 ? -(Math.round(1 / value) - 1) : Math.round(value) - 1;
  }


  PreviousPage() {
    if (this.page > 1) {
      this.page--;
      this.criteriaComparisonService.GetCriterionComparisons(this.criteriaID, this.page).subscribe(cc => this.criterionComparisons = cc);
    }
  }

  NextPage() {
    if (this.criterionComparisons.length >= 5) {
      this.criteriaComparisonService.GetCriterionComparisons(this.criteriaID, this.page + 1).subscribe(cc => {
        if (cc.length > 0) {
          this.criterionComparisons = cc;
          this.page++;
        }
      });
    }
  }

  AddToUpdate(comparison: CriteriaComparison, sign: number, ratio: number) {
    if (comparison.CriteriaRatio != ratio && ratio != null) {
      let value: number = sign >= 0 ? ratio + 1 : 1 / ((-ratio) + 1);
      comparison.CriteriaRatio = comparison.CriteriaID1 == this.criteriaID ? value : 1 / value;
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

  SelectPref(comparison: CriteriaComparison, pref: any) {
    let ratio: number = this.DisplayRatio(comparison);
    console.log('a');
    pref.value = ratio != 0 ? ratio/ratio : "1";
  }

  SelectVal(comparison: CriteriaComparison, val: any) {
    val.value = Math.abs(this.DisplayRatio(comparison));
  }
}
