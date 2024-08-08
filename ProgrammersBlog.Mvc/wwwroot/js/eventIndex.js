$(document).ready(function () {

    /* DataTables start here. */

    $('#eventsTable').DataTable({
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
                        url: '/Admin/Event/GetAllEvents/',
                        contentType: "application/json",
                        beforeSend: function () {
                            $('#eventsTable').hide();
                            $('.spinner-border').show();
                        },
                        success: function (data) {
                            const eventListDto = jQuery.parseJSON(data);
                            console.log(eventListDto);
                            if (eventListDto.ResultStatus === 0) {
                                let tableBody = "";
                                $.each(eventListDto.Events.$values,
                                    function (index, event) {
                                        tableBody += `
                                                <tr>
                                    <td>${event.Id}</td>
                                    <td>${event.Name}</td>
                                    <td>${event.Description}</td>
                                    <td>${convertFirstLetterToUpperCase(event.IsActive.toString())}</td>
                                    <td>${convertFirstLetterToUpperCase(event.IsDeleted.toString())}</td>
                                    <td>${event.Note}</td>
                                    <td>${convertToShortDate(event.CreatedDate)}</td>
                                    <td>${event.CreatedByName}</td>
                                    <td>${convertToShortDate(event.ModifiedDate)}</td>
                                    <td>${event.ModifiedByName}</td>
                                    <td>
                                <button class="btn btn-primary btn-sm btn-update" data-id="${event.Id}"><span class="fas fa-edit"></span></button>
                                <button class="btn btn-danger btn-sm btn-delete" data-id="${event.Id}"><span class="fas fa-minus-circle"></span></button>
                                    </td>
                                            </tr>`;
                                    });
                                $('#eventsTable > tbody').replaceWith(tableBody);
                                $('.spinner-border').hide();
                                $('#eventsTable').fadeIn(1400);
                            } else {
                                toastr.error(`${eventListDto.Message}`, 'İşlem Başarısız!');
                            }
                        },
                        error: function (err) {
                            console.log(err);
                            $('.spinner-border').hide();
                            $('#eventsTable').fadeIn(1000);
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
        const url = '/Admin/Event/Add/';
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
                const form = $('#form-event-add');
                const actionUrl = form.attr('action');
                const dataToSend = form.serialize();
                $.post(actionUrl, dataToSend).done(function (data) {
                    console.log(data);
                    const eventAddAjaxModel = jQuery.parseJSON(data);
                    console.log(eventAddAjaxModel);
                    const newFormBody = $('.modal-body', eventAddAjaxModel.EventAddPartial);
                    placeHolderDiv.find('.modal-body').replaceWith(newFormBody);
                    const isValid = newFormBody.find('[name="IsValid"]').val() === 'True';
                    if (isValid) {
                        placeHolderDiv.find('.modal').modal('hide');
                        const newTableRow = `
                                <tr name="${eventAddAjaxModel.EventDto.Event.Id}">
                                                    <td>${eventAddAjaxModel.EventDto.Event.Id}</td>
                                                    <td>${eventAddAjaxModel.EventDto.Event.Name}</td>
                                                    <td>${eventAddAjaxModel.EventDto.Event.Description}</td>
                                                    <td>${convertFirstLetterToUpperCase(eventAddAjaxModel.EventDto.Event.IsActive.toString())}</td>
                                                    <td>${convertFirstLetterToUpperCase(eventAddAjaxModel.EventDto.Event.IsDeleted.toString())}</td>
                                                    <td>${eventAddAjaxModel.EventDto.Event.Note}</td>
                                                    <td>${convertToShortDate(eventAddAjaxModel.EventDto.Event.CreatedDate)}</td>
                                                    <td>${eventAddAjaxModel.EventDto.Event.CreatedByName}</td>
                                                    <td>${convertToShortDate(eventAddAjaxModel.EventDto.Event.ModifiedDate)}</td>
                                                    <td>${eventAddAjaxModel.EventDto.Event.ModifiedByName}</td>
                                                    <td>
                                                        <button class="btn btn-primary btn-sm btn-update" data-id="${eventAddAjaxModel.EventDto.Event.Id}"><span class="fas fa-edit"></span></button>
                                                        <button class="btn btn-danger btn-sm btn-delete" data-id="${eventAddAjaxModel.EventDto.Event.Id}"><span class="fas fa-minus-circle"></span></button>
                                                    </td>
                                                </tr>`;
                        const newTableRowObject = $(newTableRow);
                        newTableRowObject.hide();
                        $('#categoriesTable').append(newTableRowObject);
                        newTableRowObject.fadeIn(3500);
                        toastr.success(`${eventAddAjaxModel.EventDto.Message}`, 'Başarılı İşlem!');
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
            const eventName = tableRow.find('td:eq(1)').text();
            Swal.fire({
                title: 'Silmek istediğinize emin misiniz?',
                text: `${eventName} adlı kategori silinicektir!`,
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
                        data: { eventId: id },
                        url: '/Admin/Event/Delete/',
                        success: function (data) {
                            const eventDto = jQuery.parseJSON(data);
                            if (eventDto.ResultStatus === 0) {
                                Swal.fire(
                                    'Silindi!',
                                    `${eventDto.Event.Name} adlı kategori başarıyla silinmiştir.`,
                                    'success'
                                );

                                tableRow.fadeOut(3500);
                            } else {
                                Swal.fire({
                                    icon: 'error',
                                    title: 'Başarısız İşlem!',
                                    text: `${eventDto.Message}`,
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
        const url = '/Admin/Event/Update/';
        const placeHolderDiv = $('#modalPlaceHolder');
        $(document).on('click',
            '.btn-update',
            function (event) {
                event.preventDefault();
                const id = $(this).attr('data-id');
                $.get(url, { eventId: id }).done(function (data) {
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

                const form = $('#form-event-update');
                const actionUrl = form.attr('action');
                const dataToSend = form.serialize();
                $.post(actionUrl, dataToSend).done(function (data) {
                    const eventUpdateAjaxModel = jQuery.parseJSON(data);
                    console.log(eventUpdateAjaxModel);
                    const newFormBody = $('.modal-body', eventUpdateAjaxModel.EventUpdatePartial);
                    placeHolderDiv.find('.modal-body').replaceWith(newFormBody);
                    const isValid = newFormBody.find('[name="IsValid"]').val() === 'True';
                    if (isValid) {
                        placeHolderDiv.find('.modal').modal('hide');
                        const newTableRow = `
                                <tr name="${eventUpdateAjaxModel.EventDto.Event.Id}">
                                                    <td>${eventUpdateAjaxModel.EventDto.Event.Id}</td>
                                                    <td>${eventUpdateAjaxModel.EventDto.Event.Name}</td>
                                                    <td>${eventUpdateAjaxModel.EventDto.Event
                                .Description}</td>
                                                    <td>${convertFirstLetterToUpperCase(eventUpdateAjaxModel
                                                        .EventDto.Event.IsActive.toString())}</td>
                                                    <td>${convertFirstLetterToUpperCase(eventUpdateAjaxModel
                                                        .EventDto.Event.IsDeleted.toString())}</td>
                                                    <td>${eventUpdateAjaxModel.EventDto.Event.Note}</td>
                                                    <td>${convertToShortDate(eventUpdateAjaxModel.EventDto
                                                        .Event.CreatedDate)}</td>
                                                    <td>${eventUpdateAjaxModel.EventDto.Event
                                .CreatedByName}</td>
                                                    <td>${convertToShortDate(eventUpdateAjaxModel.EventDto
                                                        .Event.ModifiedDate)}</td>
                                                    <td>${eventUpdateAjaxModel.EventDto.Event
                                .ModifiedByName}</td>
                                                    <td>
                                                        <button class="btn btn-primary btn-sm btn-update" data-id="${eventUpdateAjaxModel.EventDto.Event.Id}"><span class="fas fa-edit"></span></button>
                                                        <button class="btn btn-danger btn-sm btn-delete" data-id="${eventUpdateAjaxModel.EventDto.Event.Id
                            }"><span class="fas fa-minus-circle"></span></button>
                                                    </td>
                                                </tr>`;
                        const newTableRowObject = $(newTableRow);
                        const eventTableRow = $(`[name="${eventUpdateAjaxModel.EventDto.Event.Id}"]`);
                        newTableRowObject.hide();
                        eventTableRow.replaceWith(newTableRowObject);
                        newTableRowObject.fadeIn(3500);
                        toastr.success(`${eventUpdateAjaxModel.EventDto.Message}`, "Başarılı İşlem!");
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