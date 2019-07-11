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
  AddForm: FormGroup;
  EditForm: FormGroup;
  criteria: Criterion[];
  constructor(private criterionService: CriterionService, private router: Router, private route: ActivatedRoute) { }

  ngOnInit() {
    this.page = 1;
    this.criterionService.GetChoiceCriteria(this.route.snapshot.paramMap.get('id'), this.page).subscribe(criteria => this.criteria = criteria);
    this.EditForm = new FormGroup({
      newName: new FormControl(null, [Validators.required])
    });
    this.AddForm = new FormGroup({
      Name: new FormControl(null, [Validators.required]),
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
    let elem: HTMLElement = document.getElementById('edit' + id);
    elem.style.display = 'inline';
    elem = document.getElementById('editbutton' + id);
    elem.style.display = 'none';
    elem = document.getElementById('cname' + id);
    elem.style.display = 'none';
  }
  MakeEditInvisible(id: string) {
    let elem: HTMLElement = document.getElementById('edit' + id);
    elem.style.display = 'none';
    elem = document.getElementById('cname' + id);
    elem.style.display = 'inline';
    elem = document.getElementById('editbutton' + id);
    elem.style.display = 'inline';

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

  Add() {
    let criterion: Criterion = new Criterion();
    criterion.CriteriaName = this.AddForm.value['Name'];
    criterion.ChoiceID = this.route.snapshot.paramMap.get('id');
    this.criterionService.Add(criterion).subscribe(criterion => {
      if (criterion.CriteriaID != null) {
        this.criteria.unshift(criterion);
        this.criteria = this.criteria.slice(0, 5);
        localStorage['Criterion'] = criterion.CriteriaID;
        if (this.criteria.length > 1) {
          this.router.navigate(['criteria/' + this.route.snapshot.paramMap.get('id') + '/' + criterion.CriteriaID])
        }
        
      }
    });
  }

  AddToStorage(criterion:Criterion) {
    localStorage['Criterion'] = criterion.CriteriaName;
  }
}
