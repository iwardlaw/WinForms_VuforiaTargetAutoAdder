// ==UserScript==
// @name        Vuforia Add Target Autofill
// @namespace   iwardlaw.github.io
// @description Adds autofill option to the "Add Target" dialogue in Vuforia's target manager.
// @include     https://developer.vuforia.com/targetmanager/project/deviceTargetListing
// @include     https://en.wikipedia.org/wiki/Ern_Parker
// @version     1
// @grant       none
// ==/UserScript==
//allow pasting

//alert("We're trying?");

var formFieldDiv = document.forms.namedItem("addNonCloudTargetForm").getElementsByClassName("modal-body")[0].children[0];

formFieldDiv.appendChild(createInput("File Prefix:", "filePrefix", "prefix"));
formFieldDiv.appendChild(createInput("File Extension (without \".\"):", "fileExt", "extension"))
// Row
// Column
// Max Row
// Max Column

function createInput(labelText, inputID, inputName) {
  var outerDiv = document.createElement("div");
  outerDiv.setAttribute("class", "control-group");
  var label = document.createElement("label");
  label.setAttribute("for", inputID);
  label.setAttribute("class", "control-label pull-left label_margin label_headline");
  label.innerHTML = labelText;
  var innerDiv = document.createElement("div");
  innerDiv.setAttribute("class", "controls clear");
  var input = document.createElement("input");
  input.setAttribute("class", "span2");
  input.id = inputID;
  input.name = inputName;
  input.type = "text";

  innerDiv.appendChild(input);
  outerDiv.appendChild(label);
  outerDiv.appendChild(innerDiv);
  //formFieldDiv.appendChild(outerDiv);
  return outerDiv;
}

alert("We succeeded?");