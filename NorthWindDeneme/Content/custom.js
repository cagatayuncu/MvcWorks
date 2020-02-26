$(function () {
 
    $("#tblCategory").DataTable(); 
    $("#tblSuppliers").DataTable();
    $("#tblProducts").DataTable();
    $("#tblCategory").on("click", ".btnCategorySil", function () {
        if (confirm("Silmek istediğinize emin misiniz ?")) {
            var id = $(this).data("id");
            var btn = $(this);
            $.ajax({
                type: "GET",
                url: "/Home/Sil/" + id,
                success: function () {
                    btn.parent().parent().remove();
                }
            });       
        }      
    });

});