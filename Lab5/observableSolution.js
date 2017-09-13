var fetch = require('node-fetch');
var Rx = require('rxjs');
var results;
Rx.Observable.fromPromise
(fetch('http://jsonplaceholder.typicode.com/users/')
.then(function(res) {
    return res.json();
}))
.subscribe(function(json) {

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
    
    // Serve the index page
    app.get('/', function(req, res){
    res.render('header', {
    // PLACEHOLDER
    data: json
    });
    });
    
    if (!module.parent) {
    app.listen(8017);
    console.log('EJS Demo server started ...');
    }
});

