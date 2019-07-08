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


const appRoutes: Routes = [{ path: '', component: LoginComponent },
  { path: 'choices', component: ChoicesComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'alternatives/:id', component: AlternativesComponent },
  { path: 'alternatives/:id/add', component: AddAlternativeComponent },
  { path: 'criteria/:id', component: CriteriaComponent },
  { path: 'criteria/:id/add', component: AddCriterionComponent },
  { path: 'criteria/:id/comparisons', component: CriteriaComparisonsComponent }];

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
    CriteriaComparisonsComponent
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
