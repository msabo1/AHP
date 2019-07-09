import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

import { AppComponent } from './app.component';
import { LoginComponent } from './components/login/login.component';
import { RouterModule, Routes } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HttpHeaders } from '@angular/common/http';
import { HttpClient } from 'selenium-webdriver/http';
import { ChoicesComponent } from './components/choices/choices.component';
import { RegisterComponent } from './components/register/register.component';
import { AlternativesComponent } from './components/alternatives/alternatives.component';
import { AddAlternativeComponent } from './components/alternatives/add-alternative/add-alternative.component';
import { CriteriaComponent } from './components/criteria/criteria.component';
import { AddCriterionComponent } from './components/criteria/add-criterion/add-criterion.component';
import { CriteriaComparisonsComponent } from './components/criteria/criteria-comparisons/criteria-comparisons.component';
import { AlternativesComparisonComponent } from './components/alternatives/alternatives-comparison/alternatives-comparison.component';
import { ChooseCriterionComponent } from './components/alternatives/choose-criterion/choose-criterion.component';
import { ChoiceComponent } from './components/choices/choice/choice.component';


const appRoutes: Routes = [{ path: '', component: LoginComponent },
  { path: 'choices', component: ChoicesComponent },
  { path: 'choices/:id', component: ChoiceComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'alternatives/:id', component: AlternativesComponent },
  { path: 'alternatives/:id/add', component: AddAlternativeComponent },
  { path: 'alternatives/:id/:alternativeid', component: ChooseCriterionComponent },
  { path: 'alternatives/:id/:alternativeid/:criteriaid', component: AlternativesComparisonComponent },
  { path: 'criteria/:id', component: CriteriaComponent },
  { path: 'criteria/:id/add', component: AddCriterionComponent },
  { path: 'criteria/:id/:criteriaid', component: CriteriaComparisonsComponent }];

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    ChoicesComponent,
    RegisterComponent,
    AlternativesComponent,
    AddAlternativeComponent,
    CriteriaComponent,
    AddCriterionComponent,
    CriteriaComparisonsComponent,
    AlternativesComparisonComponent,
    ChooseCriterionComponent,
    ChoiceComponent
  ],
  imports: [
    BrowserModule,
    RouterModule.forRoot(appRoutes),
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    NgbModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
