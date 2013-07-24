$(function () {
    
    // highlight grid rows on hover
    $(".search-row").hover(
              function () {
                  $(this).addClass("hover");
              },
              function () {
                  $(this).removeClass("hover");
              }
        );
});    
