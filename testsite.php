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
        <h2 id="prompt" class="prompt">Please do the folloiwng</h2>
        <!--</div>-->
        <div class="container">
            <div class="backdrop">
            <div class="load_container">
                    <div class="loader"></div>
            </div>
                <div id="highlight" class="highlights"></div>
            </div>
            <p class="textarea" id="txt_area"></p>
        </div>
        <button id="submit_annotation" onClick="sendAnnotations()">Submit</button>
    </body>
</html>