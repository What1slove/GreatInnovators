// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$('#datetimepicker1').datetimepicker(
    {
        icons:{
            time: 'far fa-clock'
        }
    }
);

$('#datetimepicker2').datetimepicker(
    {
        icons: {
            time: 'far fa-clock'
        }
    }
);

$('#buttonAS').click(function () {
    
    $('#serviceList').append('<input type="text" readonly class="form-control-plaintext" value="Service 3">');
});