$(document).ready(function() {
    console.log("ready!");

    //Find out how to register 'click'-event as EVENT.CLICK    
    //$('.quote').on('click', { foo: "Text To Quote" }, onQuote );
    $('a[data-trigger="quote"]').on('click', { foo: "Text To Quote" }, onQuote );

    function onQuote(event) {
      alert(event.data.foo);
    }
    

});