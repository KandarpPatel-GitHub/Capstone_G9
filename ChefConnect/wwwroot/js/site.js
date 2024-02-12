// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {
    $('.carousel').carousel();
});
// Your custom JavaScript code goes here
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
    }, 5000); // Adjust the time to match your header animation duration
});


$(document).ready(function () {
    $('.highlights-list li').each(function (i) {
        // Increase the delay by 0.2 seconds for each item
        $(this).css('animation-delay', `${0.2 * i}s`);
    });
});


$(document).ready(function () {
    var $image = $('.highlists-image');

    // Initially, set the image off-screen to the right
    $image.css({
        opacity: 0,
        transform: 'translateX(100%)'
    });

    // Function to trigger the animation
    function animateImage() {
        $image.css('opacity', 1); // Make sure the image is visible
        $image.addClass('animate-in-from-right'); // Add the animation class
    }

    // Example: animate the image 2 seconds after the page loads
    setTimeout(animateImage, 2000);
});


//$(document).ready(function () {
//    // Initialize the carousel but don't auto-start
//    $('.carousel').carousel('pause');

//    // Assuming the header text animation is ready to be triggered
//    // Trigger the header animation
//    $('.animated-header').css({
//        'opacity': '1',
//        'transform': 'translateY(0)'
//    });

//    // Set a timeout that matches the header animation duration + any desired delay
//    setTimeout(function () {
//        // Start the carousel fade-in by adding the class for visibility
//        $('#imageCarousel').addClass('carousel-visible');

//        // Optionally, if you want to start or restart the carousel sliding, you can do it here
//        $('.carousel').carousel({
//            interval: 8000 // Adjust the interval as needed
//        });
//    }, 5000); // This timeout should match the duration of the header animation + any additional delay you want before the carousel starts
//});

