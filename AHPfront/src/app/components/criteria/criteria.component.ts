import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { Criterion } from '../../Models/Criterion';
import { CriterionService } from '../../services/criterion.service';
import { FormGroup, FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'app-criteria',
  templateUrl: './criteria.component.html',
  styleUrls: ['./criteria.component.css']
})
export class CriteriaComponent implements OnInit {
  page: number;
  EditForm: FormGroup;
  criteria: Criterion[];
  constructor(private criterionService: CriterionService, private route: ActivatedRoute) { }

  ngOnInit() {
    this.page = 1;
    this.criterionService.GetChoiceCriteria(this.route.snapshot.paramMap.get('id'), this.page).subscribe(criteria => this.criteria = criteria);
    this.EditForm = new FormGroup({
      newName: new FormControl(null, [Validators.required])
    });
  }

  Delete(criterion: Criterion) {
    this.criterionService.Delete(criterion).subscribe(() => {this.criteria = this.criteria.filter(c => c.CriteriaID != criterion.CriteriaID ) });
  }

  Edit(criterion: Criterion) {
    if (criterion.CriteriaName != this.EditForm.value['newName'] && this.EditForm.value['newName'] != null) {
      criterion.CriteriaName = this.EditForm.value['newName'];
      this.criterionService.Update(criterion).subscribe();
    }
  }

  MakeEditVisible(id: string) {
    let elem: HTMLElement = document.getElementById(id);
    elem.style.display = 'initial';
  }
  MakeEditInvisible(id: string) {
    let elem: HTMLElement = document.getElementById(id);
    elem.style.display = 'none';
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
