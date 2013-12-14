function parseFact(fact){
    fact = fact.trim();
    var topic1, topic2, relation;
    var data = fact.split('|'); 
    if (fact!="") {
        return { concept1: data[0], relation: data[1], concept2: data[2] }; 
    } else {
        return null;
    }
  
};
var facts;
function addCheckBox() {
    var parentElement = document.getElementById('area');   //Lấy giá trị của id là area  
    for (var i = 0; i < data.length ; i++) {
        var newCheckBox = document.createElement('input');
        newCheckBox.type = 'checkbox';
        newCheckBox.value = i;
        newCheckBox.id = "ckbox"+i;
        parentElement.appendChild(newCheckBox);
        var name = data[i].concept1 + "_" + data[i].concept2;
        var a = "<label id='n" + i + "' for='ckbox" + i + "' data-relation='" + name + "'>" + data[i].concept1 + " " + data[i].relation + " " + data[i].concept2 + " </label></br>";
        $("#area").append(a);
    }
};
function removeInData(sl) {
    for (var i = 0; i < sl.length; i++) {
        //alert(data.length);
        data.splice(sl[i], 1);
    }
   
};
function isExist(da, d) {
    //alert(da.concept1);
    if (da.concept1 == d.concept1 && da.relation == d.relation && da.concept2 == d.concept2) {
        //alert("so sanh bang het");
        return true;
    }
    return false;
};
function checkExistData(ndata) {
    var l = data.length;
    for (var i = 0; i < l; i++) {
        if (isExist(data[i], ndata)) {
            //alert("tim duoc 1 thang trung");
            return true;
        }    
    }
    return false;
};
function isExistDifRelation(da, d) {
    //alert(da.concept1);
    if (da.concept1 == d.concept1 && da.relation != d.relation && da.concept2 == d.concept2) {
       // alert("thay relation");
        return true;
    }
    return false;
};
function checkExistDataDifRelation(ndata) {
    var l = data.length;
    for (var i = 0; i < l; i++) {
        if (isExistDifRelation(data[i],ndata)) {
            //alert("tim duoc 1 thang trung");
            data[i].relation = ndata.relation;
            data[i].liID = ndata.liID;
            return true;
        }    
    }
    return false;
};
var data = [];


$(".addbutton").click(function (event) {
    
    var cc1 = $("#concept1 :selected").text();
    var ccid1 = $("#concept1 :selected").val();
    var link = $("#link :selected").text();
    var linkID = $("#link :selected").val();
    var cc2 = $("#concept2 :selected").text();
    var ccid2 = $("#concept2 :selected").val();
    var da = { concept1: cc1, relation: link, concept2: cc2,conceptid1:ccid1,conceptid2:ccid2,liID:linkID};
    if (cc1 == cc2) {
        alert("Khái niệm giống nhau!!")
    }
    else {
        if (flag) {
            data.push(da);
            flag = false;
        } else {
            if (checkExistDataDifRelation(da)) {
                alert("Ðã thay đổi liên kết");
            }else if (!checkExistData(da)) {
                data.push(da);      
            }else{
                alert("Khái niệm đã tồn tại");
            }
        }
        $("#area").empty();
        addCheckBox();
    }
    
});

$(".removebutton").click(function (event) {
    var selected = new Array();
    $('#area input:checked').each(function () {    
        selected.push($(this).attr('value'));
        var na = "label#n" + $(this).attr('value');
        $(na).remove();
    });
    $("#ckbox:checked").closest("input").remove();
    //alert(selected.toString());
    selected.sort(function (a, b) { return b - a });
    removeInData(selected);
});

$(".newbutton").click(function (event)
{
    var r = confirm("Bạn có muốn tạo mới concept map không");
    if (r == true) {
        data = [];
        $("#area").empty();
    }
    else {       
    }
    
});


function textareaLoadEngineT(conceptMap, options)
{
    $(".addbutton").click(function (event) {
        conceptMap.loadFacts(this.value);
    });
    $(".removebutton").click(function (event) {
        conceptMap.loadFacts(this.value);
    });
    $(".newbutton").click(function (event) {
        conceptMap.loadFacts(this.value);
    });

    $(".btnDanhGia").click(function (event) {
        conceptMap.loadFacts(this.value);
    });
 
    $(".btnOpen").click(function (event) {
        conceptMap.loadFacts(this.value);
    });
    $(".btnKetQua").click(function (event) {
        conceptMap.loadFacts(this.value);
    });
    $(".buttonS").addClass('deactivated');

    return data;
};
var flag = true;
$(".btnKetQua").click(function (event) {
    var levelID = $("#levelID").val();
    var topicID = $("#topicID").val();
    var conceptID=$("#ConceptID :selected").val();
    var obj = { LevelID: levelID, TopicID: topicID, ConCeptID: conceptID };
    $.ajax({
        url: '/Topic/Result',
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify(obj),
        success: function (stdata) {
            var das = stdata.split("__");
            for (var i = 0; i < das.length - 1; i++) {
                var da = das[i].trim().split("|");
                var tam = { concept1: da[0], relation: da[1], concept2: da[2], conceptid1: da[3], conceptid2: da[5], liID: da[4] };
                data.push(tam);
            }
            $("#area").empty();
            addCheckBox();
        }
    });
});

$(".btnSave").click(function (event) {
    if ($(this).hasClass('deactivated'))
    {
        e.preventDefault();
    }
    var links = [];
    for (var i = 0; i < data.length; i++) {
        links.push({ ConceptID1: data[i].conceptid1, ConceptID2: data[i].conceptid2, Text: data[i].relation, LinkID: data[i].liID })
    }
    var levelID = $("#levelID").val();
    var conceptID = $("#conceptID").val();
    var mapName = prompt("Nhập tên concept map của bạn", "Concept Name");
    if (mapName != null) {
        var obj = { links: links, mapName: mapName, LevelID: levelID, ConceptID: conceptID };
        $.ajax({
            url: '/Topic/Save',
            type: 'POST',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(obj),
            success: function (resp) {
                // alltrue = false;
                alert(resp);
            }
        });
    }
})


$(".btnDanhGia").click(function (event) {
    if (data.length <= 0) {
        alert("Bạn chưa vẽ concept map")
    } else {
        $("div").removeClass("relationFalseDiv");
        $("label").removeClass("relationFalseLabel");
        var links = [];
        
        for (var i = 0; i < data.length; i++) {
            links.push({ ConceptID1: data[i].conceptid1, ConceptID2: data[i].conceptid2, Text: data[i].relation, LinkID: data[i].liID })
        }
        var fl = 0;
        var alltrue = true;
        
        $.ajax({
            url: '/Topic/DanhGia',
            type: 'POST',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(links),
            success: function (resp) {
                //alert(resp);
                var x = resp.split("\n");
                for (var i = 0; i < x.length ; i++)
                {
                    if (!find(x[i]))
                    {
                        fl++;
                        alltrue = false;
                        var da = x[i].trim().split("|");
                        for (var j = 0; j < data.length; j++) {
                            if (data[j].conceptid1 == da[0] && data[j].conceptid2 == da[2]) {
                                var name = data[j].concept1 + "_" + data[j].concept2;
                                //alert(name);
                                $("div[data-relation='" + name + "']").addClass("relationFalseDiv");
                                $("label[data-relation='" + name + "']").addClass("relationFalseLabel");
                                break;
                            }
                        }
                    }
                }

                if (alltrue) {
                    alert("Bạn Làm đúng hết rồi!"); 
                    $('.buttonS').removeClass('deactivated');
                } else {
                    
                    var string = "Bạn làm đúng " + (data.length - fl) + "trên " + data.length + " liên kết!";
                    alert(string);
                }  
            }
        });
    }
});


function find(da) {
    var nda = da.trim().split("|");
    if (nda[3] == "False") {        
        return false;
    }
    return true;
};
function changeColer(da) {    
};