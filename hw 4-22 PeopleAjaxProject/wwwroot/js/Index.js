$(() => {

    $("#add-person").on('click', function () {
        $.post('/home/addperson', GetAndResetAddTextboxes(), function () {
            loadPeople();
        });
    });

    function loadPeople() {
        $.get('/home/getpeople', function (ppl) {
            $("#person-table tbody td").remove();
            ppl.forEach(p => {
                $("#person-table tbody").append(
                    `<tr><td>${p.firstName}</td><td>${p.lastName}</td><td>${p.age}</td>
            <td><button class="btn btn-success edit" data-id="${p.id}" data-firstname="${p.firstName}" data-lastname="${p.lastName}" data-age="${p.age}">Edit</button></td>
            <td><button class="btn btn-danger delete" data-id="${p.id}">Delete</button></td>
            </tr>`)
            });
        });
    }

    function GetAndResetAddTextboxes() {
        let firstName = $("#first-name").val();
        let lastName = $("#last-name").val();
        let age = $("#age").val();
        let person = { firstName, lastName, age };
        $("#first-name").val('');
        $("#last-name").val('');
        $("#age").val('');
        return person;
    }

    $('body').on('click', '.edit', function () {
        $("#edit-first-name").val(`${$(this).data('firstname')}`);
        $("#edit-last-name").val(`${$(this).data('lastname')}`);
        $("#edit-age").val(`${$(this).data('age')}`);
        $("#edit-person-id").val(`${$(this).data('id')}`);
        $("#edit-modal").modal();
    });

    $('body').on('click', '#save-edit', function () {
        $.post('/home/editperson', GetPersonFromModal(), function () {
            loadPeople();
            $("#edit-modal").modal('hide');
        });
    });

    function GetPersonFromModal() {
        let id = $("#edit-person-id").val();
        let firstName = $("#edit-first-name").val();
        let lastName = $("#edit-last-name").val();
        let age = $("#edit-age").val();
        return { id, firstName, lastName, age };        
    }

    $('body').on('click', '.delete', function () {
        let id = $(this).data('id');
        $.post(`/home/deleteperson`, { id }, function () {
            loadPeople();
        });
    });

    loadPeople();
});