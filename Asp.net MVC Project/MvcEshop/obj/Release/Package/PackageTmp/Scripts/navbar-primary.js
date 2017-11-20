$(document).ready(function () {
	WireEvents();
});
function MakeOtherNavBarLinksInactive() { $('#ulMenuBar li').removeClass('active'); }
function WireEvents() {
    $('#ulMenuBar li').click(function () {
        //		$(this).css('background-color', 'yellow');
        MakeOtherNavBarLinksInactive();
        $(this).addClass('active');


    });
//	$('.textBox').mouseleave(function () {
//		//		$(this).css('background-color','white');
//		$(this).removeClass('HighLight');
//	});
////	$(".textBox").keypress(function () { 
////		$(this).val("");
////	});
//	$(".textBox").focus(function () {
//		$(this).addClass('focus');
//	});
//	$(".textBox").focusout(function () {
//		$(this).removeClass('focus');
//	});

	
}