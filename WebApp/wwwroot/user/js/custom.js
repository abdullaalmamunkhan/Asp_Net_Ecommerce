$(document).ready(function () {
    $(document).scroll(function () {
        var scroll = $(this).scrollTop();
        var topDist = $("body").position();
        if (scroll > topDist.top) {
            $('.top-fixed').addClass('fixed-header');
        } else {
            $('.top-fixed').removeClass('fixed-header');
        }
    });


    // hide adsense
    $(".adsense-close_icon").click(function () {
        $(".glg-adsense").fadeOut();
    });
    // product grid

    $(".grid-view-icon").click(function () {
        $(".pd-list-view").fadeIn();
        $(".pd-grid-view").fadeOut();
    })
    $(".list-view-icon").click(function () {
        $(".pd-grid-view").fadeIn();
        $(".pd-list-view").fadeOut();
    })
});