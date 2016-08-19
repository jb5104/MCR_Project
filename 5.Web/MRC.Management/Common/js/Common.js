/*************/
/* Ajax 관련 */
/*************/
var ajax =
{    /* dateType : html/ */
    GetAjax: function (url, params, dateType, callback) {

        if (dateType == undefined || $.trim(dateType) == "") {
            dateType = "json";
        }
        else
            dateType = dateType;
        $("#dvLoading").hide();
        $("#dvLoading").show();
        $.ajax({
            type: "POST",
            url: url,
            timeout: 30000,
            data: params,
            cache: false,
            dataType: dateType,
            contentType: 'application/json; charset=utf-8',
            success: function (result) {
                callback(result);
                $("#dvLoading").hide();
            },
            error: function (e) {
                //console.log(e);
                $("#dvLoading").hide();
                //bootbox.alert('Error:' + e.status + " - " + e.statusText);
                try {
                    $.MessageBox('Error:' + e.status + " - " + e.statusText);
                } catch (e) {}
            },
        });
    }
}



var Input = {
    inputForm: {
        makeForm: function (ActionURL) {
            var frm = document.createElement("form");
            frm.setAttribute("method", "post");
            if (!(ActionURL == undefined || ActionURL == "")) {
                frm.setAttribute("action", ActionURL);
            }
            document.body.appendChild(frm);
            return frm;
        }
        , AddData: function (name, value, frm) {
            var i = document.createElement("input");
            i.setAttribute("type", "hidden");
            i.setAttribute("name", name);
            i.setAttribute("value", value);
            frm.appendChild(i);
        }
    },
    /***************/
    /* 라디오 버튼 */
    /***************/
    RadioButton:
    {
        CheckedVal: function (inputName) {

            var rtnVal = "";
            /*$("input[name='" + inputName + "']:checked").each(function () {
                var obj = new Object();
                rtnVal = $(this).val();
            });*/
            rtnVal = ($(':radio[name="' + inputName + '"]:checked').val() == undefined) ? "" : $(':radio[name="' + inputName + '"]:checked').val();
            return rtnVal;

        }
         , SetCheck: function (inputName, bChk, val1) {
             if (val1 == undefined)
                 $('input:radio[name=' + inputName + ']').prop("checked", bChk);
             else {
                 //$('input[name="' + inputName + '"][value="' + val1 + '"]').prop('checked', bChk);
                 $('input:radio[name=' + inputName + ']:input[value="' + val1 + '"]').prop("checked", bChk);
             }
             /*if (bChk) bChk = "checked";
             else bChk = "";
             if (val1 == undefined) val1 = "";
             $("input[name=" + inputName + "]").filter("input[value=" + val1 + "]").attr("checked", "checked");*/
         },
        isCheck: function (inputName) {
        }
    }
    /*******************/
    /* ComboBox-Select */
    /*******************/
    , ComboBox:
    {
        SetValue: function (objId, val) { //Loop through sequentially//
            $("#" + objId + " option").each(function () {
                if ($(this).val() == val) {
                    $(this).attr('selected', 'selected');
                }
            });

        },
        SetText: function (objId, text) {
            // $("#" + objId + " option[text='" + text + "']").attr('selected', 'selected');
            //$("#" + objId).val(text).attr("selected", "selected");

            $("#" + objId + " option").each(function () {
                if ($(this).text() == text) {
                    $(this).attr('selected', 'selected');
                }
            });
        }
        , GetText: function (objId) {
            return $("#" + objId + " option:selected").text();
        }
    }

    /***************/
    /* 체크박스 */
    /***************/
    , CheckBox:
    {
        isChecked: function (objId) {
            return $("input:checkbox[id='" + objId + "']").is(":checked");
        }, Checked: function (inputName, bChk) {
            $("input:checkbox[id='" + inputName + "']").attr('checked', bChk);
        }, CheckedAll: function (inputName, bChk) {
            try {
                if (bChk) {
                    $("input[name='" + inputName + "']:(not)checked").each(function () {
                        $(this).attr("checked", bChk);;
                    });
                }
                else {
                    $("input[name='" + inputName + "']:checked").each(function () {
                        $(this).attr("checked", bChk);;
                    });
                }
            } catch (e) { }
        }, CheckedVal: function (objId) {
            if (objId.indexOf("#") < 0) objId = "#" + objId;
            if ($(objId + ":checked").val() == undefined) {
                return null;
            }
            return $(objId + ":checked").val();
        }, CheckedValues: function (inputName) {
            var checked = []
            $("input[name='" + inputName + "']:checked").each(function () {
                var obj = new Object()
                obj.Value = $(this).val();
                checked.push(obj);
            });
            return checked;
        }, CheckedStringValues: function (inputName) {
            var rtnValue = "";
            $("input[name='" + inputName + "']:checked").each(function () {
                rtnValue = rtnValue + $(this).val() + "|";
            });
            rtnValue = rtnValue.substr(0, rtnValue.length - 1);
            return rtnValue;
        }
    }
    /***************/
    /*Text박스 */
    /***************/
    , TextBox:
    {
        notHangul: function (id) {
            $("#" + id).keyup(function (event) {
                if (window.event) { // IE, Chrome, Safari
                    keynum = event.keyCode;
                }
                if (event.which) {    // Firefox, Opera, Netscape, safari
                    keynum = event.which;
                }
                if (!(keynum >= 37 && keynum <= 40)) {
                    var inputVal = $(this).val();
                    $(this).val(inputVal.replace(/[^a-z0-9]/gi, ''));
                }
            });
        }
        , onlyNumber: function (id) {
            $("#" + id).keypress(function (event) {
                if (window.event) { // IE, Chrome, Safari
                    keynum = event.keyCode;
                }
                if (event.which) {    // Firefox, Opera, Netscape, safari
                    keynum = event.which;
                }
                if ((keynum < 48) || (keynum > 57)) {
                    event.returnValue = false;
                    return false;
                }
            });
            if ((keynum < 48) || (keynum > 57))
                event.returnValue = false;
        }, blurNumber: function (obj) {
            if (isNaN($(obj).val())) {
                $(obj).val("0");
            }
        }
        , commaNum: function (num, digits) {
            if (isNaN(num)) {
                num = "0";
            }
            else {
                if (digits != undefined && digits > 0) {
                    num = eval(num, "{0:" + digits + 1 + "}");
                }
                else {

                    num = Math.floor(num);
                }
            }
            var parts = num.toString().split(".");
            num = parts[0].replace(/\B(?=(\d{3})+(?!\d))/g, ",") + (parts[1] ? "." + parts[1] : "")

            return num;
        }
        , Round: function (val, digits) {
            return Math.round(val * Math.pow(10, digits)) / Math.pow(10, digits);
        }

        ,
        NumberBox:
        {
            increaseNum: 1
            , minNumber: 1
            , maxNumber: 100

            , getVal: function (id) {
                return BaseCommon.StringInfo.replaceAll($("#" + id).val(), ',', '');
            },
            setVal: function (objName, num, digits) {
                
                if (isNaN(num)) {
                    num = 0;
                    $(objName).val("0");
                }
                else {
                    if (num <= Input.TextBox.NumberBox.maxNumber
                        && num >= Input.TextBox.NumberBox.minNumber
                        ) $(objName).val(Input.TextBox.commaNum(num, digits));
                }

            }
            ,
            Init: function (id, digits) {

                $("#" + id + "_UP").click(function () {
                    Input.TextBox.NumberBox.setVal("#" + id, String(Number(Input.TextBox.NumberBox.getVal(id)) + Input.TextBox.NumberBox.increaseNum));
                });
                $("#" + id + "_DOWN").click(function () {
                    Input.TextBox.NumberBox.setVal("#" + id, String(Number(Input.TextBox.NumberBox.getVal(id)) - Input.TextBox.NumberBox.increaseNum));
                });
                $("#" + id).keypress(function (event) {
                    if (((event.keyCode < 48) || (event.keyCode > 57)))
                        event.preventDefault();
                });
                $("#" + id).blur(function (event) {
                    if (isNaN($("#" + id).val())) {
                        $("#" + id).val("0");
                    }
                    else {
                        var num = $("#" + id).val();

                        if (num > Input.TextBox.NumberBox.maxNumber) $("#" + id).val(String(Input.TextBox.NumberBox.maxNumber));
                        else if (num < Input.TextBox.NumberBox.minNumber) $("#" + id).val(String(Input.TextBox.NumberBox.minNumber));

                    }
                });

                $("#" + id).focus(function (event) {
                    $("#" + id).val(BaseCommon.StringInfo.replaceAll($("#" + id).val(), ',', ''));
                    $("#" + id).select();
                });
            }
        }

    }

}



var BaseCommon =
{
    HostInfo: {
        Host: function () {
            return $(location).attr("host");
        }
        , Pathname: function () {
            return $(location).attr("pathname");
        }
        , Srarch: function () {
            return $(location).attr("search");
        }

    },
    getBackOfficeURL: function (accountGubun)
    {
            var sActionUrl = "";
            if (BaseCommon.HostInfo.Host().indexOf("localhost") >= 0) {
                sActionUrl = "http://192.168.15.38:8001";
            } else if (BaseCommon.HostInfo.Host().indexOf("192.168.15.38") >= 0) {
                sActionUrl = "http://192.168.15.38:8001";
            }
            else if (BaseCommon.HostInfo.Host().indexOf("license.upsolution.com") >= 0) {
                if (accountGubun != undefined && accountGubun == "1") {
                    sActionUrl = "http://revonu.upsolutioncloud.com";
                }
                else
                    sActionUrl = "http://uprwb.upsolutioncloud.com";
            }
            else if (BaseCommon.HostInfo.Host().indexOf("licenseq.upsolution.com:7777") >= 0) {

                sActionUrl = "http://uprwb.upsolutioncloud.com";
            }
            else if (BaseCommon.HostInfo.Host().indexOf("licenseq.upsolution.com") >= 0 || BaseCommon.HostInfo.Host().indexOf("delivery.upordering.com:8080") >= 0) {
                sActionUrl = "http://uprwbq.upsolutioncloud.com";
            }
            else if (BaseCommon.HostInfo.Host().indexOf("licenseg.upsolutioncloud.com") >= 0) {
                sActionUrl = "http://uprwbg.upsolutioncloud.com";
            }
       
            return sActionUrl;
        },
    
        backOfficeLogin: function (StoreId, MerchantId, resellerCode, resellerName ,accountGubun)
        {
            
            
            if (MerchantId == undefined) MerchantId = "";
            var sActionUrl = BaseCommon.getBackOfficeURL(accountGubun);

            var frm = Input.inputForm.makeForm(sActionUrl + "/Account/SSO_Login");
            if (resellerCode == undefined || resellerCode == null) resellerID = 0;

            if (resellerName == undefined || resellerName == null) resellerName = "";

            Input.inputForm.AddData("CompanyCode", StoreId, frm);
            Input.inputForm.AddData("merchantID", MerchantId, frm);
            Input.inputForm.AddData("ResellerCode", resellerCode, frm);
            Input.inputForm.AddData("ResellerName", resellerName, frm);
        
            window.open('', 'pop_Logintarget', 'width=' + screen.width + ',  height=' + screen.height + ',top=0, left=0,fullscreen=yes, resizable=1 ,scrollbars=yes');
            frm.target = 'pop_Logintarget';
            frm.submit();
        },
    /****************/
    /*   서버정보   */
    /****************/
    ServerInfo:
    {
        Host: function () {
            var Dns;
            Dns = location.href;
            Dns = Dns.split("//");
            Dns = Dns[1].substr(0, Dns[1].indexOf("/"));
            return Dns;
        },
        getBrowser: function () {
            var agt = navigator.userAgent.toLowerCase();
            if (agt.indexOf("opera") != -1) return 'Opera';
            if (agt.indexOf("staroffice") != -1) return 'Star Office';
            if (agt.indexOf("webtv") != -1) return 'WebTV';
            if (agt.indexOf("beonex") != -1) return 'Beonex';
            if (agt.indexOf("chimera") != -1) return 'Chimera';
            if (agt.indexOf("netpositive") != -1) return 'NetPositive';
            if (agt.indexOf("phoenix") != -1) return 'Phoenix';
            if (agt.indexOf("firefox") != -1) return 'Firefox';
            if (agt.indexOf("safari") != -1) return 'Safari';
            if (agt.indexOf("skipstone") != -1) return 'SkipStone';
            if (agt.indexOf("msie") != -1) return 'msie';
            if (agt.indexOf("netscape") != -1) return 'Netscape';
            if (agt.indexOf("mozilla/5.0") != -1) return 'Mozilla';
            if (agt.indexOf('\/') != -1) {
                if (agt.substr(0, agt.indexOf('\/')) != 'mozilla') {
                    return navigator.userAgent.substr(0, agt.indexOf('\/'));
                } else
                    return 'Netscape';
            }
            else if (agt.indexOf(' ') != -1) return navigator.userAgent.substr(0, agt.indexOf(' '));
            else return navigator.userAgent;
        }, getLanguage: function () {
            var type1 = navigator.appName;
            var lang = "";

            if (type1 == "Netscape")
                lang = navigator.language.split('-')[0];
            else
                lang = navigator.userLanguage;
            return lang;

        }
    }
    , FormInfo:
    {
        InputsToJson: function (id) {
            var data = $("#" + id).serializeArray();

            return JSON.stringify(BaseCommon.FormInfo.arrayToJSON(data));
        }
        , arrayToJSON: function (serializedForm) {
            var data = {};
            for (var cnt in serializedForm) {
                if (data[serializedForm[cnt].name] == undefined)
                    data[serializedForm[cnt].name] = serializedForm[cnt].value;
            }
            return data;
        }
        //////////////////////
        // 동적 Form Submit
        // 예제 : var arrData = new Array('GROUP_CODE' + '|' + groupCode, 'ITEM_NAME' + '|' + String(itemName));
        //       BaseCommon.FormInfo.PostSubmit('/@SessionHelper.LoginInfo.LANGUAGE/order/index', arrData);
        //////////////////////
        , PostSubmit: function (url, arrData, target) {

            var $form = $('<form></form>');
            $form.attr('action', url);
            $form.attr('method', 'post');
            if (target != undefined) {
                $form.attr('target', target);
            }
            $form.appendTo('body');

            for (var i = 0; i < arrData.length; i++) {
                var pData = arrData[i].split('|');
                $form.append('<input type="hidden" value="' + pData[1] + '" name="' + pData[0] + '">')
            }
            $form.submit();
        }
    }

    /****************/
    /*   문자정보   */
    /****************/
    , StringInfo:  // String 관련 함수 정의
    {
        textAreaLimit: function (textid, limit, limitid) {
            $('#' + textid).keyup(function () {
                if (limitid == undefined) limitid = "sp_" + textid;
                BaseCommon.StringInfo.limitCharacters(textid, limit, limitid);
            })
        },
        // textarea id, 제한 글자 수, 입력 결과 메세지 저장 ID
        limitCharacters: function (textid, limit, limitid) { //(textBox Id, 제한글자수, 현자글자 체크 span Id) 
            // 잆력 값 저장
            var text = $('#' + textid).val();
            // 입력값 길이 저장
            var textlength = text.length;
            if (textlength > limit) {
                /*$('#' + limitid).html('글내용을 '+limit+
                '자 이상 쓸수 없습니다!');*/
                // 제한 글자 길이만큼 값 재 저장
                $('#' + limitid).html(String(limit));
                $('#' + textid).html(text.substr(0, limit));
                $('#' + textid).val(text.substr(0, limit));
                //return String(limit);
                return false;
            }
            else {
                /*$('#' + limitid).html('쓸수 있는 글자가 '+ (limit - textlength) +
                ' 자 남았습니다.');*/
                $('#' + limitid).html(String(limit));
                //return Sting(textlength);
                $('#' + limitid).html(String(textlength));
                return true;
            }
            val = BaseCommon.StringInfo.replaceAll(val, '-', '');
        }, smsByteChk: function (inputName, spanId) {
            $('#' + textid).keyup(function () {
                if (limitid == undefined) limitid = "sp_" + textid;
                BaseCommon.StringInfo.smsByteChk(textid, limit, limitid);
            })
        }
        , smsByteChk: function (inputName, spanId) {
            var totalByte = 0;
            var message = $("#" + inputName).val();
            if (spanId == undefined)
                spanId = "sp" + inputName;
            for (var i = 0; i < message.length; i++) {
                var currentByte = message.charCodeAt(i);
                if (currentByte > 128) totalByte += 2;
                else totalByte++;
            }
            // frm.messagebyte.value = totalByte;
            $("#sp").html(totalByte + " byte Sms로 전송");
            if (totalByte > limitByte) {
                $("#" + spanId).html(totalByte + " byte lms로 전송");
            }
        }
        , replaceAll: function (inputString, targetString, replacement) { //문자열 바꾸기
            var v_ret = null;
            var v_regExp = new RegExp(targetString, "g");
            v_ret = inputString.replace(v_regExp, replacement);
            return v_ret;
        }, GetCommaValue: function (n) {      //천단위 숫자변환
            var reg = /(^[+-]?\d+)(\d{3})/;   // 정규식
            n += '';                          // 숫자를 문자열로 변환

            while (reg.test(n))
                n = n.replace(reg, '$1' + ',' + '$2');

            return n;
        }, sqlInjection: function (val1) {
            val1 = BaseCommon.StringInfo.replaceAll(val1, "'", "''");
            //val1 = BaseCommon.StringInfo.replaceAll(val1, "'", "\"");
            return val1;
        }
    }
    /****************/
    /*   객체정보   */
    /****************/
    , OjbectInfo:
    {

        // 오브젝트 카피
        CopyObject: function (obj) {
            return JSON.parse(JSON.stringify(obj));
        }
        , union_arrays: function (x, y) {//Json Union
            var res;
            if (x.length == 0) return y;
            else if (y.length == 0) return x;
            res = x;
            for (var i = 0; i < y.length; i++) {
                res.push(y[i]);
            }

            return res;
        }
    }

    /****************/
    /*   날짜정보   */
    /****************/
    , DateInfo:
      {
          RemoveDateFormat: function (val) {
              val = BaseCommon.StringInfo.replaceAll(val, '-', '');
              val = BaseCommon.StringInfo.replaceAll(val, '/', '');
              val = val.replace('.', '').replace('.', '');
              return val;
          },
          ConvertDate: function (val) {

              if (val.length == 6) {
                  val = val + "01";
              }

              if (val.length < 9) {
                  var YYYY = val.substring(0, 4);
                  var mm = val.substring(4, 6);
                  var dd = val.substring(6, 8);
                  var val = YYYY + "-" + mm + "-" + dd + " 00:00:00";
              }

              var sDate = val.split(' ')[0];
              var sHms = val.split(' ')[1]

              return new Date(sDate.split('-')[0], Number(sDate.split('-')[1]) - 1, sDate.split('-')[2], sHms.split(':')[0], sHms.split(':')[1], sHms.split(':')[2]);
          },
          NowDate: function (delimiter) {
              var date = new Date();
              return BaseCommon.DateInfo.SetFormatDate(date, delimiter);
          },
          SetFormatDate: function (date, delimiter) {

              if (delimiter == undefined)
                  delimiter = "/";

              date = new Date(date);
              var year, month, day;
              year = date.getFullYear();
              month = (String(date.getMonth() + 1).length == 1) ? "0" + String(date.getMonth() + 1) : String(date.getMonth() + 1);
              day = String(date.getDate()).length == "1" ? "0" + String(date.getDate()) : String(date.getDate());

              switch (delimiter) {
                  case "/":
                      return month + "/" + day + "/" + year;
                      break;
                  case "-":
                      return year + "-" + month + "-" + day;
                      break;
                  case "":
                      return year + month + day;
                      break;
                  case "|":
                      return year + month + day;
                      break;
                  case "d":
                  default:
                      return year + "/" + month + "/" + day;
                      break;

              }

          },
          /////////////////////////////////////////
          // ex) BaseCommon.DateInfo.dateAdd(구분자, 시간, add 값)
          //     BaseCommon.DateInfo.dateAdd('minute', d, 30); 
          /////////////////////////////////////////
          dateAdd: function (interval, date, units) {

              var ret = new Date(date); //don't change original date
              switch (interval.toLowerCase()) {
                  case 'year': ret.setFullYear(ret.getFullYear() + units); break;
                  case 'quarter': ret.setMonth(ret.getMonth() + 3 * units); break;
                  case 'month': ret.setMonth(ret.getMonth() + units); break;
                  case 'week': ret.setDate(ret.getDate() + 7 * units); break;
                  case 'day': ret.setDate(ret.getDate() + units); break;
                  case 'hour': ret.setTime(ret.getTime() + units * 3600000); break;
                  case 'minute': ret.setTime(ret.getTime() + units * 60000); break;
                  case 'second': ret.setTime(ret.getTime() + units * 1000); break;
                  default: ret = undefined; break;
              }
              return ret;
          },
          dateSubtract: function (interval, date, units) {

              var ret = new Date(date); //don't change original date
              switch (interval.toLowerCase()) {
                  case 'hour': ret.setTime(ret.getTime() - units * 3600000); break;
                  case 'minute': ret.setTime(ret.getTime() - units * 60000); break;
                  case 'second': ret.setTime(ret.getTime() - units * 1000); break;
                  default: ret = undefined; break;
              }
              return ret;
          },
          SetDayText: function (day) {
              var ret;

              switch (day) {
                  case 0:
                      return ret = "Sunday"
                      break;
                  case 1:
                      return ret = "Monday"
                      break;
                  case 2:
                      return ret = "Tuesday"
                      break;
                  case 3:
                      return ret = "Wednesday"
                      break;
                  case 4:
                      return ret = "Thursday"
                      break;
                  case 5:
                      return ret = "Friday"
                      break;
                  case 6:
                      return ret = "Saturday"
                      break;
              }
              return ret
          }


      }

    //쿼리스트링 가져오기
    , GetQueryString: function (name) {
        name = name.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
        var regexS = "[\\?&]" + name + "=([^&#]*)";
        var regex = new RegExp(regexS);
        var results = regex.exec(window.location.href);
        if (results == null)
            return "";
        else
            return decodeURIComponent(results[1].replace(/\+/g, " "));
    }

   , validation:
       {
           //이메일 형식 체크
           emailValidate: function (name) {
               var filter = /^[0-9a-zA-Z]([-_\.]?[0-9a-zA-Z])*@[0-9a-zA-Z]([-_\.]?[0-9a-zA-Z])*\.[a-zA-Z]{2,3}$/i;

               if (filter.test($("#" + name).val()))
                   return true;
               else
                   return false;
           }

           //전화번호 포멧
           , PhoneValidate: function (phone) {
               phoneRegExp = /^\d{2,3}-?\d{3,4}-?\d{4}$/;
               if (phoneRegExp.test($("#" + phone).val()))
                   return true;
               else
                   return false;
           }
            , MobilePhoneValidate: function (name, lang) {
                var regExp = ""
                if (String(lang).toLowerCase() == "ko") {
                    regExp = /^01([0|1|6|7|8|9]?)-?([0-9]{3,4})-?([0-9]{4})$/;
                }
                else if (lang == undefined)
                    regExp = /^([\d]{1})-?([\d]{3})-?([\d]{3})-?([\d]{4})$/;


                if (!regExp.test($("#" + name).val())) {
                    //  alert("잘못된 휴대폰 번호입니다. 숫자, - 를 포함한 숫자만 입력하세요.");
                    return false
                }
                else {
                    return true;
                }
            }

           //이메일 형식 체크 $(document).ready(function () {
           , ready_emailValidate: function (emailheader, emailfooter) {
               $(emailfooter).focusout(function () {
                   var str = $(emailheader).val() + '@' + $(emailfooter).val();
                   if (str != '' && !BaseCommon.validation.emailValidate(str)) {
                       alert('이메일 형식이 잘못되었습니다.');
                       $(emailheader).val('')
                       $(emailfooter).val('')
                       $(emailheader).focus();
                       return false;
                   }
                   return true;
               });
           }

           //아이디 형식
           , IsIDValidation: function (obj) {
               //$(obj).focusout(function () {
               var str = $(obj).val();

               var reg1 = /^[a-z]+[a-z0-9,.,_,-]{3,11}$/g;;

               if (str != '') {
                   if (!reg1.test(str)) {
                       alert("영문 소문자로 시작하는 4~12자의 영문/숫자로 입력해 주세요.\n아이디는 영문과 숫자 .(점) _(언더바) -(대시) 로만 구성 할 수 있습니다.");
                       $(obj).focus();
                   }
                   else
                       return true;
               }
               else {
                   alert("아이디를 입력해 주시기 바랍니다.");
                   $(obj).focus();
               }

               return false;
               // });
           }

           //패스워드 형식
           , IsPasswordValidation: function (str) {
               var reg1 = /[!,@,#,$,%,^,&,*,?,_,~]/g;
               var reg2 = /[a-zA-Z]/g;
               var reg3 = /[0-9]/g;

               if (str.length == 0) {
                   alert("비밀번호를 입력해 주시기 바랍니다.");
                   return false;
               }
               else if (!(str.length >= 8 && str.length <= 20)) {
                   alert("8~20자리의 영문, 특수문자 및 숫자 조합으로 입력해 주세요.");
                   return false;
               } else if (!(reg1.test(str) && reg2.test(str) && reg3.test(str))) {
                   alert("영문, 특수문자 및 숫자 조합으로 입력해 주세요.");
                   return false;
               }

               return true;
           }

           //패스워드 비교&형식 $(document).ready(function () {
           , ready_passwordValidate: function (obj1, obj2) {
               $(obj1).focusout(function () {
                   if ($(obj1).val() != "") {
                       var str = $(obj1).val();
                       if (!BaseCommon.validation.IsPasswordValidation(str)) {
                           $(obj1).focus();
                           $(obj1).select();
                       }
                   }
               });

               $(obj2).focusout(function () {
                   if ($(obj2).val() != "" && $(obj2).val() != $(obj1).val()) {
                       alert("패스워드가 일치하지 않습니다.");
                       $(obj2).focus();
                   } else if ($(obj2).val() != "") {
                   }
               });
           }

       }
    , NumberInfo:
     {
         ConvertString: function (val, digits) {
             var arrd = String(val).split('.');
             if (arrd.length == 1) {
                 val = String(val) + ".";
                 for (var i = 0; i < digits; i++) {
                     val = val + "0";
                 }

             }
             else if (arrd.length == 2) {
                 for (var i = 0; i < digits - arrd[1].length; i++) {
                     val = String(val) + "0";
                 }
             }
             return val;
         },
         PerchantCheck: function (obj) {
             var _pattern = /^(\d{1,3}([.]\d{0,2})?)?$/;
             var _value = event.srcElement.value;
             if (!_pattern.test(_value)) {
                 event.srcElement.value = event.srcElement.value.substring(0, event.srcElement.value.length - 1);
                 event.srcElement.focus();
             }

             if ($(obj).attr("max") != undefined) {
                 if (Number($(obj).val()) > Number($(obj).attr("max"))) {
                     $(obj).val($(obj).attr("max"));
                 }

                 if (Number($(obj).val()) < Number($(obj).attr("min"))) {
                     $(obj).val($(obj).attr("min"));
                 }
             }
             
             if($(obj).val() =="e")
             {
                 $(obj).val("0");
             }
         }
     }
}




//////////////////////
///  파일 업로드   ///
//////////////////////
var FileUpload = {
    tableName: ""
    , Idx: ""
    , Init: function (tableName, Idx, folder) { /* folder/gallery */
        $('#fileupload')
        .fileupload(
            'option',
            {
                maxFileSize: 4000000,
                autoUpload: true
            }
        ).bind('fileuploadstop', function (e) { try { flieUploadAfter(); } catch (e) { } });;

        if (!(folder == undefined || folder == "")) {
            $("#hidFOLDER").val(folder);
        }
        FileUpload.tableName = tableName;
        FileUpload.Idx = Idx;
    }
    , SingleInit: function (tableName, Idx, folder) {
        // $("#sfile").html("<input type='file' name='files' >");
        $('#fileupload')
        .fileupload(
            'option',
            {
                maxFileSize: 4000000,
                autoUpload: true
                , singleFileUploads: true
                , maxNumberOfFiles: 1
            }
        );

        if (!(folder == undefined || folder == "")) {
            $("#hidFOLDER").val(folder);
        }
        FileUpload.tableName = tableName;
        FileUpload.Idx = Idx;
    }
    , getFileInfo: function () {
        var arrFiles = new Array();
        $('.files > tr').each(function () {
            var param = new Object();
            param.FILE_DIRECTORY = $(this).find(".name a").attr("href").split("?")[1].split("&")[1].split("=")[1];
            param.FILE_NAME = $(this).find(".name a").text();
            param.REAL_FILE_NAME = $(this).find(".name a").attr("href").split("?")[1].split("&")[0].split("=")[1];
            param.FILE_SIZE = $(this).find(".size").text();
            arrFiles.push(param);
        });
        return arrFiles;
    }
    , fileClear: function () {
        $('.delete .btn-danger').trigger('click');
    },
    fileList: function () {
        var params = new Object();
        params.FILE_KEY = FileUpload.tableName + "_" + FileUpload.Idx;
        params = JSON.stringify(params);
        var url = _baseurl + "/Common/PV_FileList/";
        ajax.GetAjax(url, params, "html", function (result) {
            $("#ddFile").html(result);
        });
    }
    , fileDelete: function (fileKey, Seq) {
        bootbox.confirm("Are you sure?", function (result) {
            if (result) {
                var params = new Object();
                params.FILE_KEY = fileKey;
                params.SEQ = Seq;
                params = JSON.stringify(params);
                var url = _baseurl + "/Common/FIleDelete/";
                ajax.GetAjax(url, params, "", function (result) {

                    if (result.message == null || result.message == "") {
                        try {
                            FileUpload.fileList();
                        } catch (e) { }
                        try {
                            fileDeleteAfter();
                        } catch (e) { }
                    }
                    else {
                        bootbox.alert(result.message);
                    }

                });
            }
        });
    }
}

var SNS = {
    Share: function (site, msg, url) {

        switch (String(site).toUpperCase()) {
            case "TWITTER":
                // url = "http://twitter.com/home?status=" + encodeURIComponent(msg) + " " + encodeURIComponent("http://www.p2c.co.kr");
                url = "http://twitter.com/home?status=" + encodeURIComponent(msg) + " " + encodeURIComponent(url);
                break;
            case "FACEBOOK":
                url = "http://www.facebook.com/sharer.php?u=" + encodeURIComponent(msg) + " " + encodeURIComponent(url);
                break;
            case "GOOGLEPLUS":
                url = "https://plus.google.com/share?url=" + encodeURIComponent(msg) + " " + encodeURIComponent(url);
                break;
            case "PINTEREST":
                url = "http://www.pinterest.com/pin/create/button/?url=http%3A%2F%2Fwww.flickr.com%2Fphotos%2Fkentbrew%2F6851755809%2F&media=http://p2c.co.kr/Common/images/main/main_img1.png&description=Next%20stop%3A%20Pinterest" + encodeURIComponent(msg) + " " + encodeURIComponent(url);
                break;
            default:
                url = "http://www.facebook.com/sharer.php?u=" + encodeURIComponent(msg) + " " + encodeURIComponent(url);
                break;
        }
        var a = window.open(url, site, 'width=800, height=500');
        if (a) {
            a.focus();
        }
    },
    ShareFacebook: function (shareurl, stitle, sdetail) {
        var shareLocation = shareurl, sDetail = sdetail;
        FB.ui({
            method: 'share',
            name: stitle,
            href: shareLocation, // 로컬로는 테스트 안됨. 38이나 실서버 링크를 걸면 됨(페북에서 로컬환경은 지원을 안하는듯)
            description: sDetail // 사이트 설명
        }, function (response) { });
    }
}


////////////////////////////////////
// 온라인 오더 추가 사항
/////////////////////////////////////
var OnlineOrder = {
    TopMenuShoppingCartList: function (baseUrl) {
        ajax.GetAjax(baseUrl + "/Order/PV_ShoppingCartList/"
                  , "", "html"
                  , function (result) {
                      $("#shoppingcart1").html(result);
                  });
    }
    //////////////////////////////////////////
    // 메뉴에서 Cart 추가/수정/삭제 세션 저장
    //////////////////////////////////////////
    , OnlineSaveCart: function (baseUrl, saveType, index, itemCode, itemName, Price, itemCnt, tmCode, tax1Rate, tax2Rate, tax3Rate, ordertype) {

        if (saveType == "D") {
            OnlineOrder.OnlineDeleteCart(baseUrl, index);
        }
        else {

            var Param = new Object();
            Param.SAVE_TYPE = saveType;
            Param.INDEX = index;

            if (saveType != "D") {
                Param.ITEM_CODE = itemCode;
                Param.ITEM_NAME = itemName;
                Param.PRICE = Price;
                Param.ITEM_CNT = itemCnt;
                Param.TM_CODE = tmCode;
                Param.TAX1RATE = tax1Rate;
                Param.TAX2RATE = tax2Rate;
                Param.TAX3RATE = tax3Rate;

            }
            Param = JSON.stringify(Param);
            ajax.GetAjax(baseUrl + "/Order/PV_SaveCart/"
                 , Param, "html"
                 , function (result) {

                     if (location.href.toUpperCase().indexOf("PAYMENT") > 0) {
                         location.reload(true);
                     }
                     else {
                         $(".btn-cart-md").html(result); /*Partial_Online/TopMenu.cshtml*/

                         if (saveType == "N") {
                             location.href = baseUrl + "/Order/submenu?ITEM_CODE=" + itemCode;
                         }
                         else {
                             OnlineOrder.TopMenuShoppingCartList(baseUrl);
                             cakeFactory.Cart.btnCartInit();
                         }
                     }
                 });
        }
    }
     , OnlineDeleteCart: function (baseUrl, index) {
         bootbox.confirm("Are you sure delete?", function (result1) {
             if (result1) {
                 var Param = new Object();
                 Param.SAVE_TYPE = "D";
                 Param.INDEX = index;

                 Param = JSON.stringify(Param);
                 ajax.GetAjax(baseUrl + "/Order/PV_SaveCart/"
                      , Param, "html"
                      , function (result) {

                          if (location.href.toUpperCase().indexOf("PAYMENT") > 0) {
                              location.reload(true);
                          }
                          else {
                              $(".btn-cart-md").html(result); /*Partial_Online/TopMenu.cshtml*/



                              OnlineOrder.TopMenuShoppingCartList(baseUrl);
                              cakeFactory.Cart.btnCartInit();

                          }
                      });
             }
         });

     }
    ,
    //////////////////////////
    // TOP Cart 리스트 보여주기
    //////////////////////////
    BtnCartList: function (baseUrl) {
        ajax.GetAjax(baseUrl + "/Order/PV_BtnCartList/"
                 , "", "html"
                 , function (result) {
                     $(".btn-cart-md").html(result); /*Partial_Online/TopMenu.cshtml*/
                     try {
                         OnlineOrder.TopMenuShoppingCartList(baseUrl);
                         cakeFactory.Cart.btnCartInit();
                     } catch (e) { }
                 });
    }
    ////////////////////////////////
    // TOP 자동완성 보여주기
    ////////////////////////////////
    , SetSearchInit: function (baseUrl, searchType, sLanAll) {
        ajax.GetAjax(baseUrl + "/main/GetGroupItemAll/"
                       , "", "json"
                       , function (result) {
                           $("#txtSearch").kendoAutoComplete({
                               dataSource: result.itemList,

                               select: function (e) {
                                   if (searchType == "1") {
                                       doQuery_MenuList("", this.dataItem(e.item.index()));
                                   } else {
                                       Order_MenuList("", this.dataItem(e.item.index()));
                                   }
                               },
                           });


                           var uiHtml = (searchType == "1") ? "<li><a href='javascript:void(0);' onclick='doQuery_MenuList(\"" + "" + "\",\"\");'>" + sLanAll + "</a></li>" : "<li><a href='javascript:void(0);' onclick='Order_MenuList(\"" + "" + "\",\"\");'>" + sLanAll + "</a></li>";
                           for (var nItem = 0; nItem < result.groupList.length; nItem++) {
                               if (searchType == "1") {
                                   uiHtml += "<li><a href='javascript:void(0);' onclick='doQuery_MenuList(\"" + String(result.groupList[nItem].GROUP_CODE).trim() + "\",\"\");'>" + result.groupList[nItem].GROUP_NAME + "</a></li>";
                               }
                               else {
                                   uiHtml += "<li><a href='javascript:void(0);' onclick='Order_MenuList(\"" + String(result.groupList[nItem].GROUP_CODE).trim() + "\",\"\");'>" + result.groupList[nItem].GROUP_NAME + "</a></li>";
                               }
                           }
                           $("#uiGroup").html(uiHtml);

                       });
    },
    disaplyLoginInfo: function () {
        if ($(".btn-cart-md ul li.ico_login .dropdown-menu").css("display") != "none")
            $(".btn-cart-md ul li.ico_login .dropdown-menu").hide();
        else
            $(".btn-cart-md ul li.ico_login .dropdown-menu").show();
    }

}

var mobile = {
    table: {
        moblied_tr: function () {
            //테이블 모바일 버전    
            $(".mobiletd tr td.left").on("click", function () {
                if ($(this).parent().hasClass("on")) {
                    $(this).parent().removeClass("on");
                } else {
                    $(this).parent().addClass("on");
                }
            });
            for (var i = 0; i < 6; i++) {
                var thText = $(".mobiletd thead th").eq(i).text();
                $(".mobiletd tbody tr").each(function () {
                    $(this).find("td").eq(i).find("i").text(thText);
                });
            }
        }
    }
}



var Popup = {
    Init: function (className) {
        $('.' + "trigger-" + className).popupLayer();
        $('.' + className + ' .container').draggable({
            //지정된 영역안에서만 이동
            containment: "body"
        });
        //팝업이 안닫혀서 임시로 넣었어요
        $("." + className + " .fa-close").click(function () {
            Popup.Hide("." + className);
        });
        $("." + className).click(function () {

            if (event.pageX < $(this).find(".ui-draggable").offset().left) {
                Popup.Hide(".pop-subject");
            } else if (event.pageX > $(this).find(".ui-draggable").offset().left + $(this).find(".ui-draggable").width()) {
                Popup.Hide(".pop-subject");
            }
            //$(this).find(".ui-draggable").pageX
        });
    },
    InitID: function (id) {
        $('#' + id).popupLayer();
        $('#' + id).draggable({
            //지정된 영역안에서만 이동
            containment: "body"
        });

        //팝업이 안닫혀서 임시로 넣었어요
        $("#" + id + " .fa-close").click(function () {
            Popup.Hide("#" + id);
        });
        $("#" + id).click(function () {

            if (event.pageX < $(this).find(".ui-draggable").offset().left) {
                Popup.Hide(".pop-subject");
            } else if (event.pageX > $(this).find(".ui-draggable").offset().left + $(this).find(".ui-draggable").width()) {
                Popup.Hide(".pop-subject");
            }
            //$(this).find(".ui-draggable").pageX
        });
    }, ShowClass: function (className) {
        $("." + className).addClass("is-visible");
    }, HideClass: function (className) {
        $("." + className).removeClass("is-visible");
    }, Show: function (obj) {
        $(obj).addClass("is-visible");
    }, Hide: function (obj) {
        $(obj).removeClass("is-visible");
    },
    loginShow: function () {
        $(".pop-login").addClass("is-visible");
        $(".pop-login").find("#LOGIN_ID").focus();
        $(".pop-login").find("#LOGIN_ID").select();
    },
    loginHide: function () {

        $(".pop-login").removeClass("is-visible");
    }
}


String.format = function (text) {
    //check if there are two arguments in the arguments list
    if (arguments.length <= 1) {
        //if there are not 2 or more arguments there's nothing to replace
        //just return the original text
        return text;
    }
    //decrement to move to the second argument in the array
    var tokenCount = arguments.length - 2;
    for (var token = 0; token <= tokenCount; token++) {
        //iterate through the tokens and replace their
        // placeholders from the original text in order
        text = text.replace(new RegExp("\\{" + token + "\\}", "gi"),
                                                arguments[token + 1]);
    }
    return text;
};

///////////////////////////////////
/// Date 관련 함수 정의
//////////////////////////////////
var DateInfo = {
    ConvertDate: function (val) {

        if (val.length == 6) {
            val = val + "01";
        }

        if (val.length < 9) {
            var YYYY = val.substring(0, 4);
            var mm = val.substring(4, 6);
            var dd = val.substring(6, 8);
            var val = YYYY + "-" + mm + "-" + dd + " 00:00:00";
        }

        var sDate = val.split(' ')[0];
        var sHms = val.split(' ')[1]

        return new Date(sDate.split('-')[0], Number(sDate.split('-')[1]) - 1, sDate.split('-')[2], sHms.split(':')[0], sHms.split(':')[1], sHms.split(':')[2]);
    },
    NowDate: function (delimiter) {
        var date = new Date();
        return DateInfo.SetFormatDate(date, delimiter);
    }
    , SetFormatDate: function (date, delimiter) {

        if (delimiter == undefined)
            delimiter = "/";

        date = new Date(date);
        var year, month, day;
        year = date.getFullYear();
        month = (String(date.getMonth() + 1).length == 1) ? "0" + String(date.getMonth() + 1) : String(date.getMonth() + 1);
        day = String(date.getDate()).length == "1" ? "0" + String(date.getDate()) : String(date.getDate());

        switch (delimiter) {
            case "/":
                return month + "/" + day + "/" + year;
                break;
            default:
                return year + "/" + month + "/" + day;
                break;
        }
    }
    ,
    GlobalAddDate: function (Interval, AddVal, date) {
        if (date == undefined) {
            date = new Date();
        }
        else {
            date = new Date(date);
        }
        var year, month, day;
        year = date.getFullYear();
        month = Number((String(date.getMonth() + 1).length == 1) ? "0" + String(date.getMonth() + 1) : String(date.getMonth() + 1));
        day = date.getDate();

        switch (Interval) {
            case "year":
                year = (year * 1) + (AddVal * 1);
                break;
            case "month":
                month = (month * 1) + (AddVal * 1);
                break;
            default: //day
                day = (day * 1) + (AddVal * 1);
                break;
        }
        date = new Date(year, month - 1, day);

        return date;

    }
    ,
    /////////////////////////////////////////
    // ex) DateInfo.AddDate("month", -3, "12/20/2013"); 
    //     => "09/20/2013"
    /////////////////////////////////////////
    AddDate: function (Interval, AddVal, date, delimiter) {
        if (delimiter == undefined) {
            if (date.indexOf("/") >= 0) {
                delimiter = "/";
            }
            else {
                delimiter = "-";
            }
        }


        date = new Date(date);
        var year, month, day;
        year = date.getFullYear();
        month = Number((String(date.getMonth() + 1).length == 1) ? "0" + String(date.getMonth() + 1) : String(date.getMonth() + 1));
        day = date.getDate();

        switch (Interval) {
            case "year":
                year = (year * 1) + (AddVal * 1);
                break;
            case "month":
                month = (month * 1) + (AddVal * 1);
                break;
            default: //day
                day = (day * 1) + (AddVal * 1);
                break;
        }
        date = new Date(year, month - 1, day);
        return DateInfo.SetFormatDate(date);
    }
}
$("document").ready(function () {
    $("input[type=text]").focus(function () {
        $(this).select();
    });
    $("input[type=number]").focus(function () {
        $(this).select();
    });
    $(".percent").keyup(function () {
        BaseCommon.NumberInfo.PerchantCheck(this);
    });
    $("input[type=number]").each(function () {
        if ($(this).val() == undefined || $(this).val() == "") {
            $(this).val("0.00");
        }
    });
});


//(function ($) {
//    var originalVal = $.fn.val;
//    $.fn.val = function (value) {
//        debugger;
//        this.trigger("change");
//        return originalVal.call(this, value);
//    };
//})(jQuery);

