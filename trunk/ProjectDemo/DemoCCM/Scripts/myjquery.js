// JavaScript Document
 $(function(){
        $("ul.nav_left li a").hover(function(){
            $(this).addClass('highlight');
        }, function(){
            $(this).removeClass('highlight');
        });
        $("ul.nav_left li a").click(function(){
            $('.highlight_stay').removeClass('highlight_stay');
            $(this).addClass('highlight_stay');
			
        });
 });
