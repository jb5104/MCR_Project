var SemanticUI = {
    ComboBox : {
        SetVal : function(objName, val)
        {
            if (String(objName).indexOf("#") < 0) objName = "#" + objName;

            $(objName).dropdown('set selected', val);
          //  $(objName).val(val);
        },
        GetVal: function (objName) {
            if (String(objName).indexOf("#") < 0) objName = "#" + objName;

            return $(objName).dropdown('get value')[0];
            //  $(objName).val(val);
        }, SetWidth: function (objName, nwidth) {
            $(objName).parent().css("width",nwidth);
        }
    },CheckBox :
    {
        GetVal: function(id)
        {
            if ($("#" + id).parent().hasClass("checked")) {
                return $("#" + id).val();
            }
            else {
                if ($("#" + id).val() == "1") return "0";
                else return null;
            };
        }
    }
}

$(document).ready(function () {
    $("select").dropdown();
    $('.ui.checkbox').checkbox();
    $("input:checkbox").removeClass("ui");
    $("input:checkbox").removeClass("checkbox");
    $("input:checkbox").addClass("ui");
    $("input:checkbox").addClass("checkbox");
    $("input:checkbox").checkbox();
    $('.right.menu.open').on("click",function(e){
        e.preventDefault();
        $('.ui.vertical.menu').toggle();
    });
    
    $('.ui.dropdown').dropdown();
});

