var form_picker = function () {
    //BEGIN TIME PICKER
    var handleTimePicker = function () {
        $('.timepicker-default').timepicker();
        $('.timepicker-24hr').timepicker({
            autoclose: true,
            minuteStep: 1,
            showSeconds: true,
            showMeridian: false
        });
    }
    //END TIME PICKER
    return {
        init: function () {
            handleTimePicker();
        }
    }
}(jQuery);

