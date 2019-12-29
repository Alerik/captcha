<!DOCTYPE html>
<html>
    <head>
        <meta charset="utf-8"/>
        <link rel="stylesheet" text="text/css" href="styles.css"/>
        <script type="text/javascript" src="http://code.jquery.com/jquery-1.7.1.min.js"></script>
        <script src="jquery.selection.js"></script>
        <script src="scripts.js"></script>
    </head>
    <body>
        <!--<div class="prompt_background">-->
            <h2 id="prompt">Please do the folloiwng</h2>
        <!--</div>-->
        <div class="container">
            <div class="backdrop">
            <div class="load_container">
                    <div class="loader"></div>
                </div>
                <div id="highlight0" class="highlights"></div>
                <div id="flag0" class="flags"></div>
                <div id="highlight1" class="highlights"></div>
                <div id="flag1" class="flags"></div>
                <div id="highlight2" class="highlights"></div>
                <div id="flag2" class="flags"></div>
                <div id="highlight3" class="highlights"></div>
                <div id="flag3" class="flags"></div>
            </div>
            <textarea spellcheck="false" 
            id="txt_area"></textarea>
        </div>
        <button id="submit_annotation" onClick="sendAnnotations()">Submit</button>
    </body>
</html>