window.initializeAutoExpandTextAreas = function () {
    const textAreas = document.querySelectorAll('textarea.auto-expand');
    textAreas.forEach(element => {
        // Ajusta la altura del textarea
        element.style.height = "auto";
        element.style.height = (element.scrollHeight) + "px";
        ///element.addEventListener('input', adjustHeightAndScroll);
    });
};

function adjustHeightAndScroll(event) {
    event.preventDefault(); // Prevenir el comportamiento por defecto del evento

    const textarea = event.target;
    const form = document.querySelector('.scrollableForm');

    // Obtener el lineHeight del textarea
    const computedStyle = window.getComputedStyle(textarea);
    const lineHeight = parseFloat(computedStyle.lineHeight);

    // Obtener posición del cursor
    const cursorPos = textarea.selectionStart;

    // Obtener texto antes del cursor
    const textBeforeCursor = textarea.value.substring(0, cursorPos);

    // Contar líneas hasta el cursor
    const linesBeforeCursor = textBeforeCursor.split('\n').length;

    // Calcular la posición del textarea dentro del formulario
    const textareaRect = textarea.getBoundingClientRect();
    const formRect = form.getBoundingClientRect();
    const textareaTopRelativeToForm = textareaRect.top - formRect.top;

    // Calcula el offset de scroll necesario
    const scrollOffset = textareaTopRelativeToForm + (linesBeforeCursor * lineHeight);

    // Aplicar scroll al formulario
    form.scrollTo({ top: scrollOffset });
}