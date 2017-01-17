
function getUrlParameter(sParam) {
    sParam = sParam.toLowerCase();

        var sURLVariables = window.location.search.substring(1).split('&'), sParameterName, i;

        for (i = 0; i < sURLVariables.length; i++) {
            sParameterName = sURLVariables[i].split('=');

            if (sParameterName[0].toLowerCase() === sParam) {
                return sParameterName[1] === undefined ? true : decodeURIComponent(sParameterName[1]);
            }
        }
    }



    $(document).ready(function() {
        // display success toast
        var success = getUrlParameter('success');
        if (typeof (success) !='undefined' && success.length> 0) {
            toastr.success(success);
        }

        // dispply error toast
        var error = getUrlParameter('error');
        if (typeof (error) != 'undefined' && error > 0) {
            toastr.error(error);
        }

    });

    $(window).load(function () {
        
    });


// $(#customers".on("click",".js-delete",function() {
// var button = $(this);
//});

/*
bootbox.confirm("Are you sure?", function (result) {
    if (result) {
//               $.ajax({
    //        url: "",
        //    method: "delete",
        //    success: function () {
         //   button.parents("tr").removeAll();
       // })
    }
});
*/

