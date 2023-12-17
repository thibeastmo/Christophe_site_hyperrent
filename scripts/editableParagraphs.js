
window.addEventListener("load", function () {
    var textbox1 = document.getElementById("ContentPlaceHolder1_txtParagraph1");
var textbox2 = document.getElementById("ContentPlaceHolder1_txtParagraph2");

console.log(textbox1);
console.log(textbox2);

textbox1.addEventListener("change", DoPostBack);
textbox2.addEventListener("change", DoPostBack);
});



function DoPostBack() {
    alert('test werkte')
    __doPostBack("txtParagraph1", "ParagraphChanged");
}