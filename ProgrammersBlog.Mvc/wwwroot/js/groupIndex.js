$(document).ready(function () {

    /* DataTables start here. */

    $('#groupsTable').DataTable({
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
                        url: '/Admin/Group/GetAllGroups/',
                        contentType: "application/json",
                        beforeSend: function () {
                            $('#groupsTable').hide();
                            $('.spinner-border').show();
                        },
                        success: function (data) {
                            const groupListDto = jQuery.parseJSON(data);
                            console.log(groupListDto);
                            if (groupListDto.ResultStatus === 0) {
                                let tableBody = "";
                                $.each(groupListDto.Groups.$values,
                                    function (index, group) {
                                        tableBody += `
                                                <tr>
                                    <td>${group.Id}</td>
                                    <td>${group.Name}</td>
                                    <td>${group.Description}</td>
                                    <td>${convertFirstLetterToUpperCase(group.IsActive.toString())}</td>
                                    <td>${convertFirstLetterToUpperCase(group.IsDeleted.toString())}</td>
                                    <td>${group.Note}</td>
                                    <td>${convertToShortDate(group.CreatedDate)}</td>
                                    <td>${group.CreatedByName}</td>
                                    <td>${convertToShortDate(group.ModifiedDate)}</td>
                                    <td>${group.ModifiedByName}</td>
                                    <td>
                                <button class="btn btn-primary btn-sm btn-update" data-id="${group.Id}"><span class="fas fa-edit"></span></button>
                                <button class="btn btn-danger btn-sm btn-delete" data-id="${group.Id}"><span class="fas fa-minus-circle"></span></button>
                                    </td>
                                            </tr>`;
                                    });
                                $('#groupsTable > tbody').replaceWith(tableBody);
                                $('.spinner-border').hide();
                                $('#groupsTable').fadeIn(1400);
                            } else {
                                toastr.error(`${groupListDto.Message}`, 'İşlem Başarısız!');
                            }
                        },
                        error: function (err) {
                            console.log(err);
                            $('.spinner-border').hide();
                            $('#groupsTable').fadeIn(1000);
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

    /* Ajax GET / Getting the _GroupAddPartial as Modal Form starts from here. */

    $(function () {
        const url = '/Admin/Group/Add/';
        const placeHolderDiv = $('#modalPlaceHolder');
        $('#btnAdd').click(function () {
            $.get(url).done(function (data) {
                placeHolderDiv.html(data);
                placeHolderDiv.find(".modal").modal('show');
            });
        });

        /* Ajax GET / Getting the _GroupAddPartial as Modal Form ends here. */

        /* Ajax POST / Posting the FormData as GroupAddDto starts from here. */

        placeHolderDiv.on('click',
            '#btnSave',
            function (event) {
                event.preventDefault();
                const form = $('#form-group-add');
                const actionUrl = form.attr('action');
                const dataToSend = form.serialize();
                $.post(actionUrl, dataToSend).done(function (data) {
                    console.log(data);
                    const groupAddAjaxModel = jQuery.parseJSON(data);
                    console.log(groupAddAjaxModel);
                    const newFormBody = $('.modal-body', groupAddAjaxModel.GroupAddPartial);
                    placeHolderDiv.find('.modal-body').replaceWith(newFormBody);
                    const isValid = newFormBody.find('[name="IsValid"]').val() === 'True';
                    if (isValid) {
                        placeHolderDiv.find('.modal').modal('hide');
                        const newTableRow = `
                                <tr name="${groupAddAjaxModel.GroupDto.Group.Id}">
                                                    <td>${groupAddAjaxModel.GroupDto.Group.Id}</td>
                                                    <td>${groupAddAjaxModel.GroupDto.Group.Name}</td>
                                                    <td>${groupAddAjaxModel.GroupDto.Group.Description}</td>
                                                    <td>${convertFirstLetterToUpperCase(groupAddAjaxModel.GroupDto.Group.IsActive.toString())}</td>
                                                    <td>${convertFirstLetterToUpperCase(groupAddAjaxModel.GroupDto.Group.IsDeleted.toString())}</td>
                                                    <td>${groupAddAjaxModel.GroupDto.Group.Note}</td>
                                                    <td>${convertToShortDate(groupAddAjaxModel.GroupDto.Group.CreatedDate)}</td>
                                                    <td>${groupAddAjaxModel.GroupDto.Group.CreatedByName}</td>
                                                    <td>${convertToShortDate(groupAddAjaxModel.GroupDto.Group.ModifiedDate)}</td>
                                                    <td>${groupAddAjaxModel.GroupDto.Group.ModifiedByName}</td>
                                                    <td>
                                                        <button class="btn btn-primary btn-sm btn-update" data-id="${groupAddAjaxModel.GroupDto.Group.Id}"><span class="fas fa-edit"></span></button>
                                                        <button class="btn btn-danger btn-sm btn-delete" data-id="${groupAddAjaxModel.GroupDto.Group.Id}"><span class="fas fa-minus-circle"></span></button>
                                                    </td>
                                                </tr>`;
                        const newTableRowObject = $(newTableRow);
                        newTableRowObject.hide();
                        $('#groupsTable').append(newTableRowObject);
                        newTableRowObject.fadeIn(3500);
                        toastr.success(`${groupAddAjaxModel.GroupDto.Message}`, 'Başarılı İşlem!');
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

    /* Ajax POST / Posting the FormData as GroupAddDto ends here. */

    /* Ajax POST / Deleting a Group starts from here */

    $(document).on('click',
        '.btn-delete',
        function (event) {
            event.preventDefault();
            const id = $(this).attr('data-id');
            const tableRow = $(`[name="${id}"]`);
            const groupName = tableRow.find('td:eq(1)').text();
            Swal.fire({
                title: 'Silmek istediğinize emin misiniz?',
                text: `${groupName} adlı kategori silinicektir!`,
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
                        data: { groupId: id },
                        url: '/Admin/Group/Delete/',
                        success: function (data) {
                            const groupDto = jQuery.parseJSON(data);
                            if (groupDto.ResultStatus === 0) {
                                Swal.fire(
                                    'Silindi!',
                                    `${groupDto.Group.Name} adlı grup başarıyla silinmiştir.`,
                                    'success'
                                );

                                tableRow.fadeOut(3500);
                            } else {
                                Swal.fire({
                                    icon: 'error',
                                    title: 'Başarısız İşlem!',
                                    text: `${groupDto.Message}`,
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

    /* Ajax GET / Getting the _GroupUpdatePartial as Modal Form starts from here. */

    $(function () {
        const url = '/Admin/Group/Update/';
        const placeHolderDiv = $('#modalPlaceHolder');
        $(document).on('click',
            '.btn-update',
            function (event) {
                event.preventDefault();
                const id = $(this).attr('data-id');
                $.get(url, { groupId: id }).done(function (data) {
                    placeHolderDiv.html(data);
                    placeHolderDiv.find('.modal').modal('show');
                }).fail(function () {
                    toastr.error("Bir hata oluştu.");
                });
            });

        /* Ajax POST / Updating a Group starts from here */

        placeHolderDiv.on('click',
            '#btnUpdate',
            function (event) {
                event.preventDefault();

                const form = $('#form-group-update');
                const actionUrl = form.attr('action');
                const dataToSend = form.serialize();
                $.post(actionUrl, dataToSend).done(function (data) {
                    const groupUpdateAjaxModel = jQuery.parseJSON(data);
                    console.log(groupUpdateAjaxModel);
                    const newFormBody = $('.modal-body', groupUpdateAjaxModel.GroupUpdatePartial);
                    placeHolderDiv.find('.modal-body').replaceWith(newFormBody);
                    const isValid = newFormBody.find('[name="IsValid"]').val() === 'True';
                    if (isValid) {
                        placeHolderDiv.find('.modal').modal('hide');
                        const newTableRow = `
                                <tr name="${groupUpdateAjaxModel.GroupDto.Group.Id}">
                                                    <td>${groupUpdateAjaxModel.GroupDto.Group.Id}</td>
                                                    <td>${groupUpdateAjaxModel.GroupDto.Group.Name}</td>
                                                    <td>${groupUpdateAjaxModel.GroupDto.Group
                                .Description}</td>
                                                    <td>${convertFirstLetterToUpperCase(groupUpdateAjaxModel
                                                        .GroupDto.Group.IsActive.toString())}</td>
                                                    <td>${convertFirstLetterToUpperCase(groupUpdateAjaxModel
                                                        .GroupDto.Group.IsDeleted.toString())}</td>
                                                    <td>${groupUpdateAjaxModel.GroupDto.Group.Note}</td>
                                                    <td>${convertToShortDate(groupUpdateAjaxModel.GroupDto
                                                        .Group.CreatedDate)}</td>
                                                    <td>${groupUpdateAjaxModel.GroupDto.Group
                                .CreatedByName}</td>
                                                    <td>${convertToShortDate(groupUpdateAjaxModel.GroupDto
                                                        .Group.ModifiedDate)}</td>
                                                    <td>${groupUpdateAjaxModel.GroupDto.Group
                                .ModifiedByName}</td>
                                                    <td>
                                                        <button class="btn btn-primary btn-sm btn-update" data-id="${groupUpdateAjaxModel.GroupDto.Group.Id}"><span class="fas fa-edit"></span></button>
                                                        <button class="btn btn-danger btn-sm btn-delete" data-id="${groupUpdateAjaxModel.GroupDto.Group.Id
                            }"><span class="fas fa-minus-circle"></span></button>
                                                    </td>
                                                </tr>`;
                        const newTableRowObject = $(newTableRow);
                        const groupTableRow = $(`[name="${groupUpdateAjaxModel.GroupDto.Group.Id}"]`);
                        newTableRowObject.hide();
                        groupTableRow.replaceWith(newTableRowObject);
                        newTableRowObject.fadeIn(3500);
                        toastr.success(`${groupUpdateAjaxModel.GroupDto.Message}`, "Başarılı İşlem!");
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