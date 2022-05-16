function ShowNotifyMessage(message, type) {
    $('.notifyjs-corner').empty();
    $.notify(message, type);
}

$('.counter-count').each(function () {
    $(this).prop('Counter', 0).animate({
        Counter: $(this).text()
    }, {
        duration: 5000,
        easing: 'swing',
        step: function (now) {
            $(this).text(Math.ceil(now));
        }
    });
});

$(document).ready(function () {
    $('.dummy_datepicker').kendoDatePicker({
        dateInput: true,
        change: ValidateKendoDate
    }).data("kendoDatePicker");

    $(".dummy_image_previewer").change(function () {
        ReadImageAndPreview(this);
    });
});

function ValidateKendoDate(element) {
    let piclerElem = element.sender.element;
    let pickerValue = this.value();
    let errorContainer = $(piclerElem).closest(".date_error_container").find(".dummy_date_error");
    if (pickerValue == null) {
        $(errorContainer).text("This value is required.");
    } else {
        $(errorContainer).text("");
    }
}


function ReadImageAndPreview(input) {
    if (input.files && input.files[0]) {
        let fileName = input.files[0].name;
        let idxDot = fileName.lastIndexOf(".") + 1;
        var extn = fileName.substr(idxDot, fileName.length).toLowerCase();
        if (extn == "gif" || extn == "png" || extn == "jpg" || extn == "jpeg") {
            if (typeof (FileReader) != "undefined") {
                var reader = new FileReader();
                let previewSource = $(input).attr("target");
                reader.onload = function (e) {
                    $(previewSource).attr("src", e.target.result);
                };

                reader.readAsDataURL(input.files[0]);
            } else {
                alert("This browser does not support FileReader.");
            }
        } else {
            alert("Please select only images");
        }
    }
}