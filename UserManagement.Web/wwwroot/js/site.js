// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.




$(document).ready(function () {

    //only accepts letters for forename and surname on AddUser and EditUser
    $('#forename').keyup(function () {
        var $letter = $(this);
        $letter.val($letter.val().replace(/[^a-zA-Z]/, function () { alert('Please use only letters.'); return ''; }));
    });
    $('#surname').keyup(function () {
        var $letter = $(this);
        $letter.val($letter.val().replace(/[^a-zA-Z]/, function () { alert('Please use only letters.'); return ''; }));
    });

    //year range is from 1904 to current day
    $(function () {
        $("#dateOfBirth").datepicker({
            changeMonth: true,
            changeYear: true,
            yearRange: "-120:+0",
            maxDate: "0",
            dateFormat: "dd/mm/yy"
        });
    });

});
