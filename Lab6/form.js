var express = require('express')
    , app = module.exports = express();

    // var session = require('express-session');
    // var cookieParser = require('cookie-parser');
    // var csrf = require('csurf');
    // app.use(session());
    // app.use(cookieParser());
    // app.use(csrf());
    // app.use(function(req,res,next){
    //     res.locals.csrf = req.csrfToken();
    //     next();
    // });

    var bodyParser = require('body-parser')
    // app.use( bodyParser.json() );       // to support JSON-encoded bodies
    // app.use(bodyParser.urlencoded({     // to support URL-encoded bodies
    //   extended: false
    // }));

// Using the .html extension instead of
// having to name the views as *.ejs
app.engine('html', require('consolidate').swig);

// Set the folder where the pages are kept
app.set('views', __dirname + '\\views');

// This avoids having to provide the 
// extension to res.render()
app.set('view engine', 'html');

// Serve the index page
app.get('/newsletter', function (req, res) {
    res.render('form', { message: 'Please enter your email address' }
    );
});

app.get('/thankyou', function (req, res) {
    res.render('thankyou', { email: req.query.email }
    );
});

var jsonParser = bodyParser.json();
var urlencodedParser = bodyParser.urlencoded({extended:false})

app.post('/newsletter',urlencodedParser, function (req, res) {
    var email = req.body.email;
    console.log(email);
    // console.log(email);
    // res.send(email);
    res.redirect('/thankyou?email='+email);
});



app.listen(8040);