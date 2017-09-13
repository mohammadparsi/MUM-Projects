var fetch = require('node-fetch');
var results;
jsonPromise = function () {
    return fetch('http://jsonplaceholder.typicode.com/users/')
        .then(function (res) {
            return res.json();
        });
}

async function showUsers() {
    var express = require('express')
        , app = module.exports = express();

    // Using the .html extension instead of
    // having to name the views as *.ejs
    app.engine('.html', require('ejs').__express);

    // Set the folder where the pages are kept
    app.set('views', __dirname + '\\views');

    // This avoids having to provide the 
    // extension to res.render()
    app.set('view engine', 'html');
    try {
        let json = await jsonPromise();
        // Serve the index page
        app.get('/', function (req, res) {

            res.render('header', {
                // PLACEHOLDER
                data: json
            });
        });

        if (!module.parent) {
            app.listen(8014);
            console.log('EJS Demo server started ..');
        }
    } catch (error) {
        console.log(error.message);
    }


};

showUsers();