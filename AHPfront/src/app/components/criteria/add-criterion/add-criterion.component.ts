import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { CriterionService } from '../../../services/criterion.service';
import { Criterion } from '../../../Models/Criterion';
import { CriteriaComparison } from '../../../Models/CriteriaComparison';
import { CriteriaComparisonService } from '../../../services/criteria-comparison.service';

@Component({
  selector: 'app-add-criterion',
  templateUrl: './add-criterion.component.html',
  styleUrls: ['./add-criterion.component.css']
})
export class AddCriterionComponent implements OnInit {

  LoginForm: FormGroup;


  constructor(private criterionService: CriterionService, private criteriaComparisonService: CriteriaComparisonService, private router: Router, private route: ActivatedRoute) { }

  ngOnInit() {
    this.LoginForm = new FormGroup({
      Name: new FormControl(null, [Validators.required]),
    });
  }

  onSubmit() {
    let criterion: Criterion = new Criterion();
    criterion.CriteriaName = this.LoginForm.value['Name'];
    criterion.ChoiceID = this.route.snapshot.paramMap.get('id');
    this.criterionService.Add(criterion).subscribe(criterion => {
      if (criterion.CriteriaID != null) {
        localStorage['Criterion'] = criterion.CriteriaID;
        this.router.navigate(['criteria/' + this.route.snapshot.paramMap.get('id') + '/' + criterion.CriteriaID])
      }
    });
  }


}
