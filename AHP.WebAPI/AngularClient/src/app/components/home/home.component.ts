import { Component, OnInit } from '@angular/core';
import * as $ from 'jquery';
import { Choice } from '../../classes/choice';
import { ChoiceService } from '../../services/choice.service';
import Calculator from './HelperFunctions.js';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  constructor(private choiceService: ChoiceService) { }

  globalChoiceName: string;

  ngOnInit() {

    var criteriaCounter = 0;
    var alternativeCounter = 0;
    var CriteriaWeight;

    $("#RemoveLastCriterion").hide();
    $("#saveCriteria").hide();
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

    $("#SetChoiceName").click(() => {
      var name = $("#ChoiceNameInput").val();
      this.globalChoiceName = name.toString();

      var add = "<h1>" + name + "</h1>";
      $("#ShowChoiceName").text("");
      $("#ShowChoiceName").append(add);
    });

    $("#SetCriterionName").click(function () {
      var name = $("#CriterionNameInput").val();
      criteriaList.push(name.toString());

      criteriaCounter = criteriaCounter + 1;
      var addCriterion = "<tr><th scope = 'row'>" + criteriaCounter + "</th><td>" + name + "</td><td><button type='button' class='btn btn-primary' val='"+criteriaCounter+"' id='CriteriaEditButton" + criteriaCounter + "'>Edit</button></td></tr>";
      $("#CriteriaTableRows").append(addCriterion);

      $("#RemoveLastCriterion").show();
      $("#CriteriaTable").show();
      $("#saveCriteria").show();

      if (criteriaCounter > 1) $("#CalculateCriteriaScores").show();

      var sliderContainer = "<div class='container sliderContainer' id='sliderContainer" + criteriaCounter + "'></div>"
      $("#CriteriaContainer").append(sliderContainer);
      $("#sliderContainer" + criteriaCounter).hide();

      $(".sliderContainer").each(function (i) {
        $(this).html("");
        for (var criterion = 0; criterion < criteriaList.length; criterion++) {
            if (criteriaList[criterion] != criteriaList[i]) {
              var slider = "<div class='row text-center'><div class='col mt-4'><span class='mr-5 mb-1' style='color: dodgerblue; font-weight: bold'>" + criteriaList[i] + "</span><input type='range' class='custom-range mt-3' min ='-9' max ='9' step ='1' value='0' style='width: 50%' id='" + criteriaList[i] + "/" + criteriaList[criterion] + "'><span class='ml-5' style='color: dodgerblue; font-weight: bold'>" + criteriaList[criterion] + "</span></div></div>";
              $(this).append(slider);
            }
           }
      })
      
      $("#CriteriaEditButton" + criteriaCounter).on('click', function () {
        if ($("#sliderContainer" + $(this).attr('val')).is(":visible")) $("#sliderContainer" + $(this).attr('val')).hide();
        else $("#sliderContainer" + $(this).attr('val')).show();
      });

      var addCriterionRow = "<div class='row mt-5'><div class='col'><h5>Compare the alternatives by criterion: " + criteriaList[criteriaCounter - 1] + "</h4></div></div>";
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
      if (alternativeList.length > 1) {
        for (var i = 1; i < alternativeList.length; i++) {
          for (var j = 0; j < i; j++) {
            var addComparison = "<div class='row justify-content-center mt-4'><span class='mr-5' style='color: dodgerblue; font-weight: bold'; padding-top: 4px>" + alternativeList[i] + "</span><input type='range' class='custom-range mt-3' min ='-9' max ='9' step ='1' style='width: 50%'><span class='ml-5' style='color: dodgerblue; font-weight: bold'>" + alternativeList[j] + "</span></div>";
            $("#CriterionRowID" + (criteriaCounter-1)).children().last().append(addComparison);
            $("#AlternativeContainer").find('input').addClass("alternativeSliderID");
          }
        }
      }

    });

    $("#RemoveLastCriterion").click(function () {
      $("#CriteriaTableRows").children().last().remove();
      $("#AlternativeContainer").children().last().remove();

      $("#CriteriaButtons").children().last().remove();

      criteriaList.pop();

      for (let i = 0; i < criteriaCounter; i++) {
        $("#sliderContainer" + (i + 1)).children().last().remove();
      }
      $("#CriteriaContainer").children().last().remove();

      if (criteriaCounter > 0) criteriaCounter = criteriaCounter - 1;

      if (criteriaCounter == 0) {
        $("#RemoveLastCriterion").hide();
        $("#saveCriteria").hide();
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
      $(".sliderContainer .custom-range").each(function () {
        var temp = parseInt($(this).val().toString());
        if (temp == -1 || temp == 0 || temp == 1) {
          criteriaComparisonValues.push(1);
        }
        else criteriaComparisonValues.push(temp);
      });
      console.log(criteriaComparisonValues);


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
          var addComparison = "<div class='row justify-content-center mt-4'><span class='mr-5' style='color: dodgerblue; font-weight: bold'; padding-top: 4px>" + name + "</span><input type='range' class='custom-range mt-3' min ='-9' max ='9' step ='1' style='width: 50%' id='" + criteriaList[criterion] + "/" + name + "/" + alternativeList[alternative] + "'><span class='ml-5' style='color: dodgerblue; font-weight: bold'>" + alternativeList[alternative] + "</span></div>";
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
        $("#CriteriaButtons").hide();
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

    $("#CriteriaContainer").on('change', '.custom-range', function () {
      var ID = this.id.split("/");
      $("#" + ID[1] + "\\/" + ID[0]).prop('value', -this.value);
    });
  }

  saveCriteria() {
    let choice: Choice = new Choice();
    choice.ChoiceName = this.globalChoiceName;
    choice.UserID = window.localStorage['UserID'];

    this.choiceService.createChoice(choice).subscribe(data => {
      console.log(data);
    });
  }
}
