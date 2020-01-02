var indicies = [];
var words = [];
var highlight;
var marklen = '<mark></mark>'.length;
var clip_words = true;

var id_entry, id_user;
var innertext = '';

window.onload = function () {
    this.init();
}
function init() {
    highlight = $('#highlight');
    this.$textarea = $('#txt_area');
    this.$backdrop = $('.backdrop');
    indicies = [];
    words = [];
    this.$textarea.html('');
    $(".loader").show();
    $.get("retreive.php", {}, function (data) {
        let jsdata = $.parseJSON(data);
        id_entry = jsdata['id_entry'];
        //var id_dataset = jsdata['id_dataset'];
        var prompt = jsdata['prompt'];
        innertext = jsdata['innertext'];
        $(".loader").hide();
        $("#prompt").html(prompt);
        $textarea.html(innertext);
        let re = /[^\s]+/g;
        
        var m;
        do{
            m = re.exec(innertext);
            if(m){
                console.log(m);
                words.push([m['index'], m['index'] + m[0].length]);
            }
        }while(m);
    });

    id_user = getCookie('id_user');

    //Create new user id
    if (id_user == "undefined") {
        $.get('newuser.php', {}, function (data) {
            let jsdata = $.parseJSON(data);
            id_user = jsdata['id_user'];
            setCookie('id_user', id_user, 90);
        });
        console.log('Got new id' + id_user);
    } else {
        console.log('Got old ' + id_user);
    }

    this.bindEvents();
    this.handleInput();
}
function bindEvents() {
    $textarea.on({
        'input': handleInput,
        'scroll': handleScroll,
        'keydown': handleKeyDown
    });
    document.onselectionchange = () =>{
        handleSelect();
    };
}


//Todo: add word clipping
function handleSelect(){
    let selection = document.getSelection();

    let start = selection.anchorOffset;
    let end = selection.focusOffset;

    if(clip_words){
        let s_word = findWord(start);
        let e_word = findWord(end);
    
        if(s_word){
            start = s_word[0];
        }
        if(e_word)
            end = e_word[1];    
    }

    addIndex(start, end, 0);
    handleInput();
    return false;
}

function applyIndicies(text) {
    let count = 0;
    for (i = 0; i < indicies.length; i++) {
        index = indicies[i];
        text = text.substring(0, index[0] + count)
            + '<mark>' + text.substring(index[0] + count, index[1] + count) + '</mark>'
            + text.substring(index[1] + count);
        count += marklen;
    }

    return text;
}

function addIndex(start, end) {
    if(start == end)
        return;
    if(start > end){
        let temp = start;
        start = end;
        end = start;
    }
    indicies.push([start, end]);
    indicies.filter(function(value, index, arr){
        return value[0] != value[1];
    })
    indicies.sort(function (a, b) { return a[0] - b[0] });
    indicies = mergeIndices(indicies);
}

function mergeIndices(arr) {
    let nIndices = [arr[0]];
    //Merge intervals
    for (j = 1; j < arr.length; j++) {
        let lastpos = nIndices.length - 1;
        //Adding
        if (nIndices[lastpos][1] < arr[j][0]) {
            nIndices.push(arr[j]);
        }
        //Extension
        else if (nIndices[lastpos][1] >= arr[j][0] && arr[j][1] > nIndices[lastpos][1]) {
            nIndices[lastpos][1] = arr[j][1];
        }
    }
    return nIndices;
}

function findWord(index) {
    for(let  i = 0; i < words.length; i++){
        if(index >= words[i][0] && index < words[i][1])
            return words[i];
    }
    return null;
}

function handleKeyDown(e) {
    var pos = $textarea.selection('getPos');
    var start = pos['start'];
    var end = pos['end'];
    var type = 0;

    // console.log('key down ' + start + '  ' + end);
    // console.log(indicies);
    addIndex(start, end, type);
    handleInput();
    return false;
}
function handleInput() {
    if (innertext === undefined)
        return;
    highlight.html(applyIndicies(innertext));
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
    var expires = "expires=" + d.toUTCString();
    document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";
}
function getCookie(cname) {
    var name = cname + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
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

function sendAnnotations() {
    console.log(indicies);
        for (let j = 0; j < indicies.length; j++) {
            sendAnnotation(indicies[j], 0);
            console.log('Send annotation');
        }
    init(1);
}
function sendAnnotation(index, category) {
    $.post('postAnnotation.php', {
        id_entry: id_entry,
        id_user: id_user,
        start: index[0],
        end: index[1],
        category: category
    }, function (data) { console.log(data); });
}

