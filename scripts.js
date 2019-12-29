var indicies = [];
var highlights = [];
var flags = [];
var marklen = '<mark class="mark3"></mark>'.length;
var colors = ['#4998d1', '#de964e', '#4bd691', '#d696b9']
var id_entry, id_user;

window.onload = function()
{
    this.init(4);
}
function init(layers){
      if(layers > 4)
          console.log('Invalid layer amount, must be less than 9');
      for(i = 0; i < layers; i++){
          indicies.push([]);
          highlights.push($('#highlight' + i));
          flags.push($('#flag' + i));
      }
      this.$textarea = $('textarea');
      this.$backdrop = $('.backdrop');
  
      $.get("retreive.php",{}, function(data){
          let jsdata = $.parseJSON(data);
          id_entry = jsdata['id_entry'];
          //var id_dataset = jsdata['id_dataset'];
          var prompt = jsdata['prompt'];
          var innertext = jsdata['innertext'];
          $(".loader").hide();
          $("#prompt").html(prompt);
          $textarea.val(innertext);
      });
  
      id_user = getCookie('id_user');
  
      //Create new user id
      if(id_user == "undefined"){
          $.get('newuser.php',{}, function(data){
              let jsdata = $.parseJSON(data);
              id_user = jsdata['id_user'];
              setCookie('id_user', id_user, 90);
          });
          console.log('Got new id' + id_user);
      }else{
          console.log('Got old ' + id_user);
      }
  
      this.bindEvents();
      this.handleInput();
}
function bindEvents() {
    $textarea.on({
      'input': handleInput,
      'scroll': handleScroll,
      'keydown':handleKeyDown
    }); 
  }

function applyIndicies(text, type){
    var count = 0;
    for(i = 0; i < indicies[type].length; i++){
        index = indicies[type][i];
        text = text.substring(0, index[0] + count) 
        + '<mark class="mark' + type + '">' + text.substring(index[0] + count, index[1] + count) + '</mark>' 
        + text.substring(index[1] + count);
        count += marklen;
    }

    return text;
}
//This adds flags to help visualize hidden highlights
function addFlags(text, callingtype){
    var count = 0;
    for(j = 0; j < indicies[callingtype].length; j++){
        if(needsFlag(indicies[callingtype][j], callingtype)){
            text = text.substring(0, index[0] + count)
            + '<mark class="mark' + callingtype + '">'
            + text[index[0] + count] + '</mark>'
            + text.substring(index[0] + count + 1)        
            count += marklen;
        }       
    }
    flags[callingtype].html(text);
}

//Checks if it needs a flag
//Needs a flag if:
//100% inside another tag :check:
//100% inside several tags
function needsFlag(query, callingtype){
    for(type = 0; type < indicies.length; type++){
        if(type == callingtype)
            continue;
        for(i = 0; i < indicies[type].length; i++){
            if(query[0] >= indicies[type][i][0] && query[1] <= indicies[type][i][1])
                return true;
        }
    }
    return false;
}

function addIndex(start, end, type = 0){
    indicies[type].push([start, end]);
    indicies[type].sort(function(a, b){return a[0] - b[0]});
    indicies[type] = mergeIndices(indicies[type]);
}

function mergeIndices(arr){
    var nIndices = [arr[0]];
    //Merge intervals
    for(j = 1; j < arr.length; j++){
        var lastpos = nIndices.length - 1;
        if(nIndices[lastpos][1] < arr[j][0]){
            nIndices.push(arr[j]);
        }
        else if(nIndices[lastpos][1] > arr[j][0] && arr[j][1] > nIndices[lastpos][1]){
            nIndices[lastpos][1] = arr[j][1];
        }
    }
    return nIndices;
}

function contained(query){
    for(type = 0; type < indicies.length; type++){
        for(index in indicies[type]){
            if(index != query && index[0] < query[0] && index[1] > query[1]){
                return true;
            }
        }
    }
    return false;
}

function handleKeyDown(e){
    var pos = $textarea.selection('getPos');
    var start = pos['start'];
    var end = pos['end'];
    var type = 0;
    if(e.key == '1')
        type = 0;
    else if (e.key == '2')
        type = 1;
    else if (e.key == '3')
        type = 2;
    else if (e.key == '4')
        type = 3;

    //Clamp type
    if(type >= indicies.length)
        type = indicies.length - 1;
    
    addIndex(start, end, type);
    handleInput();
    return false;
}
function handleInput() {
    var text = $textarea.val();
    if(text ===undefined)
      return;
      for(k = 0; k < highlights.length; k++){
          highlights[k].html(applyIndicies(text, k));
          addFlags(text, k);
      }
}

function handleScroll() {
    var scrollTop = $textarea.scrollTop();
    $backdrop.scrollTop(scrollTop);
    
    var scrollLeft = $textarea.scrollLeft();
    $backdrop.scrollLeft(scrollLeft);  
}

function setCookie(cname, cvalue, exdays) {
    var d = new Date();
    d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
    var expires = "expires="+d.toUTCString();
    document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";
  }
function getCookie(cname) {
    var name = cname + "=";
    var ca = document.cookie.split(';');
    for(var i = 0; i < ca.length; i++) {
      var c = ca[i];
      while (c.charAt(0) == ' ') {
        c = c.substring(1);
      }
      if (c.indexOf(name) == 0) {
        return c.substring(name.length, c.length);
      }
    }
    return "";
  }

function sendAnnotations(){
    console.log(indicies);
    for(let i = 0; i < indicies.length; i++){
        for(let j = 0; j < indicies[i].length; j++){
            sendAnnotation(indicies[i][j], i);
            console.log('Send annotation');
        }
    }
}
function sendAnnotation(index, category){
    $.post('postAnnotation.php',{
        id_entry: id_entry,
        id_user: id_user,
        start: index[0],
        end: index[1],
        category: category
    }, function(data){console.log(data);});
}

