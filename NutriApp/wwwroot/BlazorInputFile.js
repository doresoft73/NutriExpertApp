// This function clears the value of the input file element, effectively resetting it
window.BlazorInputFile = {
    clearValue: function (inputElement) {
        inputElement.value = '';
    },
    // This function sets the inner text of the label element that is the next sibling of the input file element
    // Alternatively, you can use a different selector or attribute to find the label element by its id, class, or data attribute, and pass that as a third parameter
    setLabelText: function (inputElement, labelText) {
        inputElement.nextElementSibling.innerText = labelText;
    }
};