import { Component, OnInit } from '@angular/core';
import { AlternativesComparison } from '../../../Models/AlternativesComparison';
import { AlternativesComparisonService } from '../../../services/alternatives-comparison.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-alternatives-comparison',
  templateUrl: './alternatives-comparison.component.html',
  styleUrls: ['./alternatives-comparison.component.css']
})
export class AlternativesComparisonComponent implements OnInit {

  toUpdate: AlternativesComparison[];
  alternativeID: string;
  criteriaID: string;
  page: number;
  alternativeComparisons: AlternativesComparison[];
  constructor(private alternativesComparisonService: AlternativesComparisonService, private route: ActivatedRoute, private router: Router) { }

  ngOnInit() {
    this.toUpdate = [];
    this.alternativeID = this.route.snapshot.paramMap.get('alternativeid');
    this.criteriaID = this.route.snapshot.paramMap.get('criteriaid');
    this.page = 1;
    this.alternativesComparisonService.GetAlternativeComparisons(this.criteriaID, this.alternativeID, this.page).subscribe(ac => { this.alternativeComparisons = ac; });
  }

  DisplayName(comparison: AlternativesComparison): string {
    return comparison.AlternativeID1 == this.alternativeID ? comparison.AlternativeName2 : comparison.AlternativeName1;
  }

  DisplayRatio(comparison: AlternativesComparison): number {
    return comparison.AlternativeID1 == this.alternativeID ? comparison.AlternativeRatio : 1 / comparison.AlternativeRatio;
  }


  PreviousPage() {
    if (this.page > 1) {
      this.page--;
      this.alternativesComparisonService.GetAlternativeComparisons(this.criteriaID, this.alternativeID, this.page).subscribe(ac => { this.alternativeComparisons = ac; });
    }
  }

  NextPage() {
    if (this.alternativeComparisons.length >= 5) {
      this.alternativesComparisonService.GetAlternativeComparisons(this.criteriaID, this.alternativeID, this.page).subscribe(ac => { this.alternativeComparisons = ac; });
    }
  }

  AddToUpdate(comparison: AlternativesComparison, ratio: number) {
    if (comparison.AlternativeRatio != ratio && ratio != null) {
      comparison.AlternativeRatio = comparison.AlternativeID1 == this.alternativeID ? ratio : 1 / ratio;
      this.toUpdate[this.criteriaID + comparison.AlternativeID1 + comparison.AlternativeID2] = comparison;
    }
  }
  Save() {
    let comparisons: AlternativesComparison[] = [];
    for (var key in this.toUpdate) {
      comparisons.push(this.toUpdate[key]);
    }
    this.alternativesComparisonService.Update(comparisons).subscribe(() => this.toUpdate = this.toUpdate.slice(0, 0));

  }

}
