function parseFact(fact){
  //fact = fact.trim();
  //var topic1, topic2, relation;
  //if (/^[^"]+"[^"]+"[^"]+$/.test(fact)) {
  //  var firstQuote = fact.indexOf('"');
  //  var secondQuote = fact.indexOf('"', firstQuote + 1);
    
  //  topic1 = fact.substr(0, firstQuote).trim();
  //  topic2 = fact.substr(secondQuote + 1).trim();
  //  relation = fact.substr(firstQuote + 1, secondQuote - firstQuote - 1).trim();
    
  //  return {concept1: topic1, relation: relation, concept2: topic2};
    
  //} else if (/^([A-Z][a-z]* )+([a-z]+ )+[A-Z][a-z]*( [A-Z][a-z]*)*$/.test(fact)) {
    
  //  var words = fact.split(/( [a-z]+)+/g);
  //  var firstLowerCaseWord = fact.indexOf(/ [a-z]/);
  //  var firstUpperAfterRelation = fact.indexOf(/ [A-z]/, firstLowerCaseWord + 1);
    
  //  topic1 = words[0].trim();
  //  topic2 = words[words.length-1].trim();
  //  relation = fact.substr(topic1.length, fact.length-topic1.length-topic2.length).trim();
    
  //  return {concept1: topic1, relation: relation, concept2: topic2};
  //}
  
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
    var parentElement = document.getElementById('area');
    for (var i = 0; i < data.length ; i++) {
        var newCheckBox = document.createElement('input');
        newCheckBox.type = 'checkbox';
        newCheckBox.value = i;
        newCheckBox.id = "ckbox"+i;
        parentElement.appendChild(newCheckBox);
        var a = "<label id='n" + i + "' for='ckbox"+i+"'>" + data[i].concept1 + " " + data[i].relation + " " + data[i].concept2 + "</label></br>";
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
        if (isExistDifRelation(data[i], ndata)) {
            //alert("tim duoc 1 thang trung");
            data[i].relation = ndata.relation;
            return true;
        }    
    }
    return false;
};
var data = [];
$(".addbutton").click(function (event) {
    
    var cc1 = $("#concept1 :selected").text();
    var link = $("#link :selected").text();
    var cc2 = $("#concept2 :selected").text();
    var da = { concept1: cc1, relation: link, concept2: cc2 };
    if (cc1 == cc2) {
        alert("Khái niệm giống nhau!!")
    } else {
        if (flag) {
            data.push(da);
            flag = false;
        } else {
            if (checkExistDataDifRelation(da)) {
                alert("Đã thay đổi liên kết");
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
function textareaLoadEngineT(conceptMap, options) {
    $(".addbutton").click(function (event) {
        conceptMap.loadFacts(this.value);
    });
    $(".removebutton").click(function (event) {
        conceptMap.loadFacts(this.value);
       
    });
    return data;
};
var flag = true;
function textareaLoadEngine(conceptMap, options) {
    var triples = [];
    var $facts = $("#concepts");
    var text = $facts.val();
    $(".addbutton").click(function (event) {
        conceptMap.loadFacts(this.value);   
    });
    
    facts = text.split('\n');
       
    var fl = facts.length;
    for (var i = 0; i < fl; i++) {
        var t = parseFact(facts[i]);
        if (t) {
            triples.push(t);
        }
    }
        
    return triples;
};
      