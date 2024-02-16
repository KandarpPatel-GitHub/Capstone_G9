﻿
// Load the carousel when the document is ready
$(document).ready(function () {
    $('.carousel').carousel();
});

//Load the animated text when the document is ready
document.addEventListener("DOMContentLoaded", function () {
    // Get the elements
    var animatedText = document.getElementById("animatedText");
    var animatedSubText = document.getElementById("animatedSubText");

    // Set initial styles
    animatedText.style.opacity = 0;
    animatedSubText.style.opacity = 0;
    animatedText.style.transform = "translateY(20px)";
    animatedSubText.style.transform = "translateY(20px)";

    // Add animation styles
    animatedText.style.transition = "opacity 1s ease, transform 1s ease";
    animatedSubText.style.transition = "opacity 1s ease, transform 1s ease";

    // Trigger the animation
    setTimeout(function () {
        animatedText.style.opacity = 1;
        animatedText.style.transform = "translateY(0)";
    }, 100);

    setTimeout(function () {
        animatedSubText.style.opacity = 1;
        animatedSubText.style.transform = "translateY(0)";
    }, 300);
});
window.addEventListener('load', function () {
    // Wait for the header animation to complete
    setTimeout(function () {
        document.getElementById('imageCarousel').classList.add('carousel-visible');
    }, 5000);

// Delay function for the animation.
$(document).ready(function () {
    $('.highlights-list li').each(function (i) {
        // Increase the delay by 0.2 seconds for each item
        $(this).css('animation-delay', `${0.2 * i}s`);
    });
});

// Animate the image in from the right
$(document).ready(function () {
    var $image = $('.highlists-image');
    // Initially, set the image off-screen to the right
    $image.css({
        opacity: 0,
        transform: 'translateX(100%)'
    });

    // Function to trigger the animation
    function animateImage() {
        $image.css('opacity', 1); 
        $image.addClass('animate-in-from-right');
    }

    setTimeout(animateImage, 2000);
});



document.addEventListener("DOMContentLoaded", function () {
    const colors = ["#FF6347", "#4682B4", "#32CD32", "#FFD700", "#6A5ACD", "#FF69B4"];
    const listItems = document.querySelectorAll('.highlights-list li');

    listItems.forEach((item) => {
        // Continuously change color every second
        setInterval(() => {
            if (!item.classList.contains('hover')) {
                const randomColorIndex = Math.floor(Math.random() * colors.length);
                item.style.color = colors[randomColorIndex]; 
            }
        }, 1000); 

        item.addEventListener('mouseover', () => {
            item.classList.add('hover'); 
            const randomColorIndex = Math.floor(Math.random() * colors.length);
            item.style.color = colors[randomColorIndex];
        });

        item.addEventListener('mouseout', () => {
            item.classList.remove('hover'); 
        });
    });
});




document.addEventListener("DOMContentLoaded", function () {
    window.addEventListener('scroll', function () {
        if (window.scrollY > 50) { 
            document.querySelector('.navbar').classList.add('scrolled');
        } else {
            document.querySelector('.navbar').classList.remove('scrolled');
        }
    });
});









