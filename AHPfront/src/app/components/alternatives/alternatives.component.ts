import { Component, OnInit } from '@angular/core';
import { ChoiceService } from '../../services/choice.service';
import { Router, ActivatedRoute } from '@angular/router';
import { Alternative } from '../../Models/Alternative';
import { AlternativeService } from '../../services/alternative.service';
import { FormGroup, FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'app-alternatives',
  templateUrl: './alternatives.component.html',
  styleUrls: ['./alternatives.component.css']
})
export class AlternativesComponent implements OnInit {
  page: number;
  AddForm: FormGroup;
  EditForm: FormGroup;
  alternatives: Alternative[];
  constructor(private alternativeService: AlternativeService, private router: Router, private route: ActivatedRoute) { }

  ngOnInit() {
    this.page = 1;
    this.alternativeService.GetChoiceAlternatives(this.route.snapshot.paramMap.get('id'), this.page).subscribe(alternatives => this.alternatives = alternatives);
    this.EditForm = new FormGroup({
      newName: new FormControl(null, [Validators.required])
    });
    this.AddForm = new FormGroup({
      Name: new FormControl(null, [Validators.required]),
    });
  }

  Add() {
    let alternative: Alternative = new Alternative();
    alternative.AlternativeName = this.AddForm.value['Name'];
    alternative.ChoiceID = this.route.snapshot.paramMap.get('id');
    this.alternativeService.Add(alternative).subscribe(alternative => {
      if (alternative.AlternativeID != null) {
        this.alternatives.unshift(alternative);
        this.alternatives = this.alternatives.slice(0, 5);
        localStorage['Alternative'] = alternative.AlternativeID;
        if (this.alternatives.length > 1) {
          this.router.navigate(['alternatives/' + this.route.snapshot.paramMap.get('id') + '/' + alternative.AlternativeID])
        }

      }
    });
  }

  Delete(alternative: Alternative) {
    this.alternativeService.Delete(alternative).subscribe(() => { this.alternatives = this.alternatives.filter(a => a.AlternativeID != alternative.AlternativeID) });
  }

  Edit(alternative: Alternative) {
    if (alternative.AlternativeName != this.EditForm.value['newName'] && this.EditForm.value['newName'] != null) {
      alternative.AlternativeName = this.EditForm.value['newName'];
      this.alternativeService.Update(alternative).subscribe();
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
      this.alternativeService.GetChoiceAlternatives(this.route.snapshot.paramMap.get('id'), this.page).subscribe(alternatives => this.alternatives = alternatives);
    }
  }

  NextPage() {
    if (this.alternatives.length >= 5) {
      this.alternativeService.GetChoiceAlternatives(this.route.snapshot.paramMap.get('id'), this.page + 1).subscribe(alternatives => { this.alternatives = alternatives; this.page++; });
    }
  }

  AddToStorage(alternative: Alternative) {
    localStorage['Alternative'] = alternative.AlternativeName;
  }

}
