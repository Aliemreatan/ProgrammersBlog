$(document).ready(function () {

    /* DataTables start here. */

    $('#articlesTable').DataTable({
        dom:
            "<'row'<'col-sm-3'l><'col-sm-6 text-center'B><'col-sm-3'f>>" +
            "<'row'<'col-sm-12'tr>>" +
            "<'row'<'col-sm-5'i><'col-sm-7'p>>",
        buttons: [
            {
                text: 'Ekle',
                attr: {
                    id: "btnAdd",
                },
                className: 'btn btn-success',
                action: function (e, dt, node, config) {
                }
            },
            {
                text: 'Yenile',
                className: 'btn btn-warning',
                action: function (e, dt, node, config) {
                    $.ajax({
                        type: 'GET',
                        url: '/Admin/Articles/GetAllArticles/',
                        contentType: "application/json",
                        beforeSend: function () {
                            $('#categoriesTable').hide();
                            $('.spinner-border').show();
                        },
                        success: function (data) {
                            const articleListDto = jQuery.parseJSON(data);
                            console.log(articleListDto);
                            if (articleListDto.ResultStatus === 0) {
                                let tableBody = "";
                                $.each(articleListDto.Articles.$values,
                                    function (index, article) {
                                        tableBody += `
                                                <tr>
                                    <td>${article.Id}</td>
                                    <td>${article.Title}</td>
                                    <td>${article.Thumbnail}</td>
                                    <td>${convertFirstLetterToUpperCase(article.IsActive.toString())}</td>
                                    <td>${convertFirstLetterToUpperCase(article.IsDeleted.toString())}</td>
                                    <td>${article.Note}</td>
                                    <td>${convertToShortDate(article.CreatedDate)}</td>
                                    <td>${article.CreatedByName}</td>
                                    <td>${convertToShortDate(article.ModifiedDate)}</td>
                                    <td>${article.ModifiedByName}</td>
                                    <td>
                                <button class="btn btn-primary btn-sm btn-update" data-id="${article.Id}"><span class="fas fa-edit"></span></button>
                                <button class="btn btn-danger btn-sm btn-delete" data-id="${article.Id}"><span class="fas fa-minus-circle"></span></button>
                                <button class="btn btn-danger btn-sm btn-delete" data-id="${article.Content}"><span class="fas fa-minus-circle"></span></button>
                                    </td>
                                            </tr>`;
                                    });
                                $('#articlesTable > tbody').replaceWith(tableBody);
                                $('.spinner-border').hide();
                                $('#articlesTable').fadeIn(1400);
                            } else {
                                toastr.error(`${articleListDto.Message}`, 'İşlem Başarısız!');
                            }
                        },
                        error: function (err) {
                            console.log(err);
                            $('.spinner-border').hide();
                            $('#articlesTable').fadeIn(1000);
                            toastr.error(`${err.responseText}`, 'Hata!');
                        }
                    });
                }
            }
        ],
        language: {
            "sDecimal": ",",
            "sEmptyTable": "Tabloda herhangi bir veri mevcut değil",
            "sInfo": "_TOTAL_ kayıttan _START_ - _END_ arasındaki kayıtlar gösteriliyor",
            "sInfoEmpty": "Kayıt yok",
            "sInfoFiltered": "(_MAX_ kayıt içerisinden bulunan)",
            "sInfoPostFix": "",
            "sInfoThousands": ".",
            "sLengthMenu": "Sayfada _MENU_ kayıt göster",
            "sLoadingRecords": "Yükleniyor...",
            "sProcessing": "İşleniyor...",
            "sSearch": "Ara:",
            "sZeroRecords": "Eşleşen kayıt bulunamadı",
            "oPaginate": {
                "sFirst": "İlk",
                "sLast": "Son",
                "sNext": "Sonraki",
                "sPrevious": "Önceki"
            },
            "oAria": {
                "sSortAscending": ": artan sütun sıralamasını aktifleştir",
                "sSortDescending": ": azalan sütun sıralamasını aktifleştir"
            },
            "select": {
                "rows": {
                    "_": "%d kayıt seçildi",
                    "0": "",
                    "1": "1 kayıt seçildi"
                }
            }
        }
    });

    /* DataTables end here */

    /* Ajax GET / Getting the _CategoryAddPartial as Modal Form starts from here. */

    $(function () {
        const url = '/Admin/Article/Add/';
        const placeHolderDiv = $('#modalPlaceHolder');
        $('#btnAdd').click(function () {
            $.get(url).done(function (data) {
                placeHolderDiv.html(data);
                placeHolderDiv.find(".modal").modal('show');
            });
        });

        /* Ajax GET / Getting the _CategoryAddPartial as Modal Form ends here. */

        /* Ajax POST / Posting the FormData as CategoryAddDto starts from here. */

        placeHolderDiv.on('click',
            '#btnSave',
            function (event) {
                event.preventDefault();
                const form = $('#form-article-add');
                const actionUrl = form.attr('action');
                const dataToSend = form.serialize();
                $.post(actionUrl, dataToSend).done(function (data) {
                    console.log(data);
                    const articleAddAjaxModel = jQuery.parseJSON(data);
                    console.log(articleAddAjaxModel);
                    const newFormBody = $('.modal-body', articleAddAjaxModel.ArticleAddPartial);
                    placeHolderDiv.find('.modal-body').replaceWith(newFormBody);
                    const isValid = newFormBody.find('[name="IsValid"]').val() === 'True';
                    if (isValid) {
                        placeHolderDiv.find('.modal').modal('hide');
                        const newTableRow = `
                                <tr name="${articleAddAjaxModel.ArticleDto.Article.Id}">
                                                    <td>${articleAddAjaxModel.ArticleDto.Article.Id}</td>
                                                    <td>${articleAddAjaxModel.ArticleDto.Article.Title}</td>
                                                    <td>${articleAddAjaxModel.ArticleDto.Article.Thumbnail}</td>
                                                    <td>${convertFirstLetterToUpperCase(articleAddAjaxModel.ArticleDto.Article.IsActive.toString())}</td>
                                                    <td>${convertFirstLetterToUpperCase(articleAddAjaxModel.ArticleDto.Article.IsDeleted.toString())}</td>
                                                    <td>${articleAddAjaxModel.ArticleDto.Article.Note}</td>
                                                    <td>${convertToShortDate(articleAddAjaxModel.ArticleDto.Article.CreatedDate)}</td>
                                                    <td>${articleAddAjaxModel.ArticleDto.Article.CreatedByName}</td>
                                                    <td>${convertToShortDate(articleAddAjaxModel.ArticleDto.Article.ModifiedDate)}</td>
                                                    <td>${articleAddAjaxModel.ArticleDto.Article.ModifiedByName}</td>
                                                    <td>
                                                        <button class="btn btn-primary btn-sm btn-update" data-id="${articleAddAjaxModel.ArticleDto.Article.Id}"><span class="fas fa-edit"></span></button>
                                                        <button class="btn btn-danger btn-sm btn-delete" data-id="${articleAddAjaxModel.ArticleDto.Article.Id}"><span class="fas fa-minus-circle"></span></button>
                                                        <button class="btn btn-danger btn-sm btn-delete" data-id="${articleAddAjaxModel.ArticleDto.Article.Content}"><span class="fas fa-minus-circle"></span></button>
                                                    </td>
                                                </tr>`;
                        const newTableRowObject = $(newTableRow);
                        newTableRowObject.hide();
                        $('#categoriesTable').append(newTableRowObject);
                        newTableRowObject.fadeIn(3500);
                        toastr.success(`${articleAddAjaxModel.ArticleDto.Message}`, 'Başarılı İşlem!');
                    } else {
                        let summaryText = "";
                        $('#validation-summary > ul > li').each(function () {
                            let text = $(this).text();
                            summaryText = `*${text}\n`;
                        });
                        toastr.warning(summaryText);
                    }
                });
            });
    });

    /* Ajax POST / Posting the FormData as CategoryAddDto ends here. */

    /* Ajax POST / Deleting a Category starts from here */

    $(document).on('click',
        '.btn-delete',
        function (event) {
            event.preventDefault();
            const id = $(this).attr('data-id');
            const tableRow = $(`[name="${id}"]`);
            const articleName = tableRow.find('td:eq(1)').text();
            Swal.fire({
                title: 'Silmek istediğinize emin misiniz?',
                text: `${articleName} adlı kategori silinicektir!`,
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Evet, silmek istiyorum.',
                cancelButtonText: 'Hayır, silmek istemiyorum.'
            }).then((result) => {
                if (result.isConfirmed) {
                    $.ajax({
                        type: 'POST',
                        dataType: 'json',
                        data: { articleId: id },
                        url: '/Admin/Article/Delete/',
                        success: function (data) {
                            const articleDto = jQuery.parseJSON(data);
                            if (articleDto.ResultStatus === 0) {
                                Swal.fire(
                                    'Silindi!',
                                    `${articleDto.Article.Title} adlı kategori başarıyla silinmiştir.`,
                                    'success'
                                );

                                tableRow.fadeOut(3500);
                            } else {
                                Swal.fire({
                                    icon: 'error',
                                    title: 'Başarısız İşlem!',
                                    text: `${articleDto.Message}`,
                                });
                            }
                        },
                        error: function (err) {
                            console.log(err);
                            toastr.error(`${err.responseText}`, "Hata!")
                        }
                    });
                }
            });
        });

    /* Ajax GET / Getting the _CategoryUpdatePartial as Modal Form starts from here. */

    $(function () {
        const url = '/Admin/Article/Update/';
        const placeHolderDiv = $('#modalPlaceHolder');
        $(document).on('click',
            '.btn-update',
            function (event) {
                event.preventDefault();
                const id = $(this).attr('data-id');
                $.get(url, { articleId: id }).done(function (data) {
                    placeHolderDiv.html(data);
                    placeHolderDiv.find('.modal').modal('show');
                }).fail(function () {
                    toastr.error("Bir hata oluştu.");
                });
            });

        /* Ajax POST / Updating a Category starts from here */

        placeHolderDiv.on('click',
            '#btnUpdate',
            function (event) {
                event.preventDefault();

                const form = $('#form-article-update');
                const actionUrl = form.attr('action');
                const dataToSend = form.serialize();
                $.post(actionUrl, dataToSend).done(function (data) {
                    const articleUpdateAjaxModel = jQuery.parseJSON(data);
                    console.log(articleUpdateAjaxModel);
                    const newFormBody = $('.modal-body', articleUpdateAjaxModel.ArticleUpdatePartial);
                    placeHolderDiv.find('.modal-body').replaceWith(newFormBody);
                    const isValid = newFormBody.find('[name="IsValid"]').val() === 'True';
                    if (isValid) {
                        placeHolderDiv.find('.modal').modal('hide');
                        const newTableRow = `
                                <tr name="${articleUpdateAjaxModel.ArticleDto.Article.Id}">
                                                    <td>${articleUpdateAjaxModel.ArticleDto.Article.Id}</td>
                                                    <td>${articleUpdateAjaxModel.ArticleDto.Article.Title}</td>
                                                    <td>${articleUpdateAjaxModel.ArticleDto.Article
                                .Description}</td>
                                                    <td>${convertFirstLetterToUpperCase(articleUpdateAjaxModel
                                                        .ArticleDto.Article.IsActive.toString())}</td>
                                                    <td>${convertFirstLetterToUpperCase(articleUpdateAjaxModel
                                                        .ArticleDto.Article.IsDeleted.toString())}</td>
                                                    <td>${articleUpdateAjaxModel.ArticleDto.Article.Note}</td>
                                                    <td>${convertToShortDate(articleUpdateAjaxModel.ArticleDto
                                                        .Article.CreatedDate)}</td>
                                                    <td>${articleUpdateAjaxModel.ArticleDto.Article
                                .CreatedByName}</td>
                                                    <td>${convertToShortDate(articleUpdateAjaxModel.ArticleDto
                                                        .Article.ModifiedDate)}</td>
                                                    <td>${articleUpdateAjaxModel.ArticleDto.Article
                                .ModifiedByName}</td>
                                                    <td>
                                                        <button class="btn btn-primary btn-sm btn-update" data-id="${articleUpdateAjaxModel.ArticleDto.Article.Id}"><span class="fas fa-edit"></span></button>
                                                        <button class="btn btn-danger btn-sm btn-delete" data-id="${articleUpdateAjaxModel.ArticleDto.Article.Id
                            }"><span class="fas fa-minus-circle"></span></button>
                                                        <button class="btn btn-primary btn-sm btn-update" data-id="${articleUpdateAjaxModel.ArticleDto.Article.Content}"><span class="fas fa-edit"></span></button>
                                                    </td>
                                                </tr>`;
                        const newTableRowObject = $(newTableRow);
                        const articleTableRow = $(`[name="${articleUpdateAjaxModel.ArticleDto.Article.Id}"]`);
                        newTableRowObject.hide();
                        articleTableRow.replaceWith(newTableRowObject);
                        newTableRowObject.fadeIn(3500);
                        toastr.success(`${articleUpdateAjaxModel.ArticleDto.Message}`, "Başarılı İşlem!");
                    } else {
                        let summaryText = "";
                        $('#validation-summary > ul > li').each(function () {
                            let text = $(this).text();
                            summaryText = `*${text}\n`;
                        });
                        toastr.warning(summaryText);
                    }
                }).fail(function (response) {
                    console.log(response);
                });
            });

    });
});