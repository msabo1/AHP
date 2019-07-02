import { Component, OnInit } from '@angular/core';
import * as $ from 'jquery';
import Calculator from './HelperFunctions.js';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  constructor() { }

  ngOnInit() {

    var criteriaCounter = 0;
    var alternativeCounter = 0;
    var CriteriaWeight;

    $("#RemoveLastCriterion").hide();
    $("#CriteriaTable").hide();
    $("#CriteriaButtons").hide();

    $("#CalculateCriteriaScores").hide();
    $("#AlternativeContainer").hide();

    $("#AlternativeRow").hide();
    $("#AlternativeTable").hide();
    $("#RemoveLastAlternative").hide();
    $("#AlternativeComparisonHelp").hide();

    $("#ConfirmAlternativeScores").hide();

    var criteriaList: string[] = [];
    var criteriaComparisonValues: number[] = [];

    var alternativeList: string[] = [];
    var alternativeComparisonValues: number[] = [];

    $("#SetChoiceName").click(function () {
      var name = $("#ChoiceNameInput").val();
      console.log("Send choiceName: "+name+" to server.");  ///////////////

      var add = "<h1>" + name + "</h1>";
      $("#ShowChoiceName").text("");
      $("#ShowChoiceName").append(add);
    });

    $("#SetCriterionName").click(function () {
      var name = $("#CriterionNameInput").val();

      console.log("Send criterionName: " + name + " to server."); /////////////

      criteriaCounter = criteriaCounter + 1;
      var addCriterion = "<tr><th scope = 'row'>" + criteriaCounter + "</th><td>" + name + "</td></tr>";
      $("#CriteriaTableRows").append(addCriterion);

      $("#RemoveLastCriterion").show();
      $("#CriteriaTable").show();

      if (criteriaCounter > 1) $("#CalculateCriteriaScores").show();

      criteriaList.push(name.toString());

      for (var criterion = 0; criterion < criteriaList.length - 1; criterion++) {
        var addComparison = "<div class='row text-center'><div class='col mt-4'><span class='mr-5 mb-1' style='color: dodgerblue; font-weight: bold'>" + name + "</span><input type='range' class='custom-range mt-3' min ='-9' max ='9' step ='1' style='width: 50%'><span class='ml-5' style='color: dodgerblue; font-weight: bold'>" + criteriaList[criterion] + "</span></div></div>";
        $("#CriteriaContainer").append(addComparison);
        $("#CriteriaContainer").find('input').addClass("criteriaSliderID");
      }

      var addCriterionRow = "<div class='row mt-5'><div class='col'><h4>Compare the alternatives by criterion: " + criteriaList[criteriaCounter - 1] + "</h4></div></div>";
      $("#AlternativeContainer").append(addCriterionRow);


      var criterionRowID = "CriterionRowID" + (criteriaCounter - 1).toString();
      $("#AlternativeContainer").children().last().attr("id", criterionRowID);
      $("#" + criterionRowID).hide();

      var addCriteriaButton = "<div class='fit'><button type='button' class='btn btn-primary mr-2'>"+name+"</button></div>";
      $("#CriteriaButtons").append(addCriteriaButton);
      var criterionButtonID = "CriterionButtonID" + (criteriaCounter - 1).toString();
      $("#CriteriaButtons").find('button').last().attr("id", criterionButtonID);
      $("#CriteriaButtons").find('button').last().click(function () {
        if ($("#" + criterionRowID).is(":visible")) $("#" + criterionRowID).hide();
        else $("#" + criterionRowID).show();
      });

      var criterionRowID = "CriterionRowID" + (criteriaCounter - 1).toString();
      $("#AlternativeContainer").children().last().attr("id", criterionRowID);

    });

    $("#RemoveLastCriterion").click(function () {
      $("#CriteriaTableRows").children().last().remove();
      $("#AlternativeContainer").children().last().remove();

      $("#CriteriaButtons").children().last().remove();

      criteriaList.pop();

      if (criteriaCounter > 0) criteriaCounter = criteriaCounter - 1;

      for (let i = 0; i < criteriaCounter; i++) {
        $("#CriteriaContainer").children().last().remove();
      }

      if (criteriaCounter == 0) {
        $("#RemoveLastCriterion").hide();
        $("#CriteriaTable").hide();

        $("#AlternativeComparisonHelp").hide();
        $("#AlternativeRow").hide();
        $("#AlternativeContainer").hide();
        $("#RemoveLastAlternative").hide();
        $("#AlternativeTable").hide();
        $("#ConfirmAlternativeScores").hide();
        alternativeList = [];
        alternativeCounter = 0;
        $("#AlternativeTableRows").html('');

      }

      if (criteriaCounter <= 1) {
        $("#CalculateCriteriaScores").hide();
      }
    });
    
    $("#CalculateCriteriaScores").click(function () {
      $(".criteriaSliderID").each(function () {
        var temp = parseInt($(this).val().toString());
        if (temp == -1 || temp == 0 || temp == 1) {
          criteriaComparisonValues.push(1);
        }
        else criteriaComparisonValues.push(temp);
      });


      /////////////////////
      let Calc = new Calculator();
      let Matrix = Calc.MatrixFiller(criteriaCounter, criteriaComparisonValues);
      let NthRoot = Calc.NthRoots(criteriaCounter, Matrix);
      CriteriaWeight = Calc.Weights(criteriaCounter, NthRoot);

      let Consistency = Calc.CalculateConsistency(criteriaCounter, Matrix);
      if (Consistency > 0.1) alert("You are incosistent!");
      //////////////////////

      criteriaComparisonValues = [];

      $("#AlternativeRow").show();

     });

    $("#SetAlternativeName").click(function () {
      var name = $("#AlternativeNameInput").val();
      alternativeCounter = alternativeCounter + 1;
      var addAlternative = "<tr><th scope = 'row'>" + alternativeCounter + "</th><td>" + name + "</td></tr>";
      $("#AlternativeTableRows").append(addAlternative);
    
      if (alternativeCounter > 1) {
        $("#ConfirmAlternativeScores").show();
        $("#AlternativeContainer").show();
        $("#AlternativeComparisonHelp").show();
        $("#CriteriaButtons").show();
      }
    
      alternativeList.push(name.toString());
    
      $("#RemoveLastAlternative").show();
      $("#AlternativeTable").show();
      
      for (var alternative = 0; alternative < alternativeList.length - 1; alternative++) {
        for (var criterion = 0; criterion < criteriaList.length; criterion++) {
          var addComparison = "<div class='row justify-content-center mt-4'><span class='mr-5' style='color: dodgerblue; font-weight: bold'; padding-top: 4px>" + name + "</span><input type='range' class='custom-range mt-3' min ='-9' max ='9' step ='1' style='width: 50%'><span class='ml-5' style='color: dodgerblue; font-weight: bold'>" + alternativeList[alternative] + "</span></div>";
          $("#CriterionRowID" + criterion.toString()).children().last().append(addComparison);
          $("#AlternativeContainer").find('input').addClass("alternativeSliderID");
        }
      }
    });
    
    $("#RemoveLastAlternative").click(function () {
      $("#AlternativeTableRows").children().last().remove();
    
      alternativeList.pop();
    
      if (alternativeCounter > 0) alternativeCounter = alternativeCounter - 1;
    
      if (alternativeCounter == 0) {
        $("#RemoveLastAlternative").hide();
        $("#AlternativeTable").hide();
      }

      if (alternativeCounter <= 1) {
        $("#ConfirmAlternativeScores").hide();
        $("#AlternativeContainer").hide();
        $("#AlternativeComparisonHelp").hide();
      }

      for (let i = 0; i < criteriaCounter; i++) {
        for (let j = 0; j < alternativeCounter; j++) {
          $("#CriterionRowID" + i.toString()).children().last().children().last().remove();
        }
      }
    });

    $("#ConfirmAlternativeScores").click(function () {

      var AlternativeComparisonMatrix = new Array();

      for (let i = 0; i < criteriaCounter; i++) {
        alternativeComparisonValues = [];
        $("#CriterionRowID" + i + " .alternativeSliderID").each(function (j) {

          var temp = parseInt($(this).val().toString());
          if (temp == -1 || temp == 0 || temp == 1) {
            alternativeComparisonValues.push(1);
          }
          else alternativeComparisonValues.push(temp);
        });

        //////////////////////
        let Calc = new Calculator();
        let Matrix = Calc.MatrixFiller(alternativeCounter, alternativeComparisonValues);
        let NthRoot = Calc.NthRoots(alternativeCounter, Matrix);
        AlternativeComparisonMatrix[i] = Calc.Weights(alternativeCounter, NthRoot);

        let Consistency = Calc.CalculateConsistency(alternativeCounter, Matrix);
        if (Consistency > 0.1) alert("You are incosistent!");
        ///////////////////////
      }

      let Calc = new Calculator();
      let FinalScore = Calc.CalculateFinalScore(AlternativeComparisonMatrix, CriteriaWeight);
      console.log(FinalScore);
    });
  }
}
