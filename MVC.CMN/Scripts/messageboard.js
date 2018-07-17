//$(document).ready(function () {
(function () {
    console.log("ready!");
    

    //Find out how to register 'click'-event as EVENT.CLICK    
    //$('.quote').on('click', { foo: "Text To Quote" }, onQuote );
    $('a[data-trigger="quote"]').on('click', onQuote);

    function onQuote(event) {
        event.preventDefault();        
        
        var source = $(event.target).closest("div").parent().siblings(".cell-content").find('p').first();//.find(".portlet-content")       
        var target = $("#NewPostInput");

        var currentText = target.val();
        var newText = currentText + '<blockquote>' + source[0].innerText + '</blockquote>';

        target.val(newText).focus();

        console.log(source);
        console.log(target);
    }
}(jQuery));    

//});