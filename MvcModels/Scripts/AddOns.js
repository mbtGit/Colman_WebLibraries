$(document).ready(function () {
    $("ul.top li").hover(function () { //When trigger is hovered...
        $(this).children("ul.sub").slideDown('fast');
    }, function () {
        $(this).children("ul.sub").slideUp('slow');
    });
});