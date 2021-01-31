const swapAttrib = 'swap';
const swapThreshold = 960;

function Swap(element) {

    const swapTarget = document.getElementById(element.getAttribute(swapAttrib));

    if (!swapTarget) {
        throw 'Target Element was not found! Check the id and swap attribute';
        return;
    }

    let mid = swapTarget.cloneNode();
    element.replaceWith(mid);
    swapTarget.replaceWith(element);

}

let swappables = [];
let lastWindowWidth = window.innerWidth;

window.addEventListener("load", () => {

    // Init Swappables
    swappables = document.querySelectorAll(`[${swapAttrib}]`);

    // Init Swappable Position if less than 
    if (window.innerWidth <= swapThreshold) swappables.forEach(i => Swap(i));

    // Handle Swapping on window Resize
    window.addEventListener('resize', () => {

        // Width Greater than Threshold (Desktop)
        if (window.innerWidth >= swapThreshold && swapThreshold > lastWindowWidth)
            swappables.forEach(i => Swap(i));

        // Width Less than Threshold (Mobile)
        else if (window.innerWidth <= swapThreshold && swapThreshold < lastWindowWidth)
            swappables.forEach(i => Swap(i));

        lastWindowWidth = window.innerWidth;

    })
})
