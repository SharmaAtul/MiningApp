function setNewEquipmentRow(data) {
    var temprow = $("tfoot #tempRowID").clone();
    temprow = temprow.removeClass("tempRow");
    temprow.attr("equipment-id", "equipment_"+data.id);
    $("tbody").append(temprow);
}

$(function () {
    $("td a.add-row").on('click', function ()
    {
        $.ajax({
            url: '/Equipment/Add',
            type: 'post',
            dataType: 'json',
            contentType: 'application/json',
            success: function (data) {
                setNewEquipmentRow(data);
            },
            data: {}
        });
    });

    $(document).on("change", "input.equipment", function () {
        var equipmentId = $(this).parents("tr").attr("equipment-id").replace("equipment_", "");
        var value = $(this).val();
        var _data = { id: equipmentId, name: value };
        $.ajax({
            url: '/Equipment/Save',
            type: 'POST',
            data: _data,
            success: function (data) {
                $("tr[equipment-id='equipment_" + equipmentId+"'] input.equipment").val(data.name);
            }
        });
    });

    $(document).on("click", ".menu .remove", function () {
        var currEquipment = $(this).parents("tr");
        var id = $(this).parents("tr").attr("equipment-id").replace("equipment_", "");
        if (window.confirm("Are you sure to remove Equipment?")) {
            $.ajax({
                url: '/Equipment/Remove',
                type: 'post',
                success: function (data) {
                    if (data.status) {
                        $(currEquipment).remove();

                        $(".total-usage").each(function () {
                            $(this).text(0);
                        });

                        for (var i = 0; i <= data.departmentUsage.length - 1; i++) {
                            var deptId = data.departmentUsage[i].departmentId;
                            var usage = data.departmentUsage[i].value;

                            setDepartmentData(deptId, usage);
                        }
                    }
                },
                data: { id: id }
            });
        }
    })

    $(document).on("change", "input.usage", function ()
    {
        var equipmentId = $(this).parents("tr").attr("equipment-id").replace("equipment_","");
        var departmentId = $(this).parents("td").attr("dept-id").replace("department__", "");
        var value = $(this).val();

        $.ajax({
            url: '/Usage/Save',
            type: 'post',
            success: function (data) {
                setDepartmentData(data.departmentId, data.usage);
            },
            data: { eqipmentId: equipmentId, departmentId:departmentId,value:value}
        });
    });
});

function setDepartmentData(deptId, usage) {
    var maxUsage = parseInt($("#department_max_" + deptId).text());
    $("#department_total_" + deptId).text(usage);

    if (usage > maxUsage) {
        $("#department_max_" + deptId).parents("td").css("color", "red");
    } else
        $("#department_max_" + deptId).parents("td").css("color", "black");
}