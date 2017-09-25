/**
 * Created by Owner on 9/12/2017.
 */
var express = require('express');
var router = express.Router();
var geolocation = require('geolocation')

/* GET home page. */
router.get('/', function(req, res, next) {


    res.render('nearest', { title: 'Express' });
});
router.post('/', function(req,res,next){

    db = req.db;
    var docs = [];
    var coords = [];
    coords[0] = parseFloat(req.body.lon);
    coords[1] = parseFloat(req.body.lat);


    var pointsDb = db.bind('point');

    var docs = [];
    pointsDb.find({location:{$near:{$geometry:{type:"Point", coordinates:coords}, $maxDistance:200000}}}).limit(3).toArray(function(err,ele){
        if(err) throw err;
        else {
            ele.forEach(function (document) {
                docs.push({longitude: document.location.coordinates[0],latitude:document.location.coordinates[1]})

            })
        }
        console.log(docs);
    });



})

module.exports = router;
