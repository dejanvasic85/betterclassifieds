$(window).scroll(function(){
            $(".rightSidebarMessage")
              .stop()
              .animate({"marginTop": ($(window).scrollTop()) + "px"}, "slow" );
});