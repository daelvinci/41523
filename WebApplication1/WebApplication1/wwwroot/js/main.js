$(document).ready(function () {
    $(document).on("click", '.open-book-modal', function (e) {

        e.preventDefault();
        var url = $(this).attr("href");

        fetch(url)
            .then(response => response.text())
            .then(modalHtml => {
                $("#quickModal .modal-dialog").html(modalHtml)
            });


        $("#quickModal").modal("show")
    })

    $(document).on("click", '.add-to-basket', function(e){
        e.preventDefault();
        var basketUrl = $(this).attr("href");

        fetch(basketUrl)
            .then(response => response.text())
            .then(modalHtml => {
                $(".single-btn .add-to-basket").html(modalHtml)
            })

        })
})

