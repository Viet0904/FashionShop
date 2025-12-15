/* ----- Custom Scripts for Destiny template (Blazor-safe) ----- */

(function ($) {
    "use strict";

    /* ===============================
       HELPER FUNCTIONS
    =============================== */

    function getOffsetTop(selector) {
        var el = document.querySelector(selector);
        if (!el) return null;
        var rect = el.getBoundingClientRect();
        return rect.top + window.pageYOffset;
    }

    function exists(selector) {
        return document.querySelector(selector) !== null;
    }

    /* ===============================
       NAVBAR SCROLL EFFECT
    =============================== */

    var mainBottom = getOffsetTop('#main');

    $(window).on('scroll', function () {
        if (mainBottom === null) return;

        var stop = Math.round($(window).scrollTop());

        if (stop > mainBottom) {
            $('.navbar').addClass('past-main effect-main');
        } else {
            $('.navbar').removeClass('past-main effect-main');
        }
    });

    /* ===============================
       COLLAPSE NAVBAR ON CLICK
    =============================== */

    $(document).on('click', '.navbar-collapse.in a', function () {
        $(this).closest('.navbar-collapse')
            .removeClass('in')
            .addClass('collapse');
    });

    /* ===============================
       OWL CAROUSEL
    =============================== */

    if ($.fn.owlCarousel) {
        if (exists('.testimonials')) {
            $('.testimonials').owlCarousel({
                slideSpeed: 200,
                items: 1,
                autoPlay: true,
                pagination: false
            });
        }

        if (exists('.clients')) {
            $('.clients').owlCarousel({
                slideSpeed: 200,
                items: 5,
                autoPlay: true,
                pagination: false
            });
        }
    }

    /* ===============================
       SMOOTH SCROLL
    =============================== */

    $('a.page-scroll').on('click', function (e) {
        var target = $(this).attr('href');
        var top = getOffsetTop(target);

        if (top !== null) {
            $('html, body').stop().animate({
                scrollTop: top
            }, 1500, 'easeInOutExpo');
        }

        e.preventDefault();
    });

    /* ===============================
       MAGNIFIC POPUP
    =============================== */

    if ($.fn.magnificPopup && exists('.popup')) {
        $('.popup').magnificPopup({
            disableOn: 0,
            type: 'iframe',
            mainClass: 'mfp-fade',
            removalDelay: 160,
            preloader: false,
            fixedContentPos: false
        });
    }

    /* ===============================
       JARALLAX
    =============================== */

    if ($.fn.jarallax) {
        if (exists('.jarallax')) {
            $('.jarallax').jarallax({ speed: 0.7 });
        }

        if (exists('.personal-jarallax')) {
            $('.personal-jarallax').jarallax({ speed: 0.7 });
        }
    }

    /* ===============================
       PRELOADER
    =============================== */

    $(window).on('load', function () {
        if (!exists('#loading')) return;

        setTimeout(function () {
            $('#loading').fadeOut('slow');
        }, 3000);
    });

    /* ===============================
       WOW ANIMATION
    =============================== */

    if (typeof WOW === 'function') {
        new WOW().init();
    }

    /* ===============================
       COUNTER UP
    =============================== */

    if ($.fn.counterUp && exists('.counter')) {
        $('.counter').counterUp({
            delay: 10,
            time: 1000
        });
    }

    /* ===============================
       COUNTDOWN
    =============================== */

    if ($.fn.countDown && exists('#countdown')) {
        $('#countdown').countDown({
            targetDate: {
                day: 14,
                month: 7,
                year: 2017,
                hour: 11,
                min: 13,
                sec: 0
            },
            omitWeeks: true
        });

        if ($('.day_field .top').html() === "0") {
            $('.day_field').hide();
        }
    }

    /* ===============================
       SCROLL TO TOP
    =============================== */

    $(window).on('scroll', function () {
        if ($(this).scrollTop() > 1000) {
            $('#back-top').fadeIn();
        } else {
            $('#back-top').fadeOut();
        }
    });

    $('#back-top').on('click', function () {
        $(this).tooltip('hide');
        $('html, body').animate({ scrollTop: 0 }, 1500);
        return false;
    });

    /* ===============================
       ANIMSITION
    =============================== */

    if ($.fn.animsition && exists('.animsition')) {
        $('.animsition').animsition({
            inClass: 'fade-in',
            outClass: 'fade-out',
            inDuration: 1500,
            outDuration: 800,
            linkElement: '.animsition-link',
            loading: true,
            loadingParentElement: 'body',
            loadingClass: 'animsition-loading',
            timeout: false,
            onLoadEvent: true,
            browser: ['animation-duration', '-webkit-animation-duration'],
            overlay: false,
            transition: function (url) {
                window.location.href = url;
            }
        });
    }

    /* ===============================
       SUBSCRIBE FORM
    =============================== */

    $('.subscribe-form').on('submit', function (e) {
        e.preventDefault();

        var postdata = $(this).serialize();

        $.ajax({
            type: 'POST',
            url: 'assets/php/subscribe.php',
            data: postdata,
            dataType: 'json',
            success: function (json) {
                if (json.valid === 0) {
                    $('.success-message').hide();
                    $('.error-message')
                        .html(json.message)
                        .fadeIn('fast');
                } else {
                    $('.error-message').hide();
                    $('.subscribe-form').hide();
                    $('.success-message')
                        .html(json.message)
                        .fadeIn('fast');
                }
            }
        });
    });

})(jQuery);
