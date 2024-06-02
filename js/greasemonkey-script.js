// ==UserScript==
// @name        Vuforia Add Target Autofill
// @namespace   iwardlaw.github.io
// @description Adds autofill option to the "Add Target" dialogue in Vuforia's target manager.
// @include     https://developer.vuforia.com/targetmanager/project/deviceTargetListing
// @include     https://en.wikipedia.org/wiki/Ern_Parker
// @version     1.1
// @grant       none
// ==/UserScript==

//alert("We're trying?");

const formFieldDiv = document.forms.namedItem("addNonCloudTargetForm").getElementsByClassName("modal-body")[0].children[0];

formFieldDiv.appendChild(createInput("File Prefix:", "filePrefix", "prefix"));
formFieldDiv.appendChild(createInput("File Extension (without \".\"):", "fileExt", "extension"))
// Row
// Column
// Max Row
// Max Column

function createInput(labelText, inputID, inputName) {
  const outerDiv = document.createElement("div");
  outerDiv.classList.add("control-group");

  const label = document.createElement("label");
  label.setAttribute("for", inputID);
  label.classList.add("control-label", "pull-left", "label_margin", "label_headline");
  label.textContent = labelText;

  const innerDiv = document.createElement("div");
  innerDiv.classList.add("controls clear");

  const input = document.createElement("input");
  input.classList.add("span2");
  input.id = inputID;
  input.name = inputName;
  input.type = "text";

  innerDiv.appendChild(input);
  outerDiv.appendChild(label);
  outerDiv.appendChild(innerDiv);
	
  return outerDiv;
}

//alert("We succeeded?");
