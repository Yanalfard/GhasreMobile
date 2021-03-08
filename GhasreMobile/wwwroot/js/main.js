﻿//import UIkit from "uikit";

const swapAttrib = 'swap';
const swapThreshold = 960;

function Swap(element) {

    const swapTarget = document.getElementById(element.getAttribute(swapAttrib));

    if (!swapTarget) {
        throw `Target Element (${element.getAttribute(swapAttrib)}) was not found! Check the id and swap attribute`;
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

function share(url, title = "قصر موبایل", text = "فروشگاه موبایل قصر موبایل") {

    if (navigator.share) {
        navigator.share({
            title: title,
            text: text,
            url: url
        }).then((e) => {
            console.log(e)
        }).catch((e) => {
            console.log(e)
        });
    }
    else {
        copy(text, 'لینک اشتراک کپی شد');
    }

}

function copy(text, message) {
    var textArea = document.createElement("textarea");
    textArea.value = text;

    // Avoid scrolling to bottom
    textArea.style.top = "0";
    textArea.style.left = "0";
    textArea.style.position = "fixed";

    document.body.appendChild(textArea);
    textArea.focus();
    textArea.select();

    try {
        var successful = document.execCommand('copy');
    } catch (err) {
    }

    document.body.removeChild(textArea);

    if (message === undefined || message === null) return;

    UIkit.notification(message);
}

// Countdown
window.addEventListener("load", () => {

    const timers = document.querySelectorAll('[countdown]');

    for (let timer of timers) {

        const second = timer.querySelector('[second]');
        const minute = timer.querySelector('[minute]');
        const hour = timer.querySelector('[hour]');
        const day = timer.querySelector('[day]');

        const date = Date.parse(timer.getAttribute('countdown'));

        setInterval(() => {
            const now = Date.now();
            const range = new Date(date - now);

            second.innerHTML = range.getUTCSeconds();
            minute.innerHTML = range.getUTCMinutes();
            hour.innerHTML = range.getUTCHours();
            day.innerHTML = range.getUTCDay();
        }, 1000);

    }

})
