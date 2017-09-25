/**
 * Created by Owner on 9/12/2017.
 */
var express = require('express');
var router = express.Router();
var ObjectId = require('mongodb').ObjectId;

router.get('/', function(req, res, next){

    var db=req.db;
    var pointsDb = db.bind("point");
    var docs=[];
    pointsDb.find({}).toArray(function(err, doc){
        if(err) throw err;
        else{
            doc.forEach(function(element){
                docs.push({id:element._id, name:element.name, category:element.category, longitude:element.location.coordinates[0], latitude:element.location.coordinates[1]});
                //console.log(docs);


            });
            
            res.render('display', {data:docs});

        }
        pointsDb.close();
    })

});
router.get('/delete/id/:id', function(req, res, next){
    console.log("this is Id2: "+req.params.id);
    var db = req.db;
    var pointsDb = db.bind('point');
    var query = {"_id":ObjectId(req.params.id)};
    pointsDb.remove(query);
    res.redirect('/display');

});
router.get('/update/id/:id',function(req, res, next){
    console.log("this is Id3: "+req.params.id);
    var db = req.db;
    var docs = [];
    var pointsDb = db.bind('point');
    var query = {"_id":ObjectId(req.params.id)};
    pointsDb.find(query).toArray(function(err, doc){
        if(err) throw err;
        else{
            doc.forEach(function(element){
                docs.push({id:element._id, name:element.name, category:element.category, longitude:element.location.coordinates[0], latitude:element.location.coordinates[1]});
                //console.log(docs);


            })
            res.render('update', {data:docs});

        }
        pointsDb.close();
    })
    //res.render('update', {data:docs});


});


module.exports = router;